using ArchECSStride.Code;

namespace ArchECSStride
{
    class ArchECSStrideApp
    {
        static void Main(string[] args)
        {
            using (var game = new CustomGame())
            {
                game.Run();
            }
        }
    }
}
