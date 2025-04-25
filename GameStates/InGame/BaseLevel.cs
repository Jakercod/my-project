
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Reflection.Metadata;

namespace GameV10.GameStates.InGame
{
    internal class TestGame : State
    {
        
        private Texture2D _pixel;
        
        public List<Song> playlist = new();
        public int currentSongIndex;
        public Song song1;
        public float roundTime; // Time in seconds
        public float roundDuration; // Duration of the round in seconds
        bool roundActive;
        public Texture2D temptexture;
        public Texture2D textureAtlas;
        public Texture2D playertextureNorth;
        public Texture2D playertextureSouth;
        public Texture2D playertextureWest;
        public Texture2D playertextureSouthWest;
        public Texture2D playertextureNorthWest;
        public Texture2D HUDBack;
        public Texture2D healthBar;
        public Texture2D healthBarBG;
        public Texture2D blockBar;
        public Texture2D blockBarBG;
        public Texture2D powerBar;
        public Texture2D powerBarBG;
        public Texture2D powerattacktex;
        public Texture2D healthboostertexture;
        public string GamerTag;
        public TestGame(Game1 game1, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, GraphicsDevice graphicsDevice) : base(game1, graphicsDeviceManager, content, graphicsDevice)
        {
            game1.GameOver = false;
            game1.GameWon = false;
            roundTime = 0;
            roundDuration = 60; // Set the round duration to 60 seconds
            roundActive = true;
            game1.scoretext = content.Load<SpriteFont>("ScoreFont");
            temptexture = content.Load<Texture2D>("TempSprite");
            textureAtlas = content.Load<Texture2D>("sprite-0002");
            playertextureNorth = content.Load<Texture2D>("PlayerNorthSpritesheetSwordShield");
            playertextureSouth = content.Load<Texture2D>("PlayerSouthSpritesheetSwordShield");
            playertextureWest = content.Load<Texture2D>("PlayerWestSpritesheetSwordShield");
            playertextureSouthWest = content.Load<Texture2D>("PlayerSouthWestSpritesheetSwordShield");
            playertextureNorthWest = content.Load<Texture2D>("PlayerNorthWestSpritesheetSwordShield");
            HUDBack = Content.Load<Texture2D>("HUD");
            healthBar = Content.Load<Texture2D>("HUDhealth");
            healthBarBG = Content.Load<Texture2D>("HUDhealthBG");
            blockBar = Content.Load<Texture2D>("HUDblock");
            blockBarBG = Content.Load<Texture2D>("HUDblockBG");
            powerBar = Content.Load<Texture2D>("HUDpower");
            powerBarBG = Content.Load<Texture2D>("HUDpowerBG");
            powerattacktex = Content.Load<Texture2D>("Fireball");
            healthboostertexture = Content.Load<Texture2D>("Healthtemp");

            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });
        }
        public override void Update(GameTime gameTime)
        {
            if (roundActive)
            {
                // Update the round time
                roundTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Check if the round duration has been reached
                //if (roundTime >= roundDuration)
                //{
                 //   roundActive = false;
                    // Round has ended, you can add your logic here
               // }
            }
            if (Game1.GameOver)
            {
                Game1.DeathIN.Play();
                MediaPlayer.Pause();
                Leaderboard.AddNewScore(GamerTag, Game1.Player.score, roundTime, Game1.Player.playerkills);
                Game1.ChangeState(new GameOver(Game1, GraphicsDeviceManager, Content, GraphicsDevice, Game1.backgroundTexture, GamerTag, Game1.Player.score, roundTime, Game1.Player.playerkills));
                Game1.sprites.Clear();
                Game1.BasicEnemys.Clear();
            }
            else if (Game1.GameWon)
            {
                //different coloured test to show difficulty and won/loss
                MediaPlayer.Pause();
                Leaderboard.AddNewScore(GamerTag, Game1.Player.score, roundTime, Game1.Player.playerkills);
                Game1.ChangeState(new GameWon(Game1, GraphicsDeviceManager, Content, GraphicsDevice, Game1.backgroundTexture, GamerTag, Game1.Player.score, roundTime, Game1.Player.playerkills));
                Game1.sprites.Clear();
                Game1.BasicEnemys.Clear();
            }
            else
            {
                MediaPlayer.Volume = 4f;
                MediaPlayer.IsRepeating = true;
                MediaPlayer_MediaStateChanged();


                Game1.Layer.Update(Game1);

                foreach (var sprite in Game1.sprites)
                {
                    sprite.Update(gameTime, Game1);
                }
                for (int i = 0; i < Game1.sprites.Count; i++)
                {
                    if (Game1.sprites[i].Dead)
                    {
                        if (Game1.sprites[i] is BaseEnemy)
                        {
                            Game1.Player.score += 100;
                            Game1.Player.playerkills += 1;
                        }
                        Game1.sprites.RemoveAt(i);
                       
                    }
                }
                for (int i = 0; i < Game1.BasicEnemys.Count; i++)
                {
                    if (Game1.BasicEnemys[i].Dead)
                    {
                        Game1.BasicEnemys.RemoveAt(i);
                    }
                }
                Game1.Camera.Update(new Vector2(GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight), Game1);
                Game1.spawnBoosters.Update(Game1);
              
            }
        }
        private void MediaPlayer_MediaStateChanged()
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                PlayNextSong();
            }
        }
        private void PlayNextSong()
        {
            if (playlist.Count > 0)
            {
                MediaPlayer.Play(playlist[currentSongIndex]);
                currentSongIndex = (currentSongIndex + 1) % playlist.Count;
            }
        }
        public override void Draw(SpriteBatch _spriteBatch, GameTime gameTime)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Game1.Camera.Transform);

           

            for (int i = 0; i <= Game1.IsometricTileMap.Layers.Count; i++)
            {
                Game1.Layer.Draw(_spriteBatch, Game1, i);
            }
            for(int i = 0; i < Game1.BasicEnemys.Count; i++)
            {
                DrawHollowRectangle(_spriteBatch, new Rectangle(Game1.BasicEnemys[i].Hitbox.X, Game1.BasicEnemys[i].Hitbox.Y, Game1.BasicEnemys[i].Hitbox.Width, Game1.BasicEnemys[i].Hitbox.Height), Color.Red, _pixel);
                DrawHollowRectangle(_spriteBatch, Game1.BasicEnemys[i].attackRect, Color.Red, _pixel);
            }
            DrawHollowRectangle(_spriteBatch, Game1.Player.attackRect, Color.Red, _pixel);
            DrawHollowRectangle(_spriteBatch, Game1.Player.blockRect, Color.Red, _pixel);


            
            Game1.spawnBoosters.Draw(_spriteBatch);
            Game1.Player._healthBarAnimated.DrawBack(_spriteBatch, Game1.Player);
            Game1.Player._healthBarAnimated.Draw(_spriteBatch);
            Game1.Player._blockBarAnimated.Draw(_spriteBatch);
            Game1.Player._powerBarAnimated.Draw(_spriteBatch);
            foreach(var sprite in Game1.sprites)
            {
                if(sprite is BaseEnemy)
                {
                    BaseEnemy enemy = (BaseEnemy)sprite;
                    enemy._healthBarAnimated.Draw(_spriteBatch);
                    DrawHollowRectangle(_spriteBatch, enemy.terrorradius, Color.Red, _pixel);
                    DrawHollowRectangle(_spriteBatch, enemy.terrorradiusfollow, Color.Red, _pixel);
                }
            }
            foreach (PowerProjectileAttack bullet in Game1.powerProjectiles)
            {
                DrawHollowRectangle(_spriteBatch, bullet.Hitbox, Color.Red, _pixel);
            }
            foreach (PowerProjectileAttack powerProjectile in Game1.powerProjectiles)
            {
                powerProjectile.Draw(_spriteBatch);
            }
            _spriteBatch.DrawString(Game1.scoretext, $"{Game1.Player.score}", new(Game1.Player.Position.X - Game1._graphics.PreferredBackBufferWidth / 2 +70, Game1.Player.Position.Y - Game1._graphics.PreferredBackBufferHeight / 2 +115), Color.White);
            _spriteBatch.End();
        }
        private void DrawHollowRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, Texture2D pixelTexture)
        {
            // Top line
            //spriteBatch.Draw(pixelTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, 1), color);
            // Left line
           // spriteBatch.Draw(pixelTexture, new Rectangle(rectangle.Left, rectangle.Top, 1, rectangle.Height), color);
            // Right line
          //  spriteBatch.Draw(pixelTexture, new Rectangle(rectangle.Right - 1, rectangle.Top, 1, rectangle.Height), color);
            // Bottom line
           // spriteBatch.Draw(pixelTexture, new Rectangle(rectangle.Left, rectangle.Bottom - 1, rectangle.Width, 1), color);
        }
    }

}
