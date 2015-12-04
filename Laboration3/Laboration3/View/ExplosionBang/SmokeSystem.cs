using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboration3.View.ExplosionBang
{
    class SmokeSystem
    {
        private Smoke[] smoke;
        public float maxParticleLife;
        public float life;

        public int maxParticleCount;
        private int offset;
        //private int firstInLine;

        private float elapsedTime;
        private Texture2D texture;
        private Random r;
        private Vector2 startPosition;

        Camera camera;
        Vector2 startPos;

        public SmokeSystem(Texture2D Texture, Vector2 startPosition, Camera Camera)
        {
            maxParticleCount = 500;//hur många partiklar ska jag ha?
            smoke = new Smoke[maxParticleCount];//lägger in alla i en array med partiklar
            elapsedTime = 0;//den gångna tiden
            offset = 0;
            maxParticleLife = 5000;
            life = 5000;
            texture = Texture;
            r = new Random();
            startPos = new Vector2(startPosition.X, startPosition.Y);
            camera = Camera;
        }

        public void Update(float gameTime)
        {
            elapsedTime += gameTime;//sätter så at det räknas i millisekunder//(float)//.ElapsedGameTime.TotalMilliseconds

            life -= gameTime;

            if (elapsedTime > maxParticleLife / maxParticleCount)// om tiden är större än max livet delat på max partiklar så körs denna!
            {
                if (offset < maxParticleCount)// går in i denna om nu en partikels tid är ute
                {

                    smoke[offset] = new Smoke(maxParticleLife);
                    resetParticle(smoke[offset], camera, startPos);// kör om med samma partiklar från början

                    offset++;
                    elapsedTime = 0;// och startar om tiden för dessa!
                }
            }

            for (int i = 0; i < offset; i++)
            {
                smoke[i].Update(gameTime);

                if (smoke[i].age > maxParticleLife && life > 0)
                    resetParticle(smoke[i], camera, startPos);
            }

        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < offset; i++)
            {
                smoke[i].Draw(sb, texture, maxParticleLife, camera);
            }
        }
        private void resetParticle(Smoke particle, Camera camera, Vector2 StartPosition)// denna kommer reseta allt
        {
            float speed = (float)r.NextDouble() * 7f;//farten på partiklarna
            Vector2 velocity = new Vector2((float)r.NextDouble() * 2f - 1f, (float)r.NextDouble() * 2f - 1f);
            startPosition = StartPosition;
            velocity.Normalize();
            velocity = velocity * speed;

            float rotation = (float)r.NextDouble() * 2f * (float)Math.PI;//denna kommer rotera partiklarna lite
            float allTheRadians = 2f * (float)Math.PI;// gör så att partiklarna roterar runt mitten av spriten
            float rotationSpeed = ((float)r.NextDouble() * allTheRadians - allTheRadians / 2f) / 300f;

            particle.Reset(startPosition, velocity, rotation, rotationSpeed, 0.3f + (float)r.NextDouble() * 0.7f);// kommer ge allt sina värden när man ska skapa om dom!//startPos*500
        }
    }
}
