namespace GameV10.World
{
    internal class IsometricTileMap
    {
        //List of each layer in the world
        public List<Dictionary<Vector2, int>> Layers = new() { };

        //Layer is stored as a dicctionary that uses a key (vecctor) so locate and int which represents different textures
        public Dictionary<Vector2, int> MapLayer1 { get; set; } 
        public Dictionary<Vector2, int> MapLayer2 { get; set ; }

        Dictionary<Vector2, int> currentlayer = new();

        private Texture2D TextureAtlas;
        private int tilesize = 64;
        public IsometricTileMap(Texture2D textureAtlas, string layer1FP, string layer2FP)
        {
            TextureAtlas = textureAtlas;
            MapLayer1 = LoadTilemap(layer1FP);
            Layers.Add(MapLayer1);
            MapLayer2 = LoadTilemap(layer2FP);
            Layers.Add(MapLayer2);
        }

        private Dictionary<Vector2, int> LoadTilemap(string filepath)
        {
            Dictionary<Vector2, int> result = new();

            //Method of reading file paths characters, lines, sections at a time
            StreamReader reader = new(filepath);

            int x = 0;
            int y = 0;
            string line;

            //reads the first line of the file and runs if it is not empty
            while ( (line = reader.ReadLine()) != null)
            {
                //removes all the commas between the numbers in the CSV file 
                string[] items = line.Split(',');

                //x acts as the cloumn position so the code checks that it hasnt reached the last column
                while (x < items.Length)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        //if the integer in the column x is not -1 (air block) 
                        if (value > -1)
                        {
                            //add the x and y coordinates as a key to the integer value that represents a texture
                            result[new Vector2(x, y)] = value;

                        }
                    }
                    x++;
                }
                x = 0;
                y++;

            }
            return result;
        }
        public void DrawTilemap(Game1 game1, SpriteBatch spritebatch, Vector2 position, int layer)
        {
            
            currentlayer = Layers[layer];
            float tileposy = (tilesize / 2 * position.Y - position.X * (tilesize / 4)) / (tilesize / 2 * (tilesize / 4) - (-(tilesize / 2) * (tilesize / 4)));
            float tileposx = position.X / (tilesize / 2) + tileposy;
            Vector2 tilepos = new Vector2((int)tileposx, (int)tileposy);
            bool found = currentlayer.TryGetValue(tilepos, out int value); //value isnt correctly working resulting in wrong textures
            if (value != 1)
            {
                
            }
            //determines texture 
            int xsrc = value % 2;
            int ysrc = value / 2;

            //creates sources rectangle to locate texture within the texture atlas
            Rectangle sourceRectangle = new(
                xsrc * tilesize,
                ysrc * tilesize,
            tilesize,
            tilesize);

            spritebatch.Draw(
                TextureAtlas,
                position,
                sourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                2f,
                SpriteEffects.None,
                0f);
        }
    }
}
