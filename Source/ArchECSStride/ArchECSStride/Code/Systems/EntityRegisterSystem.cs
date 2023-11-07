using Arch.Core.Utils;
using ArchECSStride.Code.Arch;
using StrideEntity = Stride.Engine.Entity;
using ArchEntity = Arch.Core.Entity;
using Arch.Core.Extensions;
using Stride.Engine;
using System.Collections.Generic;
using Stride.Core;
using ArchECSStride.Code.Arch.Components;
using ArchECSStride.Code.Services;

namespace ArchECSStride.Code.Systems;
/// <summary>
/// A system that registers Stride entities with Arch ECS when they are created at runtime.
/// </summary>
[DataContract(nameof(EntityRegisterSystem))]
public class EntityRegisterSystem : SystemBase
{
	private StrideEntityManager _strideEntityManager;

	public override void Start()
	{
		_strideEntityManager = Services.GetService<StrideEntityManager>();
		SceneSystem.SceneInstance.EntityAdded += SceneInstance_EntityAdded;
	}

	private void SceneInstance_EntityAdded(object sender, StrideEntity e)
	{
		if (e.GetComponent<ArchComponent>() == null) return;

		//register to Entity Manager
		ArchStrideId id = new ArchStrideId();
		StrideId strideId = new StrideId();
		strideId.Id = _strideEntityManager.AddEntity(e);
		id.ComponentValue = strideId;
		e.Add(id);


		List<object> archComponents = new();
		var components = e.GetComponents<ArchComponent>();
		foreach(var component in components)
		{
			component.SetData();
			archComponents.Add(component.ComponentValue);
		}
		CreateFromArray(archComponents.ToArray());
	}

	public ArchEntity CreateFromArray(object[] components)
	{
		ComponentType[] types = GetComponentTypesForArchetype(components);
		ArchEntity entity = World.Create(types);
		SetFromArray(entity, components);
		return entity;
	}

	public void SetFromArray(ArchEntity entity, object[] components)
	{
		entity.SetRange(components);
	}

	private ComponentType[] GetComponentTypesForArchetype(object[] components)
	{
		ComponentType[] types = new ComponentType[components.Length];
		for (int i = 0; i < components.Length; i++)
		{
			ComponentType type;
			if (!ComponentRegistry.TryGet(components[i].GetType(), out type))
			{
				type = ComponentRegistry.Add(components[i].GetType());
			}
			types[i] = type;
		}
		return types;
	}
}
