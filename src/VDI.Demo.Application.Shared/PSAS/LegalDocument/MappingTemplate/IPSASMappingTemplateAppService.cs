using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.LegalDocument.MappingTemplate.Dto;

namespace VDI.Demo.PSAS.LegalDocument.MappingTemplate
{
    public interface IPSASMappingTemplateAppService : IApplicationService
    {
        PagedResultDto<GetMappingTemplateListDto> GetMappingTemplate(GetMappingTemplateInputDto input);
        void CreateMappingTemplate(CreateMappingTemplateInputDto input);
        void UpdateMappingTemplate(CreateMappingTemplateInputDto input);
        void DeleteMappingTemplate(int mappingTemplateID);
    }
}
