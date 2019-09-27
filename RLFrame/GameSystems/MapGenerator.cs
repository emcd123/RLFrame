﻿using ConsoleLayers;
using DataModels;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSystems
{
    public class MapGenerator
    {
        public static Random rng = new Random();
        public static Map GameMap { get; set; }
        public static Player Player { get; set; }
        //public static TileBase[] Tiles { get; set; } // an array of TileBase that contains all of the tiles for a map

        public static int MaxRooms = 500;
        public static int MinRoomSize = 4;
        public static int MaxRoomSize = 15;

        // Create a player using SadConsole's Entity class
        public static void CreatePlayer()
        {
            Player = new Player(Color.Yellow, Color.Transparent);
            Point starting_coords = new Point(20, 10);
            Player.Position = starting_coords;
            while (!IsTileWalkable(Player.Position, HUD.MapWidth, HUD.MapHeight))
            {
                var a = rng.Next(0, HUD.MapWidth);
                var b = rng.Next(0, HUD.MapHeight);
                starting_coords = new Point(a, b);
                Player.Position = starting_coords;
            }
            
            // add the EntityViewSyncComponent to the player
            Player.Components.Add(new EntityViewSyncComponent());
            // add the player Entity to our console
            // so it will display on screen
            HUD.MapScrollConsole.Children.Add(MapGenerator.Player);
        }

        // Moves the Actor BY positionChange tiles in any X/Y direction
        // returns true if actor was able to move, false if failed to move
        public static bool MovePlayerBy(Point positionChange)
        {
            // Check the map if we can move to this new position
            if (IsTileWalkable(Player.Position + positionChange, HUD.MapWidth, HUD.MapHeight))
            {
                Player.Position += positionChange;
                return true;
            }
            else
                return false;
        }

        // Moves the Actor TO newPosition location
        // returns true if actor was able to move, false if failed to move
        public bool MoveTo(Point newPosition)
        {
            Player.Position = newPosition;
            return true;
        }

        // centers the viewport camera on an Actor
        public static void CenterOnPlayer()
        {
            HUD.MapScrollConsole.CenterViewPortOnPoint(Player.Position);
        }

        // centers the viewport camera on an Actor
        public static void CenterOnActor(Actor actor)
        {
            HUD.MapScrollConsole.CenterViewPortOnPoint(actor.Position);
        }

        // Carve out a rectangular floor using the TileFloors class
        public static void CreateFloors(int x_start, int y_start, int room_width, int room_height)
        {
            //Carve out a rectangle of floors in the tile array
            for (int x = x_start; x < room_width; x++)
            {
                for (int y = y_start; y < room_height; y++)
                {
                    // Calculates the appropriate position (index) in the array
                    // based on the y of tile, width of map, and x of tile
                    GameMap.Tiles[y * HUD.MapWidth + x] = new TileFloor();
                }
            }
        }

        // Flood the map using the TileWall class
        public static void CreateWalls(int room_width, int room_height)
        {
            // Create an empty array of tiles that is equal to the map size
            GameMap.Tiles = new TileBase[room_width * room_height];

            //Fill the entire tile array with floors
            for (int i = 0; i < GameMap.Tiles.Length; i++)
            {
                GameMap.Tiles[i] = new TileWall();
            }
        }

        // IsTileWalkable checks
        // to see if the actor has tried
        // to walk off the map or into a non-walkable tile
        // Returns true if the tile location is walkable
        // false if tile location is not walkable or is off-map

        public static bool IsTileWalkable(Point location, int room_width, int room_height)
        {
            // first make sure that actor isn't trying to move
            // off the limits of the map
            if (location.X < 0 || location.Y < 0 || location.X >= room_width || location.Y >= room_height)
                return false;
            // then return whether the tile is walkable
            return !GameMap.Tiles[location.Y * room_width + location.X].IsBlockingMove;
        }

        public Map _map; // Temporarily store the map currently worked on
        public Map GenerateMap(int mapWidth, int mapHeight, int maxRooms, int minRoomSize, int maxRoomSize)
        {
            // create an empty map of size (mapWidth x mapHeight)
            _map = new Map(mapWidth, mapHeight);

            // Create a random number generator
            Random randNum = new Random();

            // store a list of the rooms created so far
            List<Rectangle> Rooms = new List<Rectangle>();

            // create up to (maxRooms) rooms on the map
            // and make sure the rooms do not overlap with each other
            for (int i = 0; i < maxRooms; i++)
            {
                // set the room's (width, height) as a random size between (minRoomSize, maxRoomSize)
                int newRoomWidth = randNum.Next(minRoomSize, maxRoomSize);
                int newRoomHeight = randNum.Next(minRoomSize, maxRoomSize);

                // sets the room's X/Y Position at a random point between the edges of the map
                int newRoomX = randNum.Next(0, mapWidth - newRoomWidth - 1);
                int newRoomY = randNum.Next(0, mapHeight - newRoomHeight - 1);

                // create a Rectangle representing the room's perimeter
                Rectangle newRoom = new Rectangle(newRoomX, newRoomY, newRoomWidth, newRoomHeight);

                // Does the new room intersect with other rooms already generated?
                bool newRoomIntersects = Rooms.Any(room => newRoom.Intersects(room));

                if (!newRoomIntersects)
                {
                    Rooms.Add(newRoom);
                }
            }

            // This is a dungeon, so begin by flooding the map with walls.
            FloodWalls();

            // carve out rooms for every room in the Rooms list
            foreach (Rectangle room in Rooms)
            {
                CreateRoom(room);
            }

            // carve out tunnels between all rooms
            // based on the Positions of their centers
            for (int r = 1; r < Rooms.Count; r++)
            {
                //for all remaining rooms get the center of the room and the previous room
                Point previousRoomCenter = Rooms[r - 1].Center;
                Point currentRoomCenter = Rooms[r].Center;

                // give a 50/50 chance of which 'L' shaped connecting hallway to tunnel out
                if (randNum.Next(1, 2) == 1)
                {
                    CreateHorizontalTunnel(previousRoomCenter.X, currentRoomCenter.X, previousRoomCenter.Y);
                    CreateVerticalTunnel(previousRoomCenter.Y, currentRoomCenter.Y, currentRoomCenter.X);
                }
                else
                {
                    CreateVerticalTunnel(previousRoomCenter.Y, currentRoomCenter.Y, previousRoomCenter.X);
                    CreateHorizontalTunnel(previousRoomCenter.X, currentRoomCenter.X, currentRoomCenter.Y);
                }
            }

            // spit out the final map
            return _map;
        }

        // Builds a room composed of walls and floors using the supplied Rectangle
        // which determines its size and position on the map
        // Walls are placed at the perimeter of the room
        // Floors are placed in the interior area of the room
        private void CreateRoom(Rectangle room)
        {
            // Place floors in interior area
            for (int x = room.Left + 1; x < room.Right - 1; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom - 1; y++)
                {
                    CreateFloor(new Point(x, y));
                }
            }

            // Place walls at perimeter
            List<Point> perimeter = GetBorderCellLocations(room);
            foreach (Point location in perimeter)
            {
                CreateWall(location);
            }
        }

        // Creates a Floor tile at the specified X/Y location
        private void CreateFloor(Point location)
        {
            _map.Tiles[location.ToIndex(_map.Width)] = new TileFloor();
        }

        // Creates a Wall tile at the specified X/Y location
        private void CreateWall(Point location)
        {
            _map.Tiles[location.ToIndex(_map.Width)] = new TileWall();
        }

        // Fills the map with walls
        private void FloodWalls()
        {
            for (int i = 0; i < _map.Tiles.Length; i++)
            {
                _map.Tiles[i] = new TileWall();
            }
        }

        // carve a tunnel out of the map parallel to the x-axis
        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                CreateFloor(new Point(x, yPosition));
            }
        }

        // carve a tunnel using the y-axis
        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                CreateFloor(new Point(xPosition, y));
            }
        }

        // Returns a list of points expressing the perimeter of a rectangle
        private List<Point> GetBorderCellLocations(Rectangle room)
        {
            //establish room boundaries
            int xMin = room.Left;
            int xMax = room.Right;
            int yMin = room.Top;
            int yMax = room.Bottom;

            // build a list of room border cells using a series of
            // straight lines
            List<Point> borderCells = GetTileLocationsAlongLine(xMin, yMin, xMax, yMin).ToList();
            borderCells.AddRange(GetTileLocationsAlongLine(xMin, yMin, xMin, yMax));
            borderCells.AddRange(GetTileLocationsAlongLine(xMin, yMax, xMax, yMax));
            borderCells.AddRange(GetTileLocationsAlongLine(xMax, yMin, xMax, yMax));

            return borderCells;
        }

        // returns a collection of Points which represent
        // locations along a line
        public IEnumerable<Point> GetTileLocationsAlongLine(int xOrigin, int yOrigin, int xDestination, int yDestination)
        {
            // prevent line from overflowing
            // boundaries of the map
            xOrigin = ClampX(xOrigin);
            yOrigin = ClampY(yOrigin);
            xDestination = ClampX(xDestination);
            yDestination = ClampY(yDestination);

            int dx = Math.Abs(xDestination - xOrigin);
            int dy = Math.Abs(yDestination - yOrigin);

            int sx = xOrigin < xDestination ? 1 : -1;
            int sy = yOrigin < yDestination ? 1 : -1;
            int err = dx - dy;

            while (true)
            {

                yield return new Point(xOrigin, yOrigin);
                if (xOrigin == xDestination && yOrigin == yDestination)
                {
                    break;
                }
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err = err - dy;
                    xOrigin = xOrigin + sx;
                }
                if (e2 < dx)
                {
                    err = err + dx;
                    yOrigin = yOrigin + sy;
                }
            }
        }

        // sets X coordinate between right and left edges of map
        // to prevent any out-of-bounds errors
        private int ClampX(int x)
        {
            if (x < 0)
                x = 0;
            else if (x > _map.Width - 1)
                x = _map.Width - 1;
            return x;
            // OR using ternary conditional operators: return (x < 0) ? 0 : (x > _map.Width - 1) ? _map.Width - 1 : x;
        }

        // sets Y coordinate between top and bottom edges of map
        // to prevent any out-of-bounds errors
        private int ClampY(int y)
        {
            if (y < 0)
                y = 0;
            else if (y > _map.Height - 1)
                y = _map.Height - 1;
            return y;
            // OR using ternary conditional operators: return (y < 0) ? 0 : (y > _map.Height - 1) ? _map.Height - 1 : y;
        }
    }
}
