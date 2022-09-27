using libTracer.Common;

namespace libTracer.Shapes;

public class Bounds
{
    public TPoint Minimum { get; }
    public TPoint Maximum { get; }

    public Bounds(TPoint minimum, TPoint maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }
}
