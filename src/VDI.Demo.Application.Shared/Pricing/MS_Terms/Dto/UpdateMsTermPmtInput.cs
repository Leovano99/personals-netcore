using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class UpdateMsTermPmtInput
    {
        public int Id { get; set; }
        public short termNo { get; set; }
        public int finTypeID { get; set; }
        public string finTypeCode { get; set; }
        public short finStartDue { get; set; }
    }
}
