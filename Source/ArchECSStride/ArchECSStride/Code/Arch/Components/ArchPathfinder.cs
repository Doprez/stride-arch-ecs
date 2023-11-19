using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchECSStride.Code.Arch.Components;
[DataContract(nameof(ArchPathfinder))]
[ComponentCategory("Arch Components")]
public class ArchPathfinder : ArchComponent
{
	public override object ComponentValue { get; set; } = new Pathfinder();
	public override void SetData()
	{
		ComponentValue = new Pathfinder();
	}
}

public struct Pathfinder
{
	public Vector3 Target;
	public bool SetNewPath;
	// I think this ruins the benefit of a struct but I dont have an alternative atm.
	public List<Vector3> Path = new();
	public bool ShouldMove;
	public float Speed = 1;

	public Pathfinder()
	{
	}
}