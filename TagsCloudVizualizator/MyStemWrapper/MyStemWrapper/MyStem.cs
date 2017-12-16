using System.Diagnostics;
using System.Text;
using ResultOf;

namespace MyStemWrapper
{
    public class MyStem
    {
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
            var buffer = new StringBuilder();
            if (!process.StandardError.EndOfStream)
            {
                while (!process.StandardError.EndOfStream)
                    buffer.Append(process.StandardError.ReadLine());
                return Result.Fail<string>(buffer.ToString());
            }
            while (!process.StandardOutput.EndOfStream)
                buffer.Append(process.StandardOutput.ReadLine());
            return buffer.ToString().AsResult();
        }
    }
}