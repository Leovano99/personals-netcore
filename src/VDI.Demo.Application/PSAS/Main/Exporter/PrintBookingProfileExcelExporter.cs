using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VDI.Demo.DataExporting.Excel.EpPlus;
using VDI.Demo.Dto;
using VDI.Demo.PSAS.Main.Dto;

namespace VDI.Demo.PSAS.Main.Exporter
{
    public class PrintBookingProfileExcelExporter : EpPlusExcelExporterBase, IPrintBookingProfileExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PrintBookingProfileExcelExporter(
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

        public FileDto GenerateExcelBookingProfile(GetUniversalPsasDto dataPsasDto)
        {
            string templateDocument = @"D:\sampah\Book1.xlsx";
            string template = _hostingEnvironment.ContentRootPath + @"\wwwroot\Assets\Upload\PSASBookingProfile\BookingProfileTemplate.xlsx";
            //string outputDocument = @"D:\sampah\Booking Profile Template hasil 1.xls";
            //FileInfo template = new FileInfo(@"D:\sampah\Book1.xlsx");
            //FileInfo output = new FileInfo(@"D:\sampah\Booking Profile Template hasil2.xls");

            //using (ExcelPackage package = new ExcelPackage(output, template))
            //{
            //    ExcelWorksheet sheet = package.Workbook.Worksheets["Sheet1"];
            //    sheet.Cells["C7"].Value = ": " + dataPsasDto.psasMain.bookCode;
            //    package.SaveAs(new FileInfo(outputDocument));
            //}

            return CreateExcelPackageFromTemplate(
                "Booking Profile - "+dataPsasDto.psasMain.bookCode+" - " + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx",
                template,
                excelPackage =>
                {
                    ExcelWorksheet sheet = excelPackage.Workbook.Worksheets["Sheet1"];
                    var sheetWithData = fillDataSheet(sheet, dataPsasDto);
                    
                });

        }

        private ExcelWorksheet fillDataSheet(ExcelWorksheet sheet, GetUniversalPsasDto dataPsasDto)
        {
            sheet.Cells["A4"].Value = dataPsasDto.psasMain.projectName + " " + dataPsasDto.psasMain.clusterName + " " + dataPsasDto.psasMain.unitName + " " + dataPsasDto.psasMain.unitNo;
            sheet.Cells["C7"].Value = ": " + dataPsasDto.psasMain.bookCode;
            sheet.Cells["C8"].Value = ": " + dataPsasDto.psasMain.bookDate;
            sheet.Cells["C9"].Value = ": " + dataPsasDto.psasMain.psCode + " / " + dataPsasDto.psasMain.name;
            sheet.Cells["C10"].Value = ": " + dataPsasDto.psasMain.NPWP;
            sheet.Cells["C11"].Value = ": " + dataPsasDto.psasMain.memberID + " / " + dataPsasDto.psasMain.memberName;
            sheet.Cells["C12"].Value = ": " + dataPsasDto.psasMain.membershipType;
            sheet.Cells["C13"].Value = ": " + dataPsasDto.psasMain.termNo;
            sheet.Cells["C14"].Value = ": " + dataPsasDto.psasMain.bankName;
            sheet.Cells["C15"].Value = ": " + dataPsasDto.psasMain.cn;
            sheet.Cells["C16"].Value = ": " + dataPsasDto.psasMain.sadStatus;
            sheet.Cells["I7"].Value = ": " + dataPsasDto.psasMain.projectName;
            sheet.Cells["I8"].Value = ": " + dataPsasDto.psasMain.categoryName;
            sheet.Cells["I9"].Value = ": " + dataPsasDto.psasMain.clusterName;
            sheet.Cells["I10"].Value = ": " + dataPsasDto.psasMain.productName;
            sheet.Cells["I11"].Value = ": " + dataPsasDto.psasMain.detailName;
            sheet.Cells["I12"].Value = ": " + dataPsasDto.psasMain.ppjb;
            sheet.Cells["I13"].Value = ": " + dataPsasDto.psasMain.kpu;
            sheet.Cells["I14"].Value = ": " + dataPsasDto.psasMain.pppu;

            sheet.Cells["F20"].Value = dataPsasDto.psasPrice.PSASPrice.area.bangunan;
            sheet.Cells["F21"].Value = dataPsasDto.psasPrice.PSASPrice.grossPrice.bangunan;
            sheet.Cells["E22"].Value = dataPsasDto.psasPrice.PSASPrice.discount.discount;
            sheet.Cells["F22"].Value = "("+dataPsasDto.psasPrice.PSASPrice.discount.bangunan+")";
            sheet.Cells["F23"].Value = dataPsasDto.psasPrice.PSASPrice.netPrice.bangunan;
            sheet.Cells["K20"].Formula = "F20+H20+J20";
            sheet.Cells["K21"].Formula = "F21+H21+J21";
            sheet.Cells["K22"].Formula = "F22+H22+J22";
            sheet.Cells["K23"].Formula = "F23+H23+J23";
            var isFirst = true;
            var current = 0;
            var cellRow = 25;
            foreach(var addDisc in dataPsasDto.psasPrice.PSASPrice.discountA)
            {
                current++;
                if (isFirst)
                {
                    sheet.Cells["E24"].Value = addDisc.discount;
                    sheet.Cells["F24"].Value = "(" + addDisc.bangunan + ")";
                    sheet.Cells["G24"].Value = 0;
                    sheet.Cells["H24"].Value = "(" + 0 + ")";
                    sheet.Cells["I24"].Value = 0;
                    sheet.Cells["J24"].Value = "(" + 0 + ")";
                    if (current == dataPsasDto.psasPrice.PSASPrice.discountA.Count)
                    {
                        sheet.Cells["K24"].Formula = "F24+H24+J24";
                    }
                    isFirst = false;
                }
                else
                {
                    if (current == dataPsasDto.psasPrice.PSASPrice.discountA.Count)
                    {
                        sheet.InsertRow(cellRow, 2);
                        sheet.Cells["F"+cellRow].Formula = "F" + (cellRow - 2) + "+F" + (cellRow - 1);
                        sheet.Cells["H" + cellRow].Formula = "H" + (cellRow - 2) + "+H" + (cellRow - 1);
                        sheet.Cells["J" + cellRow].Formula = "J" + (cellRow - 2) + "+J" + (cellRow - 1);

                        sheet.Cells["E" + (cellRow + 1)].Value = addDisc.discount;
                        sheet.Cells["F" + (cellRow + 1)].Value = "(" + addDisc.bangunan + ")";
                        sheet.Cells["G" + (cellRow + 1)].Value = 0;
                        sheet.Cells["H" + (cellRow + 1)].Value = "(" + 0 + ")";
                        sheet.Cells["I" + (cellRow + 1)].Value = 0;
                        sheet.Cells["J" + (cellRow + 1)].Value = "(" + 0 + ")";

                        sheet.Cells["K" + (cellRow + 1)].Formula = "F"+ (cellRow + 1) + "+H"+ (cellRow + 1) + "+J"+ (cellRow + 1);
                        sheet.Cells["E" + (cellRow + 1) + ":K" + (cellRow + 1)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        cellRow = cellRow + 2;
                    }
                    else
                    {
                        //todo insert row
                        sheet.InsertRow(cellRow, 2);
                        sheet.Cells["E" + (cellRow + 1)].Value = addDisc.discount;
                        sheet.Cells["F" + (cellRow + 1)].Value = "(" + addDisc.bangunan + ")";
                        sheet.Cells["F" + (cellRow + 2)].Formula = "F"+ (cellRow - 1) +"+F"+ (cellRow + 1);
                        cellRow = cellRow + 2;
                    }
                    
                }

            }
            sheet.Cells["F" + cellRow].Value = dataPsasDto.psasPrice.PSASPrice.netNetPrice.bangunan;
            sheet.Cells["F" + (cellRow + 1)].Value = 0;
            sheet.Cells["F" + (cellRow + 2)].Formula = "F" + (cellRow) + "+F" + (cellRow + 1);
            sheet.Cells["F" + (cellRow + 3)].Value = dataPsasDto.psasPrice.PSASPrice.VATPrice.bangunan;
            sheet.Cells["F" + (cellRow + 4)].Value = dataPsasDto.psasPrice.PSASPrice.interest.bangunan;
            sheet.Cells["F" + (cellRow + 5)].Formula = "F" + (cellRow + 2) + "+F" + (cellRow + 3);

            sheet.Cells["K" + cellRow].Formula = "F" + (cellRow) + "+H" + (cellRow) + "+J" + (cellRow);
            sheet.Cells["K" + (cellRow + 1)].Formula = "F" + (cellRow + 1) + "+H" + (cellRow + 1) + "+J" + (cellRow + 1);
            sheet.Cells["K" + (cellRow + 2)].Formula = "F" + (cellRow + 2) + "+H" + (cellRow + 2) + "+J" + (cellRow + 2);
            sheet.Cells["K" + (cellRow + 3)].Formula = "F" + (cellRow + 3) + "+H" + (cellRow + 3) + "+J" + (cellRow + 3);
            sheet.Cells["K" + (cellRow + 4)].Formula = "F" + (cellRow + 4) + "+H" + (cellRow + 4) + "+J" + (cellRow + 4);
            sheet.Cells["K" + (cellRow + 5)].Formula = "F" + (cellRow + 5) + "+H" + (cellRow + 5) + "+J" + (cellRow + 5);

            sheet.Cells["E20:E"+ (cellRow + 5)].Style.Numberformat.Format = "0%";
            sheet.Cells["G20:G" + (cellRow + 5)].Style.Numberformat.Format = "0%";
            sheet.Cells["I20:I" + (cellRow + 5)].Style.Numberformat.Format = "0%";
            sheet.Cells["F21:F"+ (cellRow + 5)].Style.Numberformat.Format = "#,##0.00_);(#,##0.00)";
            sheet.Cells["H21:H" + (cellRow + 5)].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells["J21:J" + (cellRow + 5)].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells["K21:K" + (cellRow + 5)].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells["F21:F" + (cellRow + 5)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            sheet.Cells["H21:H" + (cellRow + 5)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            sheet.Cells["J21:J" + (cellRow + 5)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            sheet.Cells["K21:K" + (cellRow + 5)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            var paymentStart = cellRow + 8;
            var paymentCount = 0;
            foreach(var payment in dataPsasDto.psasPayment)
            {
                paymentCount++;
                sheet.InsertRow(paymentStart + paymentCount, 1);
                sheet.Cells["A" + (paymentStart + paymentCount)].Value = paymentCount;
                sheet.Cells["B" + (paymentStart + paymentCount)].Value = payment.clearDate;
                sheet.Cells["C" + (paymentStart + paymentCount)].Value = payment.PMTDate;
                sheet.Cells["D" + (paymentStart + paymentCount)].Value = payment.transNo;
                sheet.Cells["F" + (paymentStart + paymentCount)].Value = payment.type.payFor;
                sheet.Cells["G" + (paymentStart + paymentCount)].Value = payment.type.payType;
                sheet.Cells["H" + (paymentStart + paymentCount)].Value = payment.type.otherType;
                sheet.Cells["I" + (paymentStart + paymentCount)].Value = payment.netAmount;
                sheet.Cells["J" + (paymentStart + paymentCount)].Value = payment.vatAmt;
                sheet.Cells["K" + (paymentStart + paymentCount)].Value = payment.netAmount + payment.vatAmt;
                sheet.Cells["L" + (paymentStart + paymentCount)].Value = payment.remarks;
                sheet.Cells["M" + (paymentStart + paymentCount)].Value = payment.taxFP;
            }

            sheet.Cells["B" + (paymentStart + 1) + ":C" + (paymentStart + paymentCount)].Style.Numberformat.Format = "dd/MM/yyyy";
            sheet.Cells["I" + (paymentStart + 1) + ":I" + (paymentStart + paymentCount)].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells["J" + (paymentStart + 1) + ":J" + (paymentStart + paymentCount)].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells["K" + (paymentStart + 1) + ":K" + (paymentStart + paymentCount)].Style.Numberformat.Format = "#,##0.00";

            var scheduleStart = paymentStart + paymentCount + 3;
            var scheduleCount = 0;
            foreach(var schedule in dataPsasDto.psasSchedule)
            {
                scheduleCount++;
                sheet.InsertRow(scheduleStart + scheduleCount, 1);
                sheet.Cells["A" + (scheduleStart + scheduleCount)].Value = scheduleCount;
                sheet.Cells["B" + (scheduleStart + scheduleCount)].Value = schedule.dueDate;
                sheet.Cells["C" + (scheduleStart + scheduleCount)].Value = schedule.allocCode;
                sheet.Cells["E" + (scheduleStart + scheduleCount)].Value = schedule.netAmount;
                sheet.Cells["F" + (scheduleStart + scheduleCount)].Value = schedule.netOutstanding;
                sheet.Cells["G" + (scheduleStart + scheduleCount)].Value = schedule.VATAmount;
                sheet.Cells["H" + (scheduleStart + scheduleCount)].Value = schedule.VATOutstanding;
                sheet.Cells["I" + (scheduleStart + scheduleCount)].Value = schedule.penaltyAge;
                sheet.Cells["J" + (scheduleStart + scheduleCount)].Value = schedule.penaltyAmount;
                sheet.Cells["K" + (scheduleStart + scheduleCount)].Value = schedule.totalAmount;
                sheet.Cells["L" + (scheduleStart + scheduleCount)].Value = schedule.totalOutstanding;
            }
            sheet.Cells["B" + (scheduleStart + 1) + ":B" + (scheduleStart + scheduleCount)].Style.Numberformat.Format = "dd/MM/yyyy";
            sheet.Cells["E" + (scheduleStart + 1) + ":E" + (scheduleStart + scheduleCount)].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells["F" + (scheduleStart + 1) + ":F" + (scheduleStart + scheduleCount)].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells["G" + (scheduleStart + 1) + ":G" + (scheduleStart + scheduleCount)].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells["H" + (scheduleStart + 1) + ":H" + (scheduleStart + scheduleCount)].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells["J" + (scheduleStart + 1) + ":J" + (scheduleStart + scheduleCount)].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells["K" + (scheduleStart + 1) + ":K" + (scheduleStart + scheduleCount)].Style.Numberformat.Format = "#,##0.00";
            sheet.Cells["L" + (scheduleStart + 1) + ":L" + (scheduleStart + scheduleCount)].Style.Numberformat.Format = "#,##0.00";

            var summaryStart = scheduleStart + scheduleCount + 3;
            //total schedule
            sheet.Cells["E" + summaryStart].Formula = "SUM(E" + (scheduleStart + 1) + ":E" + (scheduleStart + scheduleCount) + ")";
            sheet.Cells["F" + summaryStart].Formula = "SUM(G" + (scheduleStart + 1) + ":G" + (scheduleStart + scheduleCount) + ")";
            sheet.Cells["G" + summaryStart].Formula = "SUM(K" + (scheduleStart + 1) + ":K" + (scheduleStart + scheduleCount) + ")";
            sheet.Cells["H" + summaryStart].Formula = "SUM(J" + (scheduleStart + 1) + ":J" + (scheduleStart + scheduleCount) + ")";
            sheet.Cells["I" + summaryStart].Value = 0;
            sheet.Cells["J" + summaryStart].Value = 0;
            //total payment
            sheet.Cells["E" + (summaryStart + 1)].Formula = "SUM(I" + (paymentStart + 1) + ":I" + (paymentStart + paymentCount) + ")";
            sheet.Cells["F" + (summaryStart + 1)].Formula = "SUM(J" + (paymentStart + 1) + ":J" + (paymentStart + paymentCount) + ")";
            sheet.Cells["G" + (summaryStart + 1)].Formula = "SUM(K" + (paymentStart + 1) + ":K" + (paymentStart + paymentCount) + ")";
            sheet.Cells["H" + (summaryStart + 1)].Value = 0;
            sheet.Cells["I" + (summaryStart + 1)].Value = 0;
            sheet.Cells["J" + (summaryStart + 1)].Value = 0;
            //total outstanding
            sheet.Cells["E" + (summaryStart + 2)].Formula = "SUM(F" + (scheduleStart + 1) + ":F" + (scheduleStart + paymentCount) + ")";
            sheet.Cells["F" + (summaryStart + 2)].Formula = "SUM(H" + (scheduleStart + 1) + ":H" + (scheduleStart + paymentCount) + ")";
            sheet.Cells["G" + (summaryStart + 2)].Formula = "SUM(L" + (scheduleStart + 1) + ":L" + (scheduleStart + paymentCount) + ")";
            sheet.Cells["H" + (summaryStart + 2)].Value = 0;
            sheet.Cells["I" + (summaryStart + 2)].Value = 0;
            sheet.Cells["J" + (summaryStart + 2)].Value = 0;

            sheet.Cells["E" + summaryStart + ":J" + (summaryStart + 2)].Style.Numberformat.Format = "#,##0.00";

            return sheet;
        }
    }
}
