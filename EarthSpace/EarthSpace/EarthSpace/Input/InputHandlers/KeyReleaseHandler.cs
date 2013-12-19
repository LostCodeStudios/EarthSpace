using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.Input.InputHandlers
{
    /// <summary>
    /// InputHandler that is triggered when one of any number of keys is released.
    /// </summary>
    public class KeyReleaseHandler : InputHandler
    {
        #region Fields

        List<Keys> keys = new List<Keys>();

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a new KeyReleaseHandler.
        /// </summary>
        /// <param name="keys"></param>
        public KeyReleaseHandler(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                this.keys.Add(key);
            }
        }

        #endregion

        #region InputHandler

        /// <summary>
        /// Checks whether this handler has been triggered.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override bool IsTriggered(InputState input)
        {
            foreach (Keys key in keys)
            {
                if (input.KeyReleased(key))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
