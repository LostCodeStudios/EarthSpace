using System;

namespace EarthSpace.Processing.Processes
{
    public class TransitionProcess : IProcess
    {
        /// <summary>
        /// Creates a new transition between two numbers in a recurrent process.
        /// </summary>
        /// <param name="regressionType">The type of regression to occur TransitionProcess.Linear, etc</param>
        /// <param name="start">The starting value for the transition, x_0</param>
        /// <param name="end">The ending value for the transition, x_1</param>
        /// <param name="duration">The range or duration of the transition in seconds</param>
        /// <param name="onStep">Every time the value changes, this onStep is called.</param>
        /// <param name="onEnd">Called when the processes has ended.</param>
        public TransitionProcess(Regression regressionType,
            float start, float end, float duration,
            Action<float> onStep, Action onEnd = null)
        {
            this.onStep = onStep;
            this.onEnd = onEnd;

            this.regression = regressionType;

            this.start = start;
            this.end = end;
            this.duration = duration;
        }

        #region Update

        /// <summary>
        /// Begins the Transition process.
        /// </summary>
        public void Begin()
        {
            ProcessManager.Add(this);
        }

        /// <summary>
        /// Updates the transition process by evaluating the new value at a given point relative to time..
        /// </summary>
        /// <param name="gameTime">The game time with which the regressive value for x will be determined.</param>
        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (x < duration)
            {
                x += gameTime.ElapsedGameTime.Milliseconds / 1000f;
                onStep(regression(start, end, duration, x));
            }
            else
                End();
        }

        /// <summary>
        /// Ends the Transition process.
        /// </summary>
        public void End()
        {
            if(onEnd != null)
                onEnd();
            ProcessManager.Remove(this);
        }

        #endregion Update

        #region Fields

        private Action<float> onStep;
        private Action onEnd;

        private Regression regression;

        private float start;
        private float end;
        private float x = 0;
        private float duration;

        #endregion Fields

        #region 'Tweenyboppers'

        /// <summary>
        /// The regression delegate for tweens of all sorts.
        /// </summary>
        public delegate float Regression(float start, float end, float domain, float x);

        /// <summary>
        /// The linear tween for transitions between two points.
        /// </summary>
        public static Regression Linear =
            (start, end, domain, x) =>
                ((end - start) / domain) * x + start;

        /// <summary>
        /// Derived from minimizing squared error between two points and equation y = kc^x.
        /// </summary>
        public static Regression Exponential =
            (start, end, domain, x) =>
            {
                float range = (float)Math.Abs(end - start);
                float eval = 1 * (float)Math.Pow((range +1) / 1, x / domain);
                return eval - 1 + start;
            };

        /// <summary>
        /// Returns a smooth step regression based on Ken Perlin's smoother step equation.
        /// Between x = 0 and x = 1, the equation is defined as y = 6t^5 -15t^4 + 10t^3
        /// </summary>
        public static Regression SmoothStep =
            (start, end, domain, x) =>
            {
                float px = x / domain;
                float eval = (float)(6 * Math.Pow(px, 5) - 15 * Math.Pow(px, 4) + 10 * Math.Pow(px, 3));

                return eval * (end - start) + start;
            };

        #endregion 'Tweenyboppers'
    }
}