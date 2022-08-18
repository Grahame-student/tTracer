using libTracer.Common;

namespace libTracer.Scene.Patterns
{
    public class Solid : Pattern
    {
        private TColour Colour { get; set; }

        public Solid(TColour colour)
        {
            Colour = colour;
        }

        protected override TColour LocalColourAt(TPoint point)
        {
            return Colour;
        }
    }
}
