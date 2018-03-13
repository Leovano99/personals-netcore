using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Diagramatic.Dto
{
    public class ListPriceResultDto
    {
        public int termID { get; set; }
        public short termNo { get; set; }
        public string termName { get; set; }
        public string termCode { get; set; }

        public int unitID { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public decimal grossPrice { get; set; }
        public double disc1 { get; set; }
        public double disc2 { get; set; }
        public decimal bookingFee { get; set; }
    }
}
