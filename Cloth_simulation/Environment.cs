using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Cloth_simulation
{
    public class Environment
    {
        public static double dt = 0;

        public static int point_size = 2;

        public static Point[] PointsArray = new Point[point_size];

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
            PointsArray[0].springsArray[0] = new Spring(PointsArray[0], PointsArray[1]);
            PointsArray[1].springsArray[0] = new Spring(PointsArray[0], PointsArray[1]);
        }
        public void Tick()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < dt; i++)
            {
                TickPoints(PointsArray, dt);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            dt = ts.TotalMilliseconds;
        }
        public void TickPoints(Point[] PointsArray, double dt)
        {
            for (int i = 0; i < point_size; i++)
            {
                PointsArray[i].updatePoint(dt);
                TickSprings(PointsArray[i].springsArray);
                constrainPoint(PointsArray[i]);
            }
        }
        public void TickSprings(Spring[] SpringsArray)
        {
            for (int i = 0; i < 4; i++)
            {
                SpringsArray[i].updateSpring();
            }
        }
        public void constrainPoint(Point point)
        {
            if (!point.pinned)
            {
                if (point.pos.x > GUI.width)
                {
                    point.pos.x = GUI.width;
                    if (point.vel.x > 0)
                    {
                        point.vel.x *= -1;
                    }
                }
                else if (point.pos.x < 0)
                {
                    point.pos.x = 0;
                    if (point.vel.x < 0)
                    {
                        point.vel.x *= -1;
                    }
                }

                if (point.pos.y > GUI.width)
                {
                    point.pos.y = GUI.width;
                    if (point.vel.y > 0)
                    {
                        point.vel.y *= -1;
                    }
                }
                else if (point.pos.y < 0)
                {
                    point.pos.y = 0;
                    if (point.vel.y < 0)
                    {
                        point.vel.y *= -1;
                    }
                }

                if (point.pos.z > GUI.height)
                {
                    point.pos.z = GUI.height;
                    if (point.vel.z > 0)
                    {
                        point.vel.z *= -1;
                    }
                }
                else if (point.pos.z < 0)
                {
                    point.pos.z = 0;
                    if (point.vel.z < 0)
                    {
                        point.vel.z *= -1;
                    }
                }
            }

        }
    }
}