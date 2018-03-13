using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Project.MS_Positions.Dto;

namespace VDI.Demo.MasterPlan.Project.MS_Positions
{
    public interface IMsPositionAppService : IApplicationService
    {
        ListResultDto<GetAllMsPositionListDto> GetAllMsPosition();
        ListResultDto<GetMsPositionByDepartmentListDto> GetMsPositionByDepartment(int departmentID);
        JObject UpdateMsPosition(MsPositionInput input);
        void DeleteMsPosition(int Id);
        void CreateMsPosition(MsPositionInput input);
    }
}
