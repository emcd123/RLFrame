using System;
using SadConsole;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ConsoleLayers;
using GameSystems;
using Microsoft.Xna.Framework.Input;
using DataModels;

namespace RLFrame
{
    public class Program
    {
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
            InputHandling.TakeInput(MapGenerator.Player);
        }

        private static void Init()
        {
            // Build the room's walls then carve out some floors
            //MapGenerator.CreateWalls(HUD.MapWidth, HUD.MapHeight);
            //MapGenerator.CreateFloors(1, 1, HUD.MapWidth-1, HUD.MapHeight-1);
            // Initialize an empty map
            MapGenerator.GameMap = new Map(HUD.MapWidth, HUD.MapHeight);
            // Instantiate a new map generator and
            // populate the map with rooms and tunnels
            MapGenerator mapGen = new MapGenerator();
            MapGenerator.GameMap = mapGen.GenerateMap(MapGenerator.GameMap.Width, MapGenerator.GameMap.Height, MapGenerator.MaxRooms, MapGenerator.MinRoomSize, MapGenerator.MaxRoomSize);

            HUD.InitHUD(MapGenerator.GameMap.Tiles);

            //create an instance of the player
            MapGenerator.CreatePlayer();
        }
    }
}
