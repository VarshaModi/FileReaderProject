using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader
{
    class HeaderProcessor : LineProcessor
    {
        static StringBuilder headerBuilder = new StringBuilder();
        public string _headerLine;
        public HeaderProcessor(string Header)
        {
            _headerLine = Header;
        }

        public bool CheckValidHeader()
        {
            if (!_headerLine.Contains("SequenceNumber") || !_headerLine.Contains("ClaimID")
                 || !_headerLine.Contains("ProviderID") || !_headerLine.Contains("Billed") || !_headerLine.Contains("Allowed"))
                return false;
            else
                return true;

        }
        public string BuildOutputHeader()
        {
            string outputHeader = string.Empty;
            headerBuilder.Append("SequenceNumber \t ClaimID \t MillimanBilled \t MillimanAllowed \t MedicareID ");
            return headerBuilder.ToString();
        }
        public Dictionary<string, int> FindIndexOfHeaders()
        {
            string[] arrStr = SplitLine(_headerLine);
            Dictionary<string, int> headersIndexes = new Dictionary<string, int>();
            for (int i = 0; i < arrStr.Length; i++)
            {
                string strline = arrStr[i];
                if (strline == "SequenceNumber" || strline == "ClaimID" || strline == "ProviderID" || strline == "Billed" || strline == "Allowed")
                {
                    headersIndexes.Add(strline, i);
                }
            }
            return headersIndexes;
        }
        //public string BuildOutputHeader()
        //{
        //    string outputHeader = string.Empty;
        //    string[] arrStr = SplitLine(_headerLine);
        //    for (int i = 0; i < arrStr.Length; i++)
        //    {
        //        string strline = arrStr[i];
        //        if (strline == "SequenceNumber" || strline == "ClaimID" )
        //        {
        //            headerBuilder.Append($"\"{strline}\"\t");
        //        }
        //    }
        //    headerBuilder.Append("\"MillimanBilled\"" + "\t");
        //    headerBuilder.Append("\"MillimanAllowed\"" + "\t");
        //    headerBuilder.Append("MedicareID");
        //    outputHeader = headerBuilder.ToString().Replace('"', ' ');
        //    headerBuilder.Clear();
        //    return outputHeader;
        //}
    }
}
