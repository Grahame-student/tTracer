using libTracer.Common;

namespace demoProjectile;

internal class WorldEnvironment
{
    public TVector Gravity { get; }
    public TVector Wind { get; }

    public WorldEnvironment(TVector gravity, TVector wind)
    {
        Gravity = gravity;
        Wind = wind;
    }
}