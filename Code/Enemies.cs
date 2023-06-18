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
using SharpDX.MediaFoundation;

namespace Cats
{
    class Enemy : ObjectOnMap
    {
        public Texture2D Texture;
        protected int HP;
        protected int Timer;
        public float HitDistance;
        protected float BaseSpeed;
        protected bool IsSlow;
        public static SoundEffect SmallRoar;
        public static SoundEffect MediumRoar;
        public static SoundEffect HugeRoar;
        public int Cost { get; protected set; }

        public Enemy()
        {
            Position = Gameplay.Position.GetRandomSide();
            Direction = (Position == Gameplay.Position.LeftSide) ? Directions.Right : Directions.Left;
            HitDistance = 30;
        }

        public void Update()
        {
            Position += Direction * Speed;
            if (IsSlow)
                Timer++;
            if (Timer % 150 == 0)
            {
                Timer = 0;
                IsSlow = false;
                Speed = BaseSpeed;
            }
        }
        public void Draw()
        {
            Gameplay.spriteBatch.Draw(Texture, Position, Color.White);
        }

        public static Enemy GetRandomEnemy(bool needToMakeHarder)
        {
            var rnd = new Random();
            int smallCount = 6;
            int mediumCount = 3;
            int hugeCount = 1;
            var enemyList = new List<Enemy>();
            for (int i = 0; i < smallCount; i++)
            { 
                enemyList.Add(new SmallEnemy());
            }
            for (int i = 0; i < mediumCount; i++)
            { 
                enemyList.Add(new MediumEnemy());
            }
            if (needToMakeHarder)
            { 
                for (int i = 0; i < hugeCount; i++)
                   enemyList.Add(new HugeEnemy());
            }
            return enemyList.ToArray()[rnd.Next(enemyList.Count)];
        }

        public virtual bool IsDead()
        {
            return HP <= 0;
        }

        public void Hit(int damage)
        {
            HP -= damage;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(Position.X + Texture.Width/2, Position.Y);
        }

        public void SlowDown(float multiplier)
        {
            if (!IsSlow)
            { 
                Speed *= multiplier;
                IsSlow = true;
            }
        }

        public virtual void PlayDeathSound()
        {

        }
    }

    class SmallEnemy : Enemy
    {
        public static Texture2D TextureLeft;
        public static Texture2D TextureRight;
        public SmallEnemy() : base()
        {
            Position = new Vector2(Position.X,Position.Y-200);
            BaseSpeed = 7;
            Speed = BaseSpeed;
            Texture = (Direction == Directions.Right) ? TextureLeft : TextureRight;
            HP = 10;
            Cost = 5;
        }

        public override void PlayDeathSound()
        {
            SmallRoar.Play();
        }
    }

    class MediumEnemy : Enemy
    {
        public static Texture2D TextureLeft;
        public static Texture2D TextureRight;
        public MediumEnemy() : base()
        {
            Position = new Vector2(Position.X, Position.Y - 250);
            BaseSpeed = 3;
            Speed = BaseSpeed;
            Texture = (Direction == Directions.Right) ? TextureLeft : TextureRight;
            HP = 30;
            Cost = 10;
        }

        public override void PlayDeathSound()
        {
            MediumRoar.Play();
        }
    }

    class HugeEnemy : Enemy
    {
        public static Texture2D TextureLeft;
        public static Texture2D TextureRight;
        public HugeEnemy() : base()
        {
            Position = new Vector2(Position.X, Position.Y - 300);
            BaseSpeed = 1;
            Speed = BaseSpeed;
            Texture = (Direction == Directions.Right) ? TextureLeft : TextureRight;
            HP = 100;
            Cost = 20;
        }

        public override bool IsDead()
        {
            if (HP <= 0)
                Bullet.PowerUp();
            return HP <= 0;
        }

        public override void PlayDeathSound()
        {
            HugeRoar.Play();
        }
    }
}
