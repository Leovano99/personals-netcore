using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class UpdateMsTermDPInput
    {
        public int Id { get; set; }
        public byte DPNo { get; set; }
        public short daysDue { get; set; }
        public float DPPct { get; set; }
        public decimal DPAmount { get; set; }
        public int daysDueNewKP { get; set; }

        //For Create
        public string entityCode { get; set; }
        public string termCode { get; set; }
        public short termNo { get; set; }
        public int termID { get; set; }
    }
}
