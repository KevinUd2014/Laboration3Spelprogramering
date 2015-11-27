using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboration3.Model
{
    class BallSimulation
    {
        public Ball ball;
        public BallSimulation()
        {
            ball = new Ball();
        }

        public void update()
        {
            ball.position += ball.getVelocity;
            if (ball.position.X + ball.getRadius > 95 || ball.position.X - ball.getRadius < 5)//från mitten av bollen + radien på bollen så att bollen nuddar kant med kant!
            {
                ball.setVelocityX();
            }
            if (ball.position.Y + ball.getRadius > 95 || ball.position.Y - ball.getRadius < 5)
            {
                ball.setVelocityY();
            }
        }
    }
}
