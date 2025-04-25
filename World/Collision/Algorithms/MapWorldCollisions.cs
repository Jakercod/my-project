using System.Reflection.PortableExecutable;

namespace GameV10.World.Collision.Algorithms
{
    internal class MapWorldCollisions
    {
        //List of coordinates that creates collision lines along the base of the block
        public List<CollisionBlock> collisionbases = new() { };
        //List of coordinates that creates collision lines along the top of a block
        public List<CollisionBlock> collisiontops = new() { };
        private float tilesize = 128;
        public void MapCollisions(List<Vector2> bases, List<Vector2> tops, Game1 game1)
        {
            //resets possible collisions stopping stack overflow error
            collisionbases.Clear();
            collisiontops.Clear();

            //uses an algorithm that finds out what adjacent blocks a block has and therefore enables to load the blocks correct collision face in response to it's environment this is iterated throughout every block within a certain range
            foreach (Vector2 b in bases)
            {
                //returns a list of adjacent points that will be used to find active collisions
                List<string> adjacentPoints = GetAdjacentDirections(game1.Layer.Bases, b, game1);
                CreateCollisionBlock(b, adjacentPoints, "bases", game1);
            }
            foreach (Vector2 t in tops)
            {
                List<string> adjacentPoints = GetAdjacentDirections(game1.Layer.Tops, t, game1);
                CreateCollisionBlock(t, adjacentPoints, "tops", game1);
            }
        }
        public static List<string> GetAdjacentDirections(Dictionary<Vector2, int> Collisions, Vector2 col, Game1 game1)
        {
            List<string> result = new List<string>();

            //checks whether there is a block next to the current one enabling the program to know what sides of the block should be active
            //this is important as it means the player can jump up the block without being stopped by collisions that shouldnt be there allowing there to be isometric jumping
            if (Collisions.TryGetValue(new Vector2(col.X + 1, col.Y), out var _))
            {
                //checks if there is a block below
                result.Add("South");
            }
            if (Collisions.TryGetValue(new Vector2(col.X - 1, col.Y), out var _))
            {
                //checks if there is a block above
                result.Add("North");
            }
            if (Collisions.TryGetValue(new Vector2(col.X, col.Y + 1), out var _))
            {
                //checks if there is a block left
                result.Add("West");
            }
            if (Collisions.TryGetValue(new Vector2(col.X, col.Y - 1), out var _))
            {
                //checks if there is a block right
                result.Add("East");
            }

            return result;
        }
        public void CreateCollisionBlock(Vector2 Vector, List<string> adjacentPoints, string collision, Game1 game1)
        {
            //converts from grid position to the world position
            Vector2 vector = new Vector2(
                (Vector.X - Vector.Y) * tilesize / 2,
                (Vector.X + Vector.Y) * tilesize / 4
                );

            bool tl = true;
            bool tr = true;
            bool bl = true;
            bool br = true;

            foreach (var direction in adjacentPoints)
            {
                switch (direction)
                {
                    case "North":
                        tl = false;
                        //if there is a block to the north then a north collision isnt needed
                        break;

                    case "South":
                        br = false;
                        //if there is a block to the south then a south collision isnt needed
                        break;

                    case "East":
                        tr = false;
                        //if there is a block to the east then a east collision isnt needed
                        break;

                    case "West":
                        bl = false;
                        //if there is a west to the north then a west collision isnt needed
                        break;

                    default:
                        break;
                }
            }

            Vector2 lineStart1 = Vector2.Zero;
            Vector2 lineEnd1 = Vector2.Zero;
            Vector2 lineStart2 = Vector2.Zero;
            Vector2 lineEnd2 = Vector2.Zero;
            Vector2 lineStart3 = Vector2.Zero;
            Vector2 lineEnd3 = Vector2.Zero;
            Vector2 lineStart4 = Vector2.Zero;
            Vector2 lineEnd4 = Vector2.Zero;

            if (tl || tr || bl || br)
            {
                if (collision == "bases")
                {
                    //from the given vector it adds a collision block with the predetermined properties to create the collision in the correct position
                    if (tl)
                    {
                        lineStart1 = new Vector2(vector.X + tilesize / 2, vector.Y + tilesize / 2);
                        lineEnd1 = new Vector2(vector.X, vector.Y + 3 * tilesize / 4);
                    }
                    if (tr)
                    {
                        lineStart2 = new Vector2(vector.X + tilesize / 2, vector.Y + tilesize / 2);
                        lineEnd2 = new Vector2(vector.X + tilesize, vector.Y + 3 * tilesize / 4);

                    }
                    if (bl)
                    {
                        lineStart3 = new Vector2(vector.X + tilesize / 2, vector.Y + tilesize);
                        lineEnd3 = new Vector2(vector.X, vector.Y + 3 * tilesize / 4);
                    }
                    if (br)
                    {
                        lineStart4 = new Vector2(vector.X + tilesize / 2, vector.Y + tilesize);
                        lineEnd4 = new Vector2(vector.X + tilesize, vector.Y + 3 * tilesize / 4);
                    }
                    collisionbases.Add(new CollisionBlock(vector, lineStart1, lineEnd1, lineStart2, lineEnd2, lineStart3, lineEnd3, lineStart4, lineEnd4));
                }
                else if (collision == "tops")
                {
                    if (tl)
                    {
                        lineStart1 = new Vector2(vector.X + tilesize / 2, vector.Y);
                        lineEnd1 = new Vector2(vector.X, vector.Y + tilesize / 4);
                    }
                    if (tr)
                    {
                        lineStart2 = new Vector2(vector.X + tilesize / 2, vector.Y);
                        lineEnd2 = new Vector2(vector.X + tilesize, vector.Y + tilesize / 4);

                    }
                    if (bl)
                    {
                        lineStart3 = new Vector2(vector.X + tilesize / 2, vector.Y + tilesize / 2);
                        lineEnd3 = new Vector2(vector.X, vector.Y + tilesize / 4);
                    }
                    if (br)
                    {
                        lineStart4 = new Vector2(vector.X + tilesize / 2, vector.Y + tilesize / 2);
                        lineEnd4 = new Vector2(vector.X + tilesize, vector.Y + tilesize / 4);
                    }
                    collisiontops.Add(new CollisionBlock(vector, lineStart1, lineEnd1, lineStart2, lineEnd2, lineStart3, lineEnd3, lineStart4, lineEnd4));
                }
            }
        }
    }
}
