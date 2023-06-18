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
    class Pause
    {
        public static Texture2D Texture;
        public static Texture2D DarkerBackground;
        public static SpriteBatch spriteBatch;
        private static Vector2 Position;
        public static void Draw()
        {
            spriteBatch.Draw(DarkerBackground, new Rectangle(0, 0, Gameplay.Resolution.Width, Gameplay.Resolution.Height), Color.Black);
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public static void Update()
        {

        }

        public static void Initialise()
        {
            Position = new Vector2(Gameplay.Resolution.Width/2 - Texture.Width/2, Gameplay.Resolution.Height/2 - Texture.Height/2);
        }
    }
}
