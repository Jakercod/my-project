namespace GameV10.World.Collision.Combat
{
    internal class GeneratePlayerAttackZones
    {
        //stamina parry ability
        public float damage;

        public void SwordAttack(Game1 game1, SpriteBase sprite)
        {
            damage = 300;
            Vector2 Centre = new Vector2(sprite.Hitbox.X + sprite.Hitbox.Width / 2, sprite.Hitbox.Y + sprite.Hitbox.Height / 2);
            switch (sprite.Direction)
            {
                case "Up":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width, (int)Centre.Y - sprite.Hitbox.Height, sprite.Hitbox.Height, sprite.Hitbox.Height);
                    break;
                case "Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)Centre.Y - sprite.Hitbox.Height / 2, sprite.Hitbox.Height, sprite.Hitbox.Height);
                    break;
                case "Down":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width, (int)Centre.Y, sprite.Hitbox.Height, sprite.Hitbox.Height);
                    break;
                case "Left":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Height, (int)Centre.Y - sprite.Hitbox.Height / 2, sprite.Hitbox.Height, sprite.Hitbox.Height);
                    break;
                case "Up/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - sprite.Hitbox.Height), (int)(Centre.Y - sprite.Hitbox.Height), sprite.Hitbox.Height, sprite.Hitbox.Height);
                    break;
                case "Up/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)(Centre.Y - sprite.Hitbox.Height), sprite.Hitbox.Height, sprite.Hitbox.Height);
                    break;
                case "Down/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - sprite.Hitbox.Height), (int)Centre.Y, sprite.Hitbox.Height, sprite.Hitbox.Height);
                    break;
                case "Down/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)Centre.Y, sprite.Hitbox.Height, sprite.Hitbox.Height);
                    break;
            }
        }
        public void ComboAttack(Game1 game1, SpriteBase sprite)
        {
            damage = 900;
            Vector2 Centre = new Vector2(sprite.Hitbox.X + sprite.Hitbox.Width / 2, sprite.Hitbox.Y + sprite.Hitbox.Height / 2);
            switch (sprite.Direction)
            {
                case "Up":
                    sprite.attackRect = new Rectangle((int)Centre.X - 2 * sprite.Hitbox.Width, (int)Centre.Y - sprite.Hitbox.Height, 2 * sprite.Hitbox.Height, 2 * sprite.Hitbox.Width);
                    break;
                case "Right":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2, (int)Centre.Y - sprite.Hitbox.Height, (int)(1.5 * sprite.Hitbox.Height), 2 * sprite.Hitbox.Height);
                    break;
                case "Down":
                    sprite.attackRect = new Rectangle((int)Centre.X - 2 * sprite.Hitbox.Width, (int)Centre.Y, 2 * sprite.Hitbox.Height, 2 * sprite.Hitbox.Width);
                    break;
                case "Left":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2 - sprite.Hitbox.Height, (int)Centre.Y - sprite.Hitbox.Height, (int)(1.5 * sprite.Hitbox.Height), 2 * sprite.Hitbox.Height);
                    break;
                case "Up/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Height), (int)(Centre.Y - 1.5 * sprite.Hitbox.Height), (int)(1.5 * sprite.Hitbox.Height), (int)(1.5 * sprite.Hitbox.Height));
                    break;
                case "Up/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)(Centre.Y - 1.5 * sprite.Hitbox.Height), (int)(1.5 * sprite.Hitbox.Height), (int)(1.5 * sprite.Hitbox.Height));
                    break;
                case "Down/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Height), (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Height), (int)(1.5 * sprite.Hitbox.Height));
                    break;
                case "Down/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Height), (int)(1.5 * sprite.Hitbox.Height));
                    break;
            }
        }
        public void JumpAttack(Game1 game1, SpriteBase sprite)
        {
            damage = 1000;
            Vector2 Centre = new Vector2(sprite.Hitbox.X + sprite.Hitbox.Width / 2, sprite.Hitbox.Y + sprite.Hitbox.Height / 2);
            switch (sprite.Direction)
            {
                case "Up":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2, (int)Centre.Y - sprite.Hitbox.Height, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)Centre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Down":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2, (int)Centre.Y, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Left":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Height, (int)Centre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Up/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Width), (int)(Centre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Up/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)(Centre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Width), (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
            }
        }
        public void JumpSpinAttack(Game1 game1, SpriteBase sprite)
        {
            damage = 1000;
            Vector2 Centre = new Vector2(sprite.Hitbox.X + sprite.Hitbox.Width / 2, sprite.Hitbox.Y + sprite.Hitbox.Height / 2);
            switch (sprite.Direction)
            {
                case "Up":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2, (int)Centre.Y - sprite.Hitbox.Height, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)Centre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Down":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2, (int)Centre.Y, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Left":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Height, (int)Centre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Up/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Width), (int)(Centre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Up/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)(Centre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Width), (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
            }
        }
        public void SpinAttack(Game1 game1, SpriteBase sprite)
        {
            damage = 250;
            Vector2 Centre = new Vector2(sprite.Hitbox.X + sprite.Hitbox.Width / 2, sprite.Hitbox.Y + sprite.Hitbox.Height / 2);
            sprite.attackRect = new Rectangle((int)Centre.X - game1.Player.Hitbox.Height / 2, (int)Centre.Y - game1.Player.Hitbox.Height / 2, game1.Player.Hitbox.Height, game1.Player.Hitbox.Height);
        }
        public void ChargeAttack(Game1 game1, SpriteBase sprite)
        {
            damage = 1000;
            //small hitboc ( may add a partical effect)
            //animation not working
            Vector2 PlayerCentre = new Vector2(sprite.Hitbox.X + sprite.Hitbox.Width / 2, sprite.Hitbox.Y + sprite.Hitbox.Height / 2);
            switch (sprite.Direction)
            {
                case "Up":
                    sprite.attackRect = new Rectangle((int)PlayerCentre.X - sprite.Hitbox.Width / 2, (int)PlayerCentre.Y - sprite.Hitbox.Height, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Right":
                    sprite.attackRect = new Rectangle((int)PlayerCentre.X, (int)PlayerCentre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Down":
                    sprite.attackRect = new Rectangle((int)PlayerCentre.X - sprite.Hitbox.Width / 2, (int)PlayerCentre.Y, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Left":
                    sprite.attackRect = new Rectangle((int)PlayerCentre.X - sprite.Hitbox.Height, (int)PlayerCentre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Up/Left":
                    sprite.attackRect = new Rectangle((int)(PlayerCentre.X - 1.5 * sprite.Hitbox.Width), (int)(PlayerCentre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Up/Right":
                    sprite.attackRect = new Rectangle((int)PlayerCentre.X, (int)(PlayerCentre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Left":
                    sprite.attackRect = new Rectangle((int)(PlayerCentre.X - 1.5 * sprite.Hitbox.Width), (int)PlayerCentre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Right":
                    sprite.attackRect = new Rectangle((int)PlayerCentre.X, (int)PlayerCentre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
            }
        }
        public void KnockAttack(Game1 game1, SpriteBase sprite)
        {
            damage = 100;
            Vector2 Centre = new Vector2(sprite.Hitbox.X + sprite.Hitbox.Width / 2, sprite.Hitbox.Y + sprite.Hitbox.Height / 2);
            switch (sprite.Direction)
            {
                case "Up":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2, (int)Centre.Y - sprite.Hitbox.Height, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)Centre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Down":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2, (int)Centre.Y, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Left":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Height, (int)Centre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Up/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Width), (int)(Centre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Up/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)(Centre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Width), (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
            }
        }
        public void KickAttack(Game1 game1, SpriteBase sprite)
        {
            damage = 100;
            //knockback
            Vector2 Centre = new Vector2(sprite.Hitbox.X + sprite.Hitbox.Width / 2, sprite.Hitbox.Y + sprite.Hitbox.Height / 2);
            switch (sprite.Direction)
            {
                case "Up":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2, (int)Centre.Y - sprite.Hitbox.Height, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)Centre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Down":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2, (int)Centre.Y, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Left":
                    sprite.attackRect = new Rectangle((int)Centre.X - sprite.Hitbox.Height, (int)Centre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Up/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Width), (int)(Centre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Up/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)(Centre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Left":
                    sprite.attackRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Width), (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Right":
                    sprite.attackRect = new Rectangle((int)Centre.X, (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
            }
        }
        public void BlockAttack(Game1 game1, SpriteBase sprite)
        {
            damage = 0;

            Vector2 Centre = new Vector2(sprite.Hitbox.X + sprite.Hitbox.Width / 2, sprite.Hitbox.Y + sprite.Hitbox.Height / 2);
            switch (sprite.Direction)
            {
                case "Up":
                    sprite.blockRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2, (int)Centre.Y - sprite.Hitbox.Height, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Right":
                    sprite.blockRect = new Rectangle((int)Centre.X, (int)Centre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Down":
                    sprite.blockRect = new Rectangle((int)Centre.X - sprite.Hitbox.Width / 2, (int)Centre.Y, sprite.Hitbox.Width, sprite.Hitbox.Height);
                    break;
                case "Left":
                    sprite.blockRect = new Rectangle((int)Centre.X - sprite.Hitbox.Height, (int)Centre.Y - sprite.Hitbox.Width / 2, sprite.Hitbox.Height, sprite.Hitbox.Width);
                    break;
                case "Up/Left":
                    sprite.blockRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Width), (int)(Centre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Up/Right":
                    sprite.blockRect = new Rectangle((int)Centre.X, (int)(Centre.Y - 1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Left":
                    sprite.blockRect = new Rectangle((int)(Centre.X - 1.5 * sprite.Hitbox.Width), (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
                case "Down/Right":
                    sprite.blockRect = new Rectangle((int)Centre.X, (int)Centre.Y, (int)(1.5 * sprite.Hitbox.Width), (int)(1.5 * sprite.Hitbox.Width));
                    break;
            }
        }

    }
}
