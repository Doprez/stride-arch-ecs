using ArchECSStride.Code.Arch;
using Stride.Core;
using Stride.Data;
using System.Collections.Generic;

namespace ArchECSStride.Code.Configurations;
/// <summary>
/// A central place to add Systems to the Arch ECS.
/// </summary>
[DataContract]
[Display("Arch Config")]
public class ArchSettings : Configuration
{
	public List<SystemBase> Systems { get; set; } = new();
}
