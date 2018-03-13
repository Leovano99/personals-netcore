using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class InsertTRDetailDPResultDto
    {
        public short daysDue { get; set; }
        public int DPNo { get; set; }
        public double DPPct { get; set; }
        public decimal DPAmount { get; set; }
        public short monthsDue { get; set; }
    }
}
