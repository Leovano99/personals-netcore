using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class UpdateBookingDetailInputDto
    {
        public int bookingHeaderID { get; set; }
        public int termID { get; set; }
        public List<InsertTrBookingSalesAddDiscResultDto> listSalesDisc { get; set; }
        public List<InsertTrBookingItemPriceResultDto> listBookingItemPrice { get; set; }
        public List<InsertTrBookingHeaderTermResultDto> listBookingHeaderTerm { get; set; }
        public List<bookingDetailIDDto> listBookingDetail { get; set; }

    }
}
