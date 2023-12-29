using Arch.Core;
using ArchECSStride.Code.NPC.Components;
using Doprez.Stride.Arch;
using Stride.Games;

namespace ArchECSStride.Code.NPC.Systems;
public class NPCAgentSystem : ArchSystem
{
	private QueryDescription _queryDescription;

	public override void Start()
	{
		_queryDescription = new QueryDescription().
			WithAny<NPCAgent>();
	}

	public override void Update(in GameTime state)
	{
		World.Query(in _queryDescription, (ref NPCAgent agent) =>
		{
			agent.CurrentJobId = 0;
		});
	}
}
