using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthSpace.Processing.Processes
{
    /// <summary>
    /// The recurrent delay process is similar to an interval based update loop, except for its intrinsic ability to run on finite occurrences.
    /// </summary>
    public class RecurrentDelayProcess : DelayProcess
    {
        /// <summary>
        /// Initializes a recurrent delay process.
        /// </summary>
        /// <param name="delayTime">The amount of delay per occurrence</param>
        /// <param name="onOccur">What happens when an event occurs.</param>
        /// <param name="onEnd">What happens when the occurrence process ends.</param>
        /// <param name="occurences">How many occurrences for which the process will run. -1 for infinite recurrence.</param>
        public RecurrentDelayProcess(float delayTime,
            Action onOccur, Action onEnd, int occurences = -1)
            : base(delayTime, onEnd)
        {
            this.occurences = occurences;
            this.onOccur = onOccur;
        }

        #region Fields
        int currentOccurences = 0;
        int occurences = 0;
        Action onOccur;
        #endregion Fields

        #region Update
        /// <summary>
        /// Called on each occurrences based on the delay processes code.
        /// </summary>
        public override void  End()
        {
            if (currentOccurences <= occurences || occurences == -1)
            {
                currentOccurences++;
                onOccur.Invoke();

            }
            else
                base.End();
        }

        #endregion Fields
    }
}
