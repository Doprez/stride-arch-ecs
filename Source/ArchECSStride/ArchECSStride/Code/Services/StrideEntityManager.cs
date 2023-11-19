using Stride.Engine;
using System.Collections.Generic;

namespace ArchECSStride.Code.Services;
public class StrideEntityManager
{
	public List<Entity> Entities { get; private set; } = new();

	/// <summary>
	/// Returns the index of the added entity.
	/// </summary>
	/// <param name="entity"></param>
	/// <returns></returns>
	public int AddEntity(Entity entity)
	{
		Entities.Add(entity);
		return Entities.IndexOf(entity);
	}

	public void RemoveEntity(int index)
	{
		// Need to make sure this does not shift items in the array
		Entities.RemoveAt(index);
	}

	public int IndexOf(Entity entity)
	{
		return Entities.IndexOf(entity);
	}
}
