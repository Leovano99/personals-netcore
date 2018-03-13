using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class CreateMsTermInput
    {
        public string entityCode { get; set; }
        public int entityID { get; set; }
        public string termCode { get; set; }
        public short termNo { get; set; }
        public short PPJBDue { get; set; }
        public string remarks { get; set; }
        public int termInstallment { get; set; }
        public string discBFCalcType { get; set; }
        public string DPCalcType { get; set; }
        public string GroupTermCode { get; set; }
        public int sortNo { get; set; }
        public int projectID { get; set; }
        public int termMainID { get; set; }
        public bool isActive { get; set; }
    }
}
