using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ConsoleLayers;
using GameSystems;
using Microsoft.Xna.Framework.Input;

namespace RLFrame
{
    class Program
    {
        private static SadConsole.Entities.Entity player;

        static void Main(string[] args)
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(HUD.WindowWidth, HUD.WindowHeight);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Hook the update event that happens each frame so we can trap keys and respond.
            SadConsole.Game.OnUpdate = Update;

            // Start the game.
            SadConsole.Game.Instance.Run();

            //
            // Code here will not run until the game window closes.
            SadConsole.Game.Instance.Dispose();
        }

        private static void Update(GameTime time)
        {
            // Called each logic update.            
            InputHandling.TakeInput(player);
        }

        private static void Init()
        {
            HUD.InitHUD();
            //HUD.MapConsole.DrawLine(new Point(0, 0), new Point(HUD.MapWidth, 0), Color.White, Color.Black, '#');

            //create an instance of the player
            CreatePlayer();

            // add the player Entity to our console
            // so it will display on screen
            HUD.MapConsole.Children.Add(player);
        }

        // Create a player using SadConsole's Entity class
        private static void CreatePlayer()
        {
            player = new SadConsole.Entities.Entity(1, 1);
            player.Animation.CurrentFrame[0].Glyph = '@';
            player.Animation.CurrentFrame[0].Foreground = Color.HotPink;
            player.Position = new Point(20, 10);
        }
    }
}
