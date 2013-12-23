using Microsoft.Xna.Framework;

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

        private MouseButton button;
        private Rectangle? clickArea;

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Creates a ClickHandler.
        /// </summary>
        /// <param name="button"></param>
        public ClickHandler(MouseButton button)
        {
            this.button = button;
        }

        #endregion Initialization

        #region Properties

        /// <summary>
        /// The area that the mouse must click in to trigger this handler.
        /// </summary>
        public Rectangle? ClickArea
        {
            get { return clickArea; }
            set { clickArea = value; }
        }

        #endregion Properties

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

        #endregion InputHandler
    }
}