using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.Document.Dto;

namespace VDI.Demo.PSAS.Document
{
    public interface IPSASDocumentAppService : IApplicationService
    {
        List<GetDocumentDropdownListDto> GetDocumentDropdown();
    }
}
