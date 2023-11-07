using Arch.Core;
using ArchECSStride.Code.Arch;
using ArchECSStride.Code.Arch.Components;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Games;
using Stride.Profiling;
using System.Linq;

namespace ArchECSStride.Code.Systems;
[DataContract(nameof(TestSystem))]
public class TestSystem : SystemBase
{
	private DebugTextSystem _debugText;
	private QueryDescription _queryDescription;

	public override void Start()
	{
		_debugText = Services.GetService<DebugTextSystem>();

		_queryDescription = new QueryDescription().
			WithAny<Vector3, Quaternion>();
	}

	public override void Update(in GameTime state)
	{
		var result = World.CountEntities(in _queryDescription);
		TestComponent testComponent = new();
		World.Query(in _queryDescription, (ref Vector3 position, ref Quaternion rotation, ref TestComponent test) => 
		{
			testComponent = test;
			position.X++; 
		});

		_debugText.Print($"TestSystem: {result} \nName: {testComponent.Text}", new Int2(50, 50));
	}
}
