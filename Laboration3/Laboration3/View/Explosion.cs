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
        Camera camera;
        private float radius = 10f;

        private float crosshairSize = 0.1f;
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


        public Explosion(SpriteBatch spritebatch, Texture2D Particle, Camera Camera, Texture2D Smoke, Texture2D BangExplosion, SoundEffect explosionSound, Texture2D CursorImage)
        {
            timeElapsed = 0; //denna ska vara 0 när programmet startas
                             //ballview = Ballview;

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
            explosionManager = new ExplosionManager(spriteBatch, BangExplosion, camera, startposition, soundEffect);
            ballSimulation = new BallSimulation();
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
            }//280
            MouseState mouseState = Mouse.GetState();
            cursorPos = new Vector2(mouseState.X, mouseState.Y);
        }
        public void Click(Vector2 mousePosition)
        {
            Vector2 logicalMousePosition = camera.convertToLogicalCoords(mousePosition.X, mousePosition.Y);

            

            if (logicalMousePosition.X <= 1f && logicalMousePosition.X >= 0f && logicalMousePosition.Y <= 1f && logicalMousePosition.Y >= 0f)
            {
                ballSimulation.setDeadBalls(logicalMousePosition.X, logicalMousePosition.Y, crosshairSize / 2);
                particleSpark.Add(particleSystem = new ParticleSystem(logicalMousePosition));
                smokes.Add(smokeSystem = new SmokeSystem(smoke, logicalMousePosition, camera));//får inte denna att fungera
                explosions.Add(explosionManager = new ExplosionManager(spriteBatch, bangExplosion, camera, logicalMousePosition, soundEffect));
                foreach (Ball ball in ballSimulation.RecentlyKilledBalls)
                {
                    smokes.Add(new SmokeSystem(smoke, ball.position, camera));
                }
            }
            explosionManager.PlayExplosionSound();
        }
        //public void NewExplosion(float mCoordX, float mCoordY, SpriteBatch spriteBatch)
        //{
        //    Vector2 logicalMousePosition = camera.convertToLogicalCoords(new Vector2(mCoordX, mCoordY));
        //    if (logicalLocation.X <= 1f && logicalLocation.X >= 0f && logicalLocation.Y <= 1f && logicalLocation.Y >= 0f)
        //    {
        //        //fireSound.Play(0.1f, 0, 0);
        //        //explosions.Add(new ExplosionView(_camera, spriteBatch, logicalLocation, 0.5f, splitterTexture, splitterSecondTexture, smokeTexture, explosionTexture, shockwaveTexture));
        //        ballSimulation.setDeadBalls(logicalLocation.X, logicalLocation.Y, crosshairSize / 2);
        //        foreach (Ball ball in _ballSimulation.RecentlyKilledBalls)
        //        {
        //            smokes.Add(new SmokeSystem(smokeTexture, 0.5f, ball.position, true));
        //        }
        //    }
        //}
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
