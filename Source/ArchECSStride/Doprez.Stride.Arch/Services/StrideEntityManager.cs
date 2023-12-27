using Stride.Engine;
using System;
using System.Collections.Generic;

namespace Doprez.Stride.Arch.Services;
public class StrideEntityManager
{
    public Dictionary<Guid, Entity> Entities { get; private set; } = new();

    /// <summary>
    /// Returns the index of the added entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Guid AddEntity(Entity entity)
    {
        var newId = Guid.NewGuid();
        Entities.Add(newId, entity);
        return newId;
    }

    public void RemoveEntity(Guid id)
    {
        // Need to make sure this does not shift items in the array
        Entities.Remove(id);
    }

    public Guid GetEntityId(Entity entity)
    {
        foreach (var item in Entities)
        {
            if (item.Value == entity) return item.Key;
        }

        return Guid.Empty;
    }
}
