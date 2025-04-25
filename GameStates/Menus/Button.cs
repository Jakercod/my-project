namespace GameV10.GameStates.Menus
{
    public class Button
    {
        public event EventHandler Click;
        private MouseState _currentMouse;
        private MouseState _previousMouse;
        private SpriteFont menuButtonFont;
        private Texture2D buttonTexture;
        private bool _isHovering;

        public bool Clicked { get; private set; }
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle MouseRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, buttonTexture.Width, buttonTexture.Height);
            }
        }
        public string Text { get; set; }

        public Button(Texture2D texture, SpriteFont font)
        {
            buttonTexture = texture;
            menuButtonFont = font;
            PenColour = Color.Black;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            var colour = Color.White;

            if (_isHovering)
                //set a grey tone if the mouse is over the button
                colour = Color.Gray;

            spriteBatch.Draw(buttonTexture, MouseRectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                //draws text in the middle of the button
                var x = MouseRectangle.X + MouseRectangle.Width / 2 - menuButtonFont.MeasureString(Text).X / 2;
                var y = MouseRectangle.Y + MouseRectangle.Height / 2 - menuButtonFont.MeasureString(Text).Y / 2;
                spriteBatch.DrawString(menuButtonFont, Text, new Vector2(x, y), PenColour);
            }
           
        }

        public void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            //creates a 1x1 pixel rectangle of the mouse to track any intersections
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            //if there is an itersection then the mouse is above a button
            if (mouseRectangle.Intersects(MouseRectangle))
            {
                _isHovering = true;

                //checks if there is a mouse click whilst it is above the button
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
