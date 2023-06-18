using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cats
{
    class Rules
    {
        public static Texture2D Texture;
        private static int Timer;
        private static Vector2 Position;
        public static SpriteBatch spriteBatch;
        private static bool IsOut;

        public static void Update()
        {
            if (!IsOut)
            { 
                Timer++;
                if (Timer > 300)
                    Position += new Vector2(0, 0.5f);
            }
            if (Position.X > Gameplay.Resolution.Height)
                IsOut = true;
        }

        public static void Draw()
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public static void Initialise()
        {
            IsOut = false;
            Timer = 0;
            Position = new Vector2(Gameplay.Resolution.Width / 2 - Texture.Width/2, Gameplay.Resolution.Height / 2 + Texture.Height - 200);
            spriteBatch = Gameplay.spriteBatch;
        }

        public static void Restart()
        {
            IsOut = false;
            Timer = 0;
            Position = new Vector2(Gameplay.Resolution.Width / 2 - Texture.Width / 2, Gameplay.Resolution.Height / 2 + Texture.Height - 200);
        }
    }
}
