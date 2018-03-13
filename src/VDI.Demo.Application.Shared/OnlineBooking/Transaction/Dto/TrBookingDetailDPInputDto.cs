using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class TrBookingDetailDPInputDto
    {
        public int bookingDetailID { get; set; }
        public int termID { get; set; }
        public int unitID { get; set; }

        //public List<TrbookingDetailIDDto> bookingDetailID { get; set; }
    }
    //public class TrbookingDetailIDDto
    //{
    //    public int bookingDetailID { get; set; }
    //}
}
