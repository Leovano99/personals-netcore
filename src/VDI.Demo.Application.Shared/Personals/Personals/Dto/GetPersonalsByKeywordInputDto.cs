using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetPersonalsByKeywordInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string keyword { get; set; }
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "name ASC";
            }
        }
    }
}
