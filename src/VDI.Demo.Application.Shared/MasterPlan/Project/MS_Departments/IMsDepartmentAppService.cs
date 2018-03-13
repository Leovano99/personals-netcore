using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Project.MS_Departments.Dto;

namespace VDI.Demo.MasterPlan.Project.MS_Departments
{
    public interface IMsDepartmentAppService : IApplicationService
    {
        ListResultDto<GetAllDepartmentListDto> GetAllMsDepartment();
        ListResultDto<GetMsDepartmentDropdownListDto> GetMsDepartmentDropdown();
        ListResultDto<GetMsDepartmentDropdownListDto> GetMsDepartmentPICInformation();
        void CreateMsDepartment(CreateOrUpdateMsDepartmentInput input);
        JObject UpdateMsDepartment(CreateOrUpdateMsDepartmentInput input);
        void DeleteMsDepartment(int Id);
    }
}
