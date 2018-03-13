using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_FinType.Dto
{
    public class CreateMsFinTypeInputDto
    {
        public string finTypeCode { get; set; }
        public string finTypeDesc { get; set; }
        public short finTimes { get; set; }
        public double pctComm { get; set; }
    }
}
