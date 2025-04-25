namespace GameV10
{
    internal class CentreCamera
    {
        public Matrix Transform { get; set; }
        
        public void Update(Vector2 screenSize, Game1 game1)
        {
            //Centres the player on the screen making the screen move instead of the player creating the illusion the player is moving

            Rectangle tex = game1.Player.Hitbox;
            Vector2 target = game1.Player.Position;

            var positionX = screenSize.X / 2 - target.X - tex.Width / 2;
            var positionY = screenSize.Y / 2 - target.Y - tex.Height / 2;

            Transform = Matrix.CreateTranslation(positionX, positionY, 0f);

        }
    }
}
