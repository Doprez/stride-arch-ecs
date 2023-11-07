using Stride.Core;
using Stride.Engine;

namespace ArchECSStride.Code.Arch.Components;
[DataContract(nameof(ArchTest))]
[ComponentCategory("Arch Components")]
public class ArchTest : ArchComponent
{
	public TestComponent StartComponent { get; set; } = new();

	[DataMemberIgnore]
	public override object ComponentValue { get; set; } = new TestComponent();

	public override void SetData()
	{
		ComponentValue = StartComponent;
	}
}

[DataContract]
public struct TestComponent
{
	public int Number;
	public string Text;
}
