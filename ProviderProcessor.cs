using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader
{
        public sealed class ProviderProcessor
    {
            private static readonly ProviderProcessor instance = new ProviderProcessor(); 
            static ProviderProcessor()
            {
            }
            private ProviderProcessor()
            {
            }
            public static ProviderProcessor Instance
            {
                get
                {
                    return instance;
                }
            }
        public string GetMedicareID(string ProviderID, Dictionary<string, string> Providers)
        {
            if (Providers.ContainsKey(ProviderID))
                return Providers[ProviderID];
            else
                return "NA";
        }
    }
 }
