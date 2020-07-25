using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cloth_simulation
{
    public class Vector
    {
        private float _x;
        public float x
        {
            get => _x;
            set => _x = value;
        }
        private float _y;
        public float y
        {
            get => _y;
            set => _y = value;
        }

        private float _z;
        public float z
        {
            get => _z;
            set => _z = value;
        }
        public Vector()
        {
            x = 0;
            y = 0;
            z = 0;
        }
        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector(Vector anotherVector)
        {
            x = anotherVector.x;
            y = anotherVector.y;
            z = anotherVector.z;
        }

        public static Vector operator +(Vector a, Vector b) => new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector operator -(Vector a, Vector b) => new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vector operator *(Vector a, float num) => new Vector(a.x * num, a.y * num, a.z * num);
        public static Vector operator /(Vector a, float num) => new Vector(a.x / num, a.y / num, a.z / num);
    }
}
