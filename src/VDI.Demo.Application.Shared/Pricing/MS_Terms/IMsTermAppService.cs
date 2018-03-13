using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.Pricing.MS_Terms.Dto;

namespace VDI.Demo.Pricing.MS_Terms
{
    public interface IMsTermAppService : IApplicationService
    {
        List<GetDpCalcListDto> GetDpCalc();
        ListResultDto<GetAllMsTermListDto> GetTermCodeDropdown();
        ListResultDto<GetAllMsTermListDto> GetTermNoDropdownByProjectClusterCategoryProduct(GetTermNoDropdownByProjectClusterCategoryProductInputDto input);
        List<GetMsDiscountDto> GetDiscountDropdown();
        List<GetMsDiscountDto> GetDiscountDropdownExcludeSalesLaunchDisc();
        List<GetMsFinTypeDto> GetFinTypeDropdown();
        ListResultDto<GetAllMsTermListDto> GetMsTermByTermMainID(int Id);
        GetExistingTermDto GetExistingTermByTermCode(string termCode);
        JObject CheckAvailableTermCode(string termCode);
        void UpdateMsTermStatus(UpdateMsTermStatusInputDto input);

        int CreateMsTerm(CreateMsTermInput input);
        void UpdateMsTerm(UpdateMsTermInput input);

        void CreateMsTermPmt(CreateMsTermPmtInput input);
        void UpdateMsTermPmt(UpdateMsTermPmtInput input);

        int CreateMsTermMain(CreateMsTermMainInput input);
        void UpdateMsTermMain(UpdateMsTermMainInput input);

        void CreateMsTermDP(CreateMsTermDPInput input);
        void CreateOrUpdateMsTermDP(UpdateMsTermDPInput input);

        void CreateMsTermAddDisc(CreateMsTermAddDiscInput input);
        void UpdateMsTermAddDisc(UpdateMsTermAddDiscInput input);

        void CreateUniversalMsTerm(CreateUniversalMsTermInput input);
        void UpdateUniversalMsTerm(UpdateUniversalMsTermInput input);
    }
}
