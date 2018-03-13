using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class InsertTRUnitReservedInputDto
    {
        public int unitID { get; set; }
        public int userID { get; set; }

        public int renovID { get; set; }

        public decimal sellingPrice { get; set; }

        public int projectID { get; set; }
        public int termID { get; set; }
    }
}
