using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_Companies.Dto;

namespace VDI.Demo.Personals.TR_Companies
{
    public interface ITrCompanyAppService : IApplicationService
    {
        void UpdateTrCompany(CreateOrUpdateTrCompanyListDto input);

        void DeleteTrCompany(string psCode, int refID);
    }
}
