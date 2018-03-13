using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class GetOriginalScheduleInputDto
    {
        public int bookingHeaderID { get; set; }

        public List<UpdateBfAmoutResultDto> listBfAmount { get; set; }

        public List<bookingDetailIDDto> listPctTax { get; set; }

        public List<UpdateNetPriceResultDto> listNetNetPrice { get; set; }

        public TrBookingHeaderResultDto bookDate { get; set; }

        public List<InsertTRDetailDPResultDto> listDetailDP { get; set; }
    }
}
