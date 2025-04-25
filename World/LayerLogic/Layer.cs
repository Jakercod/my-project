using System.Linq;

namespace GameV10.World.LayerLogic
{
    internal class Layer
    {
        public Zsorting Zsorting = new();
        Dictionary<Vector2, string> zsort = new();
        public Dictionary<Vector2, int> Bases { get; set; }
        public Dictionary<Vector2, int> Tops { get; set; }
        public Layers GetCurrentLayer { get; set; }
        private int tilesize = 64;
        private int x;
        public bool drawP;
        public bool drawE;
        public enum Layers
        {
            layer1,
            layer2,
            layer3
        }
        public void NextLayer()
        {
            GetCurrentLayer += 1;
        }
        public void PrevLayer()
        {
            GetCurrentLayer -= 1;
        }
        public void Update(Game1 game1)
        {
            zsort.Clear();
            try
            {
                Bases = game1.IsometricTileMap.Layers[GetCurrentLayer.GetHashCode() + 1];
            }
            catch
            {
                Bases = null;
            }
            try
            {
                Tops = game1.IsometricTileMap.Layers[GetCurrentLayer.GetHashCode()];
            }
            catch
            {
                Tops = null;
            }
            if (Bases != null)
            {
                zsort = Zsorting.Zsort(Bases, game1);
            }
            x = 0;
            drawE = false;
            drawP = false;
        }
        public void Draw(SpriteBatch spriteBatch, Game1 game1, int layernum)
        {
            if (layernum == GetCurrentLayer.GetHashCode() + 1)
            {
                try
                {
                    foreach (KeyValuePair<Vector2, int> kvp in game1.IsometricTileMap.Layers[layernum])
                    {
                        Vector2 vector = new Vector2(
                           (int)kvp.Key.X,
                           (int)kvp.Key.Y);

                        //Turns the cartesian coordinates to isometric ones
                        Vector2 position = new Vector2(
                            (vector.X - vector.Y) * tilesize,
                            (vector.X + vector.Y) * tilesize / 2

                            );
                        if (zsort.TryGetValue(position, out _))
                        {
                            //find the kvp draw it then check if the next one is player 
                            KeyValuePair<Vector2, string> Kvp1 = zsort.ElementAt(x);

                            if (Kvp1.Value == "Player")
                            {
                                drawP = true;
                                game1.Player.Draw(spriteBatch, game1, game1.Player);
                                x++;
                                Kvp1 = zsort.ElementAt(x);
                                if (Kvp1.Value == "Enemy")
                                {
                                    drawE = true;
                                    game1.BasicEnemys[0].Draw(spriteBatch, game1, game1.BasicEnemys[0]);
                                    x++;
                                    Kvp1 = zsort.ElementAt(x);
                                    if (Kvp1.Value == "Block")
                                    {
                                        game1.IsometricTileMap.DrawTilemap(game1, spriteBatch, Kvp1.Key, layernum);

                                    }
                                }
                                else
                                {
                                    game1.IsometricTileMap.DrawTilemap(game1, spriteBatch, Kvp1.Key, layernum);
                                }
                            }
                            else if (Kvp1.Value == "Enemy")
                            {
                                drawE = true;
                                foreach (var sprite in game1.sprites)
                                {
                                    sprite.Draw(spriteBatch, game1, sprite);
                                }
                                x++;
                                Kvp1 = zsort.ElementAt(x);
                                if (Kvp1.Value == "Player")
                                {
                                    drawE = true;
                                    game1.Player.Draw(spriteBatch, game1, game1.Player);
                                    x++;
                                    Kvp1 = zsort.ElementAt(x);
                                    if (Kvp1.Value == "Block")
                                    {
                                        game1.IsometricTileMap.DrawTilemap(game1, spriteBatch, Kvp1.Key, layernum);

                                    }
                                }
                                else
                                {
                                    game1.IsometricTileMap.DrawTilemap(game1, spriteBatch, Kvp1.Key, layernum);
                                }
                            }
                            else if (Kvp1.Value == "Block")
                            {
                                game1.IsometricTileMap.DrawTilemap(game1, spriteBatch, Kvp1.Key, layernum);

                            }
                            x++;
                        }
                        else
                        {
                            if (!zsort.TryGetValue(position, out _))
                            {
                                game1.IsometricTileMap.DrawTilemap(game1, spriteBatch, position, layernum);
                            }

                        }

                    }
                    if (!drawE && !drawP)
                    {
                        foreach (KeyValuePair<Vector2, string> Kvp in zsort)
                        {
                            if (Kvp.Value == "Player")
                            {
                                game1.Player.Draw(spriteBatch, game1, game1.Player);
                            }
                            foreach (var sprite in game1.sprites)
                            {
                                sprite.Draw(spriteBatch, game1, sprite);
                            }
                        }
                    }
                    else if (!drawE)
                    {
                        foreach(var enemy in game1.BasicEnemys)
                        {
                            enemy.Draw(spriteBatch, game1, enemy);
                        }
                        
                    }
                    else if (!drawP)
                    {
                        game1.Player.Draw(spriteBatch, game1, game1.Player);
                    }

                }
                catch
                {
                    foreach (var sprite in game1.sprites)
                    {
                        sprite.Draw(spriteBatch, game1, sprite);
                    }
                }

            }
            else
            {
                try
                {
                    foreach (KeyValuePair<Vector2, int> kvp in game1.IsometricTileMap.Layers[layernum])
                    {
                        Vector2 vector = new Vector2(
                            (int)kvp.Key.X,
                            (int)kvp.Key.Y);

                        //Turns the cartesian coordinates to isometric ones
                        Vector2 position = new Vector2(
                            (vector.X - vector.Y) * tilesize,
                            (vector.X + vector.Y) * tilesize / 2

                            );
                        game1.IsometricTileMap.DrawTilemap(game1, spriteBatch, position, layernum);
                    }
                }
                catch
                {

                }

            }
        }
    }

}