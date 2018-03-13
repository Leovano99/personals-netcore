using Abp.Application.Services;
using System.Collections.Generic;
using VDI.Demo.PSAS.Dto;
using VDI.Demo.PSAS.Main.Dto;
using VDI.Demo.PSAS.Term.Dto;

namespace VDI.Demo.PSAS.Term
{
    public interface IPSASPaymentAppService : IApplicationService
    {
        List<GetPSASPaymentDto> GetPaymentByBookCode(GetPSASParamsDto input);
        List<GetPSASPaymentDto> GetOtherPaymentByBookCode(GetPSASParamsDto input);
    }
}
