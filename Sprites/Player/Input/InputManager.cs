using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Net.Security;

namespace GameV10.Sprites.Player.Input
{
    internal class InputManager
    {
        public bool idle = true;
        public bool pathfinding;
        private int pathcounter;


        public string StringDirection { get; set; }
        public string PrevStringDirection { get; set; }
        public void Update(Game1 game1)
        {
            pathfinding = false;
            if (game1.Player != null)
            {
                game1.Player.running = false;
                game1.Player.walking = false;
                game1.Player.charging = false;
            }

            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            if (StringDirection != "" && StringDirection != null)
            {
                //Saves a previous direction if their is one to save
                PrevStringDirection = StringDirection;
            }
            //Resests direction so that when player lets go of key the sprite doesnt keep on moving
            StringDirection = "";
            if (capabilities.IsConnected)
            {
                //if controller is connected keyboard cannot be used (stops multipling speeds)
                HandleGamePadInput(capabilities, game1);
            }
            else
            {
                HandleKeyboardInput(game1);
            }
            if (game1.Player != null)
            {
                if (StringDirection != "")
                {
                    game1.Player.running = true;
                }
                if (game1.Player.walking)
                {
                    game1.Player.running = false;
                }
                game1.Player.Direction = StringDirection;
            }


        }
        private void HandleGamePadInput(GamePadCapabilities capabilities, Game1 game1)
        {
            GamePadState state = ControllerInput.GetState();

            Vector2 direction = new();

            //Finds two vectors directions from the origin creating a sector of the 2 pi radians.
            if (capabilities.HasLeftXThumbStick)
            {
                direction.X = state.ThumbSticks.Left.X;
            }
            if (capabilities.HasLeftYThumbStick)
            {
                direction.Y = state.ThumbSticks.Left.Y;
            }

            //Abstracts any actual player movement to nortical directions by splitting the joysticks circle into eight segments
            if (direction.X <= 0.5 && direction.X >= -0.5 && direction.Y <= 1 && direction.Y > 0)
            {
                StringDirection = "Up";
            }
            else if (direction.X < 0 && direction.X >= -1 && direction.Y <= 0.5 && direction.Y >= -0.5)
            {
                StringDirection = "Left";
            }
            else if (direction.X <= 0.5 && direction.X >= -0.5 && direction.Y < 0 && direction.Y >= -1)
            {
                StringDirection = "Down";
            }
            else if (direction.X <= 1 && direction.X > 0 && direction.Y <= 0.5 && direction.Y >= -0.5)
            {
                StringDirection = "Right";
            }
            else if (direction.X <= 1 && direction.X >= 0.5 && direction.Y <= 1 && direction.Y >= 0.5)
            {
                StringDirection = "Up/Right";
            }
            else if (direction.X <= -0.5 && direction.X >= -1 && direction.Y <= 1 && direction.Y >= 0.5)
            {
                StringDirection = "Up/Left";
            }
            else if (direction.X <= -0.5 && direction.X >= -1 && direction.Y <= -0.5 && direction.Y >= -1)
            {
                StringDirection = "Down/Left";
            }
            else if (direction.X <= 1 && direction.X >= 0.5 && direction.Y <= -0.5 && direction.Y >= -1)
            {
                StringDirection = "Down/Right";
            }


            if (state.IsButtonDown(Buttons.LeftStick) && StringDirection != "")
            {
                game1.Player.walking = true;
            }
            if (state.IsButtonDown(Buttons.A) && !game1.Player.jumping)
            {
                game1.Player.jumping = true;
            }
            if (state.IsButtonDown(Buttons.B) && StringDirection != "" && !game1.Player.sliding)
            {
                game1.Player.sliding = true;
            }
            if (state.IsButtonDown(Buttons.LeftTrigger) && !game1.Player.blocking && game1.Player.block == game1.Player.maxBlock)
            {
                game1.Player.blocking = true;
            }
            else if (game1.Player != null && !state.IsButtonDown(Buttons.LeftTrigger) && game1.Player.blocking && game1.Player.block != game1.Player.maxBlock)
            {
                game1.Player.blocking = false;
            }
            if (state.IsButtonDown(Buttons.RightTrigger))
            {
                game1.Player.attacking = true;
            }
            if (state.IsButtonDown(Buttons.X) && !game1.Player.powerattack && game1.Player.power == game1.Player.maxPower)
            {
                game1.Player.powerattack = true;
            }
            else if (game1.Player != null && !state.IsButtonDown(Buttons.X) && game1.Player.power == game1.Player.maxPower)
            {
                game1.Player.powerattack = false;
            }
        }
        private void HandleKeyboardInput(Game1 game1)
        {
            KeyboardState state = KeyboardInput.GetState();

            //Abstracts any actual player movement to nortical directions makes it easy to expand for other keys

            if (state.IsKeyDown(Keys.W) && state.IsKeyUp(Keys.D) && state.IsKeyUp(Keys.A))
            {
                StringDirection = "Up";
            }
            else if (state.IsKeyDown(Keys.A) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.S))
            {
                StringDirection = "Left";
            }
            else if (state.IsKeyDown(Keys.S) && state.IsKeyUp(Keys.D) && state.IsKeyUp(Keys.A))
            {
                StringDirection = "Down";
            }
            else if (state.IsKeyDown(Keys.D) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.S))
            {
                StringDirection = "Right";
            }

            else if (state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.D))
            {
                StringDirection = "Up/Right";
            }
            else if (state.IsKeyDown(Keys.W) && state.IsKeyDown(Keys.A))
            {
                StringDirection = "Up/Left";
            }
            else if (state.IsKeyDown(Keys.S) && state.IsKeyDown(Keys.A))
            {
                StringDirection = "Down/Left";
            }
            else if (state.IsKeyDown(Keys.S) && state.IsKeyDown(Keys.D))
            {
                StringDirection = "Down/Right";

            }

            if (state.IsKeyDown(Keys.Q) && !game1.Player.powerattack && game1.Player.power == game1.Player.maxPower)
            {
                game1.Player.powerattack = true;
            }
            else if (game1.Player != null && !state.IsKeyDown(Keys.Q) && game1.Player.power == game1.Player.maxPower)
            {
                game1.Player.powerattack = false;
            }
            if (state.IsKeyDown(Keys.LeftShift) && StringDirection != "")
            {
                game1.Player.walking = true;
            }
            if (state.IsKeyDown(Keys.Space) && !game1.Player.jumping)
            {
                game1.Player.jumping = true;
                if (game1.JumpIN.State == SoundState.Stopped)
                {
                    game1.JumpIN.Play();
                }
            }
            if (state.IsKeyDown(Keys.C) && StringDirection != "" && !game1.Player.sliding)
            {
                game1.Player.sliding = true;
            }
            if (state.IsKeyDown(Keys.E) && !game1.Player.blocking && game1.Player.block == game1.Player.maxBlock)
            {
                game1.Player.blocking = true;
            }
            else if(game1.Player != null && !state.IsKeyDown(Keys.E) && game1.Player.blocking && game1.Player.block != game1.Player.maxBlock)
            {
                game1.Player.blocking = false;
            }
            if (state.IsKeyDown(Keys.F))
            {
                game1.Player.attacking = true;
                
            }
            if (state.IsKeyDown(Keys.P))
            {
                pathcounter++;
            }
            if (pathcounter == 2) //when this value is 1 then it stops working but otherwise its jumpy
            {
                pathcounter = 0;
                pathfinding = true;
            }
        }
    }
}
