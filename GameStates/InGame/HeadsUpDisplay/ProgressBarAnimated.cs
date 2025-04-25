using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV10.GameStates.InGame.HeadsUpDisplay
{
    public class ProgressBarAnimated : ProgressBar
    {
        private float _targetValue;
        private readonly float _animationSpeed = 100;
        private Rectangle _animationPart;
        private Vector2 _animationPosition;
        private Color _animationShade;

        public ProgressBarAnimated(Texture2D baseBG, Texture2D fg, Texture2D bg, float max, Vector2 pos, Vector2 offset) : base(bg, fg, baseBG, max, pos, offset)
        {
            _targetValue = max;
            _animationPart = new(foreground.Width, 0, 0, foreground.Height);
            _animationPosition = pos;
            _animationShade = Color.DarkGray;
        }

        public override void Update(float value, Vector2 centre, Game1 game1, SpriteBase sprite)
        {
            if (sprite is BaseEnemy)
            {
                _animationPosition = new Vector2(centre.X - 2*foreground.Width/3 , centre.Y - 3*foreground.Height);
                position = new Vector2(centre.X - 2*foreground.Width/3, centre.Y - 3*foreground.Height);
            }
            else
            {
                _animationPosition = new Vector2(centre.X + foreground.Width / 6 - game1._graphics.PreferredBackBufferWidth / 2, centre.Y + background.Height - game1._graphics.PreferredBackBufferHeight / 2);
                position = new Vector2(centre.X + foreground.Width / 6 - game1._graphics.PreferredBackBufferWidth / 2, centre.Y + background.Height - game1._graphics.PreferredBackBufferHeight / 2);
            }
            if (value == currentValue) return;

            _targetValue = value;
            int x;

            if (_targetValue < currentValue)
            {
                currentValue -= _animationSpeed * Game1.Time;
                if (currentValue < _targetValue) currentValue = _targetValue;
                x = (int)(_targetValue / maxValue * foreground.Width);
                _animationShade = Color.Gray;
            }
            else
            {
                currentValue += _animationSpeed * Game1.Time;
                if (currentValue > _targetValue) currentValue = _targetValue;
                x = (int)(currentValue / maxValue * foreground.Width);
                _animationShade = Color.DarkGray * 0.5f;
            }

            part.Width = x;
            _animationPart.X = x;
            _animationPart.Width = (int)(Math.Abs(currentValue - _targetValue) / maxValue * foreground.Width);
            _animationPosition.X = position.X + x;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(foreground, new(_animationPosition.X + _offset.X, _animationPosition.Y + _offset.Y), _animationPart, _animationShade, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}
