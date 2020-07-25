using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class GravityForce : IForce
    {
        private ForceCoefficient forceCoefficient;
        ForceCoefficient IForce.forceCoefficient { get => forceCoefficient; set => forceCoefficient = value; }
        public GravityForce()
        {
            forceCoefficient = new ForceCoefficient(5F, "Gravity force");
        }
        public Vector Apply(Point point)
        {
            Vector result = new Vector();
            result.z += point.mass * forceCoefficient.coefficient;
            return result;
        }
    }
}
