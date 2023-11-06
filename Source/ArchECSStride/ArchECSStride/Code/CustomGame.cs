using Stride.Engine;
using Arch.Core;
using Stride.Games;
using ArchECSStride.Code.Configurations;
using ArchECSStride.Code.Arch;
using System.Collections.Generic;
using Arch.Core.Utils;
using StrideEntity = Stride.Engine.Entity;
using ArchEntity = Arch.Core.Entity;
using Arch.Core.Extensions;

namespace ArchECSStride.Code;
public class CustomGame : Game
{
	private World _world;
	private ArchSettings _archSettings;

	protected override void BeginRun()
	{
		base.BeginRun();
		
		_world = World.Create();
		_archSettings = Settings.Configurations.Get<ArchSettings>();

		// Register the world as a service so that it can be accessed by Stride systems.
		Services.AddService(_world);

		// Since I want to register settings through Stride the constructors can't have parameters.
		foreach(var archSystem in _archSettings.Systems)
		{
			archSystem.InitializeSystem(_world, Services, SceneSystem);
		}

		foreach (var archSystem in _archSettings.Systems)
		{
			archSystem.Start();
		}

		// Register entities at startup
		foreach(var entity in SceneSystem.SceneInstance.RootScene.Entities)
		{
			RegisterEntity(entity);
		}
	}

	protected override void Update(GameTime gameTime)
	{
		base.Update(gameTime);

		for (int i = 0; i < _archSettings.Systems.Count; i++)
		{
			_archSettings.Systems[i].Update(in gameTime);
		}
	}

	protected override void EndRun()
	{
		World.Destroy(_world);
		base.EndRun();
	}

	private void RegisterEntity(StrideEntity e)
	{
		if (e.GetComponent<ArchComponent>() == null) return;

		List<object> archComponents = new();
		var components = e.GetComponents<ArchComponent>();
		foreach (var component in components)
		{
			component.SetData();
			archComponents.Add(component.ComponentValue);
		}
		CreateFromArray(archComponents.ToArray());
	}

	public ArchEntity CreateFromArray(object[] components)
	{
		ComponentType[] types = GetComponentTypesForArchetype(components);
		ArchEntity entity = _world.Create(types);
		//SetFromArray(entity, components);
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
