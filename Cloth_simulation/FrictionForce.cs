using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class FrictionForce : IForce
    {
        private ForceCoefficient forceCoefficient;
        ForceCoefficient IForce.forceCoefficient { get => forceCoefficient; set => forceCoefficient = value; }

        public FrictionForce()
        {
            forceCoefficient = new ForceCoefficient(-0.01F, "Friction force");
        }

        public Vector Apply(Point point)
        {
            Vector result = point.getVelocity();
            result *= forceCoefficient.coefficient;
            return result;
        }
    }
}
