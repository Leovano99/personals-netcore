using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;

namespace VDI.Demo.PSAS.LegalDocument.KuasaDireksi.Dto
{
    public class PSASListOfKuasaDireksiInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "projectName,docCode,kuasaDireksiCode,remarks,isActive";
            }
        }
    }
}
