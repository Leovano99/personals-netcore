using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class GetMsDiscountDto
    {
        public int discountID { get; set; }
        public string discountCode { get; set; }
        public string discountName { get; set; }
    }
}
