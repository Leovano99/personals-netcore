using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.TR_BasePrices.Dto
{
    public class GetAllBasePriceListDto
    {
        public int basePriceID { get; set; }

        public string projectCode { get; set; }

        public string roadCode { get; set; }

        public string unitCode { get; set; }

        public string unitNo { get; set; }

        public double unitBasePrice { get; set; }
    }
}
