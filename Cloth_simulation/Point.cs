using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private float _radius = 5F;
        public float radius 
        {
            get => _radius;
            set => _radius = value;
        }
        private float _mass = 2F;
        public float mass
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

        public Point(float x = 0, float y = 0, float z = 0, float oldX = 0, float oldY = 0, float oldZ = 0, bool pinned = false)
        {
            pos = new Vector(x, y, z);
            oldPos = new Vector(oldX, oldY, oldZ);
            this.pinned = pinned;
        }
        public float distanceTo(Point point)
        {
            float dx = point.pos.x - pos.x;
            float dy = point.pos.y - pos.y;
            float dz = point.pos.z - pos.z;
            float result = (float) Math.Sqrt(dx * dx + dy * dy + dz * dz);
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
        public float getRadius()
        {
            return radius * Environment.depth / pos.x;
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
