using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_FinType.Dto
{
    public class GetAllMsFinTypeListDto
    {
        public int id { get; set; }
        public string finTypeCode { get; set; }
        public string finTypeDesc { get; set; }
        public int finTimes { get; set; }
        public double pctComm { get; set; }
        public DateTime inputTime { get; set; }
    }
}
