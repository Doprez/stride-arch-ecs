using Stride.Core;

namespace ArchECSStride.Code.Arch.Components;
/// <summary>
/// Gets added automatically if another ArchComponent exists on the same entity.
/// </summary>
public class ArchStrideId : ArchComponent
{
	[DataMemberIgnore]
	public override object ComponentValue { get; set; } = new StrideId();
}

public struct StrideId
{
	public int Id;
}
