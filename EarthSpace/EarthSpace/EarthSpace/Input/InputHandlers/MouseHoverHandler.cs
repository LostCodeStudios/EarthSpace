using Microsoft.Xna.Framework;

namespace EarthSpace.Input.InputHandlers
{
    /// <summary>
    /// Triggered when the mouse moves over an area.
    /// </summary>
    public class MouseHoverHandler : InputHandler
    {
        private Rectangle hoverArea;

        /// <summary>
        /// Creates a MouseHoverHandler.
        /// </summary>
        /// <param name="hoverArea"></param>
        public MouseHoverHandler(Rectangle hoverArea)
        {
            this.hoverArea = hoverArea;
        }

        public override bool IsTriggered(InputState input)
        {
            return hoverArea.Contains(input.MousePosition) && !hoverArea.Contains(input.LastMousePosition);
        }
    }
}