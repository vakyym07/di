using System;

namespace CliVizualizator
{
    internal class ErrorHandler
    {
        public void PrintMessageAndExit(Error error)
        {
            Console.WriteLine(error.Message);
            Environment.Exit(0);
        }
    }
}