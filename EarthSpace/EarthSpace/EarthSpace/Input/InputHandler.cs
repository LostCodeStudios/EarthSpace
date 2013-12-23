namespace EarthSpace.Input
{
    /// <summary>
    /// An event triggered by an InputHandler.
    /// </summary>
    /// <param name="input"></param>
    public delegate void InputEvent(InputState input);

    /// <summary>
    /// An object that performs an event when its input criteria are met.
    /// </summary>
    public abstract class InputHandler
    {
        /// <summary>
        /// Whether this InputHandler is enabled.
        /// </summary>
        public bool Enabled
        {
            get;
            private set;
        }

        /// <summary>
        /// Event called when this InputHandler is triggered.
        /// </summary>
        public InputEvent OnTrigger;

        /// <summary>
        /// Checks whether this input handler's event should be triggered.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract bool IsTriggered(InputState input);

        /// <summary>
        /// Enables this InputHandler.
        /// </summary>
        public void Enable()
        {
            Enabled = true;
            InputManager.Add(this);
        }

        /// <summary>
        /// Marks this InputHandler to be disabled by the InputManager.
        /// </summary>
        public void Disable()
        {
            Enabled = false;
        }
    }
}