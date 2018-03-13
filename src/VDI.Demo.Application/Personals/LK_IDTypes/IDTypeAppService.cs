using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.LK_IDTypes.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.LK_IDTypes
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkIdType)]
    public class IDTypeAppService : DemoAppServiceBase, IIDTypeAppService
    {
        private readonly IRepository<LK_IDType, string> _idTypeRepo;

        public IDTypeAppService(
            IRepository<LK_IDType, string> idTypeRepo
        )
        {
            _idTypeRepo = idTypeRepo;
        }

        public ListResultDto<GetAllIDTypeListDto> GetAllIDTypeList()
        {
            var getAllData = (from A in _idTypeRepo.GetAll()
                              select new GetAllIDTypeListDto
                              {
                                  idType = A.idType,
                                  idTypeName = A.idTypeName
                              }).ToList();

            return new ListResultDto<GetAllIDTypeListDto>(getAllData);
        }
    }
}
