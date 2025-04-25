namespace GameV10
{
    internal class StartPathfinding
    {
        private int tilesize = 120;
        private WorldGrid _world;
        private CreateGridTiles _tiles = new();
        Point[] path;
        public Point[] Pathfind(Game1 game1, Vector2 enemypos, Vector2 playerpos, HeuristicFormula formula)
        {
            float Ptileposy = (tilesize / 2 * playerpos.Y - playerpos.X * (tilesize / 4)) / (((tilesize / 2) * (tilesize / 4)) - (-(tilesize / 2) * (tilesize / 4)));
            float Ptileposx = (playerpos.X / (tilesize / 2)) + Ptileposy;
            var pathfinderOptions = new PathFinderOptions
            {
                PunishChangeDirection = true,
                UseDiagonals = true,
                SearchLimit = 100,
                HeuristicFormula = formula,
                //Weighting = Weighting.Positive,
            };
            //program to convert current level to this format
            var level = _tiles.ConvertVectorsToString(game1.IsometricTileMap.Layers[game1.Layer.GetCurrentLayer.GetHashCode() + 1], 100, 100);    

            _world = Helper.ConvertStringToPathfinderGrid(level);

            var pathfinder = new PathFinder(_world, pathfinderOptions);

            // point indexing
            try
            {
                path = pathfinder.FindPath(new Point((int)enemypos.X, (int)enemypos.Y), new Point((int)Ptileposx, (int)Ptileposy));

                if (path.Length == 0)
                {
                    try
                    {
                        path = pathfinder.FindPath(new Point((int)enemypos.X, (int)enemypos.Y), new Point((int)Ptileposx + 1, (int)Ptileposy + 1));
                        
                        if (path.Length == 0)
                        {
                            try
                            {
                                path = pathfinder.FindPath(new Point((int)enemypos.X, (int)enemypos.Y), new Point((int)Ptileposx - 1, (int)Ptileposy - 1));

                                if (path.Length == 0)
                                {
                                    try
                                    {
                                        path = pathfinder.FindPath(new Point((int)enemypos.X, (int)enemypos.Y), new Point((int)Ptileposx + 1, (int)Ptileposy - 1));

                                        if (path.Length == 0)
                                        {
                                            try
                                            {
                                                path = pathfinder.FindPath(new Point((int)enemypos.X, (int)enemypos.Y), new Point((int)Ptileposx - 1, (int)Ptileposy + 1));
                                            }
                                            catch
                                            {
                                                path = null;
                                            }
                                        }

                                    }
                                    catch
                                    {
                                        path = null;
                                    }
                                }
                            }
                            catch
                            {
                                path = null;


                            }
                        }
                    }
                    catch
                    {
                        path = null;
                    }
                }
            }
            catch
            {
                path = null;


            }

            return path;
        }
    }
}
//think error is to do with isometric coordinates and the fact that the tilemap coordinates start at 1 not zero