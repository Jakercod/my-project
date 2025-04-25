using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace GameV10.GameStates.Menus
{
    internal class Leaderboard : State
    {
        public static List<Score> Scores = new List<Score>();
        //set the filename, as there is no path, this will default to the debug folder.
        private static string MyFileName = "scoreboard.txt";
        //the high scores will be loaded into this list.
        private string PName;
        private int PScore;
        private SpriteFont buttonFont;
        private List<Button> _components;
        Vector2 screencentre;
        Texture2D backgroundTexture;
        Vector2 backgroundPosition;
        float backgroundSpeed = 1f;
        float backgroundScale;
        public Leaderboard(Game1 game1, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, GraphicsDevice graphicsDevice, Vector2 backgroundposition, Texture2D backgroundtexture, float backgroundspeed, float backgrooundscale): base(game1, graphicsDeviceManager, content, graphicsDevice)
        {
            backgroundTexture = backgroundtexture;
            backgroundPosition = backgroundposition;
            backgroundSpeed = backgroundspeed;
            backgroundScale = backgrooundscale;
            Leaderboard.LoadScoreBoard();
            var buttonTexture = content.Load<Texture2D>("Button");
            buttonFont = content.Load<SpriteFont>("File");

            screencentre = new(game1._graphics.PreferredBackBufferWidth / 2 + buttonTexture.Width / 2, game1._graphics.PreferredBackBufferHeight / 2 + buttonTexture.Height / 2);

            var MainMenuBtn = new Button(buttonTexture, buttonFont)
            {
                //Creates a new button object with a texture and font and then assigns it a position (for the texture to be placed) and a string (to go inside the button texture and using the button font)
                Position = new Vector2(0, 0),
                Text = "< Main Menu",
            };
            MainMenuBtn.Click += MainMenuBtn_Click;
            
            _components = new List<Button>()
            {
                //adds each button object to a list of components
                MainMenuBtn,
            };
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
        }
        
        public static void LoadScoreBoard()
        {
            Scores.Clear();
            string line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(MyFileName);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    string[] record = line.Split(',');
                    string PName = record[0];
                    int PScore = int.Parse(record[1]);
                    float PStime = float.Parse(record[2]);
                    int PKills = int.Parse(record[3]);
                    Scores.Add(new Score(PName, PScore, PStime, PKills));

                    //write the line to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                    //close the file
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            int NoOfRecords = Scores.Count;

            //make the records up to 10 if there aren't 10 records already.
            while (NoOfRecords < 10)
            {
                Scores.Add(new Score("Empty", 0, 0, 0));
                NoOfRecords = Scores.Count;
            }
            SortBoard();


        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gametime)
        {
            
            spriteBatch.Begin();
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
            int Y = (int)screencentre.Y - 200 ;
            int i = 0;
            foreach (var record in Scores)
            {
                i++;
                spriteBatch.DrawString(buttonFont, $"{i}.", new Vector2(screencentre.X - 220, Y), Color.Black);
                spriteBatch.DrawString(buttonFont, record.PlayerName, new Vector2(screencentre.X - 200, Y), Color.Black);
                spriteBatch.DrawString(buttonFont, record.PlayerScore.ToString(), new Vector2(screencentre.X - 100, Y), Color.Black);
                spriteBatch.DrawString(buttonFont, ((int)record.Survivaltime).ToString(), new Vector2(screencentre.X, Y), Color.Black);
                spriteBatch.DrawString(buttonFont, record.Kills.ToString(), new Vector2(screencentre.X + 100, Y), Color.Black);
                Y += 40;//move down the screen for the next record.
            }
            foreach (var component in _components)
            {
                component.Draw(gametime, spriteBatch);
            }
            spriteBatch.End();
        }

        public static void AddNewScore(string PlayerName, int PlayerScore, float PlayerStime, int PlayerKills)
        {
            Scores.Add(new Score(PlayerName, PlayerScore, PlayerStime, PlayerKills));
            SortBoard();
            //as a new score has now been added, there will be 11 in the sorted list. Remove the last one, which has the lowest score.
            if(Scores.Count > 10)
            {
                Scores.RemoveRange(10, Scores.Count - 10);
            }
            
            SaveToTextFile();
            LoadScoreBoard();

        }
        public static void SortBoard()
        {
            // this loops through the Scores List - comparing one record with the next and sorting the record based on the Player's score
            Scores.Sort((OneRecord, NextRecord) => OneRecord.PlayerScore.CompareTo(NextRecord.PlayerScore));
            Scores.Reverse();  //change to largest score first.
        }

        public static void SaveToTextFile()
        {
            //create a new streamwriter object to write to the high scores file. The 2nd parameter means do not append - i.e replace all the data that is there.
            using (StreamWriter LBFile = new StreamWriter(MyFileName, false))
                foreach (var record in Scores)
                {
                    string tempPlayerName = record.PlayerName;
                    int tempPlayerScore = record.PlayerScore;
                    float tempPlayerStime = record.Survivaltime;
                    int tempPlayerKills = record.Kills;
                    Score tempScore = new Score(tempPlayerName, tempPlayerScore, tempPlayerStime, tempPlayerKills);
                    LBFile.WriteLine(tempScore.WriteScoreRecord());
                }
        }
        private void MainMenuBtn_Click(object sender, EventArgs e)
        {
            //object sender is the object button for the specific component (if occurs within this method then the object is the newGameButton
            //implaments the change state method from game one creating a new scene with new properties
            Game1.ChangeState(new MainMenu(Game1, GraphicsDeviceManager, Content, GraphicsDevice, backgroundPosition, backgroundTexture, backgroundSpeed, backgroundScale));
        }
    }

}

