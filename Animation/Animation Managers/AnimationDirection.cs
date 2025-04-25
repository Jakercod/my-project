namespace GameV10.Animation
{
    internal class AnimationDirection 
    {
        public string SetDirection(Game1 game1, SpriteBase sprite, Texture2D TextureNorth, Texture2D TextureSouth, Texture2D TextureEastWest, Texture2D TextureNorthEast, Texture2D TextureSouthEast, ref Texture2D ActiveTexture, ref SpriteEffects spriteEffects)
        {

            //if there is an current input then that is the direction and the player is moving in that direction however if there is no movement input the player is idle in the last direction
            //Sets the Texture and orientation of the sprite
            switch (GetDirection(game1, sprite))
            {
                case "Up":
                    ActiveTexture = TextureNorth;
                    spriteEffects = SpriteEffects.None;
                    return "Up";  
                case "Right":
                    ActiveTexture = TextureEastWest;
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    return "Right";
                case "Down":
                    ActiveTexture = TextureSouth;
                    spriteEffects = SpriteEffects.None;
                    return "Down";
                case "Left":
                    ActiveTexture = TextureEastWest;
                    spriteEffects = SpriteEffects.None;
                    return "Left";
                case "Up/Left":
                    ActiveTexture = TextureNorthEast;
                    spriteEffects = SpriteEffects.None;
                    return "Up/Left";
                case "Up/Right":
                    ActiveTexture = TextureNorthEast;
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    return "Up/Right";
                case "Down/Left":
                    ActiveTexture = TextureSouthEast;
                    spriteEffects = SpriteEffects.None;
                    return "Down/Left";
                case "Down/Right":
                    ActiveTexture = TextureSouthEast;
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    return "Down/Right";
                default:
                    return "";
            }
        }
        public string GetDirection(Game1 game1, SpriteBase sprite)
        {
            if(sprite == game1.Player)
            {
                //sets the current direction
                if (game1.InputManager.StringDirection != "")
                {
                    return game1.InputManager.StringDirection;
                }
                //if there isnt a current direction due to reseting then it finds the previous direction that wasnt empty
                else
                {
                    return game1.InputManager.PrevStringDirection;
                }
            }
            else
            {
                return sprite.Direction;
            }
        }
    }

}
