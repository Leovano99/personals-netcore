using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.Dto;
using VDI.Demo.Pricing.GeneratePrice.Dto;
using Microsoft.AspNetCore.Http;
using VDI.Demo.DataExporting.Excel.EpPlus;
using OfficeOpenXml.Style;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace VDI.Demo.Pricing.GeneratePrice.Exporter
{
    public class GeneratePriceListExcelExporter : EpPlusExcelExporterBase, IGeneratePriceListExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public GeneratePriceListExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IHttpContextAccessor httpContextAccessor,
            IAbpSession abpSession,
            IHostingEnvironment environment)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = environment;
        }

        public FileDto GenerateExcelUploadGrossPrice(List<GenerateTemplateExcelListDto> generateGrossPriceListDtos)
        {
            return CreateExcelPackage(
                "Upload Gross Price.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("TransferPriceTemplate"));
                    sheet.OutLineApplyStyle = true;
                    var headerCells = sheet.Cells[1, 1];
                    headerCells.Value = "Transfer Price";
                    var headerFont = headerCells.Style.Font;
                    headerFont.Size = 14;
                    headerFont.Bold = true;

                    AddHeaders(
                        sheet,
                        2,
                        L("UnitCode"),
                        L("UnitNo"),
                        L("RenovCode"),
                        L("TanahPrice"),
                        L("BangunanPrice"),
                        L("BangunanExtTipe1Price"),
                        L("BangunanExtTipe2Price"),
                        L("BangunanRenovasiPrice"),
                        L("TanahExtTipe1Price"),
                        L("GardenPrice"),
                        L("RenovasiChicPrice"),
                        L("RenovasiDiamondPrice"),
                        L("RenovasiGoldPrice"),
                        L("RenovasiPlatinumPrice"),
                        L("ReadyToFitPrice"),
                        L("RenovasiSilverPrice"),
                        L("RenovasiStandardPrice"),
                        L("RenovasiTitaniumPrice"),
                        L("RenovasiUltimaPrice")
                    );

                    AddObjects(
                        sheet, 3, generateGrossPriceListDtos,
                        _ => _.unitCode,
                        _ => _.unitNo,
                        _ => _.renovCode,
                        _ => _.tanahPrice,
                        _ => _.bangunanPrice,
                        _ => _.bangunanExtTipe1Price,
                        _ => _.bangunanExtTipe2Price,
                        _ => _.bangunanRenovasiPrice,
                        _ => _.tanahExtTipe1Price,
                        _ => _.gardenPrice,
                        _ => _.renovasiChicPrice,
                        _ => _.renovasiDiamondPrice,
                        _ => _.renovasiGoldPrice,
                        _ => _.renovasiPlatinumPrice,
                        _ => _.readyToFitPrice,
                        _ => _.renovasiSilverPrice,
                        _ => _.renovasiStandartPrice,
                        _ => _.renovasiTitaniumPrice,
                        _ => _.renovasiUltimaPrice
                        );

                    var countList = 3 + generateGrossPriceListDtos.Count;
                    // Assign borders

                    //Format Currency
                    string modelRangeD = String.Format("D3:T{0}", countList);
                    sheet.Cells[modelRangeD].Style.Numberformat.Format = "###,###,##0";

                    //Formatting cells

                    var timeColumn = sheet.Column(1);
                    timeColumn.Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";

                    for (var i = 1; i <= 10; i++)
                    {
                        if (i.IsIn(5, 10)) //Don't AutoFit Parameters and Exception
                        {
                            continue;
                        }

                        sheet.Column(i).AutoFit();
                    }
                });
        }
        public FileDto ExportToExcelUploadGrossPrice(List<ExportToExcelUploadPriceListDto> exportToExcelUploadGrossPriceListDtos) //passed
        {
            var term = exportToExcelUploadGrossPriceListDtos[0].termName;

            var productName = exportToExcelUploadGrossPriceListDtos.GroupBy(g => g.productName)
                                                    .Select(g => new
                                                    {
                                                        productName = g.Key,
                                                        countProduct = g.Count()
                                                    })
                                                    .ToList();

            return CreateExcelPackage(
                "GrossPrice-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add("PL-GO");
                    sheet.OutLineApplyStyle = true;

                    var headerCells = sheet.Cells[4, 1];
                    headerCells.Value = L("PriceList");
                    var headerFont = headerCells.Style.Font;
                    headerFont.Size = 16;
                    headerFont.Bold = true;

                    var subHeaderCells = sheet.Cells[5, 1];
                    subHeaderCells.Value = L("GOLDFinishingOption");
                    var subHeaderFont = subHeaderCells.Style.Font;
                    subHeaderFont.Size = 16;
                    subHeaderFont.Bold = true;

                    var date = DateTime.Now.ToString("yyyy MMMM dd");
                    var subHeaderDateCells = sheet.Cells[6, 1];
                    subHeaderDateCells.Value = date;
                    var subHeaderDateFont = subHeaderDateCells.Style.Font;
                    subHeaderDateFont.Size = 13;
                    subHeaderDateFont.Bold = true;
                    subHeaderDateCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    var subHeader2Cells = sheet.Cells[7, 1];
                    subHeader2Cells.Value = L("TermConditions");
                    var subHeader2Font = subHeader2Cells.Style.Font;
                    subHeader2Font.Size = 13;
                    subHeader2Font.Bold = true;

                    //HardCode
                    var allPriceCells = sheet.Cells[18, 1];
                    allPriceCells.Value = L("AllPricesinRp");
                    var allPriceCellsFont = allPriceCells.Style.Font;
                    allPriceCellsFont.Size = 12;
                    allPriceCellsFont.Bold = true;

                    var path = @"\Assets\Template_File\lippohomes_logo.png";
                    string imgPath = _hostingEnvironment.WebRootPath + path;

                    if (File.Exists(imgPath))
                    {
                        System.Drawing.Image imgDimension = System.Drawing.Image.FromFile(imgPath);
                        var widthImg = imgDimension.Width;
                        var heightImg = imgDimension.Height;
                        AddImage(sheet, 6, 6, imgPath, widthImg, heightImg);
                    }

                    sheet.View.FreezePanes(25, 11);

                    AddHeaders(
                        sheet,
                        20,
                        L("ProductName"),
                        L("UnitCode"),
                        L("UnitNo"),
                        L("RenovCode"),
                        term,
                        L("BookingFee")
                    );

                    AddObjects(
                        sheet, 25, exportToExcelUploadGrossPriceListDtos,
                        _ => _.productName,
                        _ => _.unitCode,
                        _ => _.unitNo,
                        _ => _.renovCode,
                        _ => _.price,
                        _ => _.bookingFee
                        );

                    //Start

                    //Product Name
                    sheet.Cells["A20:A24"].Merge = true;
                    sheet.Cells["A20:A24"].Style.WrapText = true;
                    sheet.Cells["A20:A24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells["A20:A24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int lastFirstMergeCell = 0;
                    int lastMergeCell = 0;

                    foreach (var X in productName)
                    {
                        int firstCell = 0;
                        int lastCell = 0;

                        int countProduct = X.countProduct - 1;
                        if (lastFirstMergeCell == 0 && lastMergeCell == 0)
                        {
                            firstCell = 25;
                            lastCell = 25;

                            lastCell = lastCell + countProduct;

                            lastFirstMergeCell = firstCell;
                            lastMergeCell = lastCell;
                        }
                        else
                        {
                            firstCell = 1 + lastMergeCell;
                            lastCell = countProduct + firstCell;

                            lastFirstMergeCell = firstCell;
                            lastMergeCell = lastCell;
                        }

                        var rangeMerge = "A" + firstCell + ":A" + lastCell;
                        sheet.Cells[rangeMerge].Merge = true;
                        sheet.Cells[rangeMerge].Style.WrapText = true;
                        sheet.Cells[rangeMerge].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        sheet.Cells[rangeMerge].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    //UnitCode
                    sheet.Cells["B20:B24"].Merge = true;
                    sheet.Cells["B20:B24"].Style.WrapText = true;
                    sheet.Cells["B20:B24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells["B20:B24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //UnitNo
                    sheet.Cells["C20:C24"].Merge = true;
                    sheet.Cells["C20:C24"].Style.WrapText = true;
                    sheet.Cells["C20:C24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells["C20:C24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //RenovCode
                    sheet.Cells["D20:D24"].Merge = true;
                    sheet.Cells["D20:D24"].Style.WrapText = true;
                    sheet.Cells["D20:D24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells["D20:D24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //Price
                    sheet.Cells["E20:E24"].Merge = true;
                    sheet.Cells["E20:E24"].Style.WrapText = true;
                    sheet.Cells["E20:E24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells["E20:E24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //BookingFee
                    sheet.Cells["F20:F24"].Merge = true;
                    sheet.Cells["F20:F24"].Style.WrapText = true;
                    sheet.Cells["F20:F24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells["F20:F24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    var countList = 24 + exportToExcelUploadGrossPriceListDtos.Count;
                    // Assign borders
                    string modelRange = String.Format("A20:F{0}", countList);
                    sheet.Cells[modelRange].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[modelRange].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[modelRange].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[modelRange].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    //Format Currency
                    var countListCurrency = 25 + exportToExcelUploadGrossPriceListDtos.Count;

                    string modelRangeD = String.Format("D25:D{0}", countListCurrency);
                    sheet.Cells[modelRangeD].Style.Numberformat.Format = "###,###,##0";

                    string modelRangeE = String.Format("E25:E{0}", countListCurrency);
                    sheet.Cells[modelRangeE].Style.Numberformat.Format = "###,###,##0";

                    string modelRangeF = String.Format("F25:F{0}", countListCurrency);
                    sheet.Cells[modelRangeE].Style.Numberformat.Format = "###,###,##0";

                    //Formatting cells
                    var timeColumn = sheet.Column(1);
                    timeColumn.Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";

                    for (var i = 1; i <= 10; i++)
                    {
                        if (i.IsIn(5, 10)) //Don't AutoFit Parameters and Exception
                        {
                            continue;
                        }

                        sheet.Column(i).AutoFit();
                    }
                });
        }
        public FileDto ExportToExcelUploadPriceList(List<ExportToExcelUploadPriceListDto> exportToExcelUploadPriceListDto) //passed
        {
            var term = exportToExcelUploadPriceListDto[0].termName;

            bool hasKpa = term.Contains("KPA");
            bool has12 = term.Contains("12x");
            bool has24 = term.Contains("24x");
            bool has36 = term.Contains("36x");

            var productName = exportToExcelUploadPriceListDto.GroupBy(g => g.productName)
                                                    .Select(g => new
                                                    {
                                                        productName = g.Key,
                                                        countProduct = g.Count()
                                                    })
                                                    .ToList();

            return CreateExcelPackage(
                "PriceList-"+ DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add("PL-GO");
                    sheet.OutLineApplyStyle = true;

                    var headerCells = sheet.Cells[4, 1];
                    headerCells.Value = L("PriceList");
                    var headerFont = headerCells.Style.Font;
                    headerFont.Size = 16;
                    headerFont.Bold = true;

                    var subHeaderCells = sheet.Cells[5, 1];
                    subHeaderCells.Value = L("GOLDFinishingOption");
                    var subHeaderFont = subHeaderCells.Style.Font;
                    subHeaderFont.Size = 16;
                    subHeaderFont.Bold = true;

                    var date = DateTime.Now.ToString("yyyy MMMM dd");
                    var subHeaderDateCells = sheet.Cells[6, 1];
                    subHeaderDateCells.Value = date;
                    var subHeaderDateFont = subHeaderDateCells.Style.Font;
                    subHeaderDateFont.Size = 13;
                    subHeaderDateFont.Bold = true;
                    subHeaderDateCells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    var subHeader2Cells = sheet.Cells[7, 1];
                    subHeader2Cells.Value = L("TermConditions");
                    var subHeader2Font = subHeader2Cells.Style.Font;
                    subHeader2Font.Size = 13;
                    subHeader2Font.Bold = true;

                    //HardCode
                    var allPriceCells = sheet.Cells[18, 1];
                    allPriceCells.Value = L("AllPricesinRp");
                    var allPriceCellsFont = allPriceCells.Style.Font;
                    allPriceCellsFont.Size = 12;
                    allPriceCellsFont.Bold = true;

                    var path = @"\Assets\Template_File\lippohomes_logo.png";
                    string imgPath = _hostingEnvironment.WebRootPath + path;
                    
                    if (File.Exists(imgPath))
                    {
                        System.Drawing.Image imgDimension = System.Drawing.Image.FromFile(imgPath);
                        var widthImg = imgDimension.Width;
                        var heightImg = imgDimension.Height;
                        AddImage(sheet, 6, 6, imgPath, widthImg, heightImg);
                    }               

                    sheet.View.FreezePanes(25, 11);

                    AddHeaders(
                        sheet,
                        20,
                        L("ProductName"),
                        L("UnitCode"),
                        L("UnitNo"),
                        exportToExcelUploadPriceListDto.FirstOrDefault().termName,
                        L("BookingFee")
                    );

                    AddObjects(
                        sheet, 25, exportToExcelUploadPriceListDto,
                        _ => _.productName,
                        _ => _.unitCode,
                        _ => _.unitNo,
                        _ => _.price,
                        _ => _.bookingFee
                        );

                    //Start

                    //Product Name
                    sheet.Cells["A20:A24"].Merge = true;
                    sheet.Cells["A20:A24"].Style.WrapText = true;
                    sheet.Cells["A20:A24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells["A20:A24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int lastFirstMergeCell = 0;
                    int lastMergeCell = 0;

                    foreach (var X in productName)
                    {
                        int firstCell = 0;
                        int lastCell = 0;

                        int countProduct = X.countProduct - 1;
                        if (lastFirstMergeCell == 0 && lastMergeCell == 0)
                        {
                            firstCell = 25;
                            lastCell = 25;

                            lastCell = lastCell + countProduct;

                            lastFirstMergeCell = firstCell;
                            lastMergeCell = lastCell;
                        }
                        else
                        {
                            firstCell = 1 + lastMergeCell;
                            lastCell = countProduct + firstCell;

                            lastFirstMergeCell = firstCell;
                            lastMergeCell = lastCell;
                        }

                        var rangeMerge = "A" + firstCell + ":A" + lastCell;
                        sheet.Cells[rangeMerge].Merge = true;
                        sheet.Cells[rangeMerge].Style.WrapText = true;
                        sheet.Cells[rangeMerge].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        sheet.Cells[rangeMerge].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    //UnitCode
                    sheet.Cells["B20:B24"].Merge = true;
                    sheet.Cells["B20:B24"].Style.WrapText = true;
                    sheet.Cells["B20:B24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells["B20:B24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //UnitNo
                    sheet.Cells["C20:C24"].Merge = true;
                    sheet.Cells["C20:C24"].Style.WrapText = true;
                    sheet.Cells["C20:C24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells["C20:C24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //Price
                    sheet.Cells["D20:D24"].Merge = true;
                    sheet.Cells["D20:D24"].Style.WrapText = true;
                    sheet.Cells["D20:D24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells["D20:D24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //BookingFee
                    sheet.Cells["E20:E24"].Merge = true;
                    sheet.Cells["E20:E24"].Style.WrapText = true;
                    sheet.Cells["E20:E24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    sheet.Cells["E20:E24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////Type BR
                    //sheet.Cells["F20:F24"].Merge = true;
                    //sheet.Cells["F20:F24"].Style.WrapText = true;
                    //sheet.Cells["F20:F24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["F20:F24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////Size
                    //sheet.Cells["G20:G24"].Merge = true;
                    //sheet.Cells["G20:G24"].Style.WrapText = true;
                    //sheet.Cells["G20:G24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["G20:G24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////Facing
                    //sheet.Cells["H20:H24"].Merge = true;
                    //sheet.Cells["H20:H24"].Style.WrapText = true;
                    //sheet.Cells["H20:H24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["H20:H24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////View
                    //sheet.Cells["I20:I24"].Merge = true;
                    //sheet.Cells["I20:I24"].Style.WrapText = true;
                    //sheet.Cells["I20:I24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["I20:I24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////Booking Fee
                    //sheet.Cells["J20:J24"].Merge = true;
                    //sheet.Cells["J20:J24"].Style.WrapText = true;
                    //sheet.Cells["J20:J24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["J20:J24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////Gross Price
                    //sheet.Cells["K20:K24"].Merge = true;
                    //sheet.Cells["K20:K24"].Style.WrapText = true;
                    //sheet.Cells["K20:K24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["K20:K24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////Net Price
                    //sheet.Cells["L20:L24"].Merge = true;
                    //sheet.Cells["L20:L24"].Style.WrapText = true;
                    //sheet.Cells["L20:L24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["L20:L24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////Cash
                    //sheet.Cells["M20:M24"].Merge = true;
                    //sheet.Cells["M20:M24"].Style.WrapText = true;
                    //sheet.Cells["M20:M24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["M20:M24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////KPA
                    //sheet.Cells["N20:N24"].Merge = true;
                    //sheet.Cells["N20:N24"].Style.WrapText = true;
                    //sheet.Cells["N20:N24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["N20:N24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                    ////Start Installment 12x
                    //sheet.Cells["O20:S21"].Merge = true;
                    //sheet.Cells["O20:S21"].Value = "Installment 12x";
                    //sheet.Cells["O20:S21"].Style.WrapText = true;
                    //sheet.Cells["O20:S21"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["O20:S21"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["O22:O24"].Merge = true;
                    //sheet.Cells["O22:O24"].Value = "Selling Price (INCLD PPN)";
                    //sheet.Cells["O22:O24"].Style.WrapText = true;
                    //sheet.Cells["O22:O24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["O22:O24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["P22:P24"].Merge = true;
                    //sheet.Cells["P22:P24"].Value = "Selisih";
                    //sheet.Cells["P22:P24"].Style.WrapText = true;
                    //sheet.Cells["P22:P24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["P22:P24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["Q22:Q24"].Merge = true;
                    //sheet.Cells["Q22:Q24"].Value = "Selisih";
                    //sheet.Cells["Q22:Q24"].Style.WrapText = true;
                    //sheet.Cells["Q22:Q24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["Q22:Q24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["R22:R24"].Merge = true;
                    //sheet.Cells["R22:R24"].Value = "Down Payment Incld BF (10 %)";
                    //sheet.Cells["R22:R24"].Style.WrapText = true;
                    //sheet.Cells["R22:R24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["R22:R24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["S22:S24"].Merge = true;
                    //sheet.Cells["S22:S24"].Value = "Installment Per Month";
                    //sheet.Cells["S22:S24"].Style.WrapText = true;
                    //sheet.Cells["S22:S24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["S22:S24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////End Installment 12x

                    ////Start Installment 24x
                    //sheet.Cells["T20:X21"].Merge = true;
                    //sheet.Cells["T20:X21"].Value = "Installment 24x";
                    //sheet.Cells["T20:X21"].Style.WrapText = true;
                    //sheet.Cells["T20:X21"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["T20:X21"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["T22:T24"].Merge = true;
                    //sheet.Cells["T22:T24"].Value = "Selling Price (INCLD PPN)";
                    //sheet.Cells["T22:T24"].Style.WrapText = true;
                    //sheet.Cells["T22:T24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["T22:T24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["U22:U24"].Merge = true;
                    //sheet.Cells["U22:U24"].Value = "Selisih";
                    //sheet.Cells["U22:U24"].Style.WrapText = true;
                    //sheet.Cells["U22:U24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["U22:U24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["V22:V24"].Merge = true;
                    //sheet.Cells["V22:V24"].Value = "Selisih";
                    //sheet.Cells["V22:V24"].Style.WrapText = true;
                    //sheet.Cells["V22:V24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["V22:V24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["W22:W24"].Merge = true;
                    //sheet.Cells["W22:W24"].Value = "Down Payment Incld BF (20 %)";
                    //sheet.Cells["W22:W24"].Style.WrapText = true;
                    //sheet.Cells["W22:W24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["W22:W24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["X22:X24"].Merge = true;
                    //sheet.Cells["X22:X24"].Value = "Installment Per Month";
                    //sheet.Cells["X22:X24"].Style.WrapText = true;
                    //sheet.Cells["X22:X24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["X22:X24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////End Installment 24x

                    ////Start Installment 36x
                    //sheet.Cells["Y20:AC21"].Merge = true;
                    //sheet.Cells["Y20:AC21"].Value = "Installment 36x";
                    //sheet.Cells["Y20:AC21"].Style.WrapText = true;
                    //sheet.Cells["Y20:AC21"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["Y20:AC21"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["Y22:Y24"].Merge = true;
                    //sheet.Cells["Y22:Y24"].Value = "Selling Price (INCLD PPN)";
                    //sheet.Cells["Y22:Y24"].Style.WrapText = true;
                    //sheet.Cells["Y22:Y24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["Y22:Y24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["Z22:Z24"].Merge = true;
                    //sheet.Cells["Z22:Z24"].Value = "Selisih";
                    //sheet.Cells["Z22:Z24"].Style.WrapText = true;
                    //sheet.Cells["Z22:Z24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["Z22:Z24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["AA22:AA24"].Merge = true;
                    //sheet.Cells["AA22:AA24"].Value = "Selisih";
                    //sheet.Cells["AA22:AA24"].Style.WrapText = true;
                    //sheet.Cells["AA22:AA24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["AA22:AA24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["AB22:AB24"].Merge = true;
                    //sheet.Cells["AB22:AB24"].Value = "Down Payment Incld BF (20 %)";
                    //sheet.Cells["AB22:AB24"].Style.WrapText = true;
                    //sheet.Cells["AB22:AB24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["AB22:AB24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //sheet.Cells["AC22:AC24"].Merge = true;
                    //sheet.Cells["AC22:AC24"].Value = "Installment Per Month";
                    //sheet.Cells["AC22:AC24"].Style.WrapText = true;
                    //sheet.Cells["AC22:AC24"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //sheet.Cells["AC22:AC24"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ////End Installment 36x

                    //if (!hasKpa)
                    //{
                    //    sheet.Column(14).Hidden = true;
                    //}

                    //if (!has12)
                    //{
                    //    sheet.Column(15).Hidden = true;
                    //    sheet.Column(16).Hidden = true;
                    //    sheet.Column(17).Hidden = true;
                    //    sheet.Column(18).Hidden = true;
                    //    sheet.Column(19).Hidden = true;
                    //}

                    //if (!has24)
                    //{
                    //    sheet.Column(20).Hidden = true;
                    //    sheet.Column(21).Hidden = true;
                    //    sheet.Column(22).Hidden = true;
                    //    sheet.Column(23).Hidden = true;
                    //    sheet.Column(24).Hidden = true;
                    //}

                    //if (!has36)
                    //{
                    //    sheet.Column(25).Hidden = true;
                    //    sheet.Column(26).Hidden = true;
                    //    sheet.Column(27).Hidden = true;
                    //    sheet.Column(28).Hidden = true;
                    //    sheet.Column(29).Hidden = true;
                    //}


                    var countList = 24 + exportToExcelUploadPriceListDto.Count;
                    // Assign borders
                    string modelRange = String.Format("A20:E{0}", countList);
                    sheet.Cells[modelRange].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[modelRange].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[modelRange].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[modelRange].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    //Format Currency
                    var countListCurrency = 25 + exportToExcelUploadPriceListDto.Count;
                    string modelRangeD = String.Format("D25:D{0}", countListCurrency);
                    sheet.Cells[modelRangeD].Style.Numberformat.Format = "###,###,##0";

                    string modelRangeE = String.Format("E25:E{0}", countListCurrency);
                    sheet.Cells[modelRangeE].Style.Numberformat.Format = "###,###,##0";

                    //string modelRangeW = String.Format("R25:T{0}", countListCurrency);
                    //sheet.Cells[modelRangeW].Style.Numberformat.Format = "###,###,##0";

                    //string modelRangeAB = String.Format("W25:Y{0}", countListCurrency);
                    //sheet.Cells[modelRangeAB].Style.Numberformat.Format = "###,###,##0";

                    //string modelRangeAG = String.Format("AB25:AC{0}", countListCurrency);
                    //sheet.Cells[modelRangeAG].Style.Numberformat.Format = "###,###,##0";

                    //Formatting cells
                    var timeColumn = sheet.Column(1);
                    timeColumn.Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";

                    for (var i = 1; i <= 10; i++)
                    {
                        if (i.IsIn(5, 10)) //Don't AutoFit Parameters and Exception
                        {
                            continue;
                        }

                        sheet.Column(i).AutoFit();
                    }
                });
        }
        public FileDto GenerateExcelUploadPriceList(List<GenerateTemplateExcelListDto> generatePriceListDtos)
        {
            return CreateExcelPackage(
                "TransferPricePerTerm.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("TransferPriceTemplate"));
                    sheet.OutLineApplyStyle = true;
                    var headerCells = sheet.Cells[1, 1];
                    headerCells.Value = "Transfer Price";
                    var headerFont = headerCells.Style.Font;
                    headerFont.Size = 14;
                    headerFont.Bold = true;

                    AddHeaders(
                        sheet,
                        2,
                        L("UnitCode"),
                        L("UnitNo"),
                        L("RenovCode"),
                        L("TanahPrice"),
                        L("BangunanPrice"),
                        L("BangunanExtTipe1Price"),
                        L("BangunanExtTipe2Price"),
                        L("BangunanRenovasiPrice"),
                        L("TanahExtTipe1Price"),
                        L("GardenPrice"),
                        L("RenovasiChicPrice"),
                        L("RenovasiDiamondPrice"),
                        L("RenovasiGoldPrice"),
                        L("RenovasiPlatinumPrice"),
                        L("ReadyToFitPrice"),
                        L("RenovasiSilverPrice"),
                        L("RenovasiStandardPrice"),
                        L("RenovasiTitaniumPrice"),
                        L("RenovasiUltimaPrice"),
                        L("NetNetPrice")
                    );

                    AddObjects(
                        sheet, 3, generatePriceListDtos,
                        _ => _.unitCode,
                        _ => _.unitNo,
                        _ => _.renovCode,
                        _ => _.tanahPrice,
                        _ => _.bangunanPrice,
                        _ => _.bangunanExtTipe1Price,
                        _ => _.bangunanExtTipe2Price,
                        _ => _.bangunanRenovasiPrice,
                        _ => _.tanahExtTipe1Price,
                        _ => _.gardenPrice,
                        _ => _.renovasiChicPrice,
                        _ => _.renovasiDiamondPrice,
                        _ => _.renovasiGoldPrice,
                        _ => _.renovasiPlatinumPrice,
                        _ => _.readyToFitPrice,
                        _ => _.renovasiSilverPrice,
                        _ => _.renovasiStandartPrice,
                        _ => _.renovasiTitaniumPrice,
                        _ => _.renovasiUltimaPrice,
                        _ => _.netNetPrice
                        );

                    var countList = 3 + generatePriceListDtos.Count;
                    // Assign borders

                    //Format Currency
                    string modelRangeD = String.Format("D3:T{0}", countList);
                    sheet.Cells[modelRangeD].Style.Numberformat.Format = "###,###,##0";

                    //Formatting cells

                    var timeColumn = sheet.Column(1);
                    timeColumn.Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";

                    for (var i = 1; i <= 10; i++)
                    {
                        if (i.IsIn(5, 10)) //Don't AutoFit Parameters and Exception
                        {
                            continue;
                        }

                        sheet.Column(i).AutoFit();
                    }
                });
        }

        private string getAbsoluteUri()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.ToString();
            var test = uriBuilder.ToString();
            var result = test.Replace("[", "").Replace("]", "");
            return result;
        }        
    }
}
