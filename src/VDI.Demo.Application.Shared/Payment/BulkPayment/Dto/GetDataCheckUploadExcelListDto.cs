using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.BulkPayment.Dto
{
    public class GetDataCheckUploadExcelListDto
    {
        public int bookingHeaderID { get; set; }
        public string bookCode { get; set; }
        public string psCode { get; set; }
        public string name { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public int unitID { get; set; }
        public string payForCode { get; set; }
        public string payTypeCode { get; set; }
        public string othersTypeCode { get; set; }
        public int payForID { get; set; }
        public int payTypeID { get; set; }
        public int othersTypeID { get; set; }
        public double pctTax { get; set; }
        public string message { get; set; }
        public List<GetDataSchedule> dataSchedule { get; set; }
    }

    public class GetDataSchedule
    {
        public short schedNo { get; set; }
        public int allocID { get; set; }
        public string allocDesc { get; set; }
        public decimal amount { get; set; }
    }
}
