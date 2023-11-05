using Stride.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stride.Engine;
public static class EntityExtensions
{
	/// <summary>
	/// Gets all components of type T
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <returns></returns>
	public static IEnumerable<T> GetComponents<T>(this Entity entity)
	{
		foreach (var entityComponent in entity)
		{
			if (entityComponent is T t)
				yield return t;
		}
	}

	/// <summary>
	/// Allows finding components of any type.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entity"></param>
	/// <returns></returns>
	public static T GetComponent<T>(this Entity entity)
	{
		foreach (var entityComponent in entity)
		{
			if (entityComponent is T t)
				return t;
		}

		return default;
	}
}
