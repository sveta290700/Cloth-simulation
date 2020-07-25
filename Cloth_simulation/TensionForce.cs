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
                float normalLength = (float)Math.Sqrt((normal.x * normal.x) + (normal.y * normal.y) + (normal.z * normal.z));
                normal /= normalLength;
                float distance = spring.point0.distanceTo(spring.point1);
                distance -= spring.length;
                Vector force = normal * distance * spring.tensionСoefficient * -1F;
                if (!spring.point0.pinned && !spring.point1.pinned)
                {
                    force /= 2;
                }
                result += force;
            }
            return result;
        }
    }
}
