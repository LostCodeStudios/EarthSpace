using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EarthSpace.Processing
{
    /// <summary>
    /// Manages recurring processes in the game.
    /// </summary>
    public static class ProcessManager
    {
        #region Fields

        static HashSet<IProcess> processes = new HashSet<IProcess>();

        #endregion

        #region Process Management

        /// <summary>
        /// Registers a process to the ProcessManager.
        /// </summary>
        /// <param name="process"></param>
        public static void Add(IProcess process)
        {
            processes.Add(process);
        }

        /// <summary>
        /// Deregisters a process from the ProcessManager.
        /// </summary>
        /// <param name="process"></param>
        public static void Remove(IProcess process)
        {
            processes.Remove(process);
        }

        /// <summary>
        /// Clears all processes from the ProcessManager.
        /// </summary>
        public static void Clear()
        {
            processes.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <returns>Whether the given process is registered to the ProcessManager.</returns>
        public static bool IsRunning(IProcess process)
        {
            return processes.Contains(process);
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates all registered processes.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < processes.Count; i++ )
            {
                processes.ElementAt(i).Update(gameTime);
            }
        }

        #endregion
    }
}
