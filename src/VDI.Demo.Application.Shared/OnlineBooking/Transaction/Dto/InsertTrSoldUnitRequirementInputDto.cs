using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class InsertTrSoldUnitRequirementInputDto
    {
        public string scmCode { get; set; }
        
        public long userID { get; set; }

        public int unitID { get; set; }

        public string memberCode { get; set; }

        public List<bookingDetailIDDto> listBookingDetail { get; set; }
    }
}
