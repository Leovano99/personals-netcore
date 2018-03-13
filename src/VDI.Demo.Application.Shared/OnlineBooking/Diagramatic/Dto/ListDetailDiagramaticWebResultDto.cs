using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Diagramatic.Dto
{
    public class ListDetailDiagramaticWebResultDto
    {
        public int unitID { get; set; }
        public int unitItemID { get; set; }
        public string unitCode { get; set; }
        public string unitStatus { get; set; }
        public decimal bookingFee { get; set; }
        public decimal sellingPrice { get; set; }
        public string unitNo { get; set; }
        public double size { get; set; }
        public string bedroom { get; set; }
        public decimal pricePerArea { get; set; }
        public List<ListTerm> term { get; set; }
    }

    public class ListTerm
    {
        public int termID { get; set; }
        public string termName { get; set; }
        public int renovID { get; set; }
        public string renovName { get; set; }
        public decimal price { get; set; }
    }
}
