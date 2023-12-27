using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Doprez.Stride.Arch.Components;
using Doprez.Stride.Arch.Configurations;
using Doprez.Stride.Arch.Services;
using Doprez.Stride.Arch.Systems;
using Stride.Core.Threading;
using Stride.Engine;
using Stride.Engine.Design;
using Stride.Games;
using System.Collections.Generic;
using ArchEntity = Arch.Core.Entity;
using StrideEntity = Stride.Engine.Entity;

namespace Doprez.Stride.Arch;
/// <summary>
/// The main processor for Arch ECS. That gets added when its registered in the <see cref="Game"/> class.
/// </summary>
public class ArchProcessor : EntityProcessor<ArchComponent>
{
    private World _world;
    private ArchSettings _archSettings;
    private StrideEntityManager _strideEntityManager = new();
    private GameSettings _gameSettings;
    private SceneSystem _sceneSystem;

    public ArchProcessor()
    {
        Order = 50000;
    }

    protected override void OnSystemAdd()
    {
        // get base Stride systems
        _gameSettings = Services.GetService<IGameSettingsService>().Settings;
        _sceneSystem = Services.GetService<SceneSystem>();

        _world = World.Create();
        _archSettings = _gameSettings.Configurations.Get<ArchSettings>();

        // Register the world as a service so that it can be accessed by Stride systems.
        Services.AddService(_world);
        // This service will be used to quickly access Entities from Stride with the stored id in Arch Entities
        Services.AddService(_strideEntityManager);

        if (_archSettings.AddDefaultRegisterSystem)
        {
            var registerSystem = new EntityRegisterSystem();
            _archSettings.Systems.Add(registerSystem);
            registerSystem.InitializeSystem(_world, Services, _sceneSystem);
            registerSystem.Start();
        }

        // EntityRegisterSystem is run before this, causing some issues.
        // Register entities at startup
        foreach (var entity in _sceneSystem.SceneInstance.RootScene.Entities)
        {
            RegisterEntity(entity);
        }

        // Since I want to register settings through Stride the constructors can't have parameters.
        // So I have to initialize them here.
        foreach (var archSystem in _archSettings.Systems)
        {
            archSystem.InitializeSystem(_world, Services, _sceneSystem);
        }

        foreach (var archSystem in _archSettings.Systems)
        {
            archSystem.Start();
        }
    }

    public override void Update(GameTime time)
    {
        // This works but will need to make configurable in case of issues between systems.
        //Dispatcher.For(0, _archSettings.Systems.Count, i =>
        //{
        //	_archSettings.Systems[i].Update(in time);
        //});

        for (int i = 0; i < _archSettings.Systems.Count; i++)
        {
            _archSettings.Systems[i].Update(in time);
        }
    }

    private void RegisterEntity(StrideEntity entity)
    {
        if (entity.Get<ArchComponent>() == null) return;

        //register to Entity Manager
        ArchStrideId id = new();
        StrideId strideId = new()
        {
            Id = _strideEntityManager.AddEntity(entity)
        };
        id.ComponentValue = strideId;
        entity.Add(id);

        List<object> archComponents = new();
        var components = entity.GetAll<ArchComponent>();

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

    protected override void OnSystemRemove()
    {
        World.Destroy(_world);
    }

}
