using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestClient
{
    public class HUD
    {
        public static int Width = 80;
        public static int Height = 30;

        public static CustomConsole SkillsConsole { get; set; }
        public static CustomConsole GameMap { get; set; }
        public static CustomConsole SidebarInventory { get; set; }
        public static CustomConsole SidebarPlayerProfile { get; set; }
        public static CustomConsole MessageConsole { get; set; }

        public static void InitHUD()
        {
            var console = new SadConsole.Console(Width, Height);
            SadConsole.Global.CurrentScreen = console;

            SkillsConsole = new CustomConsole(Microsoft.Xna.Framework.Color.Blue, 60, 5) { Position = new Point(0, 0) };
            GameMap = new CustomConsole(Microsoft.Xna.Framework.Color.Black, 60, 20) { Position = new Point(0, 5) };
            SidebarInventory = new CustomConsole(Microsoft.Xna.Framework.Color.Green, 20, 10) { Position = new Point(60, 0) };
            SidebarPlayerProfile = new CustomConsole(Microsoft.Xna.Framework.Color.Red, 20, 20) { Position = new Point(60, 10) };
            MessageConsole = new CustomConsole(Microsoft.Xna.Framework.Color.Yellow, 60, 5) { Position = new Point(0, 25) };

            console.Children.Add(SkillsConsole);
            console.Children.Add(GameMap);
            console.Children.Add(SidebarInventory);
            console.Children.Add(SidebarPlayerProfile);
            console.Children.Add(MessageConsole);
        }
    }
}
