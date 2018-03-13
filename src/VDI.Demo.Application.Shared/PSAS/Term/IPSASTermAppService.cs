using Abp.Application.Services;
using System.Collections.Generic;
using VDI.Demo.PSAS.Dto;
using VDI.Demo.PSAS.Main.Dto;
using VDI.Demo.PSAS.Term.Dto;

namespace VDI.Demo.PSAS.Term
{
    public interface IPSASTermAppService : IApplicationService
    {
        GetPSASTermDto GetTermByBookCode(GetPSASParamsDto input);
        void UpdateTerm(UpdateTermInputDto input);
        List<GetMsTermByTermCodeDto> GetTermByCodeDropdown(string termCode);
        GetTermPmtByTermIdDto GetTermPmt(int termID);
    }
}
