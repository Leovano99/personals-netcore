using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class GetTrUnitReservedDto
    {
        public int unitReservedID { get; set; }
        public int projectID { get; set; }
        public string projectName { get; set; }
        public int clusterID { get; set; }
        public string clusterName { get; set; }
        public int unitID { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public int termID { get; set; }
        public string termName { get; set; }
        public int renovID { get; set; }
        public string renovName { get; set; }
        public decimal bookingFee { get; set; }
        public decimal sellingPrice { get; set; }
    }
}
