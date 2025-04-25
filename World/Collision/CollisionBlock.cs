namespace GameV10.World.Collision
{
    internal class CollisionBlock
    {
        protected internal Vector2 mappos;
        public Vector2 lineStart1;
        public Vector2 lineEnd1;
        public Vector2 lineStart2;
        public Vector2 lineEnd2;
        public Vector2 lineStart3;
        public Vector2 lineEnd3;
        public Vector2 lineStart4;
        public Vector2 lineEnd4;

        public CollisionBlock(Vector2 mappos, Vector2 lineStart1, Vector2 lineEnd1, Vector2 lineStart2, Vector2 lineEnd2, Vector2 lineStart3, Vector2 lineEnd3, Vector2 lineStart4, Vector2 lineEnd4)
        {
            this.mappos = mappos;
            this.lineStart1 = lineStart1;
            this.lineStart2 = lineStart2;
            this.lineStart3 = lineStart3;
            this.lineStart4 = lineStart4;
            this.lineEnd1 = lineEnd1;
            this.lineEnd2 = lineEnd2;
            this.lineEnd3 = lineEnd3;
            this.lineEnd4 = lineEnd4;

        }
    }
}
