using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.TR_BasePrices.Dto
{
    public class UploadBasePriceInputDto
    {
        public string projectCode { get; set; }

        public string categoryName { get; set; }

        public List<BasePrice> BasePrices { get; set; }

    }

    public class BasePrice
    {
        public string unitCode { get; set; }

        public string unitNo { get; set; }

        public double unitBasePrice { get; set; }
    }
}
