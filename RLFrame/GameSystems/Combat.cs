﻿using ConsoleLayers;
using DataModels.Entities;
using GoRogue.DiceNotation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystems
{
    public class Combat
    {
        // Executes an attack from an attacking actor
        // on a defending actor, and then describes
        // the outcome of the attack in the Message Log
        public void Attack(Actor attacker, Actor defender)
        {
            // Create two messages that describe the outcome
            // of the attack and defense
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenseMessage = new StringBuilder();

            // Count up the amount of attacking damage done
            // and the number of successful blocks
            int hits = ResolveAttack(attacker, defender, attackMessage);
            int blocks = ResolveDefense(defender, hits, attackMessage, defenseMessage);

            // Display the outcome of the attack & defense
            HUD.MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrWhiteSpace(defenseMessage.ToString()))
            {
                HUD.MessageLog.Add(defenseMessage.ToString());
            }

            int damage = hits - blocks;

            // The defender now takes damage
            ResolveDamage(defender, damage);
        }

        // Calculates the outcome of an attacker's attempt
        // at scoring a hit on a defender, using the attacker's
        // AttackChance and a random d100 roll as the basis.
        // Modifies a StringBuilder message that will be displayed
        // in the MessageLog
        private static int ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMessage)
        {
            // Create a string that expresses the attacker and defender's names
            int hits = 0;
            attackMessage.AppendFormat("{0} attacks {1}, ", attacker.Name, defender.Name);

            // The attacker's Attack value determines the number of D100 dice rolled
            for (int dice = 0; dice < attacker.Attack; dice++)
            {
                //Roll a single D100 and add its results to the attack Message
                int diceOutcome = Dice.Roll("1d100");

                //Resolve the dicing outcome and register a hit, governed by the
                //attacker's AttackChance value.
                if (diceOutcome >= 100 - attacker.AttackChance)
                    hits++;
            }

            return hits;
        }

        // Calculates the outcome of a defender's attempt
        // at blocking incoming hits.
        // Modifies a StringBuilder messages that will be displayed
        // in the MessageLog, expressing the number of hits blocked.
        private static int ResolveDefense(Actor defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage)
        {
            int blocks = 0;
            if (hits > 0)
            {
                // Create a string that displays the defender's name and outcomes
                attackMessage.AppendFormat("scoring {0} hits.", hits);
                defenseMessage.AppendFormat(" {0} defends and rolls: ", defender.Name);

                //The defender's Defense value determines the number of D100 dice rolled
                for (int dice = 0; dice < defender.Defense; dice++)
                {
                    //Roll a single D100 and add its results to the defense Message
                    int diceOutcome = Dice.Roll("1d100");

                    //Resolve the dicing outcome and register a block, governed by the
                    //attacker's DefenceChance value.
                    if (diceOutcome >= 100 - defender.DefenseChance)
                        blocks++;
                }
                defenseMessage.AppendFormat("resulting in {0} blocks.", blocks);
            }
            else
            {
                attackMessage.Append("and misses completely!");
            }
            return blocks;
        }

        // Calculates the damage a defender takes after a successful hit
        // and subtracts it from its Health
        // Then displays the outcome in the MessageLog.
        private static void ResolveDamage(Actor defender, int damage)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;
                HUD.MessageLog.Add($" {defender.Name} was hit for {damage} damage");
                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }
            else
            {
                HUD.MessageLog.Add($"{defender.Name} blocked all damage!");
            }
        }

        // Removes an Actor that has died
        // and displays a message showing
        // the number of Gold dropped.
        private static void ResolveDeath(Actor defender)
        {
            MapGenerator.GameMap.Remove(defender);

            if (defender is Player)
            {
                HUD.MessageLog.Add($" {defender.Name} was killed.");
            }
            else if (defender is Monster)
            {
                HUD.MessageLog.Add($"{defender.Name} died and dropped {defender.Gold} gold coins.");
            }
        }
    }
}
