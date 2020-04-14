using System;
using ev3dev.Core;
using System.Collections.Generic;
namespace ev3dev.Motor
{
    public class MediumMotor : Motor
    {
        public MediumMotor(LegoPort port, Dictionary<string,string> attributes = null)
        : base("lego-ev3-m-motor", port, attributes) { }
    }
}