using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class InsertTrBookingDetailScheduleInputDto
    {
        public int bookingHeaderID { get; set; }

        public List<UpdateNetPriceResultDto> listNetNetPrice { get; set; }
    }
}
