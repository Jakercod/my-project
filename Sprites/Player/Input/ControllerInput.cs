namespace GameV10.Sprites.Player.Input
{
    internal class ControllerInput
    {
        public static GamePadState currentPadstate { get; private set; }
        public static GamePadState prevPadstate { get; private set; }

        public static GamePadState GetState()
        {
            //On every input the button that was just pressed becomes the previous button
            prevPadstate = currentPadstate;
            currentPadstate = GamePad.GetState(PlayerIndex.One);

            return currentPadstate;
        }
        //If a button has been pressed it is the active button 
        public static bool IsPressed(Buttons buttons)
        {
            return currentPadstate.IsButtonDown(buttons) && !prevPadstate.IsButtonDown(buttons);
        }
        public static bool IsReleased(Buttons buttons)
        {
            return !currentPadstate.IsButtonDown(buttons) && prevPadstate.IsButtonDown(buttons);
        }
        //If a button is pressed it is active until released
        public static bool IsDown(Buttons buttons)
        {
            return currentPadstate.IsButtonDown(buttons);
        }
        public static bool IsUp(Buttons buttons)
        {
            return !currentPadstate.IsButtonDown(buttons);
        }
    }
}
