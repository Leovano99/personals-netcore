using System;
using System.Collections.Generic;
using System.IO;
using Abp.AspNetZeroCore.Net;
using Abp.Collections.Extensions;
using Abp.Dependency;
using VDI.Demo.Dto;
using OfficeOpenXml;
using System.Drawing;

namespace VDI.Demo.DataExporting.Excel.EpPlus
{
    public abstract class EpPlusExcelExporterBase : DemoServiceBase, ITransientDependency
    {
        public IAppFolders AppFolders { get; set; }

        protected FileDto CreateExcelPackage(string fileName, Action<ExcelPackage> creator)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);

            using (var excelPackage = new ExcelPackage())
            {
                creator(excelPackage);
                Save(excelPackage, file);
            }

            return file;
        }

        protected FileDto CreateExcelPackageFromTemplate(string fileName, string templatePath, Action<ExcelPackage> creator)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            using (FileStream templateDocumentStream = File.OpenRead(templatePath))
            {
                using (var excelPackage = new ExcelPackage(templateDocumentStream))
                {
                    creator(excelPackage);
                    Save(excelPackage, file);
                }
            }

            return file;
        }

        protected void AddHeader(ExcelWorksheet sheet, params string[] headerTexts)
        {
            if (headerTexts.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 0; i < headerTexts.Length; i++)
            {
                AddHeader(sheet, i + 1, headerTexts[i]);
            }
        }

        protected void AddHeaders(ExcelWorksheet sheet, int headerRow, params string[] headerTexts)
        {
            if (headerTexts.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 0; i < headerTexts.Length; i++)
            {
                AddHeader(sheet, i + 1, headerTexts[i], headerRow);
            }
        }

        protected void AddHeader(ExcelWorksheet sheet, int columnIndex, string headerText, int headerRow = 1)
        {
            sheet.Cells[headerRow, columnIndex].Value = headerText;
            sheet.Cells[headerRow, columnIndex].Style.Font.Bold = true;
        }

        protected void AddHeader(ExcelWorksheet sheet, int columnIndex, string headerText)
        {
            sheet.Cells[1, columnIndex].Value = headerText;
            sheet.Cells[1, columnIndex].Style.Font.Bold = true;
        }

        protected void AddObjects<T>(ExcelWorksheet sheet, int startRowIndex, IList<T> items, params Func<T, object>[] propertySelectors)
        {
            if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }

            for (var i = 0; i < items.Count; i++)
            {
                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    sheet.Cells[i + startRowIndex, j + 1].Value = propertySelectors[j](items[i]);
                }
            }
        }

        protected void Save(ExcelPackage excelPackage, FileDto file)
        {
            var filePath = Path.Combine(AppFolders.TempFileDownloadFolder, file.FileToken);
            excelPackage.SaveAs(new FileInfo(filePath));
        }

        //Start Add Image
        protected void AddImage(ExcelWorksheet oSheet, int rowIndex, int colIndex, string imagePath, int width, int height)
        {
            Bitmap image = new Bitmap(imagePath);
            {
                var excelImage = oSheet.Drawings.AddPicture("Image-" + DateTime.Now, image);
                excelImage.From.Column = colIndex - 1;
                excelImage.From.Row = rowIndex - 1;
                excelImage.SetSize(width, height);
                excelImage.From.ColumnOff = Pixel2MTU(2);
                excelImage.From.RowOff = Pixel2MTU(2);
            }
        }

        public int Pixel2MTU(int pixels)
        {
            int mtus = pixels * 9525;
            return mtus;
        }
        //End Add Image

    }
}