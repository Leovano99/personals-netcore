using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MarketingFactor.Dto
{
    public class ExportMarketingFactorListDto
    {
        public string schemeName { get; set; }
        public List<DataUnitDto> unit { get; set; }
    }

    public class DataUnitDto
    {
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public string priceM2 { get; set; }
    }
}
