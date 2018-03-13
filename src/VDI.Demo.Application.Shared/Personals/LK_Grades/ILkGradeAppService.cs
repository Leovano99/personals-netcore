using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_Grades.Dto;

namespace VDI.Demo.Personals.LK_Grades
{
    public interface ILkGradeAppService : IApplicationService
    {
        ListResultDto<GetLkGradeDropdownListDto> GetLkGradeDropdown();
    }
}
