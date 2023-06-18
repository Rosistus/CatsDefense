using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cats
{
    static class Directions
    {
        public static Vector2 Left = new Vector2(-1, 0);
        public static Vector2 Right = new Vector2(1, 0);
        public static Vector2 Up = new Vector2(0, -1);
        public static Vector2 Down = new Vector2(0, 1);
    }
    class Positions
    {
        public Vector2 Left;
        public Vector2 Right;
        public Vector2 LeftSide;
        public Vector2 RightSide;
        public Positions(Rectangle resolution)
        {
            Left = new Vector2(resolution.Width / 2 - 300, resolution.Height / 2 - 180);
            Right = new Vector2(resolution.Width / 2 + 100 , resolution.Height / 2 - 180);
            LeftSide = new Vector2(-300, resolution.Height / 2 - 50);
            RightSide = new Vector2(resolution.Width + 50, resolution.Height / 2 - 50);
        }

        public Vector2 GetRandomSide()
        {
            var random = new Random();
            var sides = new Vector2[] { LeftSide, RightSide };
            return sides[random.Next(2)];
        }
    }
    class ObjectOnMap
    {
        private protected Vector2 Position { get; set; }
        private protected Vector2 Direction { get; set; }
        private protected float Speed { get; set; }
    }
}
