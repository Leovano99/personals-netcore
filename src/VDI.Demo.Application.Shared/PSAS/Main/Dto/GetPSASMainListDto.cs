using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Main.Dto
{
    public class GetPSASMainListDto
    {
        public string projectName { get; set; }
        public string productName { get; set; }
        public string territory { get; set; }
        public string termCode { get; set; }
        public string termNo { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string NPWP { get; set; }
        public string phone { get; set; }
        public string psCode { get; set; }
        public string salesEvent { get; set; }
        public string transactionCome { get; set; }
        public string payType { get; set; }
        public string bankName { get; set; }
        public decimal amount { get; set; }
        public string noRekening { get; set; }
        public string schema { get; set; }
        public string memberID { get; set; }
        public string memberName { get; set; }
        public DateTime bookDate { get; set; }
        public string bookedStatus { get; set; }
        public DateTime? cancelDate { get; set; }
        public string remarks { get; set; }
        public string status { get; set; }
        public string reason { get; set; }
        public string reasonRemarks { get; set; }
    }

    public class GetPSASMainHeaderListDto
    {
        public string itemName { get; set; }
        public string coCode { get; set; }
        public double area { get; set; }
        public decimal netPriceMKT { get; set; }
        public decimal netPriceComm { get; set; }
        public decimal netNetPrice { get; set; }
    }
}
