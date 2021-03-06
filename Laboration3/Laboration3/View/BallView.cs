﻿using Laboration3.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboration3.View
{
    class BallView
    {
        Texture2D box;
        private Camera camera;
        Texture2D deadBall;
        private BallSimulation ballSimulation;
        Texture2D ballTexture;

        public BallView(GraphicsDeviceManager graphics, BallSimulation BallSimulation, ContentManager Content, Texture2D Ball, Texture2D deadBall)/// en konstruktor som laddar in först i klassen!
        {
            ballTexture = Ball;///laddar in bollen //detta görs bara en gång!
            this.deadBall = deadBall;
            box = new Texture2D(graphics.GraphicsDevice, 1, 1);///denna skapar en box
            box.SetData(new Color[]
            {
                Color.Black
            });/// denna sätter vilken färg!

            camera = new Camera(graphics.GraphicsDevice.Viewport);/// skapar en ny camera instans
            ballSimulation = BallSimulation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(box,
                //Vector2.One * 5,
                //null, Color.White,
                //0f,
                //Vector2.Zero,
                camera.GetGameWindow(), Color.Black);
            //SpriteEffects.None,
            //0f);/// ritar ut boxen eller snarare kvadraten!

            foreach (Ball ball in ballSimulation.getBalls())
            {
                if (ball.isBallDead == false)
                {
                    spriteBatch.Draw(ballTexture,
                    camera.returnPositionOfField(ball.position.X, ball.position.Y),
                    null, Color.White,
                    0f,
                    new Vector2(ballTexture.Width, ballTexture.Height) / 2,
                    camera.scaleSizeTo(ballTexture.Width, ball.getRadius * 2),//, ballSimulation.ball.getRadius*2
                    SpriteEffects.None,
                    0f);// denna skalar om bollen så att den passar i min skärm
                }
                else
                {
                    spriteBatch.Draw(deadBall,
                    camera.returnPositionOfField(ball.position.X, ball.position.Y),
                    null, Color.White,
                    0f,
                    new Vector2(deadBall.Width, deadBall.Height) / 2,
                    camera.scaleSizeTo(deadBall.Width, ball.getRadius * 2),//, ballSimulation.ball.getRadius*2
                    SpriteEffects.None,
                    0f);// denna skalar om bollen så att den passar i min skärm
                }
            }
        }
    }
}
