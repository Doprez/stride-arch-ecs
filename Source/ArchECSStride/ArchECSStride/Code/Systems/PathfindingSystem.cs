using Arch.Core;
using ArchECSStride.Code.Arch.Components;
using Doprez.Stride.Arch;
using Doprez.Stride.Arch.Services;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Design;
using Stride.Games;
using Stride.Navigation;
using System;
using System.Linq;

namespace ArchECSStride.Code.Systems;

/// <summary>
/// Requires a NavigationComponent Entity within the Stride scene.
/// </summary>
[DataContract(nameof(PathfindingSystem))]
public class PathfindingSystem : SystemBase
{

	private StrideEntityManager _entityManager;
	private QueryDescription _queryDescription;
	private IGame _game;
	private SceneSystem _sceneSystem;
	private NavigationComponent _navigationComponent;
	private GameSettings _gameSettings;

	public override void Start()
	{
		_entityManager = Services.GetService<StrideEntityManager>();
		_game = Services.GetSafeServiceAs<IGame>();
		_sceneSystem = Services.GetSafeServiceAs<SceneSystem>();
		_gameSettings = Services.GetService<IGameSettingsService>()?.Settings;

		var navSettings = _gameSettings.Configurations.Get<NavigationSettings>();
		
		_queryDescription = new QueryDescription().
			WithAny<Pathfinder, Position, Rotation>();

		SetupNavigationComponent();
	}

	public override void Update(in GameTime state)
	{
		var deltaTime = (float)state.Elapsed.TotalSeconds;

		World.Query(in _queryDescription, (ref Pathfinder pathfinder, ref Position position, ref Rotation rotation) =>
		{
			if(pathfinder.SetNewPath)
			{
				SetNewPath(ref pathfinder, ref position, ref _navigationComponent);
			}
			if(pathfinder.ShouldMove)
			{
				Move(ref pathfinder, ref position, ref deltaTime);
				Rotate(ref pathfinder, ref position, ref rotation);
			}
		});
	}

	private void SetupNavigationComponent()
	{
		var navComponent =_sceneSystem.SceneInstance.RootScene.Entities.FirstOrDefault(x => x.Name == "ProcessorComponents")
			.Get<NavigationComponent>();

		_navigationComponent = navComponent;
	}

	private static void SetNewPath(ref Pathfinder pathfinder, ref Position position, ref NavigationComponent navigationComponent)
	{
		navigationComponent.TryFindPath(position.CurrentPosition, pathfinder.Target, pathfinder.Path);
	}

	private static void Move(ref Pathfinder pathfinder, ref Position position, ref float deltaTime)
	{
		if (pathfinder.Path.Count == 0)
		{
			pathfinder.SetNewPath = true;
			return;
		}

		var nextWaypointPosition = pathfinder.Path[0];
		var distanceToWaypoint = Vector3.Distance(position.CurrentPosition, nextWaypointPosition);

		// When the distance between the character and the next waypoint is large enough, move closer to the waypoint
		if (distanceToWaypoint > 0.1)
		{
			var direction = nextWaypointPosition - position.CurrentPosition;
			direction.Normalize();
			direction *= pathfinder.Speed * deltaTime;

			position.CurrentPosition += direction;
		}
		else
		{
			if (pathfinder.Path.Count > 0)
			{
				// need to test if storing the index in Pathfinder would be faster than this.
				pathfinder.Path.RemoveAt(0);
			}
		}
	}

	public static void Rotate(ref Pathfinder pathfinder, ref Position position, ref Rotation rotation)
	{
		if (pathfinder.Path.Count == 0)
		{
			return;
		}

		float angle = (float)Math.Atan2(pathfinder.Target.Z - position.CurrentPosition.Z,
			pathfinder.Target.X - position.CurrentPosition.X);

		rotation.CurrentRotation = Quaternion.RotationY(-angle);
	}
}
