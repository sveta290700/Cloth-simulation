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

        public void updatePoint(Point point, double dt)
        {
            if (!point.pinned)
            {
                applyFriction(point);
                Vector newPos = point.pos + point.vel * dt + point.acc * (dt * dt * 0.5);
                applyTension(point);
                Vector newAcc = applyGravity(point);
                Vector newVel = point.vel + (point.acc + newAcc) * (dt * 0.5);
                if (point.pos != newPos)
                {
                    for (int i = 0; i < Environment.spring_size; i++)
                    {
                        if (Environment.SpringsArray[i].point0 == point || Environment.SpringsArray[i].point1 == point)
                        {
                            Environment.SpringsArray[i].length = -1;
                        }
                    }
                }
                point.pos = newPos;
                point.vel = newVel;
                point.acc = newAcc;
            }
        }
        public void applyFriction(Point point)
        {
            ForceOfFriction friction = new ForceOfFriction();
            point.vel = friction.Apply(point);
        }

        public void applyTension(Point point)
        {
            ForceOfTension tension = new ForceOfTension();
            point.pos += tension.Apply(point);
        }
        public Vector applyGravity(Point point)
        {
            ForceOfGravity gravity = new ForceOfGravity();
            Vector grav_acc = gravity.Apply(point);
            DragForce drag = new DragForce();
            Vector drag_force = drag.Apply(point);
            Vector drag_acc = drag_force / mass;
            return grav_acc - drag_acc;
        }

        public void constrainPoint(Point point)
        {
            if (!point.pinned)
            {
                applyFriction(point);

                if (point.pos.x > GUI.width)
                {
                    point.pos.x = GUI.width;
                    if (point.vel.x > 0)
                    {
                        point.vel.x *= -1;
                    }
                }
                else if (point.pos.x < 0)
                {
                    point.pos.x = 0;
                    if (point.vel.x < 0)
                    {
                        point.vel.x *= -1;
                    }
                }

                if (point.pos.y > GUI.width)
                {
                    point.pos.y = GUI.width;
                    if (point.vel.y > 0)
                    {
                        point.vel.y *= -1;
                    }
                }
                else if (point.pos.y < 0)
                {
                    point.pos.y = 0;
                    if (point.vel.y < 0)
                    {
                        point.vel.y *= -1;
                    }
                }

                if (point.pos.z > GUI.height)
                {
                    point.pos.z = GUI.height;
                    if (point.vel.z > 0)
                    {
                        point.vel.z *= -1;
                    }
                }
                else if (point.pos.z < 0)
                {
                    point.pos.z = 0;
                    if (point.vel.z < 0)
                    {
                        point.vel.z *= -1;
                    }
                }
            }
        }
    }
}
