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
        private static float _tensionСoefficient = 0.2F;
        public float TensionСoefficient
        {
            get => _tensionСoefficient;
            set => _tensionСoefficient = value;
        }
        private float _length;
        public float length
        {
            get => _length;
            set => _length = value;
        }

        public Spring(Point point0, Point point1) 
        {
            this._point0 = point0;
            this._point1 = point1;
            this.length = point0.distanceTo(point1);
        }

        private Vector getOffset()
        {
            float percent = getPercent();
            Vector offset = new Vector(point1.pos.x - point0.pos.x, point1.pos.y - point0.pos.y, point1.pos.z - point0.pos.z) * percent;
            return offset;
        }
        private float getPercent()
        {
            float distance = point0.distanceTo(point1);
            float difference = length - distance;
            float percent = difference / distance / 2;
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
