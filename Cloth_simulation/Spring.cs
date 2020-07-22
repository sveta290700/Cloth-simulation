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
        private static double _tension_сoefficient = 0.2;
        public double tension_сoefficient
        {
            get => _tension_сoefficient;
            set => _tension_сoefficient = value;
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
        private Vector getOffset()
        {
            double percent = getPercent();
            Vector offset = new Vector(point1.pos.x - point0.pos.x, point1.pos.y - point0.pos.y, point1.pos.z - point0.pos.z) * percent;
            return offset;
        }
        private double getPercent()
        {
            double distance = point0.distanceTo(point1);
            double difference = getLength() - distance;
            double percent = difference / distance / 2;
            return percent;
        }
        public void update()
        {
            if (!point0.pinned)
            {
                point0.applyOffset(getOffset(), true);
            }
            if (!point1.pinned)
            {
                point1.applyOffset(getOffset(), false);
            }
        }
    }
}
