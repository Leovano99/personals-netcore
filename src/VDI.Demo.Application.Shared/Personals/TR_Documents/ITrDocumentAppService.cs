using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_Documents.Dto;

namespace VDI.Demo.Personals.TR_Documents
{
    public interface ITrDocumentAppService : IApplicationService
    {
        void UpdateDocument(UpdateDocumentDto input);
        void deleteDocument(string psCode, string documentType);
    }
}
