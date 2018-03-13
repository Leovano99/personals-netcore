using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;

namespace VDI.Demo.Pricing.TR_BasePrices.Dto
{
    public class GetBasePriceListInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string ProjectCode { get; set; }

        public string Filter { get; set; }
        public void Normalize()
        {
            Sorting = "basePriceID DESC";
        }
    }
}
