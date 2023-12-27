using Doprez.Stride.Arch;
using Stride.Core;
using Stride.Data;
using System.Collections.Generic;

namespace Doprez.Stride.Arch.Configurations;
/// <summary>
/// A central place to add Systems to the Arch ECS.
/// </summary>
[DataContract]
[Display("Arch Config")]
public class ArchSettings : Configuration
{
    public bool AddDefaultRegisterSystem { get; set; } = true;
    public List<SystemBase> Systems { get; set; } = new();
}
