using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace libTracer
{
    public class Intersection : IComparable<Intersection>
    {
        public Single Time { get; }
        public Shape Shape { get; }

        public Intersection(Single time, Shape shape)
        {
            Time = time;
            Shape = shape;
        }

        public Int32 CompareTo(Intersection other)
        {
            if (Time < other.Time) return -1;
            if (Time > other.Time) return 1;
            return 0;
        }

        /// <summary>
        /// Return the intersection with lowest non-negative time.
        /// </summary>
        /// <param name="list"></param>
        /// <returns>The first intersection with lowest non-negative time. Returns null if no intersections meet the criteria</returns>
        public static Intersection Hit(IList<Intersection> list)
        {
            return list.FirstOrDefault(intersection => !(intersection.Time < 0));
        }
    }
}
