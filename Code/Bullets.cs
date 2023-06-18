using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace Cats
{
    class Bullet : ObjectOnMap
    {
        protected static int Damage;
        protected static int BaseDamage;
        private float Rotation;
        private float RotationSpeed;
        private Vector2 Center;
        public Texture2D BulletTexture;
        public SoundEffect Sound;
        public static bool IsPowerUp;
        protected int PowerTime;
        public Bullet(Vector2 position, Vector2 direction, float speed)
        {
            Position = (direction == Directions.Left) ? new Vector2(position.X, position.Y + 100) : new Vector2(position.X + 100, position.Y + 100);
            Direction = direction;
            RotationSpeed = 0.2f;
            Center = new Vector2(45, 45);
            Speed = speed;
        }

        public Bullet()
        {

        }

        public void Update()
        {
            Position += Direction * Speed;
            Rotation += RotationSpeed;
            if (IsPowerUp)
                Damage = BaseDamage * 3;
        }

        public void Draw()
        {
            Gameplay.spriteBatch.Draw(BulletTexture, Position, null, Color.White,Rotation,Center,1,SpriteEffects.None,0);
        }

        public bool IsOutOfBorders()
        {
            if (this.Position.X < -20 || this.Position.X > Gameplay.Resolution.Width)
                return true;
            else return
                false;
        }
        
        public virtual bool HitSomeone(List<Enemy> units)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (Gameplay.GetDistance(Position, units[i].GetPosition()) < units[i].HitDistance)
                {
                    units[i].Hit(Damage);
                    Sound.Play();
                    return true;
                }
            }
            return false;
        }

        public static void PowerUp()
        {
            IsPowerUp = true;
        }

        public static void ClearDamage()
        {
            Damage = BaseDamage;
        }

        
    }
    class RockBullet : Bullet
    {
        public static Texture2D Texture;
        public static SoundEffect SlapSound;
        public RockBullet(Vector2 position, Vector2 direction, float speed) : base(position,direction, speed)
        {
            BaseDamage = 5;
            Damage = BaseDamage;
            BulletTexture = Texture;
            Sound = SlapSound;
        }

        public override bool HitSomeone(List<Enemy> units)
        {
            return (base.HitSomeone(units));
        }

    }
    class IceBullet : Bullet
    {
        public static Texture2D Texture;
        private static float Slowing;
        public static SoundEffect IceSound;
        public IceBullet(Vector2 position, Vector2 direction, float speed) : base(position, direction, speed)
        {
            BaseDamage = 1;
            Damage = BaseDamage;
            BulletTexture = Texture;
            Slowing = 0.2f;
            Sound = IceSound;
            
        }

        public override bool HitSomeone(List<Enemy> units)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (Gameplay.GetDistance(Position, units[i].GetPosition()) < units[i].HitDistance)
                {
                    units[i].Hit(Damage);
                    units[i].SlowDown(Slowing);
                    Sound.Play();
                    return true;
                }
            }
            return false;
        }
    }
}
