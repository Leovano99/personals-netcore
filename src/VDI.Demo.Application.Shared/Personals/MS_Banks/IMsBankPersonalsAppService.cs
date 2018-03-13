using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.MS_Banks.Dto;

namespace VDI.Demo.Personals.MS_Banks
{
    public interface IMsBankPersonalsAppService : IApplicationService
    {
        ListResultDto<GetAllBankPersonalsListDto> GetAllMsBankList();
    }
}
