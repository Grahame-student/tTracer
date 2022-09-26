using System;
using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;

namespace libTracer.Shapes
{
    public abstract class Shape : IEquatable<Shape>
    {
        private readonly Lazy<TMatrix> _inverse;

        public TMatrix Transform { get; set; }
        public Material Material { get; set; }
        public Shape Parent { get; set; }
    
        public TMatrix Inverse => _inverse.Value;

        protected Shape()
        {
            Transform = new TMatrix();
            Material = new Material();
            _inverse = new Lazy<TMatrix>(() => Transform.Inverse());
        }

        public TVector Normal(TPoint point)
        {
            TPoint objectPoint = Inverse * point;
            TVector objectNormal = LocalNormal(objectPoint);
            TVector worldNormal = Inverse.Transpose() * objectNormal;
            return worldNormal.Normalise();
        }

        protected abstract TVector LocalNormal(TPoint point);

        public IList<Intersection> Intersects(TRay ray)
        {
            // Rather than translate objects to world space we translate the ray instead
            TRay transformedRay = ray.Transform(Transform.Inverse());
            return LocalIntersects(transformedRay);
        }

        protected abstract IList<Intersection> LocalIntersects(TRay ray);

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
