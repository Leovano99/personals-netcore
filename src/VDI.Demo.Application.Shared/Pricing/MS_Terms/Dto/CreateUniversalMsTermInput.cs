using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class CreateUniversalMsTermInput
    {
        public string entityCode { get; set; }
        public string termCode { get; set; }
        public string termDesc { get; set; }
        public string famDiscCode { get; set; }
        public decimal BFAmount { get; set; }
        public string remarks { get; set; }
        public int projectID { get; set; }
        public List<DtoTerm> setValue { get; set; }
    }

    public class DtoTerm
    {
        public short termNo { get; set; }
        public short PPJBDue { get; set; }
        public string remarks { get; set; }
        public int finTypeID { get; set; }
        public string finTypeCode { get; set; }
        public short finStartDue { get; set; }
        public int termInstallment { get; set; }
        public string discBFCalcType { get; set; }
        public string DPCalcType { get; set; }
        public string GroupTermCode { get; set; }
        public int sortNo { get; set; }
        public bool isActive { get; set; }
        public List<DtoDP> DtoDP { get; set; }
        public List<DtoDisc> DtoDisc { get; set; }
    }

    public class DtoDP
    {
        public byte DPNo { get; set; }
        public short daysDue { get; set; }
        public float DPPct { get; set; }
        public decimal DPAmount { get; set; }
        public int? daysDueNewKP { get; set; }

    }

    public class DtoDisc
    {
        public int discountID { get; set; }
        public byte addDiscNo { get; set; }
        public float addDiscPct { get; set; }
        public decimal addDiscAmt { get; set; }
    }
}
