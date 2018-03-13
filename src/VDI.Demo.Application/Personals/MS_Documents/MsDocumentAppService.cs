using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.MS_Documents.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.MS_Documents
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterDocument)]
    public class MsDocumentAppService : DemoAppServiceBase, IMsDocumentAppService
    {
        private readonly IRepository<MS_Document, string> _msDocumentRepo;

        public MsDocumentAppService(
            IRepository<MS_Document, string> msDocumentRepo
        )
        {
            _msDocumentRepo = msDocumentRepo;
        }

        public ListResultDto<GetAllDocumentListDto> GetAllMsDocumentList()
        {
            var getAllData = (from A in _msDocumentRepo.GetAll()
                              select new GetAllDocumentListDto
                              {
                                  documentType = A.documentType,
                                  documentName = A.documentName
                              }).ToList();

            return new ListResultDto<GetAllDocumentListDto>(getAllData);
        }
    }
}
