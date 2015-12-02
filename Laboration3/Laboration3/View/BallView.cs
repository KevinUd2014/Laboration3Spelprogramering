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
        Texture2D ballTexture;

        public BallView(GraphicsDeviceManager graphics, BallSimulation BallSimulation, ContentManager Content, Texture2D Ball)/// en konstruktor som laddar in först i klassen!
        {
            ballTexture = Ball;///laddar in bollen //detta görs bara en gång!

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
                //Vector2.One * 5,
                //null, Color.White,
                //0f,
                //Vector2.Zero,
                camera.GetGameWindow(), Color.Black);
            //SpriteEffects.None,
            //0f);/// ritar ut boxen eller snarare kvadraten!

            foreach (Ball ball in ballSimulation.ballList)
            {
                spriteBatch.Draw(ballTexture,
                camera.returnPositionOfField(ball.position.X, ball.position.Y),
                null, Color.White,
                0f,
                new Vector2(ballTexture.Width, ballTexture.Height),
                camera.scaleSizeTo(ballTexture.Width, 20f),//, ballSimulation.ball.getRadius*2
                SpriteEffects.None,
                0f);// denna skalar om bollen så att den passar i min skärm
            }
        }
    }
}
