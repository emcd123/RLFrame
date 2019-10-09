using ConsoleLayers;
using DataModels;
using GameSystems.CommandSystem;
using Microsoft.Xna.Framework;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystems
{
    // Contains all generic actions performed on entities and tiles
    // including combat, movement, and so on.

    public class CommandManager
    {
        public static ICommand up_command = new UpCommand();
        public static ICommand down_command = new DownCommand();
        public static ICommand left_command = new LeftCommand();
        public static ICommand right_command = new RightCommand();
        public static ICommandBinary attack_command = new AttackCommand();
    }
}
