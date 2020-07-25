﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Cloth_simulation
{
    public class Environment
    {
        private int _width = 1200;
        public int width
        {
            get => _width;
            set => _width = value;
        }
        private int _height = 700;
        public int height
        {
            get => _height;
            set => _height = value;
        }
        private static int _depth = 200;
        public int depth
        {
            get => _depth;
            set => _depth = value;
        }
        private long _lastTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        public long lastTime
        {
            get => _lastTime;
            set => _lastTime = value;
        }
        private List<Point> _pointsCollection = new List<Point>();
        private List<Spring> _springsCollection = new List<Spring>();
        private List<IForce> _forcesCollection = new List<IForce>();
        public List<Point> PointsCollection => _pointsCollection;
        public List<IForce> ForcesCollection => _forcesCollection;
        public List<Spring> SpringsCollection => _springsCollection;

        public Environment() 
        {
            FrictionForce frictionForce = new FrictionForce();
           _forcesCollection.Add(frictionForce);
            TensionForce tensionForce = new TensionForce();
            _forcesCollection.Add(tensionForce);
            GravityForce gravityForce = new GravityForce();
           _forcesCollection.Add(gravityForce);
        }

        public static int getDepth()
        {
            return _depth;
        }
        public void changeSize(int depth, int width, int height)
        {
            this.depth = depth;
            this.width = width;
            this.height = height;
        }
        public void inputData()
        {
            inputPoints();
            inputSprings();
        }
        private void inputPoints()
        {
            Point point0 = new Point(100, 300, 200, 100, 300, 200);
            _pointsCollection.Add(point0);
            Point point1 = new Point(100, 200, 200, 100, 200, 200);
            _pointsCollection.Add(point1);
        }
        public void addPoint(Point point)
        {
            PointsCollection.Add(point);
        }
        public void removePoint(Point point)
        {
            PointsCollection.Remove(point);
        }
        public int pointsSize()
        {
            return PointsCollection.Count();
        }
        private void inputSprings()
        {
            _springsCollection.Add(new Spring(_pointsCollection[0], _pointsCollection[1]));
            _pointsCollection[0].connectedSprings.Add(new Spring(_pointsCollection[0], _pointsCollection[1]));
            _pointsCollection[1].connectedSprings.Add(new Spring(_pointsCollection[0], _pointsCollection[1]));
        }
        public void addSpring(Spring spring)
        {
            SpringsCollection.Add(spring);
            spring.point0.connectedSprings.Add(spring);
            spring.point1.connectedSprings.Add(spring);
        }
        public void addSpring(Point point0, Point point1)
        {
            Spring spring = new Spring(point0, point1);
            SpringsCollection.Add(spring);
            point0.connectedSprings.Add(spring);
            point1.connectedSprings.Add(spring);
        }
        public bool removeSpring(Spring spring)
        {
            bool result = SpringsCollection.Remove(spring);
            if (result)
            {
                spring.point0.connectedSprings.Remove(spring);
                spring.point1.connectedSprings.Remove(spring);
            }
            return result;
        }
        public bool removeSpring(Point point0, Point point1)
        {
            Spring spring = new Spring(point0, point1);
            bool result = SpringsCollection.Remove(spring);
            if (result)
            {
                point0.connectedSprings.Remove(spring);
                point1.connectedSprings.Remove(spring);
            }
            return result;
        }
        public int springsSize()
        {
            return SpringsCollection.Count();
        }
        public int pointSpringsSize(Point point)
        {
            return point.connectedSprings.Count();
        }
        public Point[] PointsArray()
        {
            return PointsCollection.ToArray();
        }
        public Spring[] SpringsArray()
        {
            return SpringsCollection.ToArray();
        }
        public Spring[] pointSpringsArray(Point point)
        {
            return point.connectedSprings.ToArray();
        }
        public void pinPoint(Point point)
        {
            point.setPin();
        }
        public ForceCoefficient changeForceCoefficient(IForce force, float newCoefficientValue)
        {
            force.forceCoefficient.coefficient = newCoefficientValue;
            return force.forceCoefficient;
        }
        public Point findNearestPoint(float y, float z)
        {
            Point result = new Point();
            float min = 999999;
            Point comparedPoint = new Point(0, y, z);
            foreach (Point point in PointsCollection)
            {
                Point pointWithoutX = new Point(0, point.pos.y, point.pos.z);
                float distance = comparedPoint.distanceTo(pointWithoutX);
                if (min > distance)
                {
                    min = distance;
                    result = point;
                }
            }
            return result;
        }
        private float getDelta(long lastSavedTime)
        {
            long time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long deltaTime = time - lastSavedTime;
            lastTime = time;
            return deltaTime / 100f;
        }
        public void tick()
        {
            float dt = getDelta(lastTime);
            verletIntegrationStep(dt);
        }
        private void verletIntegrationStep(float dt)
        {
            for (int i = 0; i < _pointsCollection.Count; i++)
            {
                Vector resultForce = getResultForce(_pointsCollection[i]);
                _pointsCollection[i].updatePosition(resultForce, dt);
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < _pointsCollection.Count; k++)
                    {
                        constrainPoint(_pointsCollection[k]);
                    }
                }
            }
        }
        private Vector getResultForce(Point point)
        {
            Vector resultForce = new Vector();
            foreach (IForce force in _forcesCollection)
            {
                resultForce += force.Apply(point);
            }
            return resultForce;
        }
        private void constrainXAxis(Point point, float velX, float limit)
        {
            point.pos.x = limit;
            point.oldPos.x = point.pos.x + velX * 0.95F;
        }
        private void constrainYAxis(Point point, float velY, float limit)
        {
            point.pos.y = limit;
            point.oldPos.y = point.pos.y + velY * 0.95F;
        }
        private void constrainZAxis(Point point, float velZ, float limit)
        {
            point.pos.z = limit;
            point.oldPos.z = point.pos.z + velZ * 0.95F;
        }
        private void constrainPoint(Point point)
        {
            if (!point.pinned)
            {
                float min = point.getRadius();
                float maxDepth = depth - point.getRadius();
                Vector velocity = point.getVelocity();
                if (point.pos.x > maxDepth)
                {
                    constrainXAxis(point, velocity.x, maxDepth);
                }
                else if (point.pos.x < min)
                {
                    constrainXAxis(point, velocity.x, min);
                }
                float maxWidth = width - point.getRadius();
                if (point.pos.y > maxWidth)
                {
                    constrainYAxis(point, velocity.y, maxWidth);
                }
                else if (point.pos.y < min)
                {
                    constrainYAxis(point, velocity.y, min);
                }
                float maxHeight = height - point.getRadius();
                if (point.pos.z > maxHeight)
                {
                    constrainZAxis(point, velocity.z, maxHeight);
                }
                else if (point.pos.z < min)
                {
                    constrainZAxis(point, velocity.z, min);
                }
            }
        }
    }
}