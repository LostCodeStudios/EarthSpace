using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.Input.InputHandlers
{
    /// <summary>
    /// Triggered when the mouse moves over an area.
    /// </summary>
    public class MouseHoverHandler : InputHandler
    {
        Rectangle hoverArea;

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
