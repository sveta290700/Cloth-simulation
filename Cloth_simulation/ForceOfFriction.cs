using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class ForceOfFriction : IForce
    {
        public const double friction = 0.999;
        public Vector Apply(Point point)
        {
            Vector result = new Vector(point.vel.x, point.vel.y, point.vel.z);
            result *= friction;
            return result;
        }
    }
}
