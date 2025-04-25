using GameV10;
using GameV10.Animation;
using GameV10.GameStates.InGame.HeadsUpDisplay;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Diagnostics.Metrics;

namespace GameV10.Sprites.Player
{
    internal class Player : SpriteBase
    {
        
        public readonly ProgressBarAnimated _healthBarAnimated;
        public readonly ProgressBarAnimated _blockBarAnimated;
        public readonly ProgressBarAnimated _powerBarAnimated;
        public float block;
        public float power;
        public bool powerattack;
        public float maxHealth;
        public float maxBlock;
        public float maxPower;
        Texture2D powerAttackTex;
        public int playerkills;
        public int currentplayerkills;
        public int score;
        public int scorecount;
        public int damagerampup = 0;
        public int immunity = 0;
        public bool immunityactive = false;

        public Player(Texture2D north, Texture2D south, Texture2D west, Texture2D Nwest, Texture2D Swest, Vector2 position, Vector2 velocity, Vector2 size, float speed, float health, Texture2D healthBar, Texture2D healthBarBG, Texture2D blockBar, Texture2D blockBarBG, Texture2D powerBar, Texture2D powerBarBG, Texture2D HUDback, Texture2D powerattacktex) : base(north, south, west, Nwest, Swest, position, velocity, size, speed, health) 
        {
            NAME = "Player";
            Health = health;
            block = 500;
            power = 1000;
            maxHealth = Health;
            maxBlock = block;
            maxPower = power;
            powerAttackTex = powerattacktex;
            _healthBarAnimated = new(HUDback, healthBar, healthBarBG, maxHealth, position, new Vector2(82,17));
            _blockBarAnimated = new(HUDback, blockBar, blockBarBG, maxBlock, position, new Vector2(89, 25));
            _powerBarAnimated = new(HUDback, powerBar, powerBarBG, maxPower, position, new Vector2(98, 33));
        }

        public override void Update(GameTime gameTime, Game1 game1)
        {
            //health bug player seems to be taking too much damage
            if (Health > 0)
            {
                if (immunityactive)
                {
                    immunity++;
                    if (immunity > 10)
                    {
                        immunity = 0;
                        immunityactive = false;
                    }
                }
                scorecount++;
                if (playerkills > currentplayerkills)
                {
                    currentplayerkills = playerkills;
                    power = maxPower;
                }
                if (scorecount > 100)
                {
                    scorecount = 0;
                    score += 1;
                }
                foreach (BaseEnemy enemy in game1.BasicEnemys)
                {
                    if (Hitbox.Intersects(enemy.attackRect) && !Hit && !blockRect.Intersects(enemy.attackRect) && !immunityactive)
                    {
                        game1.HitSFX.Play();
                        Hit = true;
                        Health -= 100 + damagerampup;
                        if(Health <= 0)
                        {
                            Health = 0;
                        }
                        immunityactive = true;
                    }
                    
                }
                foreach (BaseEnemy enemy in game1.BasicEnemys)
                {
                    if (!Hitbox.Intersects(enemy.attackRect))
                    {
                        Hit = false;
                    }
                    else
                    {
                        break;
                    }
                    

                }
                

                if (block <= 0)
                {
                    blocking = false;
                    block = 1;
                }
                if (blocking)
                {
                    block -= 1;
                }
                else if (!blocking && block < maxBlock)
                {
                    block += 1;
                }
                if(power <= 0)
                {
                    powerattack = false;
                }
                if (!powerattack && power < maxPower)
                {
                    power += 1;
                }
                if (powerattack)
                {
                    game1.powerattackSFX.Play();
                    power -= 1000;
                    game1.powerProjectiles.Add(new PowerProjectileAttack(game1,powerAttackTex, new(Position.X + Size.X/2, Position.Y + Size.Y/2), 3, 500, 5));
                }
                foreach(PowerProjectileAttack powerProjectile in game1.powerProjectiles)
                {
                    powerProjectile.Update(gameTime, game1);
                }
                for(int i = 0; i < game1.powerProjectiles.Count; i++)
                {
                    if(game1.powerProjectiles[i].IsRemoved)
                    {
                        game1.powerProjectiles.RemoveAt(i);
                    }
                }
                Bases.Clear();
                Velocity = GetVelocity(game1);
                if ((game1.create.basecolliding || game1.create.topcolliding) && !game1.Player.jumping)
                {
                    Position = game1.create.CollisionPos;
                }
                Position += Velocity * Speed;

                switch (AnimationManager.action)
                {
                    case "idle1":
                        Speed = 0;
                        game1.RunningIN.Stop();
                        break;
                    case "idle2":
                        Speed = 0;
                        game1.RunningIN.Stop();
                        break;
                    case "attack":
                        Speed = 0;
                        if (game1.swordattack1IN.State == SoundState.Stopped)
                        {
                            game1.swordattack1IN.Play();
                        }
                        break;
                    case "walking":
                        Speed = 1;
                        break;
                    case "running":
                        Speed = 2;
                        if (game1.RunningIN.State == SoundState.Stopped)
                        {
                            game1.RunningIN.Play();
                        }
                        break;
                    case "sliding":
                        Speed = 3;
                        break;
                    case "jumpspinattack":
                        Speed = 1;
                        break;
                    case "runningjump":
                        Speed = 2;
                        break;
                    case "spinattack":
                        Speed = 2;
                        break;
                    case "comboattack":
                        Speed = 0;
                        if (game1.swordattack2IN.State == SoundState.Stopped)
                        {
                            game1.swordattack2IN.Play();
                        }
                        break;
                    default:
                        Speed = 0;
                        break;

                }

                game1.collisionManager.Update(this, game1, Position, Velocity, BoundBox, Hitbox);
                AnimationManager.Update(game1, this);

            }
            else
            {
                AnimationManager.Update(game1, this);
            }
            
            _healthBarAnimated.Update(Health, Position, game1, this);
            _blockBarAnimated.Update(block, Position, game1, this);
            _powerBarAnimated.Update(power, Position, game1, this);
            base.Update(gameTime, game1);
        }

        public Vector2 GetVelocity(Game1 game1)
        {
            switch (game1.InputManager.StringDirection)
            {
                case "Up":
                    Direction = "Up";
                    return new Vector2(0, -2);

                case "Left":
                    Direction = "Left";
                    return new Vector2(-2, 0);

                case "Down":
                    Direction = "Down";
                    return new Vector2(0, 2);

                case "Right":
                    Direction = "Right";
                    return new Vector2(2, 0);

                case "Up/Right":
                    Direction = "Up/Right";
                    return new Vector2(2, -1);
                
                case "Up/Left":
                    Direction = "Up/Left";
                    return new Vector2(-2, -1);

                case "Down/Left":
                    Direction = "Down/Left";
                    return new Vector2(-2, 1);

                case "Down/Right":
                    Direction = "Down/Right";
                    return new Vector2(2, 1);

                default:
                    Direction = game1.InputManager.PrevStringDirection;
                    return Vector2.Zero;
            }

        }
        
    }
}

