using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV10.GameStates.InGame.Difficulties
{
    internal class GameHard : TestGame
    {
        public GameHard(Game1 game1, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, GraphicsDevice graphicsDevice, string gamertag) : base(game1, graphicsDeviceManager, content, graphicsDevice)
        {
            GamerTag = gamertag;
            // Constructor logic for GameEasy
            song1 = content.Load<Song>("song1");
            playlist.Add(song1);
            Game1.Player = new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(500, 1000), new Vector2(0, 0), new Vector2(64, 128), 1, 500, healthBar, healthBarBG, blockBar, blockBarBG, powerBar, powerBarBG, HUDBack, powerattacktex);
            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(500, 500), new Vector2(0, 0), new Vector2(64, 128), 1, 2000, HeuristicFormula.Manhattan, healthBar, healthBar));
            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(200, 400), new Vector2(0, 0), new Vector2(64, 128), 1, 2000, HeuristicFormula.Manhattan, healthBar, healthBar));
            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(800, 700), new Vector2(0, 0), new Vector2(64, 128), 1, 2000, HeuristicFormula.Manhattan, healthBar, healthBar));
            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(200, 900), new Vector2(0, 0), new Vector2(64, 128), 1, 2000, HeuristicFormula.Manhattan, healthBar, healthBar));
            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(400, 1200), new Vector2(0, 0), new Vector2(64, 128), 1, 2000, HeuristicFormula.Manhattan, healthBar, healthBar));
            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(200, 900), new Vector2(0, 0), new Vector2(64, 128), 1, 2000, HeuristicFormula.Manhattan, healthBar, healthBar));
            Game1.BasicEnemys.Add(new(playertextureNorth, playertextureSouth, playertextureWest, playertextureNorthWest, playertextureSouthWest, new Vector2(400, 1200), new Vector2(0, 0), new Vector2(64, 128), 1, 2000, HeuristicFormula.Manhattan, healthBar, healthBar));
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
            // Update logic for GameEasy
            if (Game1.BasicEnemys.Count == 0)
            {
                Game1.GameWon = true;
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw logic for GameEasy
            base.Draw(spriteBatch, gameTime);
        }
    }
}
