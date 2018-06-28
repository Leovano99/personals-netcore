using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.LK_Grades.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.LK_Grades
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkGrade)]
    public class LkGradeAppService : DemoAppServiceBase, ILkGradeAppService
    {
        private readonly IRepository<LK_Grade, string> _lkGradeRepo;

        public LkGradeAppService(IRepository<LK_Grade, string> lkGradeRepo)
        {
            _lkGradeRepo = lkGradeRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkGrade_GetLkGradeDropdown)]
        public ListResultDto<GetLkGradeDropdownListDto> GetLkGradeDropdown()
        {
            var result = (from x in _lkGradeRepo.GetAll()
                          select new GetLkGradeDropdownListDto
                          {
                              gradeCode = x.gradeCode,
                              gradeName = x.gradeName
                          }).ToList();

            return new ListResultDto<GetLkGradeDropdownListDto>(result);
        }
    }
}
