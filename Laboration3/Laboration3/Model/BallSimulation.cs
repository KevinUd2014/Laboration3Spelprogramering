using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboration3.Model
{
    class BallSimulation
    {
        //public Ball ball;
        public List<Ball> ballList = new List<Ball>();
        //private List<Ball> newlyKilledBall;
        int maxBalls = 10;

        public BallSimulation()
        {
            for(int i = 0; i < maxBalls; i++)
            {
                ballList.Add(new Ball(i));
            }

            //ball = new Ball();
        }
        public void Update(float time)
        {
            //foreach (Ball ball in ballList)
            //{
            //    if (!ball.isBallDead)
            //    {
            //        ballCollision();//kanske ska skicka in ball
            //        ball.setNewPos(time);
            //    }
            //}
        }

        public void updateBallCollision()
        {
            foreach (Ball ball in ballList)
            {

                ball.position += ball.getVelocity;
                if (ball.position.X + ball.getRadius > 105 || ball.position.X - ball.getRadius < -1)//från mitten av bollen + radien på bollen så att bollen nuddar kant med kant!
                {
                    ball.setVelocityX();
                }
                if (ball.position.Y + ball.getRadius > 105 || ball.position.Y - ball.getRadius < -1)//denna fungerar inte om man gör bollen större i BallView
                {
                    ball.setVelocityY();
                }
            }
        }
    }
}
