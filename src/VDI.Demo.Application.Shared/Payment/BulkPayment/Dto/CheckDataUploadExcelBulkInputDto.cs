using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.BulkPayment.Dto
{
    public class CheckDataUploadExcelBulkInputDto
    {
        public int accID { get; set; }
        public int userID { get; set; }
        public string bookCode { get; set; }
        public string payForCode { get; set; }
        public string payTypeCode { get; set; }
        public string othersTypeCode { get; set; }
    }
}
