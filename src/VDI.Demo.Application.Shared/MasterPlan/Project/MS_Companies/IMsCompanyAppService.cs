using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Project.MS_Companies.Dto;

namespace VDI.Demo.MasterPlan.Project.MS_Companies
{
    public interface IMsCompanyAppService : IApplicationService
    {
        ListResultDto<GetMsCompanyListDto> GetMsCompany();
        ListResultDto<GetMsCompanyDropDownListDto> GetMsCompanyDropDown();
        GetDetailMsCompanyListDto GetDetailMsCompany(int companyId);
        ListResultDto<GetCountryListDto> GetMsCountry();
        ListResultDto<GetTownListDto> GetMsTownByCountry(int countryId);
        ListResultDto<GetPostCodeListDto> GetMsPostCodeByTown(int townId);

        void CreateMsCompany(CreateMsCompanyInput input);
        JObject UpdateMsCompany(UpdateMsCompanyInput input);
        void DeleteMsCompany(int Id);
    }
}
