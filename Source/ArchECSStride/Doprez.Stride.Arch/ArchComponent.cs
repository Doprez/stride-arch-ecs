using Stride.Core;
using Stride.Engine;

namespace Doprez.Stride.Arch;
/// <summary>
/// This is only to provide a way to mark a component as an ArchComponent and serialize it in the Editor.
/// </summary>
[DataContract(Inherited = true)]
[ComponentCategory("Arch Components")]
public abstract class ArchComponent : EntityComponent
{
    /// <summary>
    /// Component to add to the Arch entity
    /// </summary>
    [DataMemberIgnore]
    public abstract object ComponentValue { get; set; }

    /// <summary>
    /// Sets data After Strides Entity is created and before it is registered with Arch ECS.
    /// </summary>
    public virtual void SetData()
    {

    }
}
