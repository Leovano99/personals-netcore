using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.GeneratePrice.Dto
{
    public class GetGeneratePriceListTermListDto
    {
        public string termRemarks { get; set; }
        public double discPct { get; set; }
        public decimal discAmt { get; set; }
    }
}
