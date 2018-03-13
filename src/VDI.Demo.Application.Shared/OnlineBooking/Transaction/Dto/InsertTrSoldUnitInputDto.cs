using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class InsertTrSoldUnitInputDto
    {
        public string scmCode { get; set; }

        public string memberCode { get; set; }

        public int unitID { get; set; }

        public long userID { get; set; }

        public decimal sellingPrice { get; set; }
        
        public List<UpdateNetPriceResultDto> netNetPrice { get; set; }

        public List<bookingDetailIDDto> listBookingDetail { get; set; }

        public TrBookingHeaderResultDto listBookingHeader { get; set; }
    }
}
