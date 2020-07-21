using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class ForceOfTension : IForce
    {
        public Vector Apply(Point point)
        {
            Vector result = new Vector();
            for (int i = 0; i < 4; i++)
            {
                double distance = point.springsArray[i].point0.distanceTo(point.springsArray[i].point1);
                distance -= point.springsArray[i].getLength();
                result += new Vector(distance, distance, distance) * point.springsArray[i].tension_сoeff;
            }
            return result;
        }
    }
}
