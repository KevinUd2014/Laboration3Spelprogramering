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
        public Vector2 velocity;
        public float rotation;
        public float rotationSpeed;
        public float age;
        public float scale;

        public void Update(float gameTime)
        {
            velocity += new Vector2(0, -0.01f);
            position += velocity;
            rotation += rotationSpeed;
            age += gameTime;//(float)//.ElapsedGameTime.TotalMilliseconds
        }

        public void Draw(SpriteBatch sb, Texture2D texture, float maxAge, Camera camera)
        {
            sb.Draw(texture,
                position,
                null,
                null,
                new Vector2(texture.Width / 2f, texture.Height / 2f),
                rotation,
                camera.scaleSizeTo(texture.Width, 10f) * Vector2.One * scale * (age / maxAge),
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
