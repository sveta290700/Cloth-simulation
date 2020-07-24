using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class GravityForce : IForce
    {
        public const float gravity = 10F;
        public Vector Apply(Point point)
        {
            Vector result = new Vector();
            result.z += point.mass * gravity;
            return result;
        }
    }
}
