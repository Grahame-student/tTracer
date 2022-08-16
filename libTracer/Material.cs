using System;

namespace libTracer
{
    public class Material : IEquatable<Material>
    {
        public TColour Colour { get; set; }
        public Single Ambient { get; set; }
        public Single Diffuse { get; set; }
        public Single Specular { get; set; }
        public Single Shininess { get; set; }

        public Material()
        {
            Colour = new TColour(1, 1, 1);
            Ambient = 0.1f;
            Diffuse = 0.9f;
            Specular = 0.9f;
            Shininess = 200f;
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

        public TColour Lighting(Light light, TPoint position, TVector eye, TVector normal)
        {
            TColour diffuse;
            TColour specular;
            TColour effectiveColour = Colour * light.Intensity;
            TVector lightV = (light.Position - position).Normalise();
            TColour ambient = effectiveColour * Ambient;
            Single lightDotNormal = lightV.Dot(normal);
            if (lightDotNormal < 0)
            {
                diffuse = new TColour(0, 0, 0);
                specular = new TColour(0, 0, 0);
            }
            else
            {
                diffuse = effectiveColour * Diffuse * lightDotNormal;
                TVector reflectV = -lightV.Reflect(normal);
                Single reflectDotEye = reflectV.Dot(eye);
                if (reflectDotEye <= 0)
                {
                    specular = new TColour(0, 0, 0);
                }
                else
                {
                    Single factor = MathF.Pow(reflectDotEye, Shininess);
                    specular = light.Intensity * Specular * factor;
                }
            }

            return ambient + diffuse + specular;
        }
    }
}
