using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;

namespace VDI.Demo.Personals.Personal_Members.Dto
{
    public class GetAllPersonalMemberInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string keyword { get; set; }
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "memberCode DESC";
            }
        }
    }
}
