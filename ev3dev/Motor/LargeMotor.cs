using System;
using ev3dev.Core;
using System.Collections.Generic;
namespace ev3dev.Motor
{
    public class LargeMotor : Motor
    {
        public LargeMotor(LegoPort port, Dictionary<string,string> attributes = null)
        : base("lego-ev3-l-motor", port, attributes) { }
    }
}