using Doprez.Stride.Arch;
using Stride.Engine;

namespace ArchECSStride.Code;
public class CustomGame : Game
{
	protected override void BeginRun()
	{
		this.AddArch(Services);
	}
}
