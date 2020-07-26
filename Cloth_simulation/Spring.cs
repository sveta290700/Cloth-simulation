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
        private static float _tensionСoefficient = 8F;
        public float tensionСoefficient
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

        public Vector getCenter()
        {
            Point middle = new Point();
            middle.pos.x = (point0.pos.x + point1.pos.x) / 2;
            middle.pos.y = (point0.pos.y + point1.pos.y) / 2;
            middle.pos.z = (point0.pos.z + point1.pos.z) / 2;
            return middle.pos;
        }
    }
}
