﻿using ConsoleLayers;
using DataModels;
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
                Item item = MapGenerator.GameMap.GetEntityAt<Item>(actor.Position + new Point(0, -1));
                if (monster != null)
                {
                    CommandManager.attack_command.Execute(actor, monster);
                    return;
                }
                // if there's an item here,
                // try to pick it up
                else if (item != null)
                {
                    CommandManager.pickup_command.Execute(actor, item);
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
                Item item = MapGenerator.GameMap.GetEntityAt<Item>(actor.Position + new Point(0, 1));
                if (monster != null)
                {
                    CommandManager.attack_command.Execute(actor, monster);
                    return;
                }
                // if there's an item here,
                // try to pick it up
                else if (item != null)
                {
                    CommandManager.pickup_command.Execute(actor, item);
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
                Item item = MapGenerator.GameMap.GetEntityAt<Item>(actor.Position + new Point(-1, 0));
                if (monster != null)
                {
                    CommandManager.attack_command.Execute(actor, monster);
                    return;
                }
                // if there's an item here,
                // try to pick it up
                else if (item != null)
                {
                    CommandManager.pickup_command.Execute(actor, item);
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
                Item item = MapGenerator.GameMap.GetEntityAt<Item>(actor.Position + new Point(1, 0));
                if (monster != null)
                {
                    CommandManager.attack_command.Execute(actor, monster);
                    return;
                }
                // if there's an item here,
                // try to pick it up
                else if (item != null)
                {
                    CommandManager.pickup_command.Execute(actor, item);
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

    public class PickupCommand : ICommandItem
    {
        public void Execute(Actor actor, Item item)
        {
            // Add the item to the Actor's inventory list
            // and then destroy it
            actor.Inventory.Add(item);
            HUD.MessageLog.Add($"{actor.Name} picked up {item.Name}");
            item.Destroy(MapGenerator.GameMap);
        }
    }
}
