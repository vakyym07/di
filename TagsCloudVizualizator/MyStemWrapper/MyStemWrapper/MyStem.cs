using System;
using System.Diagnostics;
using System.Text;

namespace MyStemWrapper
{
    public class MyStem
    {
        public string Result { get; private set; }

        public void Perform(string args)
        {
            var myStemProc = new MyStemProcess(args);
            using (var exeProcess = Process.Start(myStemProc.StartInfo))
            {
                Result = ReadAllStream(exeProcess);
            }
        }

        public void PerformWithDefaultArguments(string inputFile)
        {
            Perform($"-cgin --format xml -e {Encoding.Default.CodePage} {inputFile}");
        }

        private string ReadAllStream(Process process)
        {
            var buffer = new StringBuilder();
            while (!process.StandardOutput.EndOfStream)
                buffer.Append(process.StandardOutput.ReadLine());
            return buffer.ToString();
        }
    }
}