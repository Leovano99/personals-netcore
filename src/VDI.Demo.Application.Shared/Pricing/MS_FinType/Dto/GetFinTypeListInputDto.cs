using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;

namespace VDI.Demo.Pricing.MS_FinType.Dto
{
    public class GetFinTypeListInputDto : PagedAndSortedInputDto, IShouldNormalize
    {

        public int finTypeCode { get; set; }
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "finTypeCode DESC";
            }

        }
    }
}
