using System;
using ev3dev.Core;
using System.Collections.Generic;
namespace ev3dev.Motor
{
    public class Motor : Device
	{
		private string _command;
		private string[] _commands;
		private int _countPerRot;
		private string _motorType;
		private int _maxSpeed;
		public string Command { get { return _command; } set { _command = value; SetAttributeString("command", value); } }
		public string[] Commands { get { return _commands; } }
		public int CountPerRot { get { return _countPerRot; } }
		public int TargetSpeed { get { return GetAttributeInt("speed_sp"); } set { SetAttributeInt("speed_sp", value); } }
		public int CurrentSpeed { get { return GetAttributeInt("speed"); } }
		public string MotorType { get { return _motorType; } }
		public int MaxSpeed { get { return _maxSpeed; } }
		public int CurrentPosition { get { return GetAttributeInt("position"); } }
		public int TargetPosition { get { return GetAttributeInt("position_sp"); } set { SetAttributeInt("position_sp", value); } }
		public int TargetRampUpSpeed { get { return GetAttributeInt("ramp_up_sp"); } set { SetAttributeInt("ramp_up_sp", value); } }
		public int TargetRampDownSpeed { get { return GetAttributeInt("ramp_down_sp"); } set { SetAttributeInt("ramp_down_sp", value); } }
		public MotorPolarity Polarity { get
			{
				string polarity = GetAttributeString("polarity");
				switch(polarity)
				{
					case "normal":
						return MotorPolarity.Normal;
					case "inversed":
						return MotorPolarity.Inversed;
					default:
						throw new Exception("Motor had an invalid motor polarity.");
				}
			}
			set
			{
				switch(value)
				{
					case MotorPolarity.Normal:
						SetAttributeString("polarity", "normal");
						break;
					case MotorPolarity.Inversed:
						SetAttributeString("polarity", "inversed");
						break;
				}
			}
		}

		public Motor(string motorType, LegoPort port, Dictionary<string,string> attributes = null)
		{
			if(attributes == null) attributes = new Dictionary<string, string>();
			if(attributes.ContainsKey("deviceroot_startswith"))
			{
				Console.WriteLine("WARNING: The 'deviceroot_startswith' attribute is automatically included when using the Motor class and it should not be defined manually. Overriding...");
				attributes["deviceroot_startswith"] = "motor";
			}
			else
				attributes.Add("deviceroot_startswith", "motor");
			if(attributes.ContainsKey("driver_name"))
			{
				Console.WriteLine("WARNING: The 'driver_name' attribute is automatically included when using the Motor class and it should not be defined manually. If you wish to use a custom motor type, please modify the 'motorType' argument in the Motor class constructor accordingly. Overriding...");
				attributes["driver_name"] = motorType;
			}
			else
				attributes["driver_name"] = motorType;
			InitDevice("tacho-motor", port, attributes);
			Command = "stop";
			_motorType = motorType;
			_commands = GetAttributeString("commands").Split(' ');
			_countPerRot = GetAttributeInt("count_per_rot");
			_maxSpeed = GetAttributeInt("max_speed");
		}
		public void RunForever()
		{
			Command = "run-forever";
		}
		public void RunForever(int speed_real_units)
		{
			TargetSpeed = speed_real_units;
			Command = "run-forever";
		}
		public void Stop()
		{
			Command = "stop";
		}
		public void Reset()
		{
			Command = "reset";
		}
	}
}