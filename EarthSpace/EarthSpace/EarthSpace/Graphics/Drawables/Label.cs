using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EarthSpace.Processing.Processes;
using System.Collections.Generic;

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

        #endregion Initialization

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

        #endregion IDrawable

        #region Helpers

        /// <summary>
        /// Returns the measurements of this label's text.
        /// </summary>
        /// <returns></returns>
        public Vector2 MeasureText()
        {
            return Font.MeasureString(Text) * Scale;
        }

        /// <summary>
        /// Returns a Rectangle representing this label's screen rectangle.
        /// </summary>
        /// <returns></returns>
        public Rectangle ScreenArea()
        {
            Vector2 size = MeasureText();

            return new Rectangle(
                (int)(Position.X - size.X / 2),
                (int)(Position.Y - size.Y / 2),
                (int)size.X, (int)size.Y);
        }

        #endregion Helpers

        #region Transitions

        List<TransitionProcess> transitions = new List<TransitionProcess>();

        /// <summary>
        /// Stops all transitions.
        /// </summary>
        public void ClearTransitions()
        {
            foreach (TransitionProcess transition in transitions)
            {
                transition.End();
            }

            transitions.Clear();
        }

        /// <summary>
        /// Tweens a label to a new position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="regressionType"></param>
        /// <param name="delay"></param>
        public void Reposition(Vector2 position, TransitionProcess.Regression regressionType, float delay)
        {
            Vector2 start = Position;
            
            TransitionProcess movementProcess = new TransitionProcess(regressionType, 0, 1, delay,
                (x) =>
                {
                    Vector2 offset = (position - start) * x;
                    Position = start + offset;
                });

            movementProcess.Begin();

            transitions.Add(movementProcess);
        }

        /// <summary>
        /// Tweens a label to a new color.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="regressionType"></param>
        /// <param name="delay"></param>
        public void Recolor(Color color, TransitionProcess.Regression regressionType, float delay)
        {
            Color start = Color;

            TransitionProcess colorChangeProcess = new TransitionProcess(regressionType, 0, 1, delay,
                (x) =>
                {
                    Color = Color.Lerp(start, color, x);
                });

            colorChangeProcess.Begin();

            transitions.Add(colorChangeProcess);
        }

        /// <summary>
        /// Tweens a label to a new scale.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="regressionType"></param>
        /// <param name="delay"></param>
        public void Rescale(Vector2 scale, TransitionProcess.Regression regressionType, float delay)
        {
            Vector2 start = Scale;

            TransitionProcess scaleChangeProcess = new TransitionProcess(regressionType, 0, 1, delay,
                (x) =>
                {
                    Vector2 offset = (scale - start) * x;
                    Scale = start + offset;
                });

            scaleChangeProcess.Begin();

            transitions.Add(scaleChangeProcess);
        }

        #endregion
    }
}