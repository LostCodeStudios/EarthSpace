using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.Input
{
    /// <summary>
    /// Handles the game's input.
    /// </summary>
    public static class InputManager
    {
        #region Fields

        static InputState input;
        static HashSet<InputHandler> handlers = new HashSet<InputHandler>();

        #endregion Fields

        #region Initialization

        /// <summary>
        /// Initializes the InputManager.
        /// </summary>
        public static void Initialize()
        {
            input = new InputState();
        }

        #endregion

        #region Input Handler Management

        /// <summary>
        /// Registers an InputHandler to the InputManager.
        /// </summary>
        /// <param name="handler"></param>
        public static void Add(InputHandler handler)
        {
            handlers.Add(handler);
        }

        /// <summary>
        /// Deregisters an InputHandler from the InputManager.
        /// </summary>
        /// <param name="handler"></param>
        public static void Remove(InputHandler handler)
        {
            handlers.Remove(handler);
        }

        /// <summary>
        /// Clears all InputHandlers from the InputManager.
        /// </summary>
        public static void Clear()
        {
            handlers.Clear();
        }

        #endregion Input Handler Management

        #region Update

        /// <summary>
        /// Handles all input.
        /// </summary>
        public static void Update()
        {
            input.Update();

            for (int i = handlers.Count() - 1; i >= 0; i--)
            {
                InputHandler handler = handlers.ElementAt(i);

                if (!handler.Enabled)
                {
                    handlers.Remove(handler); //Remove handlers that have been disabled.
                    continue;
                }

                if (handler.IsTriggered(input))
                {
                    if (handler.OnTrigger != null)
                    {
                        handler.OnTrigger(input); //Trigger appropriate handlers
                    }
                }
            }
        }

        #endregion Update
    }
}
