using System;
using libTracer.Common;
using libTracer.Scene.Patterns;
using libTracer.Shapes;

namespace libTracer.Scene
{
    public class Material : IEquatable<Material>
    {
        public TColour Colour { get; set; }
        public Pattern Pattern { get; set; }
        public Double Ambient { get; set; }
        public Double Diffuse { get; set; }
        public Double Specular { get; set; }
        public Double Shininess { get; set; }
        public Double Reflective { get; set; }
        public Double Transparency { get; set; }
        public Double RefractiveIndex { get; set; }

        public Material()
        {
            Colour = new TColour(1, 1, 1);
            Ambient = 0.1;
            Diffuse = 0.9;
            Specular = 0.9;
            Shininess = 200;
            Reflective = 0.0;
            Transparency = 0.0;
            RefractiveIndex = 1.0;
        }

        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals((Material)obj);
        }

        public Boolean Equals(Material other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Colour, other.Colour) &&
                   Ambient.Equals(other.Ambient) &&
                   Diffuse.Equals(other.Diffuse) &&
                   Specular.Equals(other.Specular) &&
                   Shininess.Equals(other.Shininess);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Colour);
        }

        public TColour Lighting(Shape shape, Light light, TPoint position, TVector eye, TVector normal, Boolean inShadow)
        {
            TColour diffuse;
            TColour specular;

            TColour colour = Pattern?.ColourAt(shape, position) ?? Colour;

            TColour effectiveColour = colour * light.Intensity;
            TVector lightV = (light.Position - position).Normalise();
            TColour ambient = effectiveColour * Ambient;
            Double lightDotNormal = lightV.Dot(normal);
            if (lightDotNormal < 0 || inShadow)
            {
                diffuse = new TColour(0, 0, 0);
                specular = new TColour(0, 0, 0);
            }
            else
            {
                diffuse = effectiveColour * Diffuse * lightDotNormal;
                TVector reflectV = -lightV.Reflect(normal);
                Double reflectDotEye = reflectV.Dot(eye);
                if (reflectDotEye <= 0)
                {
                    specular = new TColour(0, 0, 0);
                }
                else
                {
                    Double factor = Math.Pow(reflectDotEye, Shininess);
                    specular = light.Intensity * Specular * factor;
                }
            }

            return ambient + diffuse + specular;
        }
    }
}
