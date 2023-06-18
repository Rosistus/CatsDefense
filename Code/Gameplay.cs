using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct3D9;

namespace Cats
{
    static class Gameplay
    {
        public static Texture2D PlayerTexture;
        public static Texture2D BackgroundTexture;
        public static Song GameSong;
        public static Rectangle Resolution;
        public static Texture2D WhiteCat;
        public static Positions Position;
        public static OrangeCat OrangCat;
        public static BlueCat BluCat;
        public static List<Bullet> Bullets = new List<Bullet>();
        public static List<Enemy> Enemies = new List<Enemy>();
        public static SpriteBatch spriteBatch;
        public static SpriteFont Font;
        public static int GeneralScore { get; private set; }
        public static int BestScore { get; private set; }
        private static int PrivateScore;
        private static int PowerUpTimer;
        private static Vector2 Center;
        public static Double Timer { get; private set; }
        private static Double DTimer;

        static public void Draw()
        {
            Position = new Positions(Resolution);
            spriteBatch.Draw(BackgroundTexture, Resolution, Color.White);
            spriteBatch.Draw(WhiteCat, new Vector2(Resolution.Width/2 - WhiteCat.Width/2, Resolution.Height/2 - WhiteCat.Height/2), Color.White);
            spriteBatch.DrawString(Font, "Ваш счет: " + GeneralScore.ToString(), new Vector2(Resolution.Width / 2 - 150, 50), Color.White);
            foreach (var enemy in Enemies)
                enemy.Draw();
            foreach (var bullet in Bullets)
                bullet.Draw();
            OrangCat.Draw();
            BluCat.Draw();
            Rules.Draw();
            if (Bullet.IsPowerUp)
                spriteBatch.DrawString(Font, "Временный бонус: тройной урон ", new Vector2(Resolution.Width / 2 - 350, 220), Color.White);
        }

        static public void Update()
        {
            BluCat.Update();
            OrangCat.Update();
            for (var i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Update();
                if (Bullets[i].IsOutOfBorders() || Bullets[i].HitSomeone(Enemies))
                {
                    Bullets.RemoveAt(i);
                    i--;
                }

            }
            for (var i = 0; i < Enemies.Count; i++)
            { 
                Enemies[i].Update();
                if (Enemies[i].IsDead())
                {
                    GeneralScore += Enemies[i].Cost;
                    PrivateScore += Enemies[i].Cost;
                    Enemies[i].PlayDeathSound();
                    Enemies.RemoveAt(i);
                    i--;
                }
            }
            DifficultyUp();
            PowerTimer(PowerUpTimer);
            Timer += DTimer;
            Rules.Update();
        }

        static public void Initialize()
        {
            Position = new Positions(Resolution);
            Center = new Vector2(Resolution.Width/2, Resolution.Height/2);
            OrangCat = new OrangeCat(Position.Left);
            BluCat = new BlueCat(Position.Right);
            GeneralScore = 0;
            DTimer = 1.0;
            Rules.Initialise();
            BestScore = 0;
        }

        static public void Swap()
        {
            OrangCat.Swap();
            BluCat.Swap();
        }

        static public Cat GetLeftCat()
        {
            if (OrangCat.GetPosition() == Position.Left)
                    return OrangCat;
            else
                return BluCat;

        }

        static public Cat GetRightCat()
        {
            if (OrangCat.GetPosition() == Position.Right)
                return OrangCat;
            else
                return BluCat;
        }

        static public float GetDistance(Vector2 obj, Vector2 target)
        {
            return Math.Abs(obj.X - target.X);
        }

        private static void DifficultyUp()
        {
            if (PrivateScore >= 50.0)
            {
                DTimer += 0.1;
                PrivateScore = 0;
            }
        }

        public static void SpawnEnemy()
        {
            if (Timer >= 100)
            {
                Enemies.Add(Enemy.GetRandomEnemy(GeneralScore>300));
                Timer = 0;
            }
        }

        public static bool GameOver()
        {
            foreach(var enemy in Enemies)
            {
                if (GetDistance(enemy.GetPosition(), Center) < enemy.HitDistance)
                {
                    if (BestScore < GeneralScore)
                        BestScore = GeneralScore;
                    return true;
                }
            }
            return false;
        }

        public static void Restart()
        {
            Rules.Restart();
            GeneralScore = 0;
            PrivateScore = 0;
            Timer = 0;
            DTimer = 1;
            Bullets = new List<Bullet>();
            Enemies = new List<Enemy>();
        }

        public static void PowerTimer(int i)
        {
            if (i >= 180)
            {
                Bullet.IsPowerUp = false;
                PowerUpTimer = 0;
                Bullet.ClearDamage();
            }
            if (i < 180 && Bullet.IsPowerUp)
                PowerUpTimer++;
        }
    }
}
