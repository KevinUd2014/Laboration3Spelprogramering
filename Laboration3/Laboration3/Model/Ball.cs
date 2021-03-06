﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboration3.Model
{
    class Ball
    {
        public Vector2 position;
        private Vector2 velocity = new Vector2(1f, 1f);
        private float radius = 2.5f;
        private bool deadBall = false;

        public Ball(int seed)
        {
            Random r = new Random(seed);// gör så att mönstret slumpas ut
            velocity = (new Vector2(r.Next(100) / 100f, r.Next(100) / 100f) * 2f - Vector2.One);// denna uträkning tillhör det!
            velocity.Normalize();///https://msdn.microsoft.com/en-us/library/microsoft.xna.framework.vector2.normalize.aspx // för att mönstret ska fortsätta i sammma linje!
            //velocity *= 0.008f; // denna ändrar hastigheten!
            position = new Vector2(r.Next(2,98), r.Next(2,98));//new Vector2(50f, 50f);
        }
        public bool isBallDead
        {
            get
            {
                //velocity *= 0; 
                return deadBall;
            }
            set
            {
                velocity *= 0;
                //velocity = new Vector2(0, 0);
                deadBall = value;
            }
        }
        public void setNewPosition(float time)
        {
            position += velocity * time;
        }
        public Vector2 getVelocity
        {
            get
            {
                return velocity;
            }
        }
        public void setVelocityX()
        {
            velocity.X = -velocity.X;
        }
        public void setVelocityY()
        {
            velocity.Y = -velocity.Y;
        }
        public float getRadius
        {
            get
            {
                return radius;
            }
        }
    }
}
