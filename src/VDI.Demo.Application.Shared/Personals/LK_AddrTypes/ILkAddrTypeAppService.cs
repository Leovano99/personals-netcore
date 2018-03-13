﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_AddrTypes.Dto;

namespace VDI.Demo.Personals.LK_AddrTypes
{
    public interface ILkAddrTypeAppService : IApplicationService
    {
        ListResultDto<GetLkAddrTypeDropdownListDto> GetLkAddrTypeDropdown();
    }
}
