using Abp.AspNetZeroCore.Net;
using Abp.Dependency;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using VDI.Demo.Dto;

namespace VDI.Demo.DataExporting.Excel.EpPlus
{
    public abstract class PdfExporterBase : DemoServiceBase, ITransientDependency
    {
        public IAppFolders AppFolders { get; set; }

        public PdfExporterBase()
        {
        }

        protected FileDto CreatePdfPackage(string fileName, string htmlContent)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationPdf);
            var filePath = Path.Combine(AppFolders.TempFileDownloadFolder, file.FileToken);

            if (!Directory.Exists(AppFolders.TempFileDownloadFolder))
            {
                Directory.CreateDirectory(AppFolders.TempFileDownloadFolder);
            }
                        
            var pdfDoc = new Document(PageSize.A4,5f,5f,5f,5f);
            var fileStream = new FileStream(filePath, FileMode.Create);
            PdfWriter.GetInstance(pdfDoc, fileStream);
            
            pdfDoc.Open();
                        
            var styles = new StyleSheet();
            PdfPCell pdfCell = new PdfPCell
            {
                Border = 0,
                RunDirection = PdfWriter.RUN_DIRECTION_LTR
            };

            using (var reader = new StringReader(htmlContent))
            {
                var parsedHtmlElements = HtmlWorker.ParseToList(reader, styles);

                foreach (IElement htmlElement in parsedHtmlElements)
                {
                    pdfCell.AddElement(htmlElement);
                }
            }

            var table1 = new PdfPTable(1);
            table1.AddCell(pdfCell);
            pdfDoc.Add(table1);
            pdfDoc.Close();

            fileStream.Dispose();
            
            return file;
        }
    }
}