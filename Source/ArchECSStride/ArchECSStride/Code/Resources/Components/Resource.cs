using ArchECSStride.Code.GamePlay.Data;
using Doprez.Stride.Arch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchECSStride.Code.Resources.Components;
public class ArchResource : ArchComponent
{
    public int Amount { get; set; }
    public override object ComponentValue { get; set; } = new Resource();
}

public struct Resource
{
    public ResourceType ResourceType;
    public int Amount;
}

