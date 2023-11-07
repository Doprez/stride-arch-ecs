using Arch.Core;
using ArchECSStride.Code.Arch;
using ArchECSStride.Code.Arch.Components;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Games;
using Stride.Profiling;
using System.Linq;

namespace ArchECSStride.Code.Systems;
[DataContract(nameof(TestSystem))]
public class TestSystem : SystemBase
{
	private DebugTextSystem _debugText;
	private QueryDescription _queryDescription;
	private IGame _game;

	public override void Start()
	{
		_debugText = Services.GetService<DebugTextSystem>();
		_game = Services.GetSafeServiceAs<IGame>();

		_queryDescription = new QueryDescription().
			WithAny<StrideId>();
	}

	public override void Update(in GameTime state)
	{
		var result = World.CountEntities(in _queryDescription);

		_debugText.Print($"TestSystem entities: {result} \nFPS: {_game.UpdateTime.FramePerSecond} \nUpdate Time: {state.TimePerFrame}", new Int2(50, 50));
	}
}
