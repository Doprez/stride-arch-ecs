﻿using ArchECSStride.Code.Arch;
using Stride.Core;
using Stride.Core.Serialization;
using Stride.Core.Serialization.Contents;
using Stride.Engine;

namespace ArchECSStride.Code.Systems;
[DataContract(nameof(SpawnPrefabSystem))]
public class SpawnPrefabSystem : SystemBase
{
	public UrlReference PrefabToSpawn { get; set; }
	public int AmountOfPrefabs { get; set; } = 100;

	private ContentManager _contentManager;

	public override void Start()
	{
		_contentManager = Services.GetService<ContentManager>();
		var prefab = _contentManager.Load<Prefab>(PrefabToSpawn.Url);

		for (int i = 0; i < AmountOfPrefabs; i++)
		{
			var entity = prefab.Instantiate();
			SceneSystem.SceneInstance.RootScene.Entities.Add(entity[0]);
		}
	}
}