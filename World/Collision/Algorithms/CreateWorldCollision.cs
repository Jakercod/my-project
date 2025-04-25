namespace GameV10.World.Collision.Algorithms
{
    internal class CreateWorldCollision
    {
        public Vector2 CollisionPos;
        private float tilesize = 128;
        public bool basecolliding;
        public bool topcolliding;
        public bool basejump;
        public bool topjump;
        private bool isColliding1;
        private bool isColliding2;
        private bool isColliding3;
        private bool isColliding4;

        public void DetermineCollisions(Game1 game1, Vector2 position, Vector2 velocity, Rectangle hitbox, List<CollisionBlock> Bases, List<CollisionBlock> Tops)
        {
            basecolliding = false;
            topcolliding = false;
            basejump = false;
            topjump = false;

            Vector2 rightpos = new Vector2(position.X + hitbox.Width + tilesize / 16, position.Y + hitbox.Height);
            Vector2 leftpos = new Vector2(position.X, position.Y + hitbox.Height);

            foreach (var block in Bases)
            {
                isColliding1 = DoLinesIntersect(leftpos, rightpos, block.lineStart1, block.lineEnd1); // top left
                isColliding2 = DoLinesIntersect(leftpos, rightpos, block.lineStart2, block.lineEnd2); // top right
                isColliding3 = DoLinesIntersect(leftpos, rightpos, block.lineStart3, block.lineEnd3); // bottom left
                isColliding4 = DoLinesIntersect(leftpos, rightpos, block.lineStart4, block.lineEnd4); // bottom right

                if (isColliding1 || isColliding2 || isColliding3 || isColliding4)
                {

                    if (!game1.Player.jumping)
                    {
                        GenerateCollisions(position, velocity, game1);

                    }
                    basecolliding = true;
                }
                if (game1.collisionManager.jumping)
                {
                    Vector2 Trightpos = new Vector2(position.X + hitbox.Width + tilesize / 32, position.Y + hitbox.Height + tilesize / 4);
                    Vector2 Tleftpos = new Vector2(position.X - tilesize / 32, position.Y + hitbox.Height + tilesize / 4);
                    isColliding1 = DoLinesIntersect(Tleftpos, Trightpos, block.lineStart1, block.lineEnd1); // top left
                    isColliding2 = DoLinesIntersect(Tleftpos, Trightpos, block.lineStart2, block.lineEnd2); // top right
                    isColliding3 = DoLinesIntersect(Tleftpos, Trightpos, block.lineStart3, block.lineEnd3); // bottom left
                    isColliding4 = DoLinesIntersect(Tleftpos, Trightpos, block.lineStart4, block.lineEnd4); // bottom right
                    if (isColliding1 || isColliding2 || isColliding3 || isColliding4)
                    {
                        basejump = true;
                    }
                }
            }
            foreach (var block in Tops)
            {
                isColliding4 = DoLinesIntersect(leftpos, rightpos, block.lineStart1, block.lineEnd1); // top left
                isColliding3 = DoLinesIntersect(leftpos, rightpos, block.lineStart2, block.lineEnd2); // top right
                isColliding2 = DoLinesIntersect(leftpos, rightpos, block.lineStart3, block.lineEnd3); // bottom left
                isColliding1 = DoLinesIntersect(leftpos, rightpos, block.lineStart4, block.lineEnd4); // bottom right

                if (isColliding1 || isColliding2 || isColliding3 || isColliding4 && !game1.Player.jumping)
                {
                    if (!game1.Player.jumping)
                    {
                        GenerateCollisions(position, velocity, game1);

                    }
                    topcolliding = true;
                }
                if (game1.collisionManager.jumping)
                {
                    Vector2 Trightpos = new Vector2(position.X + hitbox.Width + tilesize / 32, position.Y + hitbox.Height + tilesize / 4);
                    Vector2 Tleftpos = new Vector2(position.X - tilesize / 32, position.Y + hitbox.Height + tilesize / 4);
                    isColliding1 = DoLinesIntersect(Tleftpos, Trightpos, block.lineStart1, block.lineEnd1); // top left
                    isColliding2 = DoLinesIntersect(Tleftpos, Trightpos, block.lineStart2, block.lineEnd2); // top right
                    isColliding3 = DoLinesIntersect(Tleftpos, Trightpos, block.lineStart3, block.lineEnd3); // bottom left
                    isColliding4 = DoLinesIntersect(Tleftpos, Trightpos, block.lineStart4, block.lineEnd4); // bottom right
                    if (isColliding1 || isColliding2 || isColliding3 || isColliding4)
                    {
                        topjump = true;
                    }
                }
            }
        }
        public static bool DoLinesIntersect(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2)
        {
            // Calculate the direction vectors
            Vector2 r = p2 - p1;
            Vector2 s = q2 - q1;


            // Calculate the cross product of r and s
            float rxs = r.X * s.Y - r.Y * s.X;

            // Calculate the cross products of (q1 - p1) and r, and (q1 - p1) and s
            Vector2 qp = q1 - p1;
            float t = (qp.X * s.Y - qp.Y * s.X) / rxs;
            float u = (qp.X * r.Y - qp.Y * r.X) / rxs;

            // Check if t and u are between 0 and 1, indicating an intersection
            return t >= 0 && t <= 1 && u >= 0 && u <= 1;
        }
        public void GenerateCollisions(Vector2 position, Vector2 velocity, Game1 game1)
        {

            if (isColliding1)
            {
                if (velocity.X > 0 && velocity.Y > 0)
                {
                    CollisionPos.X = position.X - 2 * game1.Player.Speed;
                    CollisionPos.Y = position.Y - game1.Player.Speed;
                }
                else if (velocity.X > 0)
                {
                    CollisionPos.X = position.X - 2 * game1.Player.Speed;
                    CollisionPos.Y = position.Y;
                }
                else if (velocity.Y > 0)
                {
                    CollisionPos.X = position.X;
                    CollisionPos.Y = position.Y - 2 * game1.Player.Speed;
                }
            }
            if (isColliding2)
            {
                if (velocity.X < 0 && velocity.Y > 0)
                {
                    CollisionPos.X = position.X + 2 * game1.Player.Speed;
                    CollisionPos.Y = position.Y - game1.Player.Speed;
                }
                else if (velocity.X < 0)
                {
                    CollisionPos.X = position.X + 2 * game1.Player.Speed;
                    CollisionPos.Y = position.Y;
                }
                else if (velocity.Y > 0)
                {
                    CollisionPos.X = position.X;
                    CollisionPos.Y = position.Y - 2 * game1.Player.Speed;
                }
            }
            if (isColliding3)
            {
                if (velocity.X > 0 && velocity.Y < 0)
                {
                    CollisionPos.X = position.X - 2 * game1.Player.Speed;
                    CollisionPos.Y = position.Y + game1.Player.Speed;
                }
                else if (velocity.X > 0)
                {
                    CollisionPos.X = position.X - 2 * game1.Player.Speed;
                    CollisionPos.Y = position.Y;
                }
                else if (velocity.Y < 0)
                {
                    CollisionPos.X = position.X;
                    CollisionPos.Y = position.Y + 2 * game1.Player.Speed;
                }
            }
            if (isColliding4)
            {
                if (velocity.X < 0 && velocity.Y < 0)
                {
                    CollisionPos.X = position.X + 2 * game1.Player.Speed;
                    CollisionPos.Y = position.Y + game1.Player.Speed;
                }
                else if (velocity.X < 0)
                {
                    CollisionPos.X = position.X + 2 * game1.Player.Speed;
                    CollisionPos.Y = position.Y;
                }
                else if (velocity.Y < 0)
                {
                    CollisionPos.X = position.X;
                    CollisionPos.Y = position.Y + 2 * game1.Player.Speed;
                }
            }

        }
    }
}
