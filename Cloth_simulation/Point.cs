using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloth_simulation
{
    public class Point
    {
        public double x;

        public double y;

        public double oldx;

        public double oldy;

        public bool pin;

        public Point(double newx, double newy, double initial_velocity, bool newpin)
        {
            x = newx;
            y = newy;
            oldx = newx - initial_velocity;
            oldy = newy - initial_velocity;
            pin = newpin;
        }
    }
}
