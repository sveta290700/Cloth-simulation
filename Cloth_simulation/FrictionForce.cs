using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class FrictionForce : IForce
    {
        public const float friction = -0.01F;
        public Vector Apply(Point point)
        {
            Vector result = point.getVelocity();
            result *= friction;
            return result;
        }
    }
}
