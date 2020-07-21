using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloth_simulation
{
    public class Vector
    {
        private double _x;
        public double x
        {
            get => _x;
            set => _x = value;
        }
        private double _y;
        public double y
        {
            get => _y;
            set => _y = value;
        }

        private double _z;
        public double z
        {
            get => _z;
            set => _z = value;
        }
        public Vector()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }
        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static Vector operator +(Vector a, Vector b) => new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector operator -(Vector a, Vector b) => new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vector operator *(Vector a, double num) => new Vector(a.x * num, a.y * num, a.z * num);
        public static Vector operator /(Vector a, double num) => new Vector(a.x / num, a.y / num, a.z / num);
    }
}
