using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GetDataPrintORByTransNoListDto
    {
        public int accountID { get; set; }
        public string accountCode { get; set; }
        public string accountName { get; set; }
        public DateTime paymentDate { get; set; }
        public DateTime? clearDate { get; set; }
        public int bookingHeaderID { get; set; }
        public string bookCode { get; set; }
        public int payForID { get; set; }
        public string payFor { get; set; }
        public int projectID { get; set; }
        public string project { get; set; }
        public string ketPaymentHeader { get; set; }
        public List<GetDataPayment> listDataPayment { get; set; }
    }

    public class GetDataPayment
    {
        public int payNo { get; set; }
        public string payTypeCode { get; set; }
        public string bankName { get; set; }
        public string chequeNo { get; set; }
        public string status { get; set; }
        public decimal amount { get; set; }
        public decimal netAlloc { get; set; }
        public string ket { get; set; }
        public DateTime dueDate { get; set; }
    }
}
