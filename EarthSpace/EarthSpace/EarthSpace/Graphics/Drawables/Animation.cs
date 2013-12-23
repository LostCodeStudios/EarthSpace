using EarthSpace.Processing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.Graphics.Drawables
{
    /// <summary>
    /// An animated sprite.
    /// </summary>
    public class Animation : EarthSpace.Graphics.IDrawable, IProcess
    {
        #region Fields

        Sprite sprite;
        float frameTime;
        float elapsedTime;
        Rectangle[] frames;
        int currentFrame;

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a new Animation.
        /// </summary>
        /// <param name="frameTime"></param>
        /// <param name="frames"></param>
        public Animation(float frameTime, params Rectangle[] frames)
        {
            sprite = new Sprite();

            this.frameTime = frameTime;
            this.frames = frames;
        }

        /// <summary>
        /// Creates a new animation.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="frameTime"></param>
        /// <param name="source"></param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        public Animation(Texture2D texture, float frameTime, Rectangle source, int cols, int rows)
        {
            sprite = new Sprite();
            sprite.Texture = texture;

            this.frameTime = frameTime;

            List<Rectangle> frames = new List<Rectangle>();

            int frameWidth = source.Width / cols;
            int frameHeight = source.Height / rows;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Rectangle frame = new Rectangle(
                        source.X + frameWidth * c,
                        source.Y + frameHeight * r,
                        frameWidth,
                        frameHeight);

                    frames.Add(frame);
                }
            }

            this.frames = frames.ToArray();
        }

        /// <summary>
        /// Creates a new animation.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="frameTime"></param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        public Animation(Texture2D texture, float frameTime, int cols, int rows)
            : this(texture, frameTime, new Rectangle(0, 0, texture.Width, texture.Height), cols, rows)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// The texture of the sprite.
        /// </summary>
        public Texture2D Texture
        {
            get { return sprite.Texture; }
            set { sprite.Texture = value; }
        }

        /// <summary>
        /// The position of the sprite.
        /// </summary>
        public Vector2 Position
        {
            get { return sprite.Position; }
            set { sprite.Position = value; }
        }
        
        /// <summary>
        /// Color of the sprite.
        /// </summary>
        public Color Color
        {
            get { return sprite.Color; }
            set { sprite.Color = value; }
        }

        /// <summary>
        /// Rotation of the sprite, in radians.
        /// </summary>
        public float Rotation
        {
            get { return sprite.Rotation; }
            set { sprite.Rotation = value; }
        }

        /// <summary>
        /// Rotational/Transformational origin of the sprite.
        /// </summary>
        public Vector2 Origin
        {
            get { return sprite.Origin; }
            set { sprite.Origin = value; }
        }

        /// <summary>
        /// The scale of the sprite as a Vector2
        /// </summary>
        public Vector2 Scale
        {
            get { return sprite.Scale; }
            set { sprite.Scale = value; }
        }

        /// <summary>
        /// The effects with which the sprite is rendered.
        /// </summary>
        public SpriteEffects Effects
        {
            get { return sprite.Effects; }
            set { sprite.Effects = value; }
        }

        /// <summary>
        /// Layer depth of the sprite.
        /// </summary>
        public float LayerDepth
        {
            get { return sprite.LayerDepth; }
            set { sprite.LayerDepth = value; }
        }

        #endregion Properties

        #region IDrawable

        public void Show()
        {
            sprite.Show();
            sprite.Source = frames[currentFrame];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void Hide()
        {
            sprite.Hide();
        }

        #endregion

        #region IProcess

        public void Begin()
        {
            ProcessManager.Add(this);
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= frameTime)
            {
                elapsedTime -= frameTime;

                currentFrame = (currentFrame + 1) % frames.Length;

                sprite.Source = frames[currentFrame];
            }
        }

        public void End()
        {
            ProcessManager.Remove(this);
        }

        #endregion
    }
}
