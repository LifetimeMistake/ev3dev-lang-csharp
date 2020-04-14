using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace ev3dev.Core
{
    public class Device
	{
		public static string ROOT = "/sys/";
		public string path; //readonly
		public string Class; //readonly
		public LegoPort Address; //readonly
		public string DriverName;
		
		public Device(string classDir, LegoPort port, Dictionary<string,string> attributes)
		{
			InitDevice(classDir, port, attributes);
		}

		public Device() {}

		public void InitDevice(string classDir, LegoPort port, Dictionary<string,string> attributes)
		{
			bool device_found = false;
			foreach(string dir in IOError_Workaround.GetDirectories(Path.Combine(ROOT, "class", classDir)))
			{
				path = dir;
				if(Utils.String2LegoPort(GetAttributeString("address")) != port) continue;
				bool attributes_match = true;
				foreach(KeyValuePair<string,string> attribute in attributes)
				{
					if(attribute.Key == "deviceroot_startswith")
					{
						if(!Path.GetFileName(dir).StartsWith(attributes["deviceroot_startswith"]))
						{
							attributes_match = false;
							break;
						}
					}
					else if(GetAttributeString(attribute.Key) != attribute.Value)
					{
						attributes_match = false;
						break;
					}
				}

				if(!attributes_match)
					continue;

				// Found the desired device
				Class = classDir;
				Address = port;
				DriverName = GetAttributeString("driver_name");
				device_found = true;
				break;
			}

			if(!device_found)
			{
				path = null;
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("No devices matching the attributes below:");
				sb.AppendLine($"Class: {classDir}");
				sb.AppendLine($"Port: {port.ToString()} ({Utils.LegoPort2String(port)}");
				sb.AppendLine("Attributes:");
				foreach(KeyValuePair<string,string> kvp in attributes)
					sb.AppendLine($"   '{kvp.Key}'='{kvp.Value}'");

				throw new Exception(sb.ToString());
			}
		}

		public string GetAttributeString(string attributeName)
		{
			using(StreamReader sr = OpenStreamReader(attributeName))
			{
				return sr.ReadToEnd().TrimEnd();
			}
		}

		public void SetAttributeString(string attributeName, string attributeValue)
		{
			using(StreamWriter sw = OpenStreamWriter(attributeName))
			{
				sw.Write(attributeValue);
			}
		}

		public int GetAttributeInt(string attributeName)
		{
			return int.Parse(GetAttributeString(attributeName));
		}

		public void SetAttributeInt(string attributeName, int attributeValue)
		{
			SetAttributeString(attributeName, attributeValue.ToString());
		}

		private StreamReader OpenStreamReader(string name)
        {
			Console.WriteLine($"OpenNewReader: {Path.Combine(path, name)}");
            return new StreamReader(new FileStream(Path.Combine(path, name), FileMode.Open, FileAccess.Read));
        }

        private StreamWriter OpenStreamWriter(string name)
        {
			Console.WriteLine($"OpenNewWriter: {Path.Combine(path, name)}");
            return new StreamWriter(new FileStream(Path.Combine(path, name), FileMode.Open, FileAccess.Write));
        }
	}
}