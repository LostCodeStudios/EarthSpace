using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.Input
{
    /// <summary>
    /// Contains a snapshot of input information.
    /// </summary>
    public class InputState
    {
        #region Fields

        KeyboardState keyState;
        KeyboardState lastKeyState;

        MouseState mouseState;
        MouseState lastMouseState;

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Creates a new InputState.
        /// </summary>
        public InputState()
        {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            lastKeyState = keyState;
            lastMouseState = mouseState;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The current state of the keyboard.
        /// </summary>
        public KeyboardState KeyState
        {
            get { return keyState; }
        }

        /// <summary>
        /// The previous state of the keyboard.
        /// </summary>
        public KeyboardState LastKeyState
        {
            get { return lastKeyState; }
        }

        /// <summary>
        /// The current state of the mouse.
        /// </summary>
        public MouseState MouseState
        {
            get { return mouseState; }
        }

        /// <summary>
        /// The previous state of the mouse.
        /// </summary>
        public MouseState LastMouseState
        {
            get { return lastMouseState; }
        }

        #endregion Properties

        #region Update

        /// <summary>
        /// Reads new input.
        /// </summary>
        public void Update()
        {
            lastKeyState = keyState;
            lastMouseState = mouseState;

            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        #endregion Update

        #region Keyboard Helpers

        /// <summary>
        /// Checks whether a key was pressed in the last frame.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyPressed(Keys key)
        {
            return keyState.IsKeyDown(key) && lastKeyState.IsKeyUp(key);
        }

        /// <summary>
        /// Checks whether a key was released in the last frame.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyReleased(Keys key)
        {
            return keyState.IsKeyUp(key) && lastKeyState.IsKeyDown(key);
        }

        #endregion

        #region Mouse Helpers

        /// <summary>
        /// Checks whether the left mouse button was clicked in the last frame.
        /// </summary>
        /// <returns></returns>
        public bool LeftClicked()
        {
            return mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Checks whether the left mouse button was released in the last frame.
        /// </summary>
        /// <returns></returns>
        public bool LeftClickReleased()
        {
            return mouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Checks whether the right mouse button was clicked in the last frame.
        /// </summary>
        /// <returns></returns>
        public bool RightClicked()
        {
            return mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// Checks whether the right mouse button was released in the last frame.
        /// </summary>
        /// <returns></returns>
        public bool RightClickReleased()
        {
            return mouseState.RightButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// The position of the mouse.
        /// </summary>
        public Point MousePosition
        {
            get
            {
                return new Point(mouseState.X, mouseState.Y);
            }
        }

        #endregion
    }
}
