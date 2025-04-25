namespace GameV10.World.LayerLogic
{
    internal class Zsorting
    {
        private int tilesize = 64;
        //sorting algorithm that orders from top left to bottom right the order of drawing of each block players and enemies
        public Dictionary<Vector2, string> Zsort(Dictionary<Vector2, int> LayerAbove, Game1 game1)
        {
            Vector2 prevposPlayer = Vector2.Zero;
            Vector2 prevposEnemy = Vector2.Zero;
            Dictionary<Vector2, string> res = new()
            {

            };

            foreach (KeyValuePair<Vector2, int> KVP in LayerAbove)
            {
                Vector2 vector = KVP.Key;

                //Turns the cartesian coordinates to isometric ones
                Vector2 position = new Vector2(
                    (vector.X - vector.Y) * tilesize,
                    (vector.X + vector.Y) * tilesize / 2

                    );
                foreach (var sprite in game1.sprites)
                {
                    if (sprite.Bases != null)
                    {
                        foreach (Vector2 Base in sprite.Bases)
                        {
                            if (Base.X == vector.X && Base.Y == vector.Y)
                            {
                                string direction = CalculateDirection(new Vector2(position.X, position.Y + tilesize / 2), new Vector2(sprite.Position.X + sprite.Hitbox.Width / 2, sprite.Position.Y + sprite.Hitbox.Height / 2));

                                if (direction == "North" || direction == "North-East" || direction == "North-West")
                                {
                                    if (sprite.NAME == "Player")
                                    {
                                        res.Remove(prevposPlayer);
                                        try
                                        {
                                            res.Add(sprite.Position, "Player");
                                            if (!res.ContainsKey(position))
                                            {
                                                res.Add(position, "Block");

                                            }
                                        }
                                        catch
                                        {
                                            res.Add(new Vector2(sprite.Position.X + 1, sprite.Position.Y), "Player");
                                            if (!res.ContainsKey(position))
                                            {
                                                res.Add(position, "Block");

                                            }
                                        }
                                        prevposPlayer = sprite.Position;

                                    }
                                    else
                                    {
                                        res.Remove(prevposEnemy);
                                        try
                                        {
                                            res.Add(sprite.Position, "Enemy");
                                            if (!res.ContainsKey(position))
                                            {
                                                res.Add(position, "Block");

                                            }
                                        }
                                        catch
                                        {
                                            res.Add(new Vector2(sprite.Position.X + 1, sprite.Position.Y), "Enemy");
                                            if (!res.ContainsKey(position))
                                            {
                                                res.Add(position, "Block");

                                            }
                                        }
                                        prevposEnemy = sprite.Position;

                                    }
                                }

                            }
                        }
                    }

                }

            }
            if (!res.ContainsValue("Player") && !res.ContainsValue("Enemy"))
            {
                if (game1.Player.Position.Y < game1.BasicEnemys[0].Position.Y)
                {
                    try
                    {
                        res.Add(game1.Player.Position, "Player");
                    }
                    catch
                    {
                        res.Add(new Vector2(game1.Player.Position.X + 1, game1.Player.Position.Y), "Player");
                    }
                    try
                    {
                        res.Add(game1.BasicEnemys[0].Position, "Enemy");
                    }
                    catch
                    {
                        res.Add(new Vector2(game1.BasicEnemys[0].Position.X + 1, game1.BasicEnemys[0].Position.Y), "Enemy");
                    }
                }
                else
                {
                    try
                    {
                        res.Add(game1.BasicEnemys[0].Position, "Enemy");
                    }
                    catch
                    {
                        res.Add(new Vector2(game1.BasicEnemys[0].Position.X + 1, game1.BasicEnemys[0].Position.Y), "Enemy");
                    }
                    try
                    {
                        res.Add(game1.Player.Position, "Player");
                    }
                    catch
                    {
                        res.Add(new Vector2(game1.Player.Position.X + 1, game1.Player.Position.Y), "Player");
                    }
                }
            }
            else if (!res.ContainsValue("Player"))
            {
                try
                {
                    res.Add(game1.Player.Position, "Player");
                }
                catch
                {
                    res.Add(new Vector2(game1.Player.Position.X + 1, game1.Player.Position.Y), "Player");
                }
            }
            else if (!res.ContainsValue("Enemy"))
            {
                try
                {
                    res.Add(game1.BasicEnemys[0].Position, "Enemy");
                }
                catch
                {
                    res.Add(new Vector2(game1.BasicEnemys[0].Position.X + 1, game1.BasicEnemys[0].Position.Y), "Enemy");
                }
            }

            return res;
        }
        private string CalculateDirection(Vector2 pointB, Vector2 pointA)
        {
            Vector2 directionVector = pointB - pointA;
            float angle = MathHelper.ToDegrees((float)Math.Atan2(directionVector.Y, directionVector.X));

            if (angle >= 0 && angle < 90)
                return "North-East";
            else if (angle >= 90 && angle < 180)
                return "North-West";
            else if (angle <= -1 && angle > -90)
                return "South-West";
            else if (angle <= -90 && angle > -180)
                return "South-East";
            else
                return "";
        }
    }
}
