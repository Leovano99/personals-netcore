using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class PrintORDto
    {
        public int projectID { get; set; }
        public string projectImage { get; set; }
        public int accountID { get; set; }
        public string accountName { get; set; }
        public string accountAddress { get; set; }
        public string accountNPWP { get; set; }
        public string transNo { get; set; }
        public DateTime paymentDate { get; set; }
        public string paymentDateFormat { get; set; }
        public string psCode { get; set; }
        public string custName { get; set; }
        public string custAddress { get; set; }
        public string custNPWP { get; set; }
        public string lantai { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public string clusterName { get; set; }
        public int payForID { get; set; }
        public string paymentFor { get; set; }
        public string dataAllocHtml { get; set; }
        public string userLogin { get; set; }
        public decimal totalAmount { get; set; }
        public string totalAmountFormat { get; set; }
        public string ppn { get; set; }
        public string totalAll { get; set; }
        public string terbilang { get; set; }
        public List<GetDataAlloc> listDataAlloc { get; set; }
    }

    public class GetDataAlloc
    {
        public string payType { get; set; }
        public decimal netAlloc { get; set; }
        public string netAllocFormat { get; set; }
    }
}
