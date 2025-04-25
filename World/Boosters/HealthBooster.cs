using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV10.World.Boosters
{
    internal class HealthBooster
    {
        public bool IsRemoved;
        public Rectangle Hitbox { get; set; }
        public Texture2D Texture { get; set; }
        public int HealthBoost { get; set; }
        public HealthBooster(Texture2D texture, Vector2 position, int healthBoost)
        {
            Texture = texture;
            Hitbox = new((int)position.X + texture.Width/2, (int)position.Y + texture.Height/2, texture.Width, texture.Height);
            HealthBoost = healthBoost;
        }
        public void Update(Game1 game1)
        {
            if (game1.Player.Hitbox.Intersects(Hitbox))
            {
                game1.Player.Health += HealthBoost;
                game1.BoosterSFX.Play();
                if (game1.Player.Health > game1.Player.maxHealth)
                {
                    game1.Player.Health = game1.Player.maxHealth;
                }
                IsRemoved = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Hitbox, Color.White);
        }
    }
}
