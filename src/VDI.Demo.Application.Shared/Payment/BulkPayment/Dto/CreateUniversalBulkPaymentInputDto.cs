using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.BulkPayment.Dto
{
    public class CreateUniversalBulkPaymentInputDto
    {
        public int accID { get; set; }
        public int? bookingHeaderID { get; set; }
        public int payForID { get; set; }
        public string bookCode { get; set; }
        public string psCode { get; set; }
        public string name { get; set; }
        public int unitID { get; set; }
        public DateTime clearDate { get; set; }
        public double pctTax { get; set; }
        public List<DataAlloc> dataScheduleList { get; set; }
        public List<DataPayment> dataForPayment { get; set; }
    }

    public class DataPayment
    {
        public decimal amount { get; set; }
        public int payTypeID { get; set; }
        public int othersTypeID { get; set; }
        public string othersTypeCode { get; set; }
        public List<DataAlloc> dataAllocList { get; set; }
    }

    public class DataAlloc
    {
        public int allocID { get; set; }
        public short schedNo{ get; set; }
        public decimal amount { get; set; }
        public decimal amountPerSchedNo { get; set; }
    }
}
