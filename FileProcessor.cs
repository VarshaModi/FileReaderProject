using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FileReader;
using System.Linq;

namespace FileReader
{
    class FileProcessor
    {
        static StringBuilder builder = new StringBuilder();
        static StringBuilder valueBuilder = new StringBuilder();
        public string _inputClaimFilePath;
        public string _inputProviderFilePath;
        public string _outputFilePath;
        public FileProcessor(string InputClaimFilePath, string InputProviderFilePath, string outputFilePath)
        {
            _inputClaimFilePath = InputClaimFilePath;
            _inputProviderFilePath = InputProviderFilePath;
            _outputFilePath = outputFilePath;

        }
        public void ReadAllClaims()
        {
            int flag = 0;
            int flag2 = 0;
            string HeaderLine = "";
            using (StreamReader sr = new StreamReader(_inputClaimFilePath))
            {
                HeaderLine = sr.ReadLine();
                HeaderProcessor headerProcessor = new HeaderProcessor(HeaderLine);
                if (!headerProcessor.CheckValidHeader())
                    Console.WriteLine("The input Claim file provided is invalid");
                else
                {
                    builder.Append($"{headerProcessor.BuildOutputHeader()}\n");
                    Dictionary<string, int> headersIndexes = new Dictionary<string, int>();

                    string[] readProviderText = File.ReadAllLines(_inputProviderFilePath);
                    List<string> allProviders = new List<string>(readProviderText.Skip(1));
                    Dictionary<string, string> dictProviders = new Dictionary<string, string>();
                    foreach (var (providerID, MedicareID) in from item in allProviders
                                                             let arrProviderList = item.Split('\t')
                                                             let providerID = Convert.ToString(arrProviderList[0])
                                                             let MedicareID = Convert.ToString(arrProviderList[1])
                                                             select (providerID, MedicareID))
                    {
                        dictProviders.Add(providerID, MedicareID);
                    }
                    decimal MillimanBilled = 0;
                    Claim claimLine = null;
                    headersIndexes = headerProcessor.FindIndexOfHeaders();
                    string valueLine = string.Empty;
                    while ((valueLine = sr.ReadLine()) != null)
                    {
                        ValueProcessor valueProcessor = new ValueProcessor(valueLine);
                        Claim claimLineNext = valueProcessor.ValueLinetoClaimObject(headersIndexes);
                        if (flag == 1)
                        {
                            flag2 = 1;
                            if (claimLineNext.ClaimID == claimLine.ClaimID)
                            {
                                MillimanBilled += claimLineNext.Billed;
                                if (claimLineNext.Allowed > claimLine.Allowed)
                                    claimLine = claimLineNext;
                                else if (claimLineNext.Allowed == claimLine.Allowed)
                                {
                                    if (claimLineNext.SequenceNumber > claimLine.SequenceNumber)
                                        claimLine = claimLineNext;
                                }
                            }
                            else
                            {
                                string MedicareID = ProviderProcessor.Instance.GetMedicareID(claimLine.ProviderID, dictProviders);
                                valueBuilder.Append($"{claimLine.SequenceNumber}\t");
                                valueBuilder.Append($"{claimLine.ClaimID}\t");
                                valueBuilder.Append($"{MillimanBilled}\t");
                                valueBuilder.Append($"{claimLine.Allowed}\t");
                                valueBuilder.Append($"{MedicareID}\n");
                                claimLine = claimLineNext;
                                MillimanBilled = claimLine.Billed;
                            }
                        }
                        if (flag == 0)
                        {
                            claimLine = claimLineNext;
                            MillimanBilled = claimLine.Billed;
                            flag = 1;
                        }
                    }
                    if (flag == 1 && flag2 == 0 || flag == 1 && flag2 == 1)
                    {
                        string MedicareID = ProviderProcessor.Instance.GetMedicareID(claimLine.ProviderID, dictProviders);
                        valueBuilder.Append($"{claimLine.SequenceNumber}\t");
                        valueBuilder.Append($"{claimLine.ClaimID}\t");
                        valueBuilder.Append($"{MillimanBilled}\t");
                        valueBuilder.Append($"{claimLine.Allowed}\t");
                        valueBuilder.Append($"{MedicareID}\n");
                    }
                    builder.Append(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                using (StreamWriter writer = new StreamWriter(_outputFilePath))
                {
                        writer.Write(builder.ToString());
                }
                builder.Clear();
            }
        }
    }
}