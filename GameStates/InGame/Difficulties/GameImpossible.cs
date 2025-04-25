using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV10.GameStates.InGame.Difficulties
{
    internal class GameImpossible : TestGame
    {
        int rampupcount =1;
        int rampuphealth;
        public int rampupdamage;
        Random random = new();
        public GameImpossible(Game1 game1, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, GraphicsDevice graphicsDevice, string gamertag) : base(game1, graphicsDeviceManager, content, graphicsDevice)
        {
            GamerTag = gamertag;
            // Constructor logic for GameEasy
            song1 = content.Load<Song>("song1");
            playlist.Add(song1);
            Game1.Player = new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(500, 1000), new Vector2(0, 0), new Vector2(64, 128), 1, 1000, healthBar, healthBarBG, blockBar, blockBarBG, powerBar, powerBarBG, HUDBack, powerattacktex);
            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(500, 500), new Vector2(0, 0), new Vector2(64, 128), 1, 2000, HeuristicFormula.Manhattan, healthBar, healthBar));

            Game1.spawnBoosters = new(healthboostertexture);
            foreach (var enemy in Game1.BasicEnemys)
            {
                Game1.sprites.Add(enemy);
            }
            Game1.sprites.Add(Game1.Player);


            Game1.IsometricTileMap = new IsometricTileMap(
                textureAtlas,
                "../../../TileMaps/_Tile Layer 1.csv",
                "../../../TileMaps/_Tile Layer 2.csv"
            );

        }
        public override void Update(GameTime gameTime)
        {
            Game1.InputManager.Update(Game1);
            if (Game1.BasicEnemys.Count == 0)
            {
                rampuphealth += 200;
                rampupcount++;
                Game1.Player.damagerampup += 50;
                if(Game1.Player.damagerampup >= 500)
                {
                    Game1.Player.damagerampup = 500;
                }
                for (int i = 0; i < rampupcount; i++)
                {
                    Game1.SpawnIN.Play();
                    switch (random.Next(1, 8))
                    {
                        case 0:
                            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(500, 500), new Vector2(0, 0), new Vector2(64, 128), 1 , 2000 + rampuphealth, HeuristicFormula.Manhattan, healthBar, healthBar));
                            break;
                        case 1:
                            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(200, 400), new Vector2(0, 0), new Vector2(64, 128), 1 , 2000 + rampuphealth, HeuristicFormula.Manhattan, healthBar, healthBar));
                            break;
                        case 2:
                            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(800, 700), new Vector2(0, 0), new Vector2(64, 128), 1, 2000 + rampuphealth, HeuristicFormula.Manhattan, healthBar, healthBar));
                            break;
                        case 3:
                            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(200, 900), new Vector2(0, 0), new Vector2(64, 128), 1, 2000 + rampuphealth, HeuristicFormula.Manhattan, healthBar, healthBar));
                            break;
                        case 4:
                            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(400, 1200), new Vector2(0, 0), new Vector2(64, 128), 1, 2000 + rampuphealth, HeuristicFormula.Manhattan, healthBar, healthBar));
                            break;
                        case 5:
                            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(600, 900), new Vector2(0, 0), new Vector2(64, 128), 1, 2000 + rampuphealth, HeuristicFormula.Manhattan, healthBar, healthBar));
                            break;
                        case 6:
                            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(400, 800), new Vector2(0, 0), new Vector2(64, 128), 1, 2000 + rampuphealth, HeuristicFormula.Manhattan, healthBar, healthBar));
                            break;
                        case 7:
                            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(1000, 900), new Vector2(0, 0), new Vector2(64, 128), 1, 2000 + rampuphealth, HeuristicFormula.Manhattan, healthBar, healthBar));
                            break;
                    }
                    Game1.sprites.Add(Game1.BasicEnemys[i]);
                }
            }
            // Update logic for GameEasy
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw logic for GameEasy
            base.Draw(spriteBatch, gameTime);
        }
    }
}
