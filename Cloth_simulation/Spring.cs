using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class Spring
    {
        private Point _point0;
        public Point point0
        {
            get => _point0;
        }
        private Point _point1;
        public Point point1
        {
            get => _point1;
        }

        private const double _tension_сoeff = 0.2;
        public double tension_сoeff
        {
            get => _tension_сoeff;
        }

        private double _length;
        public double length
        {
            get => _length;
            set => _length = value;
        }
        public Spring(Point point0, Point point1) 
        {
            this._point0 = point0;
            this._point1 = point1;
            this.length = -1;
        }
        public double getLength()
        {
            if (length == -1)
            {
                length = point0.distanceTo(point1);
            }
            return length;
        }

        public void updateSpring()
        {
            double dx = point1.pos.x - point0.pos.x;
            double dy = point1.pos.y - point0.pos.y;
            double dz = point1.pos.z - point0.pos.z;
            double distance = point0.distanceTo(point1);
            double difference = getLength() - distance;
            double percent = difference / distance / 2;
            double offsetX = dx * percent;
            double offsetY = dy * percent;
            double offsetZ = dz * percent;

            if (!point0.pinned)
            {
                point0.pos.x -= offsetX;
                point0.pos.y -= offsetY;
                point0.pos.z -= offsetZ;
            }
            if (!point1.pinned)
            {
                point1.pos.x += offsetX;
                point1.pos.y += offsetY;
                point0.pos.z += offsetZ;
            }
        }
    }
}
