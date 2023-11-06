using Stride.Core;
using Stride.Engine;

namespace ArchECSStride.Code.Arch.Components;
[DataContract(nameof(ArchTest))]
[ComponentCategory("Arch Components")]
public class ArchTest : ArchComponent
{
	[DataMemberIgnore]
	public override object ComponentValue { get; set; } = new TestComponent();
}

public struct TestComponent
{
	public int Number;
	public string Text;
}
