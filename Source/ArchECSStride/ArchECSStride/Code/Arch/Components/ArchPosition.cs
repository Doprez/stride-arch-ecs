using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using System;

namespace ArchECSStride.Code.Arch.Components;
[DataContract(nameof(ArchPosition))]
[ComponentCategory("Arch Components")]
public class ArchPosition : ArchComponent<Vector3>, IArchComponent
{
	[DataMemberIgnore]
	object IArchComponent.ComponentType { get; set; } = new Vector3();
}
