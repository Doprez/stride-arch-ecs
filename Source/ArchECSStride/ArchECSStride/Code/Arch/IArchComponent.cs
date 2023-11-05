using Stride.Core;
using System;

namespace ArchECSStride.Code.Arch;
public interface IArchComponent
{
	/// <summary>
	/// Component to add to the Arch entity
	/// </summary>
	public object ComponentType { get; set; }
}
