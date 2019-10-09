﻿using Microsoft.Xna.Framework;

namespace DataModels.Entities
{
    //A generic monster capable of
    //combat and interaction
    //yields treasure upon death
    public class Monster : Actor
    {
        public Monster(Color foreground, Color background) : base(foreground, background, 'M')
        {

        }
    }
}
