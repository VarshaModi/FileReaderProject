using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader
{
    class Claim
    {
        //Read-Only Properties
        public int SequenceNumber { get; }
        public string ClaimID { get; }
        public string ProviderID { get; }
        public decimal Billed { get; }
        public decimal Allowed { get; }
        public Claim(int sequenceNumber, string claimID, string providerID, decimal billed, decimal allowed)
        {
            SequenceNumber = sequenceNumber;
            ClaimID = claimID;
            ProviderID = providerID;
            Billed = billed;
            Allowed = allowed;
        }
    }
}
