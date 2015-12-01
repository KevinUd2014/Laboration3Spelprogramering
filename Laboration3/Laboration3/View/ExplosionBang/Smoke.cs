using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboration3.View.ExplosionBang
{
    class Smoke
    {
        public Vector2 position;
        public Vector2 velocity; //= new Vector2(0.00f, -0.01f);
        public float rotation;
        public float rotationSpeed;
        public float age;
        public float scale;

        public void Update(float gameTime)
        {
            velocity += new Vector2(0, -0.01f);//new Vector2(0, -0.01f);
            position += velocity*gameTime/1000;//*gameTime / 500;
            rotation += rotationSpeed;
            age -= gameTime;//(float)//.ElapsedGameTime.TotalMilliseconds//denna sätter att smoken ska sluta!
        }

        public void Draw(SpriteBatch sb, Texture2D texture, float maxAge, Camera camera)
        {
            sb.Draw(texture,
                camera.convertToVisualCoords(position),
                null,
                null,
                new Vector2(texture.Width / 2f, texture.Height / 2f),
                rotation,
                Vector2.One * scale * (age / maxAge)*2,//storleken på smoke är *3
                Color.White * (1 - (age / maxAge)),
                SpriteEffects.None,
                0);
        }

        public void Reset(Vector2 p, Vector2 v, float r, float rs, float s)
        {
            position = p;
            velocity = v;
            rotation = r;
            rotationSpeed = rs;
            scale = s;
            age = 0;

        }
    }
}
