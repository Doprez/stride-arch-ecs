using Stride.Core;
using Stride.Engine;

namespace ArchECSStride.Code.Arch;
[DataContract]
public abstract class ArchComponent<T> : EntityComponent
{
	[DataMemberIgnore]
	public T Component { get; set; }
}
