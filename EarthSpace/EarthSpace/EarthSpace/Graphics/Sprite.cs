﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EarthSpace.Graphics
{
    /// <summary>
    /// A sprite.
    /// </summary>
    public class Sprite : IDrawable
    {
        #region Properties

        /// <summary>
        /// The texture of the sprite.
        /// </summary>
        public Texture2D Texture
        {
            get;
            set;
        }

        /// <summary>
        /// The position of the sprite.
        /// </summary>
        public Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Source of the sprite on the texture
        /// </summary>
        public Rectangle? Source
        {
            get;
            set;
        }
        
        /// <summary>
        /// Color of the sprite.
        /// </summary>
        public Color Color
        {
            get;
            set;
        }

        /// <summary>
        /// Rotation of the sprite, in radians.
        /// </summary>
        public float Rotation
        {
            get;
            set;
        }

        /// <summary>
        /// Rotational/Transformational origin of the sprite.
        /// </summary>
        public Vector2 Origin
        {
            get;
            set;
        }

        /// <summary>
        /// The scale of the sprite as a Vector2
        /// </summary>
        public Vector2 Scale
        {
            get;
            set;
        }

        /// <summary>
        /// The effects with which the sprite is rendered.
        /// </summary>
        public SpriteEffects Effects
        {
            get;
            set;
        }

        /// <summary>
        /// Layer depth of the sprite.
        /// </summary>
        public float LayerDepth
        {
            get;
            set;
        }

        #endregion Properties

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Sprite()
        {
            Color = Color.White;
            Rotation = 0f;
            Scale = new Vector2(1f, 1f);
            Effects = SpriteEffects.None;
        }

        #region IDrawable

        public void Show()
        {
            GraphicsManager.Add(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Source, Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }

        public void Hide()
        {
            GraphicsManager.Remove(this);
        }

        #endregion IDrawable
    }
}
