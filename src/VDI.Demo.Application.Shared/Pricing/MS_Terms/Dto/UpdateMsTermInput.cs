using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class UpdateMsTermInput
    {
        public int id { get; set; }
        public int termMainID { get; set; }
        public decimal BFAmount { get; set; }
        public int? projectID { get; set; }
        public short termNo { get; set; }
        public short PPJBDue { get; set; }
        public string remarks { get; set; }
        public int? termInstallment { get; set; }
        public int sortNo { get; set; }
        public string DPCalcType { get; set; }
        public bool isActive { get; set; }
    }
}
