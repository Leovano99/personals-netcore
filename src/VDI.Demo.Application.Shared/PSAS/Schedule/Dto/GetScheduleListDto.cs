using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class GetScheduleListDto
    {
        public int allocID { get; set; }
        public DateTime dueDate { get; set; }
        public string allocCode { get; set; }
        public decimal netAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal totalAmount { get; set; }
        public decimal netOutstanding { get; set; }
        public decimal VATOutstanding { get; set; }
        public decimal paymentAmount { get; set; }
        public decimal totalOutstanding { get; set; }
        public short schedNo { get; set; }
        public string remarks { get; set; }
        public List<DataPaymentListDto> dataPayment { get; set; }
    }

    public class DataPaymentListDto
    {
        public DateTime? clearDate { get; set; }
        public string transNo { get; set; }
        public int payNo { get; set; }
        public string payType { get; set; }
        public string otherType { get; set; }
        public decimal netAmountPayment { get; set; }
        public decimal vatAmountPayment { get; set; }
        public decimal totalAmountPayment { get; set; }
    }
}
