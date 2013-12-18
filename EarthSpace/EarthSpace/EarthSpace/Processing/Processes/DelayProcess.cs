using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.Processing.Processes
{
    public sealed class DelayProcess : IProcess
    {
        /// <summary>
        /// Creates a new delay process.
        /// </summary>
        /// <param name="delayTime">The delay time in seconds.</param>
        /// <param name="onEnd">The action to be called, when the delay time is reached/ </param>
        public DelayProcess(float delayTime, Action onEnd)
        {
            this.delayTime = delayTime;
            this.onEnd = onEnd;
        }

        #region Fields

        float delayTime;
        float currentTime = 0;
        Action onEnd;

        #endregion Fields 

        #region Update
        public void Begin()
        {
            ProcessManager.Add(this);
        }

        /// <summary>
        /// Updates the dleay processes. When the delay time is equal to the current time, invoke onEnd.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            currentTime += gameTime.ElapsedGameTime.Seconds;
            if (currentTime > delayTime)
            {
                onEnd.Invoke();
                End();
            }
        }

        public void End()
        {
            ProcessManager.Remove(this);
        }
        #endregion
    }
}
