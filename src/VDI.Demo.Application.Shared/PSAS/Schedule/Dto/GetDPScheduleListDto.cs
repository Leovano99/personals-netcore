using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class GetDPScheduleListDto
    {
        public decimal DPAmount { get; set; }

        public double DPPct { get; set; }

        public byte dpNo { get; set; }

        public short daysDue { get; set; }

        public short? monthsDue { get; set; }

        public int? DpCalcID { get; set; }

        public string DPCalcDesc { get; set; }
    }
}
