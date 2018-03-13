using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class GetTermListInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public int Id { get; set; }
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "termCode DESC";
            }

        }
    }
}
