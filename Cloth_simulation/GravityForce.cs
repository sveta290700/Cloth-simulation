using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class GravityForce : IForce
    {
        public const double gravity = -0.5;
        public Vector Apply(Point point)
        {
            Vector result = new Vector(0,0, point.pos.z);
            result.z += point.mass * gravity;
            return result;
        }
    }
}
