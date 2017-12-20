using System.Diagnostics;
using System.Text;
using ResultOf;

namespace MyStemWrapper
{
    public class MyStem
    {
        private readonly int timeout = 1000;
        public Result<string> PerformResult { get; private set; }

        public void Perform(string args)
        {
            var myStemProc = new MyStemProcess(args);
            using (var exeProcess = Process.Start(myStemProc.StartInfo))
            {
                PerformResult = ReadAllStream(exeProcess);
            }
        }

        public void PerformWithDefaultArguments(string inputFile)
        {
            Perform($"-cgin --format xml -e {Encoding.Default.CodePage} {inputFile}");
        }

        private Result<string> ReadAllStream(Process process)
        {
            return process.StandardOutput.ReadToEnd().AsResult();
        }
    }
}