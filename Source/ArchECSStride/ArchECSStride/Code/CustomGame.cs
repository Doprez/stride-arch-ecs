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
using ArchECSStride.Code.Services;
using ArchECSStride.Code.Arch.Components;

namespace ArchECSStride.Code;
public class CustomGame : Game
{
	private World _world;
	private ArchSettings _archSettings;
	private StrideEntityManager _strideEntityManager = new();

	protected override void BeginRun()
	{
		base.BeginRun();
		
		_world = World.Create();
		_archSettings = Settings.Configurations.Get<ArchSettings>();

		// Register the world as a service so that it can be accessed by Stride systems.
		Services.AddService(_world);
		// This service will be used to quickly access Entities from Stride with the stored index in Arch Entities
		Services.AddService(_strideEntityManager);

		// Since I want to register settings through Stride the constructors can't have parameters.
		foreach(var archSystem in _archSettings.Systems)
		{
			archSystem.InitializeSystem(_world, Services, SceneSystem);
		}

		foreach (var archSystem in _archSettings.Systems)
		{
			archSystem.Start();
		}

		// EntityRegisterSystem is run before this, causing some issues.
		// Register entities at startup
		//foreach(var entity in SceneSystem.SceneInstance.RootScene.Entities)
		//{
		//	RegisterEntity(entity);
		//}
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

		//register to Entity Manager
		ArchStrideId id = new ArchStrideId();
		StrideId strideId = new StrideId();
		strideId.Id = _strideEntityManager.AddEntity(e);
		id.ComponentValue = strideId;
		e.Add(id);

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
