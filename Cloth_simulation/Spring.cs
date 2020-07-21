using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        private const double _tension_koeff = 0.2;
        public double tension_koeff
        {
            get => _tension_koeff;
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
            if (this.length == -1)
            {
                this.length = point0.distanceTo(point1);
            }
            return this.length;
        }

        public void updateSpring(Spring spring)
        {
            double dx = spring.point1.pos.x - spring.point0.pos.x;
            double dy = spring.point1.pos.y - spring.point0.pos.y;
            double dz = spring.point1.pos.z - spring.point0.pos.z;
            double distance = spring.point0.distanceTo(spring.point1);
            double difference = spring.getLength() - distance;
            double percent = difference / distance / 2;
            double offsetX = dx * percent;
            double offsetY = dy * percent;
            double offsetZ = dz * percent;

            if (!spring.point0.pinned)
            {
                spring.point0.pos.x -= offsetX;
                spring.point0.pos.y -= offsetY;
                spring.point0.pos.z -= offsetZ;
            }
            if (!spring.point1.pinned)
            {
                spring.point1.pos.x += offsetX;
                spring.point1.pos.y += offsetY;
                spring.point0.pos.z += offsetZ;
            }
        }
    }
}
