using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.DataExporting.Excel.EpPlus;
using VDI.Demo.Dto;
using VDI.Demo.Pricing.MarketingFactor.Dto;

namespace VDI.Demo.Pricing.MarketingFactor.Exporter
{
    public class MarketingFactorExporter : EpPlusExcelExporterBase, IMarketingFactorExporter
    {
        public FileDto ExportToExcelMarketingFactor(ExportMarketingFactorListDto exportMarketingListDto)
        {

            return CreateExcelPackage(
                exportMarketingListDto.schemeName + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                    var headerCells = sheet.Cells[1, 1];
                    var headerFont = headerCells.Style.Font;
                    headerFont.Size = 16;
                    headerFont.Bold = true;
                    headerFont.Italic = true;
                    headerCells.Value = "View Detail Unit";
                    sheet.DefaultColWidth = 25;
                    AddHeaders(
                        sheet,
                        3,
                        L("UnitCode"),
                        L("UnitNo"),
                        L("Price/m2")
                    );

                    AddObjects(
                        sheet, 4, exportMarketingListDto.unit,
                        _ => _.unitCode,
                        _ => _.unitNo,
                        _ => _.priceM2
                    );
                }
                );
        }
    }
}
