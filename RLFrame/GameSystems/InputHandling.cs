﻿using DataModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameSystems
{
    public class InputHandling
    {
        public static void TakeInput(Actor actor)
        {
            // As an example, we'll use the F11 key to make the game full screen
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F11))
            {
                SadConsole.Settings.ToggleFullScreen();
            }

            // Keyboard movement for Player character: Up arrow
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                CommandManager.up_command.Execute(actor);
            }

            // Keyboard movement for Player character: Down arrow
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                CommandManager.down_command.Execute(actor);
            }

            // Keyboard movement for Player character: Left arrow
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                CommandManager.left_command.Execute(actor);
            }

            // Keyboard movement for Player character: Right arrow
            if (SadConsole.Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                CommandManager.right_command.Execute(actor);
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
