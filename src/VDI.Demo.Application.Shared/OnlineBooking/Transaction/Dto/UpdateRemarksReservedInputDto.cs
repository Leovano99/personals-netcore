using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class UpdateRemarksReservedInputDto
    {
        public int termID { get; set; }

        public int unitID { get; set; }

        public decimal sellingPrice { get; set; }

        public int projectID { get; set; }

        public long userID { get; set; }
    }
}
