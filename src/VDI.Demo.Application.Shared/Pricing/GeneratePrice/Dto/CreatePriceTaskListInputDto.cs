using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.GeneratePrice.Dto
{
    public class CreatePriceTaskListInputDto
    {
        public int projectID { get; set; }
        public string priceListFile { get; set; }
    }
}
