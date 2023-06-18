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
using SharpDX.Direct3D9;

namespace Cats
{
    class Cat : ObjectOnMap
    {
        protected bool IsShooting;
        public Texture2D Texture;
        public Cat(Vector2 Position)
        {
            this.Position = Position;
        }

        public Cat()
        {

        }
        public void Draw()
        {
            Gameplay.spriteBatch.Draw(Texture, Position, Color.White);
        }

        public virtual void Update()
        {

        }

        public void Shoot()
        {
            if (this.GetType() == typeof(OrangeCat))
                Gameplay.Bullets.Add(new RockBullet(Position, Direction, Speed));
            else if (this.GetType() == typeof(BlueCat))
                Gameplay.Bullets.Add(new IceBullet(Position, Direction, Speed));
            IsShooting = true;
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public Vector2 GetDirection()
        {
            return Direction;
        }

        public void ChangePosition(Vector2 pos)
        {
            Position = pos;
        }
        public void ChangeDirection(Vector2 dir)
        {
            Direction = dir;
        }

        public void Swap()
        {
            Position = (Position == Gameplay.Position.Left) ? Gameplay.Position.Right : Gameplay.Position.Left;
            Direction = (Direction == Directions.Left) ? Directions.Right : Directions.Left;
        }

        public void MakeShootingTexture()
        {
            IsShooting = true;
        }

        public void MakePeaceTexture()
        {
            IsShooting = false;
        }

        public void PowerUp()
        {
            
        }
    }

    class OrangeCat: Cat
    {
        public static Texture2D PassiveTextureLeft;
        public static Texture2D PassiveTextureRight;
        public static Texture2D ActiveTextureLeft;
        public static Texture2D ActiveTextureRight;

        public OrangeCat(Vector2 position) : base()
        {
            Position = position;
            Direction = (position == Gameplay.Position.Left) ? Directions.Left : Directions.Right;
            IsShooting = false;
            Texture = PassiveTextureLeft;
            Speed = 25.0f;
        }

        public override void Update()
        {
            if (IsShooting)
                Texture = (Position == Gameplay.Position.Left) ? ActiveTextureLeft : ActiveTextureRight;
            else
                Texture = (Position == Gameplay.Position.Left) ? PassiveTextureLeft : PassiveTextureRight;
            MakePeaceTexture();
        }
    }

    class BlueCat : Cat
    {
        public static Texture2D PassiveTextureLeft;
        public static Texture2D PassiveTextureRight;
        public static Texture2D ActiveTextureLeft;
        public static Texture2D ActiveTextureRight;

        public BlueCat(Vector2 position) : base()
        {
            Position = position;
            Direction = (position == Gameplay.Position.Left) ? Directions.Left : Directions.Right;
            IsShooting = false;
            Texture = PassiveTextureRight;
            Speed = 15.0f;
        }

        public override void Update()
        {
            if (IsShooting)
                Texture = (Position == Gameplay.Position.Left) ? ActiveTextureLeft : ActiveTextureRight;
            else
                Texture = (Position == Gameplay.Position.Left) ? PassiveTextureLeft : PassiveTextureRight;
            MakePeaceTexture();
        }
    }
}
