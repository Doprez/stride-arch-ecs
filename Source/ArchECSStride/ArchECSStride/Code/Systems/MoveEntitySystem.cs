using Arch.Core;
using ArchECSStride.Code.Arch.Components;
using Doprez.Stride.Arch;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Games;
using System;

namespace ArchECSStride.Code.Systems;
/// <summary>
/// Randomly moves entities in both the Arch world and the Stride scene.
/// </summary>
[DataContract(nameof(MoveEntitySystem))]
public class MoveEntitySystem : ArchSystem
{
	private QueryDescription _queryDescription;
	private Random _random = new();

	public override void Start()
	{
		_queryDescription = new QueryDescription().
			WithAny<Pathfinder, Position>();
	}

	public override void Update(in GameTime state)
	{
		World.Query(in _queryDescription, (ref Pathfinder pathfinder, ref Position position) =>
		{
			if (Vector3.Distance(pathfinder.Target, position.CurrentPosition) <= 2 || pathfinder.Path.Count == 0)
			{
				pathfinder.Target.Z = _random.Next(-100, 100);
				pathfinder.Target.X = _random.Next(-100, 100);
				pathfinder.SetNewPath = true;
				pathfinder.ShouldMove = true;
			}
			else
			{
				pathfinder.SetNewPath = false;
			}
		});
	}
}
