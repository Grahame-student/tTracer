using System;
using System.Collections.Generic;

namespace libTracer
{
    public abstract class Shape : IEquatable<Shape>
    {
        public TMatrix Transform { get; set; }
        public Material Material { get; set; }

        public abstract TVector Normal(TPoint point);
        public abstract IList<Intersection> Intersects(TRay ray);

        public Boolean Equals(Shape other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Transform, other.Transform) && Equals(Material, other.Material);
        }

        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Shape)obj);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Transform, Material);
        }
    }
}
