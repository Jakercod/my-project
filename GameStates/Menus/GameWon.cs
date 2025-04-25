using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GameV10.GameStates.Menus
{
    internal class GameWon : State
    {
        public static List<Score> Scores = new List<Score>();
        //set the filename, as there is no path, this will default to the debug folder.
        private static string MyFileName = "scoreboard.txt";
        //the high scores will be loaded into this list.
        private string PName;
        private int PScore;
        private float PStime;
        private int PKills;
        private SpriteFont buttonFont;
        private List<Button> _components;
        Vector2 screencentre;
        Texture2D backgroundTexture;
        Texture2D BackgroundTextureMM;
        Vector2 backgroundPosition;
        float backgroundSpeed = 1f;
        float backgroundScale;
        public GameWon(Game1 game1, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, GraphicsDevice graphicsDevice, Texture2D backgroundTextureMM, string PlayerName, int PlayerScore, float PlayerStime, int PlayerKills) : base(game1, graphicsDeviceManager, content, graphicsDevice)
        {
            PName = PlayerName;
            PScore = PlayerScore;
            PStime = PlayerStime;
            PKills = PlayerKills;

            BackgroundTextureMM = backgroundTextureMM;
            backgroundTexture = content.Load<Texture2D>("GameWon");
            var buttonTexture = content.Load<Texture2D>("Button");
            buttonFont = content.Load<SpriteFont>("File");

            screencentre = new(game1._graphics.PreferredBackBufferWidth / 2 + buttonTexture.Width / 2, game1._graphics.PreferredBackBufferHeight / 2 + buttonTexture.Height / 2);
            backgroundScale = (float)GraphicsDevice.Viewport.Height / backgroundTexture.Height;
            var MainMenuBtn = new Button(buttonTexture, buttonFont)
            {
                //Creates a new button object with a texture and font and then assigns it a position (for the texture to be placed) and a string (to go inside the button texture and using the button font)
                Position = new Vector2(screencentre.X - buttonTexture.Width / 2, screencentre.Y - buttonTexture.Height / 2),
                Text = "Main Menu",
            };
            MainMenuBtn.Click += MainMenuBtn_Click;

            _components = new List<Button>()
            {
                //adds each button object to a list of components
                MainMenuBtn,
            };
            game1.GameWonIN.Play();
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, Vector2.Zero, null, Color.White, 0f, new Vector2(0, 0), backgroundScale, SpriteEffects.None, 0f);
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.DrawString(buttonFont, "Name", new Vector2(screencentre.X - 200, 900), Color.Red);
            spriteBatch.DrawString(buttonFont, "Score", new Vector2(screencentre.X - 100, 900), Color.Red);
            spriteBatch.DrawString(buttonFont, "Time", new Vector2(screencentre.X, 900), Color.Red);
            spriteBatch.DrawString(buttonFont, "Kills", new Vector2(screencentre.X + 100, 900), Color.Red);

            spriteBatch.DrawString(buttonFont, PName, new Vector2(screencentre.X - 200, 1000), Color.Red);
            spriteBatch.DrawString(buttonFont, PScore.ToString(), new Vector2(screencentre.X - 100, 1000), Color.Red);
            spriteBatch.DrawString(buttonFont, ((int)PStime).ToString(), new Vector2(screencentre.X, 1000), Color.Red);
            spriteBatch.DrawString(buttonFont, PKills.ToString(), new Vector2(screencentre.X + 100, 1000), Color.Red);
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            //updates all the components checking if the mouse is hovering or clicking whilst hovering
            
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }
        private void MainMenuBtn_Click(object sender, EventArgs e)
        {
            //object sender is the object button for the specific component (if occurs within this method then the object is the newGameButton
            //implaments the change state method from game one creating a new scene with new properties
            Game1.ChangeState(new MainMenu(Game1, GraphicsDeviceManager, Content, GraphicsDevice, backgroundPosition, BackgroundTextureMM, backgroundSpeed, backgroundScale));
            Game1.GameWonIN.Stop();
        }
    }
}
