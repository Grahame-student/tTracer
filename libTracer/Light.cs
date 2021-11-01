using System;

namespace libTracer
{
    public class Light
    {
        public Light(TPoint position, TColour intensity)
        {
            Intensity = intensity;
            Position = position;
        }

        public TPoint Position { get; set; }
        public TColour Intensity { get; }
    }
}
