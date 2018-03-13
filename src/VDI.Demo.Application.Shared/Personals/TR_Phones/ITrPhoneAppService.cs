using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_Phones.Dto;

namespace VDI.Demo.Personals.TR_Phones
{
    public interface ITrPhoneAppService : IApplicationService
    {
        void UpdateTrPhone(List<GetUpdateTrPhoneInputDto> inputs);
        void DeleteTrPhone(GetDeleteTrPhoneInputDto input);
    }
}
