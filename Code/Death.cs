using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cats
{
    class Death
    {
        public static SpriteBatch spriteBatch;
        public static Texture2D Texture;
        public static Texture2D RedSquare;
        private static Vector2 Position;
        public static SoundEffect SadMeow;
        private static bool DidMeow;
        public static SpriteFont Font;
        public static void Draw()
        {
            spriteBatch.Draw(RedSquare, new Rectangle(0, 0, Gameplay.Resolution.Width, Gameplay.Resolution.Height), Color.Red);
            spriteBatch.Draw(Texture, Position, Color.White);
            spriteBatch.DrawString(Font, "Ваш счет: " + Gameplay.GeneralScore.ToString(), new Vector2(Gameplay.Resolution.Width / 2 - 150, 50), Color.White);
            spriteBatch.DrawString(Font, "Рекорд: " + Gameplay.BestScore.ToString(), new Vector2(Gameplay.Resolution.Width / 2 - 150, 120), Color.White);
        }

        public static void Update()
        {
            if (!DidMeow)
                SadMeow.Play();
            DidMeow = true;
        }

        public static void Initialise()
        {
            Position = new Vector2(Gameplay.Resolution.Width / 2 - Texture.Width / 2, Gameplay.Resolution.Height / 2 - Texture.Height / 2);
        }

        public static void Clear()
        {
            DidMeow = false;
        }
    }
}
