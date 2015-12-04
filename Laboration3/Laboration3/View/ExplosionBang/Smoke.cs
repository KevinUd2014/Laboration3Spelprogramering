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
        public float scale = 0.2f;
        private const float maxSize = 10f;
        private float size = 0f;
        private float maxLife;

        public Smoke(float maxLife)
        {
            this.maxLife = maxLife;
        }

        public void Update(float gameTime)
        {
            velocity += new Vector2(0, -0.001f);//new Vector2(0, -0.01f);
            position += velocity*gameTime/1000;//*gameTime / 500;
            rotation += rotationSpeed;
            scale = 0.2f + ((age * 2f) / maxLife) * 0.8f;
            age += gameTime;//(float)//.ElapsedGameTime.TotalMilliseconds//denna sätter att smoken ska sluta!
        }

        public void Draw(SpriteBatch sb, Texture2D texture, float maxAge, Camera camera)
        {

            sb.Draw(texture,
                camera.convertToVisualCoords(position),
                null,
                Color.White,
                0f,
                new Vector2(texture.Width, texture.Height) / 2,
                camera.scaleSizeTo(texture.Width, scale * size),
                SpriteEffects.None,
                0f);

            //sb.Draw(texture,
            //    camera.convertToVisualCoords(position),
            //    null,
            //    null,
            //    new Vector2(texture.Width / 2f, texture.Height / 2f),
            //    rotation,
            //    camera.scaleSizeTo(texture.Width, size),//storleken på smoke är *3
            //    Color.White,
            //    SpriteEffects.None,
            //    0);
        }

        public void Reset(Vector2 p, Vector2 v, float r, float rs, float s)
        {
            position = p;
            velocity = v;
            rotation = r;
            rotationSpeed = rs;
            size = s*maxSize;
            age = 0;

        }
    }
}
