using System;

namespace IDGTF2WeaponGenerator
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TF2WeaponGenerator())
                game.Run();
        }
    }
}
