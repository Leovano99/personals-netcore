using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.BulkPayment.Dto
{
    public class CreateTrPaymentBulkInputDto
    {
        public string bulkPaymentKey { get; set; }
        public int? bookingHeaderID { get; set; }
        public int unitID { get; set; }
        public int payForID { get; set; }
        public int payTypeID { get; set; }
        public int othersTypeID { get; set; }
        public string psCode { get; set; }
        public string name { get; set; }
        public DateTime clearDate { get; set; }
        public decimal amount { get; set; }
    }
}
