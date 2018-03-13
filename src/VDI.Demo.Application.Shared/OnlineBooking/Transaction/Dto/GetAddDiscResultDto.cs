using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class GetAddDiscResultDto
    {
        public int unitID { get; set; }
        public int itemID { get; set; }
        public byte addDiscNo { get; set; }
        public double addDisc { get; set; }
        public string coCode { get; set; }
        public int bookingDetailID { get; set; }
        public int bookNo { get; set; }
        public int entityID { get; set; }
    }
}
