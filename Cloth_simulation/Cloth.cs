using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloth_simulation
{
    class Cloth
    {
        public static int point_size = 2;

        public static int stick_size = 1;

        public static Point[] PointsArray = new Point[point_size];

        public static Stick[] SticksArray = new Stick[stick_size];

        /*public static void InputPoints()
        {
            for (int i = 0; i < point_size; i++)
            {
                Point point = new Point(100, 100, 0, false);
                PointsArray[i] = point;
            }
        }*/
        public static void InputPoints()
        {
            Point point0 = new Point(100, 100, 5, false);
            PointsArray[0] = point0;
            Point point1 = new Point(200, 100, 0, false);
            PointsArray[1] = point1;
        }

        /*public static void InputSticks()
        {
            for (int i = 0; i < stick_size; i++)
            {
                Stick stick = new Stick(PointsArray[0], PointsArray[1]);
                SticksArray[i] = stick;
            }
        }*/
        public static void InputSticks()
        {
            Stick stick0 = new Stick(PointsArray[0], PointsArray[1]);
            SticksArray[0] = stick0;
        }
        public static void UpdatePoints()
        {
            foreach (Point p in PointsArray)
            {
                if (!p.pin)
                {
                    double vx = (p.x - p.oldx) * Globals.friction;
                    double vy = (p.y - p.oldy) * Globals.friction;

                    p.oldx = p.x;
                    p.oldy = p.y;
                    p.x += vx;
                    p.y += vy;
                    p.y += Globals.gravity;
                }
            }
        }
        public static void ConstrainPoints()
        {
            foreach (Point p in PointsArray)
            {
                if (!p.pin)
                {
                    double vx = (p.x - p.oldx) * Globals.friction;
                    double vy = (p.y - p.oldy) * Globals.friction;

                    if (p.x > Globals.width)
                    {
                        p.x = Globals.width;
                        p.oldx = p.x + vx * Globals.bounce;
                    }
                    else if (p.x < 0)
                    {
                        p.x = 0;
                        p.oldx = p.x + vx * Globals.bounce;
                    }
                    if (p.y > Globals.height)
                    {
                        p.y = Globals.height;
                        p.oldy = p.y + vy * Globals.bounce;
                    }
                    else if (p.y < 0)
                    {
                        p.y = 0;
                        p.oldy = p.y + vy * Globals.bounce;
                    }
                }
            }
        }
        public static void UpdateSticks()
        {
            foreach (Stick s in SticksArray)
            {
                double dx = s.p1.x - s.p0.x;
                double dy = s.p1.y - s.p0.y;
                double distance = Stick.Distance(s.p0, s.p1);
                double difference = s.length - distance;
                double percent = difference / distance / 2;
                double offsetX = dx * percent;
				double offsetY = dy * percent;

                if (!s.p0.pin)
                {
                    s.p0.x -= offsetX;
                    s.p0.y -= offsetY;
                }
                if (!s.p1.pin)
                {
                    s.p1.x += offsetX;
                    s.p1.y += offsetY;
                }
            }
        }
    }
}
