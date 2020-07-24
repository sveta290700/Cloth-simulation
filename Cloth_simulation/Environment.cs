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
        private const int width = 650;
        private const int height = 650;
        private const int depth = 300;

        private long _lastTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        public long lastTime
        {
            get => _lastTime;
            set => _lastTime = value;
        }

        private List<Point> _pointsCollection = new List<Point>();
        private List<IForce> _forcesCollection = new List<IForce>();
        public List<Point> PointsCollection => _pointsCollection;

        public List<IForce> ForcesCollection => _forcesCollection;

        public Environment() 
        { 
           _forcesCollection.Add(new FrictionForce()); 
           _forcesCollection.Add(new TensionForce()); 
           _forcesCollection.Add(new GravityForce());
        }
        public void inputData()
        {
            inputPoints();
            inputSprings();
        }
        private void inputPoints()
        {
            Point point0 = new Point(100, 100, 100, 100, 100, 100);
            _pointsCollection.Add(point0);
            Point point1 = new Point(200, 100, 100, 200, 100, 100);
            _pointsCollection.Add(point1);
        }
        private void inputSprings()
        {
            _pointsCollection[0].connectedSprings.Add(new Spring(_pointsCollection[0], _pointsCollection[1]));
            _pointsCollection[1].connectedSprings.Add(new Spring(_pointsCollection[0], _pointsCollection[1]));
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
            for (int i = 0; i < 2; i++)
            {
                verletIntegrationStep(dt);
            }
        }
        private void verletIntegrationStep(float dt)
        {
            for (int i = 0; i < _pointsCollection.Count; i++)
            {
                Vector resultForce = getResultForce(_pointsCollection[i]);
                _pointsCollection[i].updatePosition(resultForce*dt);
                updateSprings(_pointsCollection[i].connectedSprings);
                constrainPoint(_pointsCollection[i]);
            }
        }
        private void updateSprings(List<Spring> spring)
        {
            for (int i = 0; i < spring.Count(); i++)
            {
                spring[i].update();
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
        private void constrainXAxis(Point point, int limit)
        {
            point.pos.x = limit;
            point.oldPos.x = point.pos.x + point.getVelocity().x;
        }
        private void constrainYAxis(Point point, int limit)
        {
            point.pos.y = limit;
            point.oldPos.y = point.pos.y + point.getVelocity().y;
        }
        private void constrainZAxis(Point point, int limit)
        {
            point.pos.z = limit;
            point.oldPos.z = point.pos.z + point.getVelocity().z;
        }
        private void constrainPoint(Point point)
        {
            if (!point.pinned)
            {
                if (point.pos.x > depth)
                {
                    constrainXAxis(point, depth);
                }
                else if (point.pos.x < 0)
                {
                    constrainXAxis(point, 0);
                }
                if (point.pos.y > width)
                {
                    constrainYAxis(point, width);
                }
                else if (point.pos.y < 0)
                {
                    constrainYAxis(point, 0);
                }
                if (point.pos.z > height)
                {
                    constrainZAxis(point, height);
                }
                else if (point.pos.z < 0)
                {
                    constrainZAxis(point, 0);
                }
            }
        }
    }
}