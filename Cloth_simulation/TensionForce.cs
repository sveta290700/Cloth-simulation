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
            for (int i = 0; i < 4; i++)
            {
                double distance = point.connectedSprings[i].point0.distanceTo(point.connectedSprings[i].point1);
                distance -= point.connectedSprings[i].getLength();
                result += new Vector(distance, distance, distance) * point.connectedSprings[i].tension_сoefficient;
            }
            return result;
        }
    }
}
