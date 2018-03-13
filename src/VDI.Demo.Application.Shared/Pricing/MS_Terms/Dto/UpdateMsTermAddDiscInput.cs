using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class UpdateMsTermAddDiscInput
    {
        public int Id { get; set; }
        public string entityCode { get; set; }
        public string termCode { get; set; }
        public short termNo { get; set; }
        public byte addDiscNo { get; set; }
        public int discountID { get; set; }
        public float addDiscPct { get; set; }
        public decimal addDiscAmt { get; set; }
    }
}
