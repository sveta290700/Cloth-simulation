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

        private Vector _vel;
        public Vector vel
        {
            get => _vel;
            set => _vel = value;
        }
        private Vector _acc;
        public Vector acc
        {
            get => _acc;
            set => _acc = value;
        }
        private double _mass;
        public double mass
        {
            get => _mass;
            set => _mass = value;
        }
        private bool _pinned;
        public bool pinned
        {
            get => _pinned;
            set => _pinned = value;
        }

        public Spring[] springsArray = new Spring[3];

        public Point(double x, double y, double z, double mass)
        {
            this.pos.x = x;
            this.pos.y = y;
            this.pos.z = z;
            this.vel.x = 0;
            this.vel.y = 0;
            this.vel.z = 0;
            this.mass = mass;
            pinned = false;
        }
        public Point(double x, double y, double z, double mass, bool pinned)
        {
            this.pos.x = x;
            this.pos.y = y;
            this.pos.z = z;
            this.vel.x = 0;
            this.vel.y = 0;
            this.vel.z = 0;
            this.mass = mass;
            this.pinned = pinned;
        }
        public Point(double x, double y, double z, double xVel, double yVel, double zVel, double mass)
        {
            this.pos.x = x;
            this.pos.y = y;
            this.pos.z = z;
            this.vel.x = xVel;
            this.vel.y = yVel;
            this.vel.z = zVel;
            this.mass = mass;
            pinned = false;
        }
        public Point(double x, double y, double z, double xVel, double yVel, double zVel, double mass, bool pinned)
        {
            this.pos.x = x;
            this.pos.y = y;
            this.pos.z = z;
            this.vel.x = xVel;
            this.vel.y = yVel;
            this.vel.z = zVel;
            this.mass = mass;
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

        public void updatePoint(double dt)
        {
            if (!pinned)
            {
                ForceOfFriction friction = new ForceOfFriction();
                vel = friction.Apply(this);

                Vector newPos = pos + vel * dt + acc * (dt * dt * 0.5);

                ForceOfTension tension = new ForceOfTension();
                pos += tension.Apply(this);

                ForceOfGravity gravity = new ForceOfGravity();
                Vector grav_acc = gravity.Apply(this);
                DragForce drag = new DragForce();
                Vector drag_force = drag.Apply(this);
                Vector drag_acc = drag_force / mass;
                Vector newAcc = grav_acc - drag_acc;

                Vector newVel = vel + (acc + newAcc) * (dt * 0.5);
                if (this.pos != newPos)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        springsArray[i].length = -1;
                    }
                }
                pos = newPos;
                vel = newVel;
                acc = newAcc;
            }
        }
    }
}
