using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class UpdateUniversalMsTermInput
    {
        public int termMainID { get; set; }
        public decimal BFAmount { get; set; }
        public string remarks { get; set; }
        public int? projectID { get; set; }
        public List<DtoTermUpdate> setValue { get; set; }
    }

    public class DtoTermUpdate
    {
        public int termID { get; set; }
        public int termPmtID { get; set; }
        public short termNo { get; set; }
        public int sortNo { get; set; }
        public short PPJBDue { get; set; }
        public string remarks { get; set; }
        public string finTypeCode { get; set; }
        public int finTypeID { get; set; }
        public short finStartDue { get; set; }
        public int termInstallment { get; set; }
        public string DPCalcType { get; set; }
        public bool isActive { get; set; }
        public List<DtoDPUpdate> DtoDP { get; set; }
        public List<DtoDiscUpdate> DtoDisc { get; set; }
    }

    public class DtoDPUpdate
    {
        public int termDPID { get; set; }
        public byte DPNo { get; set; }
        public short daysDue { get; set; }
        public float DPPct { get; set; }
        public decimal DPAmount { get; set; }
        public int daysDueNewKP { get; set; }

    }

    public class DtoDiscUpdate
    {
        public int termAddDiscID { get; set; }
        public int discountID { get; set; }
        public byte addDiscNo { get; set; }
        public float addDiscPct { get; set; }
        public decimal addDiscAmt { get; set; }
    }
}
