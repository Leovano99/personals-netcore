using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.GeneratePrice.Dto
{
    public class ExportToExcelGeneratePriceListDto
    {
        //Digunakan untuk dinamisin Term headers
        public string arrayTerm { get; set; }
        public string productName { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public string interConnectable { get; set; }
        public string type { get; set; }
        public string typeBR { get; set; }
        public decimal? sizeSemiGross { get; set; }
        public string facing { get; set; }
        public string view { get; set; }
        public decimal? bookingFee { get; set; }
        public double? grossPrice { get; set; }
        public double? netPrice { get; set; }
        public double? xCash { get; set; }
        public double? xKpa { get; set; }
        public double? x12x { get; set; }
        public short? selisih1 { get; set; }
        public short? selisih2 { get; set; }
        public double? downPayment10KpaInstallment12 { get; set; }
        public double? installmentPerMonth12 { get; set; }
        public double? x24x { get; set; }
        public double? downPayment20KpaInstallment24 { get; set; }
        public double? installmentPerMonth24 { get; set; }
        public double? x36x { get; set; }
        public double? downPayment20KpaInstallment36 { get; set; }
        public double? installmentPerMonth36 { get; set; }
        public int termMainID { get; set; }

        public int projectID { get; set; }
    }
}
