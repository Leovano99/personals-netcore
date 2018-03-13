using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Price.Dto
{
    public class GetUniversalListDto
    {
        public GetPSASPriceListDto PSASPrice { get; set; }

        public GetPSASPriceListDto MarketingPrice { get; set; }

        public GetPSASPriceListDto CommisionPrice { get; set; }
    }
}
