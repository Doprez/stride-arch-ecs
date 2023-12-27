using Arch.Core;
using Doprez.Stride.Arch;
using Doprez.Stride.Arch.Components;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Games;
using Stride.Profiling;

namespace ArchECSStride.Code.Systems;
/// <summary>
/// So far this test class turned into basic logging of entity count and FPS.
/// </summary>
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

		_debugText.Print($"Entities: {result} \nFPS: {_game.UpdateTime.FramePerSecond} \nUpdate Time: {state.TimePerFrame.TotalMilliseconds}", new Int2(50, 50));
	}
}
