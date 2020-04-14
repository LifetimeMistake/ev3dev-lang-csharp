using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ev3dev.Motor
{
    public class MoveTank
    {
        public Motor Left;
        public Motor Right;
        public MoveTank(Motor left, Motor right)
        {
            Left = left;
            Right = right;
        }

        public void SetSpeed_Sp(int speed_left, int speed_right)
        {
            Left.TargetSpeed = speed_left;
            Right.TargetSpeed = speed_right;
        }

        public void RunForever()
        {
            Left.RunForever();
            Right.RunForever();
        }

        public void Stop()
        {
            Left.Stop();
            Right.Stop();
        }

        public void Reset()
        {
            Left.Reset();
            Right.Reset();
        }
    }
}
