using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cloth_simulation
{
    class Stick
    {
        public Point p0;

        public Point p1;

        public double length;
        public static double Distance(Point p0, Point p1)
        {
            double dx = p1.x - p0.x;
            double dy = p1.y - p0.y;
            double res = Math.Sqrt(dx * dx + dy * dy);
            return res;
        }
        public Stick(Point newp0, Point newp1)
        {
            p0 = newp0;
            p1 = newp1;
            length = Distance(newp0, newp1);
        }
    }
}
