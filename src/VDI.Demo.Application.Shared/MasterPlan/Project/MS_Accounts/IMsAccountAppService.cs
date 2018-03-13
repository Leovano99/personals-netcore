using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Project.MS_Accounts.Dto;

namespace VDI.Demo.MasterPlan.Project.MS_Accounts
{
    public interface IMsAccountAppService : IApplicationService
    {
        ListResultDto<GetAllAccountListDto> GetAllMsAccount();
        void CreateMsAccount(CreateMsAccountInput input);
        JObject UpdateMsAccount(CreateMsAccountInput input);
        void DeleteMsAccount(int Id);
    }
}
