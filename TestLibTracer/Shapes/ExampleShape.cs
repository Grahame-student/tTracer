using System.Collections.Generic;
using libTracer.Common;
using libTracer.Scene;
using libTracer.Shapes;

namespace TestLibTracer.Shapes;

internal class ExampleShape : Shape
{
    protected override TVector LocalNormal(TPoint point)
    {
        throw new System.NotImplementedException();
    }

    protected override IList<Intersection> LocalIntersects(TRay ray)
    {
        throw new System.NotImplementedException();
    }
}
