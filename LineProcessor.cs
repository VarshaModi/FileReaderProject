using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader
{
    public abstract class LineProcessor
    {
        public string _inputFilePath;
        public string[] SplitLine(string Line)
        {
            string[] arrStr = Line.Split("\t", StringSplitOptions.RemoveEmptyEntries);
            return arrStr;
        }
    }
}
