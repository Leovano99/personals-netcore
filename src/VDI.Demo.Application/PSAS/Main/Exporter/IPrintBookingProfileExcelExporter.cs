using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;
using VDI.Demo.PSAS.Main.Dto;

namespace VDI.Demo.PSAS.Main.Exporter
{
    public interface IPrintBookingProfileExcelExporter
    {
        FileDto GenerateExcelBookingProfile(GetUniversalPsasDto dataPsasDto);
    }
}
