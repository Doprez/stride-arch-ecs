using Arch.Core;
using Stride.Core;
using Stride.Engine;
using Stride.Games;

namespace Doprez.Stride.Arch;
/// <summary>
/// This is the base class for all Systems in the Arch ECS. Can be added throught the GameSettings asset.
/// </summary>
[DataContract]
public abstract class ArchSystem
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
    public IServiceRegistry Services { get; private set; }
    [DataMemberIgnore]
    public SceneSystem SceneSystem { get; private set; }

    /// <summary>
    /// Constructor parameters must be empty for Stride to be able to serialize them, this is a work around.
    /// </summary>
    /// <param name="world"></param>
    /// <param name="services"></param>
    public void InitializeSystem(World world, IServiceRegistry services, SceneSystem sceneSystem)
    {
        World = world;
        Services = services;
        SceneSystem = sceneSystem;
    }

    /// <summary>
    /// Called after all Systems have been initialized.
    /// </summary>
    public virtual void Start() { }

    /// <summary>
    /// Should be called within the update loop to update this system and execute its logic.
    /// </summary>
    /// <param name="state">A external state being passed to this method to be used.</param>
    public virtual void Update(in GameTime state) { }

}
