using System;
using libTracer.Common;
using libTracer.Scene;

namespace demoProjectile;

internal class Program
{
    static void Main(String[] args)
    {
        var start = new TPoint(0, 1, 0);
        TVector velocity = new TVector(1, 1.8, 0).Normalise() * 11.25;
        var projectile = new Projectile(start, velocity);

        var gravity = new TVector(0, -0.1, 0);
        var wind = new TVector(-0.01, 0, 0);
        var env = new WorldEnvironment(gravity, wind);

        var canvas = new Canvas(900, 550);
        var colour = new TColour(255, 255, 255);

        canvas.SetPixel((Int32)projectile.Position.X, (Int32)projectile.Position.Y, colour);
        while (projectile.Position.Y > 0)
        {
            canvas.SetPixel((Int32)projectile.Position.X, (Int32)projectile.Position.Y, colour);
            projectile = Tick(projectile, env);
        }

        var write = new BitmapWriter();
        write.SaveToBitmap(canvas, @"result.png");
    }

    public static Projectile Tick(Projectile projectile, WorldEnvironment env)
    {
        TPoint position = projectile.Position + projectile.Velocity;
        TVector velocity = projectile.Velocity + env.Gravity + env.Wind;
        return new Projectile(position, velocity);
    }
}