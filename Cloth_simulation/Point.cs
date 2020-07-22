using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class Point
    {
        private Vector _pos;
        public Vector pos
        {
            get => _pos;
            set => _pos = value;
        }
        private Vector _oldPos;
        public Vector oldPos 
        {
            get => _oldPos;
            set => _oldPos = value;
        }
        private double _mass = 2;
        public double mass
        {
            get => _mass;
        }
        private bool _pinned = true;
        public bool pinned
        {
            get => _pinned;
            set => _pinned = value;
        }

        public List<Spring> connectedSprings = new List<Spring>();

        public Point(double x = 0, double y = 0, double z = 0, double oldX = 0, double oldY = 0, double oldZ = 0, bool pinned = false)
        {
            pos = new Vector(x, y, z);
            oldPos = new Vector(oldX, oldY, oldZ);
            this.pinned = pinned;
        }
        public double distanceTo(Point point)
        {
            double dx = point.pos.x - pos.x;
            double dy = point.pos.y - pos.y;
            double dz = point.pos.z - pos.z;
            double result = Math.Sqrt(dx * dx + dy * dy + dz * dz);
            return result;
        }
        public Vector getVelocity()
        {
            Vector result = pos - oldPos;
            return result;
        }
        public void applyOffset(Vector offset, bool isNegative)
        {
            if (isNegative)
            {
                pos -= offset;
            }
            else
            {
                pos += offset;
            }
        }
        public void updatePosition(Vector changePos)
        {
            if (!pinned)
            {
                oldPos = pos;
                pos = pos + getVelocity() + changePos;
            }
        }
    }
}
