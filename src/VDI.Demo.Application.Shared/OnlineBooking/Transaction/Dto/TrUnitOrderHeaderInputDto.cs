using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class TrUnitOrderHeaderInputDto
    {
        public string psCode { get; set; }
        public int payTypeID { get; set; }
        public long userID { get; set; }

        public decimal totalAmt { get; set; }

        public List<UnitUniversalResultDto> arrUnit { get; set; }


    }
}
