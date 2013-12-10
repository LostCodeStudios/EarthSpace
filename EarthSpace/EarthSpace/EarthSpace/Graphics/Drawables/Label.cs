using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EarthSpace.Graphics.Drawables
{
    /// <summary>
    /// Text that will be drawn to the screen.
    /// </summary>
    public class Label : IDrawable
    {
        #region Properties

        /// <summary>
        /// The font of this label.
        /// </summary>
        public SpriteFont Font
        {
            get;
            set;
        }

        /// <summary>
        /// The text of this label.
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// The position of this label.
        /// </summary>
        public Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// The color of this label.
        /// </summary>
        public Color Color
        {
            get;
            set;
        }

        /// <summary>
        /// The rotation of this label, in radians.
        /// </summary>
        public float Rotation
        {
            get;
            set;
        }

        /// <summary>
        /// The origin of this label.
        /// </summary>
        public Vector2 Origin
        {
            get;
            set;
        }

        /// <summary>
        /// The scale of this label.
        /// </summary>
        public Vector2 Scale
        {
            get;
            set;
        }

        /// <summary>
        /// The SpriteEffects that will be applied to this label.
        /// </summary>
        public SpriteEffects Effects
        {
            get;
            set;
        }

        /// <summary>
        /// The layer depth of this label.
        /// </summary>
        public float LayerDepth
        {
            get;
            set;
        }

        #endregion Properties

        #region Initialization

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Label()
        {
            Color = Color.Black;
            Rotation = 0f;
            Scale = new Vector2(1f, 1f);
            Effects = SpriteEffects.None;
        }

        #endregion

        #region IDrawable

        public void Show()
        {
            GraphicsManager.Add(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }

        public void Hide()
        {
            GraphicsManager.Remove(this);
        }

        #endregion
    }
}
