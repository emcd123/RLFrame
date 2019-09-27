using DataModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystems
{
    public class InputHandling
    {
        public static void TakeInput(Player player)
        {
            // As an example, we'll use the F11 key to make the game full screen
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F11))
            {
                SadConsole.Settings.ToggleFullScreen();
            }

            // Keyboard movement for Player character: Up arrow
            // Decrement player's Y coordinate by 1
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                MapGenerator.MovePlayerBy(new Point(0, -1));
            }

            // Keyboard movement for Player character: Down arrow
            // Increment player's Y coordinate by 1
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                MapGenerator.MovePlayerBy(new Point(0, 1));
            }

            // Keyboard movement for Player character: Left arrow
            // Decrement player's X coordinate by 1
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                MapGenerator.MovePlayerBy(new Point(-1, 0));
            }

            // Keyboard movement for Player character: Right arrow
            // Increment player's X coordinate by 1
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                MapGenerator.MovePlayerBy(new Point(1, 0));
            }
        }


        //Could be modified later for an 'Enter character name' section
        private static string _stringValue = string.Empty;
        public static void MatchInputToString()
        {
            var keyboardState = Keyboard.GetState();
            var keys = keyboardState.GetPressedKeys();

            if (keys.Length > 0)
            {
                var keyValue = keys[0].ToString();
                _stringValue += keyValue;
            }
        }
    }
}
