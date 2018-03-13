using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class GetMsFinTypeDto
    {
        public int finTypeID { get; set; }
        public string finTypeCode { get; set; }
        public string finTypeDesc { get; set; }
        public short finTimes { get; set; }
    }
}
