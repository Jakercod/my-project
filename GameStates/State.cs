namespace GameV10.GameStates
{
    public abstract class State 
    {
        //protected fields means only derrived classes can access these fields
        protected ContentManager Content;
        protected GraphicsDevice GraphicsDevice;
        protected Game1 Game1;
        protected GraphicsDeviceManager GraphicsDeviceManager;

        public State(Game1 game,  GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            Content = contentManager;
            GraphicsDevice = graphicsDevice;
            Game1 = game;
            GraphicsDeviceManager = graphicsDeviceManager;
        }
        //abstract voids act as base methods that will be called by override methods from different states 
        public abstract void Update(GameTime gameTime); 
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
