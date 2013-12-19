using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.Input.InputHandlers
{
    /// <summary>
    /// InputHandler that is triggered when one of any number of keys is pressed.
    /// </summary>
    public class KeyPressHandler : InputHandler
    {
        #region Fields

        List<Keys> keys = new List<Keys>();

        #endregion

        #region Initialization

        /// <summary>
        /// Creates a new KeyPressHandler.
        /// </summary>
        /// <param name="keys"></param>
        public KeyPressHandler(params Keys[] keys)
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
                if (input.KeyPressed(key))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
