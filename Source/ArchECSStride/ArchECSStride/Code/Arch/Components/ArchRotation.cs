using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;

namespace ArchECSStride.Code.Arch.Components;
[DataContract(nameof(ArchRotation))]
[ComponentCategory("Arch Components")]
public class ArchRotation : ArchComponent
{
	public Quaternion StartRotation { get; set; }

	[DataMemberIgnore]
	public override object ComponentValue { get; set; } = new Quaternion();

	public override void SetData()
	{
		ComponentValue = StartRotation;
	}
}
