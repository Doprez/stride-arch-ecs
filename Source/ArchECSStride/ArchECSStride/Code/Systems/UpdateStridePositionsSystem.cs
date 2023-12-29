using Arch.Core;
using ArchECSStride.Code.Arch;
using ArchECSStride.Code.Arch.Components;
using Doprez.Stride.Arch;
using Doprez.Stride.Arch.Components;
using Doprez.Stride.Arch.Services;
using Stride.Core;
using Stride.Core.Threading;
using Stride.Games;
using System;
using System.Runtime.CompilerServices;

namespace ArchECSStride.Code.Systems;
/// <summary>
/// Syncs Arch world positions to Strides Positions.
/// </summary>
[DataContract(nameof(UpdateStridePositionsSystem))]
public class UpdateStridePositionsSystem : ArchSystem
{
	private StrideEntityManager _entityManager;
	private QueryDescription _queryDescription;

	public override void Start()
	{
		_entityManager = Services.GetService<StrideEntityManager>();

		_queryDescription = new QueryDescription().
			WithAny<StrideId, Position, Rotation>();
	}

	public override void Update(in GameTime state)
	{
		World.Query(in _queryDescription, (ref StrideId strideId, ref Position position, ref Rotation rotation) =>
		{
			_entityManager.Entities[strideId.Id].Transform.Position = position.CurrentPosition;
			_entityManager.Entities[strideId.Id].Transform.Rotation = rotation.CurrentRotation;
		});

		//var testQuery = World.Query(in _queryDescription);
		// this works but is slower due to needing to use chunk.GetFirst AND
		// the chunk size being limited to 166 Entities per chunk.
		//foreach(var chunk in testQuery.GetChunkIterator())
		//{
		//	Dispatcher.For(0, chunk.Entities.Length, j =>
		//	{
		//		var reference = chunk.GetFirst<StrideId, Position, Rotation>();
		//	
		//		ref var strideId = ref Unsafe.Add(ref reference.t0, j);
		//		ref var position = ref Unsafe.Add(ref reference.t1, j);
		//		ref var rotation = ref Unsafe.Add(ref reference.t2, j);
		//	
		//		// strideId is not initialized on startup
		//		if (strideId.Id == Guid.Empty) return;
		//		_entityManager.Entities[strideId.Id].Transform.Position = position.CurrentPosition;
		//		_entityManager.Entities[strideId.Id].Transform.Rotation = rotation.CurrentRotation;
		//	});
		//}
	}
}
