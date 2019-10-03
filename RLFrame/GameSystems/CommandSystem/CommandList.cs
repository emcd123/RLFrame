using ConsoleLayers;
using DataModels;
using GameSystems.CommandSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystems
{
    public class UpCommand : ICommand
    {
        public void Execute(Actor actor)
        {
            if (MapGenerator.IsTileWalkable(actor.Position + new Point(0, -1), HUD.MapWidth, HUD.MapHeight))
            {
                actor.Position += new Point(0, -1);
                CommandHelpers.CenterOnActor(actor);
            }
            else
                return;
        }
    }

    public class DownCommand : ICommand
    {
        public void Execute(Actor actor)
        {
            if (MapGenerator.IsTileWalkable(actor.Position + new Point(0, 1), HUD.MapWidth, HUD.MapHeight))
            {
                actor.Position += new Point(0, 1);
                CommandHelpers.CenterOnActor(actor);
            }
            else
                return;
        }
    }

    public class LeftCommand : ICommand
    {
        public void Execute(Actor actor)
        {
            if (MapGenerator.IsTileWalkable(actor.Position + new Point(-1, 0), HUD.MapWidth, HUD.MapHeight))
            {
                actor.Position += new Point(-1, 0);
                CommandHelpers.CenterOnActor(actor);
            }
            else
                return;
        }
    }

    public class RightCommand : ICommand
    {
        public void Execute(Actor actor)
        {
            if (MapGenerator.IsTileWalkable(actor.Position + new Point(1, 0), HUD.MapWidth, HUD.MapHeight))
            {
                actor.Position += new Point(1, 0);
                CommandHelpers.CenterOnActor(actor);
            }
            else
                return;
        }
    }
}
