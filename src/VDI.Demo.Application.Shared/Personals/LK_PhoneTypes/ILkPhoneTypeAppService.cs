using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_PhoneTypes.Dto;

namespace VDI.Demo.Personals.LK_PhoneTypes
{
    public interface ILkPhoneTypeAppService : IApplicationService
    {
        List<GetLkPhoneTypeListDto> GetLkPhoneTypeDropdown();
    }
}
