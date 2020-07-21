using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Cloth_simulation
{
    public class Environment
    {
        public static int point_size = 2;

        public static int spring_size = 1;

        public static Point[] PointsArray = new Point[point_size];

        public static Spring[] SpringsArray = new Spring[spring_size];
        public static void InputData()
        {
            InputPoints();
            InputSprings();
        }
        public static void InputPoints()
        {
            Point point0 = new Point(100, 100, 0, 2);
            PointsArray[0] = point0;
            Point point1 = new Point(200, 100, 0, 2, false);
            PointsArray[1] = point1;
        }
        public static void InputSprings()
        {
            Spring spring0 = new Spring(PointsArray[0], PointsArray[1]);
            SpringsArray[0] = spring0;
        }

        public void Tick()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Thread.Sleep(100);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            double dt = ts.TotalMilliseconds;
            int timesteps = (int)Math.Floor(dt / 16);
            for (int i = 0; i < timesteps; i++)
            {
                TickPoints(PointsArray, dt);
                TickSprings(SpringsArray);
            }
        }
        public void TickPoints(Point[] PointsArray, double dt)
        {
            for (int i = 0; i < point_size; i++)
            {
                PointsArray[i].updatePoint(PointsArray[i], dt);
                PointsArray[i].constrainPoint(PointsArray[i]);
            }
        }
        public void TickSprings(Spring[] SpringsArray)
        {
            for (int i = 0; i < point_size; i++)
            {
                SpringsArray[i].updateSpring(SpringsArray[i]);
            }
        }
        
    }
}
