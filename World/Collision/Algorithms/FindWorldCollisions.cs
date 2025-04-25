namespace GameV10.World.Collision.Algorithms
{
    internal class FindWorldCollisions
    {
        //size of the standard world map tiles
        private int tilesize = 128;
        //list of collision Vectors that map the top left position of the block
        public List<Vector2> bases = new() { };
        public List<Vector2> tops = new() { };
        public void CheckBounds(SpriteBase sprite, Game1 game1, Rectangle bounds)
        {
            //resets possible collisions stopping stack overflow error
            bases.Clear();
            tops.Clear();

            //All tile positions will be found in x: intergrals of 32 and y: integrals of 16
            //This is determined by a bound rectangle which meaning only collisions within the rectangle are active
            Vector2 startbound = new Vector2(bounds.X / (tilesize / 2) * (tilesize / 2), bounds.Y / (tilesize / 4) * (tilesize / 4));
            Vector2 endbound = new Vector2(startbound.X + bounds.Width, startbound.Y + bounds.Height);

            //iterates through coordinates using simultaneous equations to see if they match possible points of collision
            for (int x = (int)startbound.X; x < (int)endbound.X; x += tilesize / 2)
            {
                for (int y = (int)startbound.Y; y < (int)endbound.Y; y += tilesize / 4)
                {
                    //simultaneous equation to find possible coordinate matches
                    float tileposy = (tilesize / 2 * y - x * (tilesize / 4)) / (tilesize / 2 * (tilesize / 4) - (-(tilesize / 2) * (tilesize / 4)));
                    float tileposx = x / (tilesize / 2) + tileposy;
                    Vector2 tilepos = new Vector2(tileposx, tileposy);
                    Vector2 boundpos = new Vector2(x, y);

                    bool validbound = CheckValidBound(tilepos, boundpos);

                    //checks the layer above current layer and finds the bottom of each block
                    if (game1.Layer.Bases != null && game1.Layer.Bases.TryGetValue(tilepos, out var _) && validbound)
                    {
                        bases.Add(tilepos);
                        sprite.Bases.Add(tilepos);
                    }
                    //checks the layer above current layer and finds the top of each block
                    else if (game1.Layer.Tops != null && game1.Layer.Tops.TryGetValue(tilepos, out var _) && validbound)
                    {
                        tops.Add(tilepos);
                    }
                }
            }
        }
        public bool CheckValidBound(Vector2 tilepos, Vector2 boundpos)
        {
            //due to the simultaneous equations finding multiple solutions not every coordinate pair that creates the correct position are correct this method checks whether they are
            Vector2 Xbound = new Vector2(tilepos.X * (tilesize / 2), tilepos.X * (tilesize / 4));
            Vector2 Ybound = new Vector2(tilepos.Y * -(tilesize / 2), tilepos.Y * (tilesize / 4));
            Vector2 vailidBound = new Vector2(Xbound.X + Ybound.X, Xbound.Y + Ybound.Y);

            if (vailidBound == boundpos)
            {
                return true;
            }
            return false;
        }
    }
}
