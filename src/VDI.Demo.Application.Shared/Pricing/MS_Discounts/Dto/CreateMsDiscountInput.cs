using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Discounts.Dto
{
    public class CreateMsDiscountInput
    {
        public int? discountID { get; set; }
        public string discountCode { get; set; }

        public string discountName { get; set; }
        public bool isActive { get; set; }
    }
}
