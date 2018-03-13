using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class GetListTermDPResultDto
    {
        public byte DPNo { get; set; }
        public short daysDue { get; set; }
        public double DPPct { get; set; }
        public short finTimes { get; set; }
        public decimal DPAmount { get; set; }
        public int bookingDetailID { get; set; }
        public int entityID { get; set; }
    }
}
