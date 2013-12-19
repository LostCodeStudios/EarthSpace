using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.Input.InputHandlers
{
    /// <summary>
    /// A mouse button.
    /// </summary>
    public enum MouseButton
    {
        Left,
        Right
    }

    /// <summary>
    /// Handles mouse click input.
    /// </summary>
    public class ClickHandler : InputHandler
    {
        #region Fields

        MouseButton button;
        Rectangle? clickArea;

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a ClickHandler.
        /// </summary>
        /// <param name="button"></param>
        public ClickHandler(MouseButton button)
        {
            this.button = button;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The area that the mouse must click in to trigger this handler.
        /// </summary>
        public Rectangle? ClickArea
        {
            get { return clickArea; }
            set { clickArea = value; }
        }

        #endregion

        #region InputHandler

        /// <summary>
        /// Checks whether this InputHandler is triggered.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override bool IsTriggered(InputState input)
        {
            bool clicked = false;

            switch (button)
            {
                case MouseButton.Left:
                    clicked = input.LeftClicked();
                    break;

                case MouseButton.Right:
                    clicked = input.RightClicked();
                    break;
            }

            if (clicked && clickArea.HasValue)
            {
                clicked = clickArea.Value.Contains(input.MousePosition);
            }

            return clicked;
        }

        #endregion
    }
}
