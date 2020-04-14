using System.Linq;
using System.Diagnostics;

namespace ev3dev.Core
{
    public static class IOError_Workaround
    {
        private static string ExecuteBashCommand(string command)
        {
            command = command.Replace("\"","\"\"");

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \""+ command + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            proc.WaitForExit();

            return proc.StandardOutput.ReadToEnd().TrimEnd();
        }

        public static string[] GetDirectories(string path)
        {
            string output = ExecuteBashCommand($"find {path} -type d,l");
            return output.Split('\n').Skip(1).ToArray();
        }
    }
}
