using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;

namespace VDI.Demo.Commission.MS_Developer_Schemas.Dto
{
    public class GetDeveloperSchemaListInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "schemaID DESC";
            }

        }
    }
}
