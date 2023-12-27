using Stride.Core;
using Stride.Engine;

namespace Doprez.Stride.Arch;
public static class ArchGameExtensions
{
    public static void AddArch(this Game game, IServiceRegistry services)
    {
        var scene = services.GetService<SceneSystem>().SceneInstance;
        scene.Processors.Add(new ArchProcessor());
    }
}
