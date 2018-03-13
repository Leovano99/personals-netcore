using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class TrUnitOrderDetailInputDto
    {
        public int orderHeaderID { get; set; }

        public long userID { get; set; }

        public List<UnitUniversalResultDto> arrUnit { get; set; }
    }
    public class ListUnitOrderDetail
    {
        public int projectID { get; set; }
        public int unitID { get; set; }
        public decimal sellingPrice { get; set; }
        public decimal bfAmount { get; set; }

    }
}
