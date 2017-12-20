using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MyStemWrapper
{
    internal class MyStemProcess
    {
        private readonly string exePath =
            Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName).FullName;
        public MyStemProcess(string args)
        {
            StartInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = Path.Combine(exePath, "mystem.exe"),
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.Default
            };
        }

        public ProcessStartInfo StartInfo { get; }
    }
}