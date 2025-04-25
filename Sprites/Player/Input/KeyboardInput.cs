namespace GameV10.Sprites.Player.Input
{
    internal class KeyboardInput
    {
        public static KeyboardState currentKeyState { get; private set; }
        public static KeyboardState previousKeyState { get; private set; }

        //On every input the key that was just pressed becomes the previous key
        public static KeyboardState GetState()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            return currentKeyState;
        }
        //If a key has been pressed it is the active key 
        public bool IsKeyPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }
        public bool IsKeyReleased(Keys key)
        {
            return !currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyDown(key);
        }
        //If a key is pressed it is active until released
        public bool IsKeyDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            return !currentKeyState.IsKeyDown(key);
        }
    }
}
