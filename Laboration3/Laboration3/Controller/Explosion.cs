using Laboration3.Model;
using Laboration3.View.ExplosionBang;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboration3.View
{
    class Explosion
    {
        SpriteBatch spriteBatch;
        Texture2D particle;
        Texture2D smoke;
        Texture2D bangExplosion;
        Texture2D cursorImage;
        Vector2 cursorPos;
        //Texture2D masterBallDead;
        Camera camera;
        private float radius = 10f;
        //Vector2 CursorPosition;


        SoundEffect soundEffect;
        public int Width;
        public int Height;

        public float timeElapsed;
        public float maxTimer = 0.5f;

        Vector2 startposition = new Vector2(1f, 1f);// hela planen är just nu 100*100// så denna börjar i mitten 50*50
        ParticleSystem particleSystem;
        BallSimulation ballSimulation;
        SmokeSystem smokeSystem;
        ExplosionManager explosionManager;
        List<ExplosionManager> explosions = new List<ExplosionManager>();
        List<ParticleSystem> particleSpark = new List<ParticleSystem>();
        List<SmokeSystem> smokes = new List<SmokeSystem>();
        //BallView ballview;


        public Explosion(SpriteBatch spritebatch, Texture2D Particle, Camera Camera, Texture2D Smoke, Texture2D BangExplosion, SoundEffect explosionSound, Texture2D CursorImage, BallSimulation bs)
        {
            timeElapsed = 0; //denna ska vara 0 när programmet startas
                             //ballview = Ballview;
            //this.masterBallDead = masterBallDead;
            cursorImage = CursorImage;
            //cursorPos = CursorPosition;

            camera = Camera;//så jag kan använda kameran i klassen!
            spriteBatch = spritebatch;
            particle = Particle;

            bangExplosion = BangExplosion;
            smoke = Smoke;
            Width = particle.Width; // posFramesX; //delar explosionens bredd med positions framesen!
            Height = particle.Height; // posFramesY;
            soundEffect = explosionSound;

            particleSystem = new ParticleSystem(startposition);
            smokeSystem = new SmokeSystem(smoke, startposition, camera);//får inte denna att fungera
            explosionManager = new ExplosionManager(spriteBatch, BangExplosion, camera, startposition);
            ballSimulation = bs;
        }

        public void Update(float totalseconds)
        {
            foreach (SmokeSystem smokeSystem in smokes)
            {
                smokeSystem.Update(totalseconds);
            }
            foreach (ParticleSystem Particle in particleSpark)
            {
                Particle.Update(totalseconds/1000);
            }
            MouseState mouseState = Mouse.GetState();
            cursorPos = new Vector2(mouseState.X, mouseState.Y);
        }
        public void Click(Vector2 mousePosition)
        {
            Vector2 logicalMousePosition = camera.convertToLogicalCoords(mousePosition.X, mousePosition.Y);
            
            if (logicalMousePosition.X <= 100f && logicalMousePosition.X >= 0f && logicalMousePosition.Y <= 100f && logicalMousePosition.Y >= 0f)
            {
                ballSimulation.setDeadBalls(logicalMousePosition.X, logicalMousePosition.Y, radius);//om jag sätter *100 här så får musen rätt position!
                particleSpark.Add(particleSystem = new ParticleSystem(logicalMousePosition));
                smokes.Add(smokeSystem = new SmokeSystem(smoke, logicalMousePosition, camera));//får inte denna att fungera
                explosions.Add(explosionManager = new ExplosionManager(spriteBatch, bangExplosion, camera, logicalMousePosition));
                //foreach (Ball ball in ballSimulation.RecentlyKilledBalls)
                //{
                //    smokes.Add(new SmokeSystem(smoke, ball.position, camera));
                //}
            }
            //explosionManager.PlayExplosionSound();
            soundEffect.Play();
        }
        public void Draw(float totalSeconds, SpriteBatch spriteBatch)
        {
            timeElapsed += totalSeconds;
            spriteBatch.Draw(cursorImage,
                cursorPos,
                null, Color.White,
                0f,
                new Vector2(cursorImage.Width / 2, cursorImage.Height / 2),//denna sätter ut cirkeln!
                camera.scaleSizeTo(cursorImage.Width, radius * 2),
                SpriteEffects.None,
                0f);//denna håller koll på muspekarbilden!

            foreach (ParticleSystem Particle in particleSpark)
            {
                Particle.Draw(spriteBatch, camera, particle);
            }//280
            foreach (SmokeSystem smokeSystem in smokes)
            {
                smokeSystem.Draw(spriteBatch);
            }
            foreach (ExplosionManager explosion in explosions)
            {
                explosion.Draw(totalSeconds);
            }

            //explosionManager.Draw(totalSeconds);
            
        }
    }
}
