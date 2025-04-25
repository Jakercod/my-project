using GameV10.GameStates.InGame.Levels;
using GameV10.Sprites.Enemy;
using GameV10.World.Boosters;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GameV10
{
    public class Game1 : Game
    {
        public static float Time { get; private set; }
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public State currentstate;
        public SpriteFont scoretext;
        internal InputManager InputManager = new();
        internal CentreCamera Camera = new();
        internal FindWorldCollisions find = new();
        internal MapWorldCollisions map = new();
        internal CreateWorldCollision create = new();
        internal Layer Layer = new();
        internal List<SpriteBase> sprites = new();
        internal List<BaseEnemy> BasicEnemys = new();
        internal CollisionManager collisionManager = new();
        internal GeneratePlayerAttackZones GenerateAttacks = new();
        internal List<PowerProjectileAttack> powerProjectiles = new();
        public SoundEffect swordattack1SFX;
        public SoundEffectInstance swordattack1IN;
        public SoundEffect swordattack2SFX;
        public SoundEffectInstance swordattack2IN;
        public SoundEffect swordattack3SFX;
        public SoundEffectInstance swordattack3IN;
        public SoundEffect blockSFX;
        public SoundEffectInstance blockIN;
        public SoundEffect powerattackSFX;
        public SoundEffectInstance powerattackIN;
        public SoundEffect BoosterSFX;
        public SoundEffectInstance BoosterIN;
        public SoundEffect JumpSFX;
        public SoundEffectInstance JumpIN;
        public SoundEffect HitSFX;
        public SoundEffectInstance HitIN;
        public SoundEffect RunningSFX;
        public SoundEffectInstance RunningIN;
        public SoundEffect SpawnSFX;
        public SoundEffectInstance SpawnIN;
        public SoundEffect DeathSFX;
        public SoundEffectInstance DeathIN;
        public SoundEffect GameOverSFX;
        public SoundEffectInstance GameOverIN;
        public SoundEffect GameWonSFX;
        public SoundEffectInstance GameWonIN;


        public bool GameOver;
        public bool GameWon;

        internal Player Player { get; set; }
        internal IsometricTileMap IsometricTileMap { get; set; }
        internal AnimationManager AnimationManager { get; set; }
        internal AnimationManager PlayerAnimation { get; set; }
        internal AnimationManager EnemyAnimation { get; set; }
        internal BaseEnemy BasicEnemy { get; set; }
        internal PowerProjectileAttack projectileAttacks { get; set; }
        internal SpawnBoosters spawnBoosters { get; set; }
        public Texture2D backgroundTexture;
        Vector2 backgroundPosition;
        float backgroundSpeed = 1f;
        float backgroundScale;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            // Load the background texture
            backgroundTexture = Content.Load<Texture2D>("knightbackground");

            // Initialize background positions
            backgroundPosition = Vector2.Zero;

            // Calculate the scale to fit the screen height
            backgroundScale = (float)GraphicsDevice.Viewport.Height / backgroundTexture.Height;

            //Initializes the game in the MainMenu state from which they can access all other states
            currentstate = new MainMenu(this, _graphics, Content, GraphicsDevice, backgroundPosition, backgroundTexture, backgroundSpeed, backgroundScale);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            swordattack1SFX = Content.Load<SoundEffect>("SwordSFX1");
            swordattack1IN = swordattack1SFX.CreateInstance();
            swordattack1IN.Volume = 0.5f;
            swordattack2SFX = Content.Load<SoundEffect>("SwordSFX2");
            swordattack2IN = swordattack2SFX.CreateInstance();
            swordattack2IN.Volume = 0.5f;
            swordattack3SFX = Content.Load<SoundEffect>("SwordSFX3");
            swordattack3IN = swordattack3SFX.CreateInstance();
            swordattack3IN.Volume = 0.5f;
            blockSFX = Content.Load<SoundEffect>("BlockSFX");
            blockIN = blockSFX.CreateInstance();
            blockIN.Volume = 0.5f;
            powerattackSFX = Content.Load<SoundEffect>("FireballSFX");
            powerattackIN = powerattackSFX.CreateInstance();
            powerattackIN.Volume = 1f;
            BoosterSFX = Content.Load<SoundEffect>("BoostSFX");
            BoosterIN = BoosterSFX.CreateInstance();
            BoosterIN.Volume = 1f;
            JumpSFX = Content.Load<SoundEffect>("JumpSFX");
            JumpIN = JumpSFX.CreateInstance();
            JumpIN.Volume = 0.5f;
            HitSFX = Content.Load<SoundEffect>("takingdamageSFX");
            HitIN = HitSFX.CreateInstance();
            HitIN.Volume = 0.00001f;
            RunningSFX = Content.Load<SoundEffect>("RunningSFX3");
            RunningIN = RunningSFX.CreateInstance();
            SpawnSFX = Content.Load<SoundEffect>("Cool");
            SpawnIN = SpawnSFX.CreateInstance();
            SpawnIN.Volume = 1f;
            DeathSFX = Content.Load<SoundEffect>("Death");
            DeathIN = DeathSFX.CreateInstance();
            DeathIN.Volume = 1f;
            GameOverSFX = Content.Load<SoundEffect>("GameOverSFX");
            GameOverIN = GameOverSFX.CreateInstance();
            GameOverIN.Volume = 1f;
            GameWonSFX = Content.Load<SoundEffect>("Victory");
            GameWonIN = GameWonSFX.CreateInstance();
            GameWonIN.Volume = 1f;
            RunningIN.Volume = 0.05f;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            //updates the current state this changes depending on the section of the players exxperience
            currentstate.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //draws the current state this changes depending on the section of the players exxperience
            currentstate.Draw(_spriteBatch, gameTime);

            base.Draw(gameTime);
        }
        public void ChangeState(State state)
        {
            //sets current state to the state that is held in the perameter
            currentstate = state;
        }
    }
}

