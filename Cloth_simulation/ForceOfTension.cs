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
            for (int i = 0; i < Environment.spring_size; i++)
            {
                if (Environment.SpringsArray[i].point0 == point || Environment.SpringsArray[i].point1 == point)
                {
                    double distance = Environment.SpringsArray[i].point0.distanceTo(Environment.SpringsArray[i].point1);
                    distance -= Environment.SpringsArray[i].getLength();
                    Vector result = new Vector(distance, distance, distance) * Environment.SpringsArray[i].tension_koeff;
                    return result;
                }
            }
            Vector emptyResult = new Vector();
            return emptyResult;
        }
    }
}
