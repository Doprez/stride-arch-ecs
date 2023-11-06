using Arch.Core.Utils;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using System;

namespace ArchECSStride.Code.Arch.Components;
[DataContract(nameof(ArchPosition))]
[ComponentCategory("Arch Components")]
public class ArchPosition : ArchComponent
{
	public bool UseStridePosition { get; set; } = true;
	public Vector3 StartPosition { get; set; }

	[DataMemberIgnore]
	public override object ComponentValue { get; set; } = new Vector3();

	public override void SetData()
	{
		if (UseStridePosition)
		{
			ComponentValue = Entity.Transform.Position;
		}
		else
		{
			ComponentValue = StartPosition;
		}
	}
}
