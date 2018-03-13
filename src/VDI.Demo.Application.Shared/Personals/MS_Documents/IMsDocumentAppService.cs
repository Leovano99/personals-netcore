using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.MS_Documents.Dto;

namespace VDI.Demo.Personals.MS_Documents
{
    public interface IMsDocumentAppService : IApplicationService
    {
        ListResultDto<GetAllDocumentListDto> GetAllMsDocumentList();
    }
}
