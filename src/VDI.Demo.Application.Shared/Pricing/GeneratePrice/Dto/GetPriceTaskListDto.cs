using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.GeneratePrice.Dto
{
    public class GetPriceTaskListDto
    {
        public int Id { get; set; }
        public string priceListFile { get; set; }
        public DateTime creationTime { get; set; }
    }
}
