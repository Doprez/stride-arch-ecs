using Stride.Engine;
using System.Linq;

namespace ArchECSStride.Code;
/// <summary>
/// Searches for a instanced component and assigns the value to this Entity.
/// </summary>
public class InitializeInstanceMesh : StartupScript
{
	public InstanceComponent InstanceComponent { get; set; }
	public string InstancedModelName { get; set; } = "InstancedMesh";

	public override void Start()
	{
		AddInstanceModel();
	}

	private void AddInstanceModel()
	{
		var scene = SceneSystem.SceneInstance.RootScene;
		var instancingComponent = scene.Entities.Where(x => x.Name == InstancedModelName).FirstOrDefault().Get<InstancingComponent>();

		InstanceComponent.Master = instancingComponent;
	}
}
