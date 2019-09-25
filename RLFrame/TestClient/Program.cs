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
            SadConsole.Game.Create(HUD.Width, HUD.Height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Init()
        {
            HUD.InitHUD();
            HUD.GameMap.DrawLine(new Point(0, 0), new Point(60, 0), Color.White, Color.Black, '#');
            // Set the background
            //HUD.SkillsConsole.SetBackground(0, 0, Microsoft.Xna.Framework.Color.BlanchedAlmond);
            //GameMap.SetBackground(0, 0, Microsoft.Xna.Framework.Color.Purple);
            //Sidebar.SetBackground(0, 0, Microsoft.Xna.Framework.Color.Green);
        }
    }
}
