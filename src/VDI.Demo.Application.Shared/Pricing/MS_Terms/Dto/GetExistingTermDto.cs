using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class GetExistingTermDto
    {
        public string entityCode { get; set; }
        public string termCode { get; set; }
        public decimal BFAmount { get; set; }
        public string projectName { get; set; }
        public string remarks { get; set; }
        public int? projectID { get; set; }
        public int termMainID { get; set; }
        public List<DtoGetTerm> setValue { get; set; }
    }

    public class DtoGetTerm
    {
        public int termID { get; set; }
        public int termMainID { get; set; }
        public int? projectID { get; set; }
        public string projectName { get; set; }
        public string entityCode { get; set; }
        public string termCode { get; set; }
        public decimal BFAmount { get; set; }
        public short termNo { get; set; }
        public short PPJBDue { get; set; }
        public string remarksMain { get; set; }
        public string remarks { get; set; }
        public int? termInstallment { get; set; }
        public string discBFCalcType { get; set; }
        public string DPCalcType { get; set; }
        public string GroupTermCode { get; set; }
        public int sortNo { get; set; }
        public int termPmtID { get; set; }
        public int finTypeID { get; set; }
        public string finTypeCode { get; set; }
        public short finStartDue { get; set; }
        public bool isActive { get; set; }
        public List<DtoGetDP> DtoDP { get; set; }
        public List<DtoGetDisc> DtoDisc { get; set; }
    }

    public class DtoGetDP
    {
        public int termDPID { get; set; }
        public short DPNo { get; set; }
        public short daysDue { get; set; }
        public double DPPct { get; set; }
        public decimal DPAmount { get; set; }
        public int? daysDueNewKP { get; set; }
    }

    public class DtoGetDisc
    {
        public int termAddDiscID { get; set; }
        public int discountID { get; set; }
        public double addDiscPct { get; set; }
        public decimal addDiscAmt { get; set; }
        public short addDiscNo { get; set; }
    }
}
