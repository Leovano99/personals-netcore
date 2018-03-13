using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Project.MS_Entities.Dto;

namespace VDI.Demo.MasterPlan.Project.MS_Entities
{
    public interface IMsEntityAppService : IApplicationService
    {
        ListResultDto<GetAllMsEntityListDto> GetAllMsEntity();
        ListResultDto<GetAllMsEntityListDto> GetMsEntityDropdown();
        void CreateMsEntity(GetAllMsEntityListDto input);
        void UpdateMsEntity(GetAllMsEntityListDto input);
        void DeleteMsEntity(int Id);
    }
}
