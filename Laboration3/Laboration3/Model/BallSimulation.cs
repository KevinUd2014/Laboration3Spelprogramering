using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboration3.Model
{
    class BallSimulation
    {
        //public Ball ball;
        private List<Ball> ballList = new List<Ball>();
        //private List<Ball> newlyKilledBall;
        int maxBalls = 10;

        public BallSimulation()
        {
            for(int i = 0; i < maxBalls; i++)
            {
                ballList.Add(new Ball(i));
            }
        }
        public void Update(float time)
        {
            foreach (Ball ball in ballList)
            {
                if (!ball.isBallDead)
                {
                    updateBallCollision();//kanske ska skicka in ball
                    ball.setNewPosition(time);
                }
            }
        }
        public void updateBallCollision()
        {
            foreach (Ball ball in ballList)
            {
                ball.position += ball.getVelocity;
                if (ball.position.X + ball.getRadius > 100 || ball.position.X - ball.getRadius < 1)//från mitten av bollen + radien på bollen så att bollen nuddar kant med kant!
                {
                    ball.setVelocityX();
                }
                if (ball.position.Y + ball.getRadius > 100 || ball.position.Y - ball.getRadius < 1)//denna fungerar inte om man gör bollen större i BallView
                {
                    ball.setVelocityY();
                }
            }
        }
        public void setDeadBalls(float X, float Y, float crosshairSize)
        {
            //newlyKilledBall = new List<Ball>();
            foreach (Ball ball in ballList)
            {
                float distance = (ball.position - new Vector2(X, Y)).Length();
                if (!ball.isBallDead)
                {
                    if (distance < crosshairSize + ball.getRadius)
                    {
                        //newlyKilledBall.Add(ball);
                        ball.isBallDead = true;
                    }
                }
            }
        }
        public List<Ball> getBalls()
        {
            return ballList;
        }
    }
}
