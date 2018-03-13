using Abp.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using VDI.Demo.Dto;
using VDI.Demo.Pricing.MarketingFactor.Dto;
using VDI.Demo.Pricing.MarketingFactor.Exporter;

namespace VDI.Demo.Pricing.MarketingFactor
{
    public class MarketingFactorAppService : DemoAppServiceBase, IMarketingFactorAppService
    {
        private readonly IMarketingFactorExporter _marketingFactorExporter;
        public IAppFolders AppFolders { get; set; }

        public MarketingFactorAppService(
            IMarketingFactorExporter marketingFactorExporter
            )
        {
            _marketingFactorExporter = marketingFactorExporter;
        }

        public FileDto ExportToExcelMarketingFactor(ExportMarketingFactorListDto param)
        {
            var fileExcel = _marketingFactorExporter.ExportToExcelMarketingFactor(param);

            var filePath = Path.Combine(AppFolders.TempFileDownloadFolder, fileExcel.FileToken);
            if (!System.IO.File.Exists(filePath))
            {
                throw new UserFriendlyException(L("RequestedFileDoesNotExists"));
            }
            var pathExport = ConfigurationManager.AppSettings["marketingFactorExportPath"].ToString();
            //retrieve data excel from temporary download folder
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            //write excel file to share folder / local folder
            File.WriteAllBytes(pathExport + fileExcel.FileName, fileBytes);


            return fileExcel;
        }
    }
}
