using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_Families.Dto;

namespace VDI.Demo.Personals.TR_Families
{
    public interface ITrFamilyAppService : IApplicationService
    {
        void UpdateTrFamily(UpdateTrFamilyListDto input);

        void DeleteTrFamily(string psCode, int refID);
    }
}
