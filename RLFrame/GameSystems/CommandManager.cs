using ConsoleLayers;
using DataModels;
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
        public CommandManager() { }

        // Move the actor BY +/- X&Y coordinates
        // returns true if the move was successful
        // and false if unable to move there
        public static void MoveActorBy(Actor actor, Point position)
        {
            MoveTo(actor, position);
        }

        // Moves the Actor TO newPosition location
        // returns true if actor was able to move, false if failed to move
        public static bool MoveTo(Actor actor, Point newPosition)
        {
            // Check the map if we can move to this new position
            if (MapGenerator.IsTileWalkable(actor.Position + newPosition, HUD.MapWidth, HUD.MapHeight))
            {
                actor.Position += newPosition;
                return true;
            }
            else
                return false;
        }

        // centers the viewport camera on an Actor
        public static void CenterOnActor(Actor actor)
        {
            HUD.MapScrollConsole.CenterViewPortOnPoint(actor.Position);
        }

        #region Currently Unused
        // Moves the Actor BY positionChange tiles in any X/Y direction
        // returns true if actor was able to move, false if failed to move
        public static bool MovePlayerBy(Point positionChange)
        {
            // Check the map if we can move to this new position
            if (MapGenerator.IsTileWalkable(MapGenerator.Player.Position + positionChange, HUD.MapWidth, HUD.MapHeight))
            {
                MapGenerator.Player.Position += positionChange;
                return true;
            }
            else
                return false;
        }

        // centers the viewport camera on an Actor
        public static void CenterOnPlayer()
        {
            HUD.MapScrollConsole.CenterViewPortOnPoint(MapGenerator.Player.Position);
        }
        #endregion
    }
}
