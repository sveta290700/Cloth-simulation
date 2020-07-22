using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class GravityForce : IForce
    {
        public const double gravity = -9.81;
        public Vector Apply(Point point)
        {
            Vector result = new Vector();
            result.z += point.mass * gravity;
            return result;
        }
    }
}
