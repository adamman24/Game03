using System;

namespace Game03
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game03())
                game.Run();
        }
    }
}
