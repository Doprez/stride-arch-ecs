using Doprez.Stride.Arch;
using Stride.Core;
using System;

namespace Doprez.Stride.Arch.Components;
/// <summary>
/// Gets added automatically if another ArchComponent exists on the same entity.
/// </summary>
public class ArchStrideId : ArchComponent
{
    [DataMemberIgnore]
    public override object ComponentValue { get; set; } = new StrideId();
}

public struct StrideId
{
    public Guid Id;
}
