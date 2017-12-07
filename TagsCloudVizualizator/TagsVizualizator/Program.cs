using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MyStemWrapper;
using TextParser;

namespace TagsVizualizator
{
    class Program
    {
        static void Main(string[] args)
        {
            var myStem = new MyStem();
            myStem.PerformWithDefaultArguments("test.txt");
            var myStemParser = new MyStemResponceParser();
            myStemParser.Load(myStem.Result);
            var words = myStemParser.GetWords();
        }
    }
}
