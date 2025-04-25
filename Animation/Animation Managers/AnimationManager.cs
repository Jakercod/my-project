using System.Diagnostics.Metrics;
using Microsoft.Xna.Framework.Audio;
namespace GameV10.Animation
{
    public class AnimationManager : AnimationDetails
    {
        //Textures linking to spritesheets of each isometric direction 
        public Texture2D ActiveTexture;
        public Texture2D TextureNorth { get; private set; }
        public Texture2D TextureSouth { get; private set; }
        public Texture2D TextureEastWest { get; private set; }
        public Texture2D TextureSouthEast { get; private set; }
        public Texture2D TextureNorthEast { get; private set; }

        //reduces the amount of assets needed by fliping the texture for opposite directions
        public SpriteEffects spriteEffects;

        //classes linking managed by the animation manager
        public AnimationDetails CurrentAnimation = new();
        private AnimationDirection Direction = new();
        private Animations animation = new();
        public string direction;
        //acts as the string to to the definition in (Animatiions)
        public string action = "";
        //contains the previous animation
        public string prevaction;
        //used to add differences to some animations 
        private Random random = new();
        private int randomint;
        //used to track combo attack
        private int attackcount;
        public AnimationManager(Texture2D textureNorth, Texture2D textureSouth, Texture2D textureEastWest, Texture2D textureNorthEast, Texture2D textureSouthEast)
        {
            TextureNorth = textureNorth;
            TextureSouth = textureSouth;
            TextureEastWest = textureEastWest;
            TextureNorthEast = textureNorthEast;
            TextureSouthEast = textureSouthEast;
            //starting texture 
            ActiveTexture = textureNorth;
        }

        public void Update(Game1 game1, SpriteBase sprite)
        {
            randomint = random.Next(0, 500);
            direction = Direction.SetDirection(game1, sprite, TextureNorth, TextureSouth, TextureEastWest, TextureNorthEast, TextureSouthEast, ref ActiveTexture,  ref spriteEffects);
            action = GetState(game1, sprite);
            if(action != prevaction)
            {
                CurrentAnimation = animation.animations[action];
            }
            NextInterval(game1, CurrentAnimation, sprite);
        }
        public string GetState(Game1 game1, SpriteBase sprite)
        {
            prevaction = action;
            
            if (game1.InputManager.idle)
            {
                action = "idle1";
                if(randomint == 1 || prevaction == "idle2")
                {
                    action = "idle2";
                }
            }
            if (sprite.running)
            {
                action = "running";
            }
            if (sprite.charging)
            {
                action = "charging";
            }
            if (sprite.walking)
            {
                action = "walking";
            }
            if (sprite.blocking)
            {
                action = "block1";
            }
            if (sprite.sliding)
            {
                action = "sliding";
            }
            if (sprite.jumping)
            {
                action = "jumping";
                
                if (sprite.running)
                {
                    action = "runningjump";
                }
                
            }
            if (sprite.attacking)
            {
                action = "attack";

                if (attackcount == 2 || prevaction == "comboattack")
                {
                    action = "comboattack";
                    attackcount = 0;
                }
                if (sprite.running)
                {
                    action = "spinattack";
                }
                if (sprite.jumping)
                {
                    action = "jumpattack";
                }
                if (sprite.running && sprite.jumping)
                {
                    action = "jumpspinattack";
                }
                if (sprite.sliding)
                {
                    action = "kick";
                }
                if (sprite.blocking)
                {
                    action = "knock";
                }
                
            }
            if (!sprite.charging && action == "charging")
            {
                action = "chargeattack";
            }
            if (sprite.Stunned)
            {
                action = "impact";
            }
            if (sprite.Health <= 0)
            {
                int randomintdie = random.Next(0,1);
                if (randomintdie == 1)
                {
                    action = "die1";
                }
                else
                {
                    action = "die2";
                }
            }
            return action;
        }
        public void NextInterval(Game1 game1, AnimationDetails currentanimation, SpriteBase sprite)
        {
            counter++;
            if (counter > currentanimation.interval)
            {
                currentanimation.rowpos = currentanimation.state.GetHashCode();
                counter = 0;
                NextFrame(game1, currentanimation, sprite);
            }
        }

        public void NextFrame(Game1 game1, AnimationDetails currentanimation, SpriteBase sprite)
        {
            //shifts through to the next source column
            
            currentanimation.colpos++;

            //checks for the end of the animation

            sprite.attackRect = Rectangle.Empty;
            sprite.blockRect = Rectangle.Empty;
            if (currentanimation.colpos >= currentanimation.numcol)
            {
                
                switch (currentanimation.state)
                {
                    case Stance.attack:
                        game1.GenerateAttacks.SwordAttack(game1, sprite);
                        break;
                    case Stance.comboattack:
                        game1.GenerateAttacks.ComboAttack(game1, sprite);
                        break;
                    case Stance.jumpattack:
                        game1.GenerateAttacks.JumpAttack(game1, sprite);
                        break;
                    case Stance.jumpspinattack:
                        game1.GenerateAttacks.JumpSpinAttack(game1, sprite);
                        break;
                    case Stance.spinattack:
                        game1.GenerateAttacks.SpinAttack(game1, sprite);
                        break;
                    case Stance.chargeattack:
                        game1.GenerateAttacks.ChargeAttack(game1, sprite);
                        break;
                    case Stance.knock:
                        game1.GenerateAttacks.KnockAttack(game1, sprite);
                        break;
                    case Stance.kick:
                        game1.GenerateAttacks.KickAttack(game1, sprite);
                        break;
                    case Stance.block1:
                        game1.GenerateAttacks.BlockAttack(game1, sprite);
                        break;


                }
                currentanimation.colpos = 0;
                if(action == "die1"|| action == "die2")
                {
                    sprite.Dead = true;
                }
                if (action == "attack")
                {
                    attackcount++;
                }
                if (sprite.blocking)
                {
                    currentanimation.colpos = 4;
                }
                if (sprite.charging)
                {
                    currentanimation.colpos = 12;
                }
                if((action == "die2" || action == "die1") && sprite == game1.Player)
                {
                    game1.GameOver = true;
                }
                action = "";
                sprite.jumping = false;
                sprite.sliding = false;
                sprite.attacking = false;
                sprite.Stunned = false;
                
            }
        }
        public Rectangle GetFrame(AnimationDetails currentanimation)
        {
            //finds the active frame rectangle so it can be accessed for drawing
            Rectangle sourceRect = new Rectangle(
                currentanimation.colpos * 256,
                currentanimation.rowpos * 256,
                256,
                256
                );

            return sourceRect;
        }

    }
}

