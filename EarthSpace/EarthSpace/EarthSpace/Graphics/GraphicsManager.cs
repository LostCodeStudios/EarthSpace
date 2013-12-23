using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EarthSpace.Graphics
{
    /// <summary>
    /// Manages all game rendering.
    /// </summary>
    public static class GraphicsManager
    {
        #region Fields

        private static SpriteBatch spriteBatch;
        private static GraphicsDeviceManager graphics;
        private static GraphicsDevice graphicsDevice;
        private static HashSet<IDrawable> drawables = new HashSet<IDrawable>();

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Initializes the GraphicsManager.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="graphics"></param>
        public static void Initialize(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            GraphicsManager.spriteBatch = spriteBatch;
            GraphicsManager.graphics = graphics;
            GraphicsManager.graphicsDevice = graphics.GraphicsDevice;

            GraphicsManager.BackgroundColor = Color.CornflowerBlue;
        }

        #endregion Initialization

        #region Graphics Properties

        /// <summary>
        /// The game viewport.
        /// </summary>
        public static Viewport Viewport
        {
            get { return graphicsDevice.Viewport; }
        }

        /// <summary>
        /// The game's background color.
        /// </summary>
        public static Color BackgroundColor
        {
            get;
            set;
        }

        #endregion Graphics Properties

        #region Drawable Management

        /// <summary>
        /// Registers an IDrawable to be drawn.
        /// </summary>
        /// <param name="drawable"></param>
        public static void Add(IDrawable drawable)
        {
            drawables.Add(drawable);
        }

        /// <summary>
        /// Deregisters an IDrawable so it will not be drawn.
        /// </summary>
        /// <param name="drawable"></param>
        public static void Remove(IDrawable drawable)
        {
            drawables.Remove(drawable);
        }

        /// <summary>
        /// Clears all IDrawables.
        /// </summary>
        public static void Clear()
        {
            drawables.Clear();
        }

        /// <summary>
        /// Check whether the given IDrawable is registered to the GraphicsManager.
        /// </summary>
        /// <param name="drawable"></param>
        /// <returns></returns>
        public static bool IsVisible(IDrawable drawable)
        {
            return drawables.Contains(drawable);
        }

        #endregion Drawable Management

        #region Rendering

        /// <summary>
        /// Draws every registered IDrawable.
        /// </summary>
        public static void Draw()
        {
            graphicsDevice.Clear(BackgroundColor);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            foreach (IDrawable drawable in drawables)
            {
                drawable.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        #endregion Rendering
    }
}