using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Project.MS_Officers.Dto;

namespace VDI.Demo.MasterPlan.Project.MS_Officers
{
    public interface IMsOfficerAppService : IApplicationService
    {
        ListResultDto<GetAllOfficerListDto> GetAllMsOfficer();
        GetOfficerDivDto GetMsOfficerPerDepartmentDropdown();
        string GetOfficerPhoneByOfficerId(GetOfficerPhoneByOfficerIdInput input);
        GetCreateMsOfficerListDto CreateMsOfficer(CreateMsOfficerInput input);
        JObject UpdateMsOfficer(CreateMsOfficerInput input);
        void DeleteMsOfficer(int Id);
        List<GetOfficerByNameOrPositionListDto> GetOfficerByNameOrPosition(string name = null, string position = null);
    }
}
