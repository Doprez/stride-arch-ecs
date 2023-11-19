using Arch.Core;
using ArchECSStride.Code.Arch;
using ArchECSStride.Code.Arch.Components;
using ArchECSStride.Code.Services;
using Stride.Core;
using Stride.Games;

namespace ArchECSStride.Code.Systems;
/// <summary>
/// Syncs Arch world positions to Strides Positions.
/// </summary>
[DataContract(nameof(UpdateStridePositionsSystem))]
public class UpdateStridePositionsSystem : SystemBase
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
	}
}
