using Stride.Engine;
using Arch.Core;
using Stride.Games;
using ArchECSStride.Code.Configurations;

namespace ArchECSStride.Code;
public class CustomGame : Game
{
	private World _world;
	//private JobScheduler.JobScheduler _jobScheduler;
	private ArchSettings _archSettings;

	protected override void BeginRun()
	{
		base.BeginRun();
		
		_world = World.Create();
		//_jobScheduler = new("SampleWorkerThreads");
		_archSettings = Settings.Configurations.Get<ArchSettings>();

		// Register the world as a service so that it can be accessed by Stride systems.
		Services.AddService(_world);

		// Since I want to register settings through Stride the constructors can't have parameters.
		foreach(var archSystem in _archSettings.Systems)
		{
			archSystem.InitializeSystem(_world, Services);
		}

		foreach (var archSystem in _archSettings.Systems)
		{
			archSystem.Start();
		}
		_world.Create();
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
		//_jobScheduler.Dispose();
		base.EndRun();
	}
}
