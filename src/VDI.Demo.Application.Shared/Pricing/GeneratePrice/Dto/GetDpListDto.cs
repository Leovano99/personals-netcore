using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.GeneratePrice.Dto
{
    public class GetDpListDto
    {
        public string productName { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public short termNo { get; set; }
        public string termName { get; set; }
        public decimal bfAmount { get; set; }
        public short installmentPerMonth { get; set; }
        public double DP { get; set; }
    }
}
