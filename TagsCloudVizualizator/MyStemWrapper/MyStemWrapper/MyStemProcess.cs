using System.Diagnostics;
using System.Text;

namespace MyStemWrapper
{
    internal class MyStemProcess
    {
        public MyStemProcess(string args)
        {
            StartInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "mystem.exe",
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.Default
            };
        }

        public ProcessStartInfo StartInfo { get; }
    }
}