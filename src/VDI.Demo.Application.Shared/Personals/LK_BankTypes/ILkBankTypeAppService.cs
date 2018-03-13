using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_BankTypes.Dto;

namespace VDI.Demo.Personals.LK_BankTypes
{
    public interface ILkBankTypeAppService : IApplicationService
    {
        ListResultDto<GetAllBankTypeListDto> GetAllLkBankTypeList();
    }
}
