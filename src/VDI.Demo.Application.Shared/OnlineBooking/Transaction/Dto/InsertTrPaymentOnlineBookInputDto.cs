using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class InsertTrPaymentOnlineBookInputDto
    {
        public int UnitOrderHeaderID { get; set; }
        
        public string paymentType { get; set; }
        
        public decimal paymentAmt { get; set; }
        
        public string bankName { get; set; }
        
        public string bankAccName { get; set; }
        
        public string bankAccNo { get; set; }
        
        public string resiImage { get; set; }
        
        public string resiNo { get; set; }
        
        public string offlineType { get; set; }
    }
}
