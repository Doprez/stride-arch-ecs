using Arch.Core;
using ArchECSStride.Code.Arch;
using ArchECSStride.Code.Arch.Components;
using ArchECSStride.Code.Services;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Games;
using System;

namespace ArchECSStride.Code.Systems;
/// <summary>
/// Randomly moves entities in both the Arch world and the Stride scene.
/// </summary>
[DataContract(nameof(MoveEntitySystem))]
public class MoveEntitySystem : SystemBase
{
	private StrideEntityManager _entityManager;
	private QueryDescription _queryDescription;
	private Random _random = new();

	public override void Start()
	{
		_entityManager = Services.GetService<StrideEntityManager>();

		_queryDescription = new QueryDescription().
			WithAny<StrideId, Vector3>();
	}

	public override void Update(in GameTime state)
	{
		World.Query(in _queryDescription, (ref Vector3 position, ref StrideId strideId) =>
		{
			position.Z = _random.Next(-100, 100);
			position.X = _random.Next(-100, 100);

			_entityManager.Entities[strideId.Id].Transform.Position = position;
		});
	}
}
