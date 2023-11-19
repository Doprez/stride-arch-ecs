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
	public Position StartPosition { get; set; }

	[DataMemberIgnore]
	public override object ComponentValue { get; set; } = new Position();

	public override void SetData()
	{
		if (UseStridePosition)
		{
			var position = new Position()
			{
				CurrentPosition = Entity.Get<TransformComponent>().Position
			};
			ComponentValue = position;
		}
		else
		{
			ComponentValue = StartPosition;
		}
	}
}

[DataContract]
public struct Position
{
	public Vector3 CurrentPosition;
}
