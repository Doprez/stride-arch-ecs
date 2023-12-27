using Arch.Core.Utils;
using StrideEntity = Stride.Engine.Entity;
using ArchEntity = Arch.Core.Entity;
using Arch.Core.Extensions;
using Stride.Engine;
using Doprez.Stride.Arch.Components;
using Doprez.Stride.Arch.Services;
using Arch.Core;

namespace Doprez.Stride.Arch.Systems;
/// <summary>
/// A system that registers Stride entities with Arch ECS when they are created at runtime.
/// </summary>
public class DefaultEntityRegisterSystem : SystemBase
{
    private StrideEntityManager _strideEntityManager;
    private QueryDescription _query;

    public override void Start()
    {
        _strideEntityManager = Services.GetService<StrideEntityManager>();

        _query = new QueryDescription().WithAny<StrideId>();

        SceneSystem.SceneInstance.EntityAdded += SceneInstance_EntityAdded;
        SceneSystem.SceneInstance.EntityRemoved += SceneInstance_EntityRemoved;
    }

    // TODO This doesnt work and would be very slow even if it did.
    private void SceneInstance_EntityRemoved(object sender, StrideEntity e)
    {
        if (e.Get<ArchComponent>() == null) return;

        var id = _strideEntityManager.GetEntityId(e);

        ArchEntity entityToDestroy = new();
        bool found = false;
        World.Query(in _query, (ref ArchEntity entity, ref StrideId strideId) =>
        {
            if (strideId.Id == id)
            {
                entityToDestroy = entity;
				found = true;
			}
        });

		// Remove from Arch ECS
        //if(found)
		//    World.Destroy(entityToDestroy);
		// Unregister from Entity Manager
		_strideEntityManager.RemoveEntity(id);
    }

    private void SceneInstance_EntityAdded(object sender, StrideEntity e)
    {
        if (e.Get<ArchComponent>() == null) return;

        //register to Entity Manager
        ArchStrideId id = new();
        StrideId strideId = new()
        {
            Id = _strideEntityManager.AddEntity(e)
        };
        id.ComponentValue = strideId;
        e.Add(id);


        List<object> archComponents = new();
        var components = e.GetAll<ArchComponent>();
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
