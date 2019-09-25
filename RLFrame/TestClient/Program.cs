using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TestClient;

namespace RLFrame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(HUD.WindowWidth, HUD.WindowHeight);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Init()
        {
            HUD.InitHUD();
            HUD.MapConsole.DrawLine(new Point(0, 0), new Point(HUD.MapWidth, 0), Color.White, Color.Black, '#');
        }
    }
}
