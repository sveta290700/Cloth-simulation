using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class TensionForce : IForce
    {
        private ForceCoefficient forceCoefficient;
        ForceCoefficient IForce.forceCoefficient { get => forceCoefficient; set => forceCoefficient = value; }
        public Vector Apply(Point point)
        {
            Vector result = new Vector();
            for (int i = 0; i < point.connectedSprings.Count; i++)
            {
                Spring spring = point.connectedSprings[i];
                Vector normal = point.pos - spring.getCenter();
                float distance = spring.point0.distanceTo(spring.point1);
                distance -= spring.length;
                result += normal * distance * spring.tensionСoefficient * -1F;
            }
            return result;
        }
    }
}
