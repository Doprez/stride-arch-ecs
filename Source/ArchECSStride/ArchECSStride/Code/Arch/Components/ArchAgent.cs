using ArchECSStride.Code.GamePlay.Data;
using Doprez.Stride.Arch;
using Stride.Core;

namespace ArchECSStride.Code.Arch.Components;
public class ArchAgent : ArchComponent
{
	[DataMemberIgnore]
	public override object ComponentValue { get; set; }
}

public struct Agent
{
	public AgentJobType CurrentJob;
}