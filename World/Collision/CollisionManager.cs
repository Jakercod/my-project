namespace GameV10.World.Collision
{
    internal class CollisionManager
    {

        public float tilesize = 128;
        public bool jumping;
        public bool singleaction1 = true;
        public bool jumpedbase;
        public bool jumpedtop;
        public bool falledbase;
        public bool falledtop;

        public void Update(SpriteBase sprite, Game1 game1, Vector2 position, Vector2 velocity, Rectangle bounds, Rectangle hitbox)
        {
            if (sprite.NAME == "Enemy")
            {
                game1.find.CheckBounds(sprite, game1, bounds);
            }
            else if (game1.Player.jumping)
            {
                jumping = true;
                game1.create.DetermineCollisions(game1, position, velocity, hitbox, game1.map.collisionbases, game1.map.collisiontops);
                if (game1.create.basecolliding)
                {
                    jumpedbase = true;
                }
                if (game1.create.basejump)
                {
                    jumpedtop = true;
                }
                if (jumpedtop && jumpedbase && singleaction1)
                {
                    game1.Layer.NextLayer();
                    singleaction1 = false;

                }
                if (game1.create.topcolliding)
                {
                    falledbase = true;
                }
                if (game1.create.topjump)
                {
                    falledtop = true;
                }
                if (falledtop && falledbase && singleaction1)
                {
                    game1.Layer.PrevLayer();
                    singleaction1 = false;
                }
            }
            else if (!game1.Player.jumping)
            {

                if (jumping)
                {
                    jumping = false;

                    if (jumpedbase && !jumpedtop)
                    {
                        game1.create.CollisionPos.X = position.X;
                        game1.create.CollisionPos.Y = position.Y + tilesize / 2;
                        game1.create.basecolliding = true;

                    }
                    if (falledtop && !falledbase)
                    {

                    }
                }
                jumpedbase = false;
                falledbase = false;
                jumpedtop = false;
                falledtop = false;
                singleaction1 = true;
                game1.find.CheckBounds(sprite, game1, bounds);
                game1.map.MapCollisions(game1.find.bases, game1.find.tops, game1);
                game1.create.DetermineCollisions(game1, position, velocity, hitbox, game1.map.collisionbases, game1.map.collisiontops);

            }
        }
    }
}
/*if (game1.InputManager.jumping)
{
    jumping = true;
    if (game1.create.basecolliding)
    {
        if (!Jumporder.Contains("Base"))
        {
            Jumporder.Add("Base");
        }
    }
    if (game1.create.topcolliding)
    {
        if (!Jumporder.Contains("Top"))
        {
            Jumporder.Add("Top");
        }
    }
}
else
{
    if (jumping)
    {
        jumping = false;

        if (Jumporder.Count == 2)
        {
            if (Jumporder[0] == "Base")
            {
                game1.Layer.NextLayer();
            }
            else
            {
                game1.Layer.PrevLayer();
            }
        }
        else
        {
            //base collision position or top collision position
        }
    }
    Jumporder.Clear();
 public List<string> Jumporder = new() { };
        public bool jumping;
}*/