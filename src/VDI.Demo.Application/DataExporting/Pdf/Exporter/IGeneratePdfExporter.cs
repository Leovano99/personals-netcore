using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;
using VDI.Demo.Sessions.Dto;

namespace VDI.Demo.DataExporting.Pdf.Exporter
{
    public interface IGeneratePdfExporter
    {
        FileDto GeneratePdfSKLFinance(string htmlContent, GetOnlinePdfDto data);
    }
}
