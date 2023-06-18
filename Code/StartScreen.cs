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
    static class StartScreen
    {
        public static Texture2D Screen { get; set; }
        static Color color = Color.White;
        static Color fontColor = Color.Black;
        public static SpriteBatch spriteBatch;
        public static Rectangle Resolution;
        public static SpriteFont Font { get; set; }

        static public void Draw()
        {
            spriteBatch.Draw(Screen, Resolution, color);
            spriteBatch.DrawString(Font, "НАЧАТЬ ИГРУ", new Vector2(Resolution.Width/2-180, Resolution.Height/2-30), fontColor);
        }
        static public void Update()
        {

        }
    }
}
