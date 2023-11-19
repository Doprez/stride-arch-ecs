using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;

namespace ArchECSStride.Code.Arch.Components;
[DataContract(nameof(ArchRotation))]
[ComponentCategory("Arch Components")]
public class ArchRotation : ArchComponent
{
	[DataMemberIgnore]
	public override object ComponentValue { get; set; } = new Rotation();

	public override void SetData()
	{
		ComponentValue = new Rotation();
	}
}

public struct Rotation
{
	public Quaternion CurrentRotation;
}
