using System;
using System.Collections.Generic;
using System.Text;
using FileReader;

namespace FileReader
{
    class ValueProcessor : LineProcessor
    {
        static StringBuilder valueBuilder = new StringBuilder();
        public string _valueLine;
        public ValueProcessor(string Value)
        {
            _valueLine = Value;
        }
        public Claim ValueLinetoClaimObject(Dictionary<string, int> headersIndexes)
        {
            string[] arrStr = SplitLine(_valueLine);
            int sequenceNumber = int.Parse(arrStr[headersIndexes["SequenceNumber"]]);
            string claimID = arrStr[headersIndexes["ClaimID"]];
            string providerID = arrStr[headersIndexes["ProviderID"]];
            decimal billed = decimal.Parse(arrStr[headersIndexes["Billed"]]);
            decimal allowed = decimal.Parse(arrStr[headersIndexes["Allowed"]]);
            return new Claim(sequenceNumber, claimID, providerID, billed, allowed);
        }
    }
}
