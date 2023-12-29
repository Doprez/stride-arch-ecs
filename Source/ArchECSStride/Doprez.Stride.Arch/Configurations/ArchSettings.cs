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
    /// <summary>
    /// These systems will run sequentially.
    /// </summary>
    public List<ArchSystem> Systems { get; set; } = new();
    /// <summary>
    /// These systems with run in parallel with eachother AFTER Systems.
    /// </summary>
    public List<ArchSystem> ParallelSystems { get; set; } = new();
}
