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
        private const int width = 650;
        private const int height = 650;
        private const int depth = 300;

        private long _lastTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        public long lastTime
        {
            get => _lastTime;
            set => _lastTime = value;
        }

        private FrictionForce friction = new FrictionForce();
        private TensionForce tension = new TensionForce();
        private GravityForce gravity = new GravityForce();

        public static int point_size = 2;
        public static Point[] PointsArray = new Point[point_size];

        public static void inputData()
        {
            inputPoints();
            inputSprings();
        }
        private static void inputPoints()
        {
            Point point0 = new Point(100, 100, 100, 100, 100, 100);
            PointsArray[0] = point0;
            Point point1 = new Point(200, 100, 1000, 200, 100, 100);
            PointsArray[1] = point1;
        }
        private static void inputSprings()
        {
            PointsArray[0].connectedSprings[0] = new Spring(PointsArray[0], PointsArray[1]);
            PointsArray[1].connectedSprings[0] = new Spring(PointsArray[0], PointsArray[1]);
        }
        private long getDelta(long lastSavedTime)
        {
            long time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long deltaTime = time - lastSavedTime;
            lastTime = time;
            return deltaTime;
        }
        public void tick()
        {
            long dt = getDelta(lastTime);
            for (int i = 0; i < dt; i++)
            {
                tickPoints(PointsArray, dt);
            }
        }
        private void tickPoints(Point[] PointsArray, double dt)
        {
            for (int i = 0; i < point_size; i++)
            {
                PointsArray[i].updatePosition(getResultForce(PointsArray[i])*dt);
                updateSprings(PointsArray[i].connectedSprings);
                constrainPoint(PointsArray[i]);
            }
        }
        private void updateSprings(Spring[] SpringsArray)
        {
            for (int i = 0; i < 4; i++)
            {
                SpringsArray[i].update();
            }
        }
        private Vector getResultForce(Point point)
        {
            Vector resultForce = friction.Apply(point) + tension.Apply(point) + gravity.Apply(point);
            return resultForce;
        }
        private void constrainPoint(Point point)
        {
            if (!point.pinned)
            {
                if (point.pos.x > depth)
                {
                    point.pos.x = depth;
                    point.oldPos.x = point.pos.x + point.getVelocity().x;
                }
                else if (point.pos.x < 0)
                {
                    point.pos.x = 0;
                    point.oldPos.x = point.pos.x + point.getVelocity().x;
                }
                if (point.pos.y > width)
                {
                    point.pos.y = width;
                    point.oldPos.y = point.pos.y + point.getVelocity().y;
                }
                else if (point.pos.y < 0)
                {
                    point.pos.y = 0;
                    point.oldPos.y = point.pos.y + point.getVelocity().y;
                }
                if (point.pos.z > height)
                {
                    point.pos.z = height;
                    point.oldPos.z = point.pos.z + point.getVelocity().z;
                }
                else if (point.pos.z < 0)
                {
                    point.pos.z = 0;
                    point.oldPos.z = point.pos.z + point.getVelocity().z;
                }
            }
        }
    }
}