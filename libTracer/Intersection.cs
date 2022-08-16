﻿using System;
using System.Collections.Generic;
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

        public Computations PrepareComputations(TRay ray)
        {
            var result = new Computations
            {
                Time = Time,
                Object = Shape
            };
            result.Point = ray.Position(result.Time);
            result.EyeV = -ray.Direction;
            result.NormalV = result.Object.Normal(result.Point);
            if (!(result.NormalV.Dot(result.EyeV) < 0)) return result;

            result.Inside = true;
            result.NormalV = -result.NormalV;
            return result;
        }
    }
}
