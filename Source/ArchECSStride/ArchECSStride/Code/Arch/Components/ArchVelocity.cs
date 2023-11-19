using Stride.Core;
using Stride.Engine;

namespace ArchECSStride.Code.Arch.Components;
[DataContract(nameof(ArchVelocity))]
[ComponentCategory("Arch Components")]
public class ArchVelocity : ArchComponent
{
	public override object ComponentValue { get; set; } = new Velocity();

	public override void SetData()
	{
		ComponentValue = new Velocity()
		{
			X = 0.5f,
			Y = 0.5f,
			Z = 0.5f
		};
	}
}

public struct Velocity
{
	public float X;
	public float Y;
	public float Z;
}