using Microsoft.Xna.Framework;

namespace GameV10.Sprites.Player
{
    internal class PowerProjectileAttack
    {
        private float _timer;
        public float LifeSpan;
        public bool IsRemoved;
        public Vector2 Position;
        public Vector2 Direction;
        public Vector2 Origin;
        public float Rotation;
        public float Speed;
        public float Damage;
        public Rectangle Hitbox;
        public Texture2D Texture;
        public bool playershoot;
        int colpos;
        int rowpos;
        int counter;
        int numcol = 6;
        int interval = 5;
        public PowerProjectileAttack(Game1 game1,Texture2D texture, Vector2 position, float speed, float damage, float lifespan)
        {
            playershoot = false;
            Texture = texture;
            Position = position;
            Direction = DirectionVelocity(game1, game1.InputManager.StringDirection);
            Speed = speed;
            Damage = damage;
            LifeSpan = lifespan;
            Origin = new Vector2(112 / 2, 80 / 2);
            Hitbox = new Rectangle((int)Position.X - 112/2, (int)Position.Y - 80/2, 112, 80);
        }
        public void Update(GameTime gameTime, Game1 game1)
        {
            NextInterval();
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Direction * Speed;
            Hitbox = new Rectangle((int)Position.X - 112/2, (int)Position.Y - 80/2, 112, 80);
            Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
            if (_timer > LifeSpan)
            {
                IsRemoved = true;
            }
            if (game1.Player.powerattack)
            {
                Shoot();
                game1.Player.powerattack = false;
            }
            foreach(BaseEnemy enemy in game1.BasicEnemys)
            {
                if (Hitbox.Intersects(enemy.Hitbox))
                {
                    enemy.Health -= Damage;
                    enemy.burning = true;
                    enemy.Hit = true;
                    IsRemoved = true;
                }
            }
            

        }
        private void Shoot()
        {
            playershoot = true;
        }
        private Vector2 DirectionVelocity(Game1 game1, string stringdirection)
        {
            string projectiledirection = stringdirection;
            switch (projectiledirection)
            {
                case "Up":
                    return new Vector2(0, -2);

                case "Left":
                    return new Vector2(-2, 0);

                case "Down":
                    return new Vector2(0, 2);

                case "Right":
                    return new Vector2(2, 0);

                case "Up/Right":
                    return new Vector2(2, -1);

                case "Up/Left":
                    return new Vector2(-2, -1);

                case "Down/Left":
                    return new Vector2(-2, 1);

                case "Down/Right":
                    return new Vector2(2, 1);

                default:
                    return DirectionVelocity(game1, game1.InputManager.PrevStringDirection);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, GetFrame(), Color.White, Rotation, Origin, 1f, SpriteEffects.None, 0);
        }
        public void NextInterval()
        {
            counter++;
            if (counter > interval)
            {
                counter = 0;
                NextFrame();
            }
        }
        public void NextFrame()
        {
            colpos++;

            if (colpos >= numcol)
            {
                colpos = 0;
            }
            
        }
        public Rectangle GetFrame()
        {
            //finds the active frame rectangle so it can be accessed for drawing
            Rectangle sourceRect = new Rectangle(
                colpos * 112,
                rowpos * 80,
                112,
                80
                );

            return sourceRect;
        }

    }
}
