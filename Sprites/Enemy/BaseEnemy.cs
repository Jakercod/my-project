using GameV10.GameStates.InGame.HeadsUpDisplay;
using Microsoft.Xna.Framework.Audio;

namespace GameV10.Sprites.Enemy
{
    internal class BaseEnemy : SpriteBase
    {
        public readonly ProgressBarAnimated _healthBarAnimated;
        private StartPathfinding StartPathfinding = new();
        private int i = 1;
        private Vector2 direction;
        private Vector2 mappos;
        private Point[] path;
        private bool pointreached = true;
        Vector2 position;
        Vector2 pos;
        public Rectangle terrorradius;
        public Rectangle terrorradiusfollow;
        private int tilesize = 120;
        int attackcount;
        HeuristicFormula Formula;
        public bool burning;
        int burncount;
        int followtimer = 10;
        Random random = new();
        public BaseEnemy(Texture2D north, Texture2D south, Texture2D west, Texture2D Nwest, Texture2D Swest, Vector2 position, Vector2 velocity, Vector2 size, float speed, float health, HeuristicFormula formula, Texture2D healthBar, Texture2D healthBarBG) : base(north, south, west, Nwest, Swest, position, velocity, size, speed, health)
        {
            float tileposy = (tilesize / 2 * position.Y - position.X * (tilesize / 4)) / (tilesize / 2 * (tilesize / 4) - (-(tilesize / 2) * (tilesize / 4)));
            float tileposx = position.X / (tilesize / 2) + tileposy;
            mappos = new Vector2(tileposx, tileposy);
            NAME = "Enemy";
            Formula = formula;
            Health = health;
            int maxHealth = (int)health;
            _healthBarAnimated = new(healthBarBG, healthBar, healthBarBG, maxHealth, position, new Vector2(82, 17));
        }
        public override void Update(GameTime gameTime, Game1 game1)
        {
            terrorradius = new Rectangle((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2), (int)(2*Size.X ), (int)(2*Size.Y ));
            terrorradiusfollow = new Rectangle((int)(Position.X - 2*Size.Y), (int)(Position.Y - 2*Size.Y), (int)(4*Size.Y), (int)(4*Size.Y));
            if (Health > 0)
            {
                if(Hit)
                {
                    colour = Color.Red;
                }
                else
                {
                    colour = Color.White;
                }
                if (terrorradius.Intersects(game1.Player.Hitbox))
                {
                    attackcount++;
                    if (attackcount > 10)
                    {
                        attackcount = 0;
                        attacking = true;
                    }
                }
                if (Hitbox.Intersects(game1.Player.attackRect) && !Hit)
                {
                    Hit = true;
                    Health -= game1.GenerateAttacks.damage;
                }
                if (!Hitbox.Intersects(game1.Player.attackRect))
                {
                    Hit = false;
                }
                if (attackRect.Intersects(game1.Player.blockRect))
                {
                    if (game1.blockIN.State == SoundState.Stopped)
                    {
                        game1.blockIN.Play();
                    }
                    
                    Stunned = true;
                    game1.Player.block -= 20;
                }
                
                if (burning)
                {
                    burncount++;
                    Health -= 1;
                    if (burncount == 500)
                    {
                        burncount = 0;
                        burning = false;
                    }
                }
                _healthBarAnimated.Update(Health, Position, game1, this);
                Bases.Clear();
                if (terrorradiusfollow.Intersects(game1.Player.Hitbox))
                {
                    followtimer = 0;
                }
                if (!Hitbox.Intersects(game1.Player.Hitbox) && pointreached) //stops working once taken out pathfinding in input manager
                {
                    if(terrorradiusfollow.Intersects(game1.Player.Hitbox) || followtimer < 10)
                    {
                        followtimer++;
                        path = StartPathfinding.Pathfind(game1, mappos, game1.Player.Position, Formula);
                    }
                    else
                    {
                        int x = random.Next(-100,100);
                        int y = random.Next(-100,100);
                        
                        Vector2 mapposreal = new Vector2(
                        (mappos.X - mappos.Y) * 64,
                        (mappos.X + mappos.Y) * 32);
                        bool accept = false;
                        while (!accept)
                        {
                            Vector2 find = new Vector2(mapposreal.X + x, mapposreal.Y + y);
                            if (StartPathfinding.Pathfind(game1, mappos, find, Formula) != null)
                            {
                                path = StartPathfinding.Pathfind(game1, mappos, find, Formula);
                                accept = true;
                            }
                        }
                    }
                    
                    running = true;

                    if (path.Length > 1)
                    {
                        try
                        {
                            direction = new Vector2(path[i].ToVector2().X - path[i - 1].ToVector2().X, path[i].ToVector2().Y - path[i - 1].ToVector2().Y);
                        }
                        catch
                        {

                        }
                        mappos = new Vector2(mappos.X + direction.X, mappos.Y + direction.Y);
                        position = new Vector2(
                        (direction.X - direction.Y) * 64,
                        (direction.X + direction.Y) * 32);

                        switch (direction) // added this so that not just diagonal movement
                        {
                            case Vector2(1, 1):
                                Velocity = new Vector2(0, 2);
                                Direction = "Down";
                                break;
                            case Vector2(-1, 1):
                                Velocity = new Vector2(-2, 0);
                                Direction = "Left";
                                break;
                            case Vector2(1, -1):
                                Velocity = new Vector2(2, 0);
                                Direction = "Right";
                                break;
                            case Vector2(-1, -1):
                                Velocity = new Vector2(0, -2);
                                Direction = "Up";
                                break;
                            case Vector2(1, 0):
                                Velocity = new Vector2(2, 1);
                                Direction = "Down/Right";
                                break;
                            case Vector2(0, 1):
                                Velocity = new Vector2(-2, 1);
                                Direction = "Down/Left";
                                break;
                            case Vector2(-1, 0):
                                Velocity = new Vector2(-2, -1);
                                Direction = "Up/Left";
                                break;
                            case Vector2(0, -1):
                                Velocity = new Vector2(2, -1);
                                Direction = "Up/Right";
                                break;
                        }

                        pointreached = false;
                    }
                }
                else if (!pointreached)
                {
                    Position += Velocity;
                    pos += Velocity;
                    if (pos.X == position.X && pos.Y == position.Y)
                    {
                        i = 1;
                        pos = Vector2.Zero;
                        pointreached = true;
                    }
                }
                else
                {
                    running = false;
                }

                game1.collisionManager.Update(this, game1, Position, Velocity, BoundBox, Hitbox);
                AnimationManager.Update(game1, this);
            }
            else
            {
                //QUEUE DEATH ANIMATION AND THEN TERMINATE ENEMY
                AnimationManager.Update(game1, this);
            }


            base.Update(gameTime, game1);
        }

    }
}
