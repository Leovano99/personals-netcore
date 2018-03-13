using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.GeneratePrice.Dto
{
    public class ExportToExcelUploadPriceListDto
    {
        public string arrayTerm { get; set; }
        public string productName { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public double price { get; set; }
        public string termName { get; set; }
        public decimal? bookingFee { get; set; }
        public int termMainID { get; set; }
        public string renovCode { get; set; }
        public int projectID { get; set; }
        public int clusterID { get; set; }
        public int categoryID { get; set; }
        public int productID { get; set; }
    }
}
