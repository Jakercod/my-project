using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV10.GameStates.InGame.HeadsUpDisplay
{
    public class ProgressBar
    {
        protected readonly Texture2D HUDbackground;
        protected readonly Texture2D background;
        protected readonly Texture2D foreground;
        protected Vector2 position;
        protected readonly float maxValue;
        protected float currentValue;
        protected Rectangle part;
        protected Vector2 _offset;

        public ProgressBar(Texture2D baseBG, Texture2D fg, Texture2D bg, float max, Vector2 pos, Vector2 offset)
        {
            HUDbackground = baseBG;
            background = bg;
            foreground = fg;
            maxValue = max;
            currentValue = max;
            position = pos;
            _offset = offset;
            part = new(0, 0, foreground.Width, foreground.Height);
        }

        public virtual void Update(float value, Vector2 centre, Game1 game1, SpriteBase sprite)
        {
            currentValue = value;
            part.Width = (int)(currentValue / maxValue * foreground.Width);
            
            position = new Vector2(centre.X + foreground.Width/6 -game1._graphics.PreferredBackBufferWidth / 2, centre.Y + background.Height - game1._graphics.PreferredBackBufferHeight / 2);

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(foreground, new(position.X + _offset.X, position.Y + _offset.Y), part, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
        public virtual void DrawBack(SpriteBatch spriteBatch, SpriteBase sprite)
        {
            spriteBatch.Draw(background, position, Color.White);
        }
    }
}
