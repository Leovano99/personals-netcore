using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;

namespace VDI.Demo.Pricing.MS_Discounts.Dto
{
    public class GetDiscountListInput : PagedAndSortedInputDto, IShouldNormalize
    {

        public string Filter { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "discountID DESC";
            }
        }
    }
}
