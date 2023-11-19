using ArchECSStride.Code.GamePlay.Data;
using Stride.Core;
using Stride.Engine;

namespace ArchECSStride.Code.Arch.Components;
[DataContract(nameof(ArchAgentJob))]
[ComponentCategory("Arch Components")]
public class ArchAgentJob : ArchComponent
{
	public AgentJobType CurrentAgentJob { get; set; } = AgentJobType.WoodCutter;

	[DataMemberIgnore]
	public override object ComponentValue { get; set; } = new AgentJob();

	public override void SetData()
	{
		ComponentValue = CurrentAgentJob;
	}
}

public struct AgentJob
{
	public AgentJobType CurrentJob;
}
