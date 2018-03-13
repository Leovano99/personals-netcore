using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;
using VDI.Demo.PSAS.Dto;
using VDI.Demo.PSAS.Main.Dto;

namespace VDI.Demo.PSAS.Main
{
    public interface IMainPSASAppService : IApplicationService
    {
        GetUniversalMainListDto GetPSASMain(GetPSASParamsDto input);
        FileDto GetUniversalPSASToExport(GetPSASParamsDto input);
    }
}
