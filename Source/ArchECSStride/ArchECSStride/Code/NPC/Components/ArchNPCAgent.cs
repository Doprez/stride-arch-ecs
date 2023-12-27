using ArchECSStride.Code.Arch;
using Doprez.Stride.Arch;
using Stride.Core;

namespace ArchECSStride.Code.NPC.Components;
public class ArchNPCAgent : ArchComponent
{
	[DataMemberIgnore]
	public override object ComponentValue { get; set; }
}

public struct NPCAgent
{
	public ushort CurrentStateId;
}
