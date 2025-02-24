﻿using Microsoft.Xna.Framework;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    // TileBase is an abstract base class 
    // representing the most basic form of of all Tiles used.
    public abstract class TileBase : Cell
    {
        // Movement and Line of Sight Flags
        public bool IsBlockingMove;
        public bool IsBlockingLOS;

        // Tile's name
        protected string Name;

        // TileBase is an abstract base class 
        // representing the most basic form of of all Tiles used.
        // Every TileBase has a Foreground Colour, Background Colour, and Glyph
        // IsBlockingMove and IsBlockingLOS are optional parameters, set to false by default
        public TileBase(Color foreground, Color background, int glyph, bool blockingMove = false, bool blockingLOS = false, String name = "") : base(foreground, background, glyph)
        {
            IsBlockingMove = blockingMove;
            IsBlockingLOS = blockingLOS;
            Name = name;
        }
    }
}
