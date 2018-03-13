using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Project.MS_Banks.Dto;

namespace VDI.Demo.MasterPlan.Project.MS_Banks
{
    public interface IMsBankAppService : IApplicationService
    {
        ListResultDto<GetAllBankListDto> GetAllMsBank();
        ListResultDto<GetBankDropDownListDto> GetMsBankDropDown();
        ListResultDto<GetTypeBankDropDownListDto> GetBankTypeDropdown();
        void CreateMsBank(CreateMsBankInput input);
        JObject UpdateMsBank(GetAllBankListDto input);
        void DeleteMsBank(int Id);
        ListResultDto<GetBankListDto> GetAllMsBankList();
    }
}
