using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class GetMsGroupSchemaListInput : PagedAndSortedInputDto, IShouldNormalize
    {

        public string groupSchemaCode { get; set; }
        public string Filter { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "groupSchemaCode DESC";
            }
        }
    }
}
