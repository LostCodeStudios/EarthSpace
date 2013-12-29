using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EarthSpace.Gameplay
{
    /// <summary>
    /// A circle on a 2D cartesian plane.
    /// </summary>
    public struct Circle
    {
        /// <summary>
        /// The position of the circle's center.
        /// </summary>
        public Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// The size of the circle's radius.
        /// </summary>
        public float Radius
        {
            get;
            set;
        }

        /// <summary>
        /// Checks whether the circle is intersecting another circle.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Intersects(Circle other)
        {
            return Vector2.Distance(Position, other.Position) < Radius + other.Radius;
        }
    }
}
