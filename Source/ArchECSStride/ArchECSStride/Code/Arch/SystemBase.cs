using Arch.Core;
using Stride.Core;
using Stride.Games;

namespace ArchECSStride.Code.Arch;
[DataContract]
public abstract class SystemBase
{

	/// <summary>
	///     The <see cref="World"/> for which this system works and must access.
	/// </summary>
	[DataMemberIgnore]
	public World World { get; private set; }
	/// <summary>
	/// Gives systems direct access to registered <see cref="ServiceRegistry"/> Services in Stride.
	/// </summary>
	[DataMemberIgnore]
	public ServiceRegistry Services { get; private set; }

	/// <summary>
	/// Constructor parameters must be empty for Stride to be able to serialize them, this is a work around.
	/// </summary>
	/// <param name="world"></param>
	/// <param name="services"></param>
	public void InitializeSystem(World world, ServiceRegistry services)
	{
		World = world;
		Services = services;
	}

	/// <summary>
	/// Called after all Systems have been initialized.
	/// </summary>
	public virtual void Start() { }

	/// <summary>
	///     Should be called within the update loop to update this system and executes its logic.
	/// </summary>
	/// <param name="state">A external state being passed to this method to be used.</param>
	public abstract void Update(in GameTime state);

}
