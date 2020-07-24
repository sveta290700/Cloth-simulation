using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class TensionForce : IForce
    {
        public Vector Apply(Point point)
        {
            Vector result = new Vector();
            for (int i = 0; i < point.connectedSprings.Count; i++)
            {
                Spring spring = point.connectedSprings[i];
                float distance = spring.point0.distanceTo(spring.point1);
                distance -= spring.length;
                result += new Vector(distance, distance, distance) * spring.TensionСoefficient * -1;
            }
            return result;
        }
    }
}
