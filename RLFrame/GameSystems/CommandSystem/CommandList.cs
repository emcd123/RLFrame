using ConsoleLayers;
using DataModels.Entities;
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
                Monster monster = MapGenerator.GameMap.GetEntityAt<Monster>(actor.Position + new Point(0, -1));
                if (monster != null)
                {
                    CommandManager.attack_command.Execute(actor, monster);
                    return;
                }

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
                Monster monster = MapGenerator.GameMap.GetEntityAt<Monster>(actor.Position + new Point(0, 1));
                if (monster != null)
                {
                    CommandManager.attack_command.Execute(actor, monster);
                    return;
                }

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
                Monster monster = MapGenerator.GameMap.GetEntityAt<Monster>(actor.Position + new Point(-1, 0));
                if (monster != null)
                {
                    CommandManager.attack_command.Execute(actor, monster);
                    return;
                }

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
                Monster monster = MapGenerator.GameMap.GetEntityAt<Monster>(actor.Position + new Point(1, 0));
                if(monster != null)
                {
                    CommandManager.attack_command.Execute(actor, monster);
                    return;
                }

                actor.Position += new Point(1, 0);
                CommandHelpers.CenterOnActor(actor);
            }
            else
                return;
        }
    }

    public class AttackCommand : ICommandBinary
    {
        public void Execute(Actor attacker, Actor defender)
        {
            var CombatSystem = new Combat();
            CombatSystem.Attack(attacker, defender);            
        }
    }
}
