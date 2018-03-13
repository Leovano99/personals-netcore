using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GetDataBookCodeListDto
    {
        public int bookingHeaderID { get; set; }
        public int projectID { get; set; }
        public string bookCode { get; set; }
        public DateTime bookDate { get; set; }
        public DateTime? cancelDate { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public double pctTax { get; set; }
    }
}
