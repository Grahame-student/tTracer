using libTracer.Common;
using libTracer.Shapes;

namespace libTracer.Scene.Patterns
{
    public abstract class Pattern
    {
        public TMatrix Transform { get; set; }

        protected Pattern()
        {
            Transform = new TMatrix();
        }

        public TColour ColourAt(Shape shape, TPoint point)
        {
            TPoint objectPoint = shape.Transform.Inverse() * point;
            TPoint patternpoint = Transform.Inverse() * objectPoint;
            return LocalColourAt(shape, patternpoint);
        }

        protected abstract TColour LocalColourAt(Shape shape, TPoint point);
    }
}
