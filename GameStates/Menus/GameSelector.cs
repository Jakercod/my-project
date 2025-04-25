using GameV10.GameStates.InGame.Levels;
using Microsoft.Xna.Framework.Media;
namespace GameV10.GameStates.Menus
{
    internal class GameSelector : State
    {
        private List<Button> _components;
        Texture2D backgroundTexture;
        Vector2 backgroundPosition;
        float backgroundSpeed;
        float backgroundScale;
        SpriteFont font;
        private string _inputText = "";
        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;
        private Texture2D _textBoxTexture;
        private Rectangle _textBoxRectangle;
        private Keys? _heldKey = null;
        private double _keyHoldTime = 0;
        private const double InitialDelay = 0.5; // 500ms before repeating starts
        private const double RepeatRate = 0.05; // 50ms between repeats
        private bool _isLocked = false; // Indicates whether the text input is locked
        public GameSelector(Game1 game1, GraphicsDeviceManager graphicsDeviceManager, ContentManager content, GraphicsDevice graphicsDevice, Vector2 backgroundposition, Texture2D backgroundtexture, float backgroundspeed, float backgrooundscale) : base(game1, graphicsDeviceManager, content, graphicsDevice)
        {
            backgroundTexture = backgroundtexture;
            backgroundPosition = backgroundposition;
            backgroundSpeed = backgroundspeed;
            backgroundScale = backgrooundscale;
            var buttonTexture = content.Load<Texture2D>("Button");
            var buttonFont = content.Load<SpriteFont>("File");
            font = content.Load<SpriteFont>("File");
            _textBoxRectangle = new Rectangle(100, 100, 150, 40);
            // Create a white rectangle texture to serve as the text box background
            _textBoxTexture = new Texture2D(GraphicsDevice, 1, 1);
            _textBoxTexture.SetData(new[] { Color.White });
            Vector2 screencentre = new(game1._graphics.PreferredBackBufferWidth / 2 + buttonTexture.Width / 2, game1._graphics.PreferredBackBufferHeight / 2 + buttonTexture.Height / 2);

            var EasynewGameButton = new Button(buttonTexture, buttonFont)
            {
                //Creates a new button object with a texture and font and then assigns it a position (for the texture to be placed) and a string (to go inside the button texture and using the button font)
                Position = new Vector2(screencentre.X, screencentre.Y - 300),
                Text = "Easy",
            };
            EasynewGameButton.Click += EasyNewGameButton_Click;
            //"Click?.Invoke(this, new EventArgs());" if an increment has invoked then a click has occured under
            //"this" specifing the current component which then sends the code to a method relating to that components properties
            var MediumnewGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(screencentre.X, screencentre.Y - 100),
                Text = "Medium",
            };
            MediumnewGameButton.Click += MediumNewGameButton_Click;
            var HardnewGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(screencentre.X, screencentre.Y + 100),
                Text = "Hard",
            };
            HardnewGameButton.Click += HardNewGameButton_Click;
            var ImpossiblenewGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(screencentre.X, screencentre.Y + 300),
                Text = "Impossible",
            };
            ImpossiblenewGameButton.Click += ImpossibleNewGameButton_Click;
            var MainMenuBtn = new Button(buttonTexture, buttonFont)
            {
                //Creates a new button object with a texture and font and then assigns it a position (for the texture to be placed) and a string (to go inside the button texture and using the button font)
                Position = new Vector2(0, 0),
                Text = "< Main Menu",
            };
            MainMenuBtn.Click += MainMenuBtn_Click;
            _components = new List<Button>()
            {
                //adds each button object to a list of components
                EasynewGameButton,
                MediumnewGameButton,
                HardnewGameButton,
                ImpossiblenewGameButton,
                MainMenuBtn
            };
        }
        public override void Update(GameTime gameTime)
        {
            _currentKeyboardState = Keyboard.GetState();

            if (!_isLocked)
            {
                HandleKeyboardInput(gameTime);
            }

            _previousKeyboardState = _currentKeyboardState;
            //updates all the components checking if the mouse is hovering or clicking whilst hovering

            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
            // Move the background position
            backgroundPosition.X -= backgroundSpeed;

            // Reset position when it goes off screen
            if (backgroundPosition.X <= -backgroundTexture.Width * backgroundScale)
            {
                backgroundPosition.X = 0;
            }
        }
        private void HandleKeyboardInput(GameTime gameTime)
        {
            var pressedKeys = _currentKeyboardState.GetPressedKeys();

            // Check if there is a new key press
            if (pressedKeys.Length > 0)
            {
                var key = pressedKeys[0];

                // If the key is newly pressed or different, reset the hold timer
                if (_heldKey != key)
                {
                    _heldKey = key;
                    _keyHoldTime = 0;

                    ProcessKey(key);
                }
                else
                {
                    // Increment the hold time if the same key is being held
                    _keyHoldTime += gameTime.ElapsedGameTime.TotalSeconds;

                    // Start repeating if the hold time exceeds the initial delay
                    if (_keyHoldTime > InitialDelay)
                    {
                        if ((_keyHoldTime - InitialDelay) % RepeatRate < gameTime.ElapsedGameTime.TotalSeconds)
                        {
                            ProcessKey(key);
                        }
                    }
                }
            }
            else
            {
                // Reset if no key is being pressed
                _heldKey = null;
                _keyHoldTime = 0;
            }
        }

        private void ProcessKey(Keys key)
        {
            // Lock input if Enter key is pressed
            if (key == Keys.Enter)
            {
                _isLocked = true;
                return;
            }

            // Calculate the width of the current input text
            float currentTextWidth = font.MeasureString(_inputText).X;

            // Check if adding a new character would exceed the text box width
            float maxWidth = _textBoxRectangle.Width - 20; // Reduce width slightly for padding

            if (currentTextWidth < maxWidth || key == Keys.Back)
            {
                if (key == Keys.Back && _inputText.Length > 0)
                {
                    // Handle backspace
                    _inputText = _inputText.Substring(0, _inputText.Length - 1);
                }
                else if (key == Keys.Space)
                {
                    // Handle space
                    _inputText += " ";
                }
                else if (key >= Keys.A && key <= Keys.Z)
                {
                    // Handle letter keys
                    bool isShift = _currentKeyboardState.IsKeyDown(Keys.LeftShift) || _currentKeyboardState.IsKeyDown(Keys.RightShift);
                    _inputText += isShift ? key.ToString() : key.ToString().ToLower();
                }
                else if (key >= Keys.D0 && key <= Keys.D9)
                {
                    // Handle number keys
                    _inputText += key.ToString().Substring(1);
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            // Draw the first part of the background
            spriteBatch.Draw(backgroundTexture, backgroundPosition, null, Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);

            // Draw the second part of the background to create a seamless loop
            if (backgroundPosition.X <= -GraphicsDevice.Viewport.Width)
            {
                spriteBatch.Draw(backgroundTexture, new Vector2(backgroundPosition.X + backgroundTexture.Width * backgroundScale, 0), null, Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(backgroundTexture, new Vector2(backgroundPosition.X + backgroundTexture.Width * backgroundScale, 0), null, Color.White, 0f, Vector2.Zero, backgroundScale, SpriteEffects.None, 0f);
            }
            //draws all the components that have been added to the list with their corresponding texture, font, colour, string and position
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            // Draw the text box background
            spriteBatch.Draw(_textBoxTexture, _textBoxRectangle, Color.LightGray);

            // Draw the input text
            spriteBatch.DrawString(font, _inputText, new Vector2(_textBoxRectangle.X + 10, _textBoxRectangle.Y + 10), Color.Black);

            // Draw a message prompting the user for their gamertag
            spriteBatch.DrawString(
                font,
                _isLocked ? "Gamertag locked in." : "Enter your gamertag and press Enter:",
                new Vector2(100, 60),
                Color.White
            );
            spriteBatch.End();
        }
        private void EasyNewGameButton_Click(object sender, EventArgs e)
        {
            //object sender is the object button for the specific component (if occurs within this method then the object is the newGameButton
            //implaments the change state method from game one creating a new scene with new properties
            MediaPlayer.Stop();
            Game1.ChangeState(new GameEasy(Game1, GraphicsDeviceManager, Content, GraphicsDevice, _inputText));
        }
        private void MediumNewGameButton_Click(object sender, EventArgs e)
        {
            MediaPlayer.Stop();
            Game1.ChangeState(new GameMedium(Game1, GraphicsDeviceManager, Content, GraphicsDevice, _inputText));
        }
        private void HardNewGameButton_Click(object sender, EventArgs e)
        {
            MediaPlayer.Stop();
            Game1.ChangeState(new GameHard(Game1, GraphicsDeviceManager, Content, GraphicsDevice, _inputText));
        }
        private void ImpossibleNewGameButton_Click(object sender, EventArgs e)
        {
            MediaPlayer.Stop();
            Game1.ChangeState(new GameImpossible(Game1, GraphicsDeviceManager, Content, GraphicsDevice, _inputText));
        }
        private void MainMenuBtn_Click(object sender, EventArgs e)
        {
            //object sender is the object button for the specific component (if occurs within this method then the object is the newGameButton
            //implaments the change state method from game one creating a new scene with new properties
            Game1.ChangeState(new MainMenu(Game1, GraphicsDeviceManager, Content, GraphicsDevice, backgroundPosition, backgroundTexture, backgroundSpeed, backgroundScale));
        }
    }
}
