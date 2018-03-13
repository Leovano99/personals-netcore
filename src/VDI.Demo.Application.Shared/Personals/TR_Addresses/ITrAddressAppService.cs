using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_Addresses.Dto;

namespace VDI.Demo.Personals.TR_Addresses
{
    public interface ITrAddressAppService : IApplicationService
    {
        void UpdateAddress(GetUpdateAddressInputDto input);
        void DeleteAddress(GetDeleteAddressInputDto input);
    }
}
