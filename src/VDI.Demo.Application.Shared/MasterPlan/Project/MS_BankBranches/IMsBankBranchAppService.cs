using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Project.MS_BankBranches.Dto;

namespace VDI.Demo.MasterPlan.Project.MS_BankBranches
{
    public interface IMsBankBranchAppService : IApplicationService
    {
        ListResultDto<GetBankBranchListDto> GetMsBankBranch();
        ListResultDto<GetBankBranchListDto> GetMsBankBranchByBank(int bankId);
        ListResultDto<GetBankBranchTypeListDto> GetBankBranchTypeDropdown();

        void CreateMsBankBranch(CreateMsBankBranchInputDto input);
        JObject UpdateMsBankBranch(UpdateMsBankBranchInputDto input);
        void DeleteMsBankBranch(int Id);
    }
}
