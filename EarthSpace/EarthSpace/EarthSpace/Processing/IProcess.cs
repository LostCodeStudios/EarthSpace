using Microsoft.Xna.Framework;

namespace EarthSpace.Processing
{
    /// <summary>
    /// A recurring process.
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Begins the process. Update() will now be called every tick.
        /// </summary>
        void Begin();

        /// <summary>
        /// Updates the process.
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Ends the process. Update() will no longer be called every tick.
        /// </summary>
        void End();
    }
}