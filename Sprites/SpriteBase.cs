namespace GameV10.Sprites
{
    public class SpriteBase
    {
        public Texture2D North;
        public Texture2D South;
        public Texture2D West;
        public Texture2D NWest;
        public Texture2D SWest;
        public AnimationManager AnimationManager;
       
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Size { get; set; }
        public List<Vector2> Bases = new() { };
        public Rectangle attackRect = new(0, 0, 0, 0);
        public Rectangle blockRect = new(0, 0, 0, 0);
        public bool walking { get; set; }
        public bool running { get; set; }
        public bool jumping { get; set; }
        public bool sliding { get; set; }
        public bool blocking { get; set; }
        public bool attacking { get; set; }
        public bool charging { get; set; }
        public string Direction { get; set; }
        public string NAME { get; set; }
        public bool Dead { get; set; }
        public bool Hit {  get; set; }
        public bool Stunned { get; set; }
        public Color colour = Color.White;


        public Rectangle BoundBox
        {
            get
            {
                return new Rectangle(
                    (int)(Position.X - Size.Y),
                    (int)(Position.Y - Size.X),
                    (int)Size.X * 5,
                    (int)Size.Y * 2
                    );
            }
        }
        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    (int)Size.X,
                    (int)Size.Y
                    );
            }
        }
        public float Speed { get; set; }
        public float Health { get; set; }

        public SpriteBase(Texture2D north, Texture2D south, Texture2D west, Texture2D Nwest, Texture2D Swest, Vector2 position, Vector2 velocity, Vector2 size, float speed, float health)
        {
            North = north;
            South = south;
            West = west;
            NWest = Nwest;
            SWest = Swest;
            Position = position;
            Velocity = velocity;
            Size = size;
            Speed = speed;
            Health = health;
            AnimationManager = new(north, south , west, Nwest, Swest);
        }
        public virtual void Update(GameTime gameTime, Game1 game1)
        {
            
        }
        public virtual void Draw(SpriteBatch spriteBatch, Game1 game1, SpriteBase sprite)
        {
            spriteBatch.Draw(
                AnimationManager.ActiveTexture,
                new Vector2(Position.X - (int)(3.5 * Size.X), Position.Y - Size.Y),
                AnimationManager.GetFrame(AnimationManager.CurrentAnimation),
                sprite.colour,
                0f,
                Vector2.Zero,
                2,
                AnimationManager.spriteEffects,
                0
                );
        }
    }
}
