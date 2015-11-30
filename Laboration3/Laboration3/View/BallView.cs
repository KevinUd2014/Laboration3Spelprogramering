using Laboration3.Model;
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
        private BallSimulation ballSimulation;
        Texture2D ball;

        public BallView(GraphicsDeviceManager graphics, BallSimulation BallSimulation, ContentManager Content, Texture2D Ball)/// en konstruktor som laddar in först i klassen!
        {
            ball = Ball;///laddar in bollen //detta görs bara en gång!

            box = new Texture2D(graphics.GraphicsDevice, 1, 1);///denna skapar en box
            box.SetData<Color>(new Color[]
            {
                Color.Black
            });/// denna sätter vilken färg!

            camera = new Camera(graphics.GraphicsDevice.Viewport);/// skapar en ny camera instans
            ballSimulation = BallSimulation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(box,
                Vector2.One * 5,
                null, Color.White,
                0f,
                Vector2.Zero,
                camera.scaleSizeTo(box.Width, 90f),
                SpriteEffects.None,
                0f);/// ritar ut boxen eller snarare kvadraten!
                spriteBatch.Draw(ball,
                ballSimulation.ball.position,
                null, Color.White,
                0f,
                new Vector2(ball.Width / 2, ball.Height / 2),
                camera.scaleSizeTo(ball.Width, ballSimulation.ball.getRadius * 2f),
                SpriteEffects.None,
                0f);/// denna skalar om bollen så att den passar i min skärm
        }
    }
}
