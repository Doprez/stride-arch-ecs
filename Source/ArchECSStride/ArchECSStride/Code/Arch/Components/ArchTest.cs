using Stride.Core;
using Stride.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchECSStride.Code.Arch.Components;
[DataContract(nameof(ArchTest))]
[ComponentCategory("Arch Components")]
public class ArchTest : ArchComponent<TestComponent>, IArchComponent
{
	[DataMemberIgnore]
	object IArchComponent.ComponentType { get; set; } = new TestComponent();
}

public struct TestComponent
{
	public int Number;
	public string Text;
}
