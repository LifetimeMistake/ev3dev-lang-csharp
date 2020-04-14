using System;
using System.Collections.Generic;
using System.Linq;
namespace ev3dev.Core
{
    public static class Utils
	{
		private static Dictionary<LegoPort, string> LegoPortMap = new Dictionary<LegoPort, string>()
		{
			{ LegoPort.A, "ev3-ports:outA" },
			{ LegoPort.B, "ev3-ports:outB" },
			{ LegoPort.C, "ev3-ports:outC" },
			{ LegoPort.D, "ev3-ports:outD" },
			{ LegoPort.One, "ev3-ports:in1" },
			{ LegoPort.Two, "ev3-ports:in2" },
			{ LegoPort.Three, "ev3-ports:in3" },
			{ LegoPort.Four, "ev3-ports:in4" }
		};
		public static string LegoPort2String(LegoPort port)
		{
			return LegoPortMap[port];
		}

		public static LegoPort String2LegoPort(string port)
		{
			if(LegoPortMap.Where(kvp => kvp.Value == port).Count() == 0) return LegoPort.Unknown;
			return LegoPortMap.Where(kvp => kvp.Value == port).First().Key;
		}
	}
}