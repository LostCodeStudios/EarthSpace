using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace EarthSpace.Graphics
{
    /// <summary>
    /// An object than can be drawn.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Registers the Drawable to call Draw() with the world's Draw loop.
        /// </summary>
        void Show();

        /// <summary>
        /// Draws this Drawable.
        /// </summary>
        /// <param name="spriteBatch"></param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Deregisters the Drawable to stop calling Draw() with the world's Draw loop.
        /// </summary>
        void Hide();
    }
}
