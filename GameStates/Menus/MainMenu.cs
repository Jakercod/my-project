using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GameV10.GameStates.Menus
{
    internal class MainMenu : State
    {
        private List<Button> _components;
        Texture2D backgroundTexture;
        Vector2 backgroundPosition;
        Vector2 backgroundPosition2;
        float backgroundSpeed = 1f;
        Vector2 screenSize;
        private Song song1;
        float backgroundScale;
        private List<Song> playlist = new();
        int currentSongIndex;
        SpriteFont font;
        
        public MainMenu(Game1 game1, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, GraphicsDevice graphicsDevice, Vector2 backgroundposition, Texture2D backgroundtexture, float backgroundspeed, float backgroundscale) : base(game1, graphicsDeviceManager, content, graphicsDevice)
        {
            backgroundTexture = backgroundtexture;
            backgroundPosition = backgroundposition;
            backgroundSpeed = backgroundspeed;
            backgroundScale = backgroundscale;
            var buttonTexture = content.Load<Texture2D>("Button");
            var butonFont = content.Load<SpriteFont>("File");
            font = content.Load<SpriteFont>("File");

            Vector2 screencentre = new(game1._graphics.PreferredBackBufferWidth / 2 + buttonTexture.Width/2, game1._graphics.PreferredBackBufferHeight / 2 + buttonTexture.Height / 2);

            var newGameButton = new Button(buttonTexture, butonFont)
            {
                //Creates a new button object with a texture and font and then assigns it a position (for the texture to be placed) and a string (to go inside the button texture and using the button font)
                Position = new Vector2(screencentre.X, screencentre.Y - 200),
                Text = "New Game",
            };
            newGameButton.Click += NewGameButton_Click;
            //"Click?.Invoke(this, new EventArgs());" if an increment has invoked then a click has occured under
            //"this" specifing the current component which then sends the code to a method relating to that components properties
            var leaderboardButton = new Button(buttonTexture, butonFont)
            {
                Position = new Vector2(screencentre.X, screencentre.Y),
                Text = "LeaderBoard",
            };
            leaderboardButton.Click += leaderboardButton_Click;
            var quitGameButton = new Button(buttonTexture, butonFont)
            {
                Position = new Vector2(screencentre.X, screencentre.Y + 200),
                Text = "Quit Game",
            };
            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Button>()
            {
                //adds each button object to a list of components
                newGameButton,
                leaderboardButton,
                quitGameButton
            };
            
            song1 = content.Load<Song>("medievalMM");
            playlist.Add(song1);
            
        }
        public override void Update(GameTime gameTime)
        {
            
            //updates all the components checking if the mouse is hovering or clicking whilst hovering

            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
            // Move the background position
            backgroundPosition.X -= backgroundSpeed;

            // Reset position when it goes off screen
            if (backgroundPosition.X <= -backgroundTexture.Width * backgroundScale)
            {
                backgroundPosition.X = 0;
            }
            MediaPlayer.Volume = 3f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer_MediaStateChanged();
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
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();

            //draws all the components that have been added to the list with their corresponding texture, font, colour, string and position
            // Draw the first part of the background
            spriteBatch.Draw(backgroundTexture, backgroundPosition, null, Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);

            // Draw the second part of the background to create a seamless loop
            if (backgroundPosition.X <= -GraphicsDevice.Viewport.Width)
            {
                spriteBatch.Draw(backgroundTexture, new Vector2(backgroundPosition.X + backgroundTexture.Width * backgroundScale, 0), null, Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(backgroundTexture, new Vector2(backgroundPosition.X + backgroundTexture.Width * backgroundScale, 0), null, Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);
            }

            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            
            spriteBatch.End();
        }
        public void DrawBack(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundTexture, backgroundPosition, null, Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);

            // Draw the second part of the background to create a seamless loop
            if (backgroundPosition.X <= -GraphicsDevice.Viewport.Width)
            {
                spriteBatch.Draw(backgroundTexture, new Vector2(backgroundPosition.X + backgroundTexture.Width * backgroundScale, 0), null, Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(backgroundTexture, new Vector2(backgroundPosition.X + backgroundTexture.Width * backgroundScale, 0), null, Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);
            }
            
        }
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            //object sender is the object button for the specific component (if occurs within this method then the object is the newGameButton
            //implaments the change state method from game one creating a new scene with new properties
            Game1.ChangeState(new GameSelector(Game1, GraphicsDeviceManager, Content, GraphicsDevice, backgroundPosition, backgroundTexture, backgroundSpeed, backgroundScale));
        }
        private void leaderboardButton_Click(object sender, EventArgs e)
        {
            //implaments the change state method from game one creating a new scene with new properties
            Game1.ChangeState(new Leaderboard(Game1, GraphicsDeviceManager, Content, GraphicsDevice, backgroundPosition, backgroundTexture, backgroundSpeed, backgroundScale));
        }
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            //Exits Game1 closing down the program
            Game1.Exit();
        }
    }
}
