using Arch.Core.Utils;
using ArchECSStride.Code.Arch;
using StrideEntity = Stride.Engine.Entity;
using ArchEntity = Arch.Core.Entity;
using Arch.Core.Extensions;
using Stride.Engine;
using System.Collections.Generic;
using Stride.Core;

namespace ArchECSStride.Code.Systems;
[DataContract(nameof(EntityRegisterSystem))]
public class EntityRegisterSystem : SystemBase
{
	public override void Start()
	{
		SceneSystem.SceneInstance.EntityAdded += SceneInstance_EntityAdded;
	}

	private void SceneInstance_EntityAdded(object sender, StrideEntity e)
	{
		if (e.GetComponent<IArchComponent>() == null) return;

		List<object> archComponents = new();
		var components = e.GetComponents<IArchComponent>();
		foreach(var component in components)
		{
			archComponents.Add(component.ComponentType);
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
		switch (components.Length)
		{
			case 1:
				entity.Set(components[0]);
				break;
			case 2:
				entity.Set(components[0], components[1]);
				break;
			case 3:
				entity.Set(components[0], components[1], components[2]);
				break;
			case 4:
				entity.Set(components[0], components[1], components[2], components[3]);
				break;
			case 5:
				entity.Set(components[0], components[1], components[2], components[3], components[4]);
				break;
			case 6:
				entity.Set(components[0], components[1], components[2], components[3], components[4], components[5]);
				break;
			case 7:
				entity.Set(components[0], components[1], components[2], components[3], components[4], components[5], components[6]);
				break;
			case 8:
				entity.Set(components[0], components[1], components[2], components[3], components[4], components[5], components[6], components[7]);
				break;
			case 9:
				entity.Set(components[0], components[1], components[2], components[3], components[4], components[5], components[6], components[7], components[8]);
				break;
			case 10:
				entity.Set(components[0], components[1], components[2], components[3], components[4], components[5], components[6], components[7], components[8], components[9]);
				break;
			case 11:
				entity.Set(components[0], components[1], components[2], components[3], components[4], components[5], components[6], components[7], components[8], components[9], components[10]);
				break;
			case 12:
				entity.Set(components[0], components[1], components[2], components[3], components[4], components[5], components[6], components[7], components[8], components[9], components[10], components[11]);
				break;
			case 13:
				entity.Set(components[0], components[1], components[2], components[3], components[4], components[5], components[6], components[7], components[8], components[9], components[10], components[11], components[12]);
				break;
			case 14:
				entity.Set(components[0], components[1], components[2], components[3], components[4], components[5], components[6], components[7], components[8], components[9], components[10], components[11], components[12], components[13]);
				break;
			case 15:
				entity.Set(components[0], components[1], components[2], components[3], components[4], components[5], components[6], components[7], components[8], components[9], components[10], components[11], components[12], components[13], components[14]);
				break;
			case 16:
				entity.Set(components[0], components[1], components[2], components[3], components[4], components[5], components[6], components[7], components[8], components[9], components[10], components[11], components[12], components[13], components[14], components[15]);
				break;
		}
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
