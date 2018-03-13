using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.LegalDocument.DocTemplate.Dto;

namespace VDI.Demo.PSAS.LegalDocument.DocTemplate
{
    public interface IPSASDocTemplateAppService : IApplicationService
    {
        List<GetDocTemplateListDto> GetDocTemplate();
        List<GetDocTemplateListDto> GetDocTemplateByDocID(int docID);
    }
}
