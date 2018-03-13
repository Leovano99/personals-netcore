using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using VDI.Demo.DataExporting.Excel.EpPlus;
using VDI.Demo.Dto;
using Visionet_Backend_NetCore.Komunikasi;
using VDI.Demo.Sessions.Dto;
using System.Text.RegularExpressions;
using System.Reflection;

namespace VDI.Demo.DataExporting.Pdf.Exporter
{
    public class GeneratePdfExporter : PdfExporterBase, IGeneratePdfExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public GeneratePdfExporter(
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

        #region pdf untuk test
        /*
        public Demo.Dto.FileDto GetOnlinePdf()
        {
            #region test work
            string html = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                 <!DOCTYPE html 
                     PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN""
                    ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
                 <html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">
                    <head>
                        <title>Judul</title>
                    </head>
                  <body>
                  Test coba coba <img alt='' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAodJREFUeNpsk0tME1EUhv87UwlCREhRFpi4cGMMRrTE4MaoxBhAsDyMssFHfCQu3BlXGuNKNy5NmqALoqEEMJWCgEUjYojllSpofIUNBNqmIKU6OnQennunUxvgJF86957z/+d27hkGigMlDJfOAmV7AcYsKGqIZljRSvhNE+CMTwEtXmBy2gQb7mCQJUBKkTIQYtfJYCNMAxO9hzq5CYmFiWFY6ISE9VFLRedc1SONeqwf+uJLuKreNPI9nltbLG0orhpqUCM90DRVoEbJ5MSLho1MMg1O0bHOuyoD9crCcxL+xa0HqwL+rEQHsb/CW89reO1aAyEuq+yp+zXvg66rgng8LrDXSmwYpUc8dZkmDsJNL+NCeVVXbWK+O32cpJ7E6OgkwuEwrl8phaHrVsfYD+x03XTPjN3nzZnD0HGxvPppTSLcLwo0I4lldRFK8jdCoZBlJquAbBnr0BD9GUTRvubahclW5qDukqkpIqlodGQ1At3UxZXaIUvauqsyjBV+jZJEJ3s83HO5j+UWI7E6C4mp2EQCTixyV2CvbbKzNmN2zNfHtbzPM3p4FOy/M5CXtwsOKZmmsOi2IHMvyyFhJhgY4BqutQ/aRRstocEngZzswnQnO+x1lqTjy8hIgNdyDc+x5nomxrKJhpcSp2lSrx48WlZhGArynG5hsLLoE7/jQ59f0aR7ZBkdbf7U6Ge+mKYaBvdx8wwZXjtWvfswfTrp3Over29J8NAXYO1t/v/7csZA5U5/Q35nH+aKt8OMR2POPSUFOyRmorvje3BiCt4b9zBANTmwGvP/aMoZRluJbURB8APmnPlQliNLzk8flxbeh9Du8eId5bYQ2SnxH36b/wQYABNFRsIaESsTAAAAAElFTkSuQmCC' /> </body></html>";
            #endregion

            return _generatePdfExporter.GeneratePdfSKLFinance(html);
        }
        */
        #endregion        

        public FileDto GeneratePdfSKLFinance(string htmlContent, GetOnlinePdfDto data)
        {
            try
            {
                string tmphtmlContent = htmlContent;
                Regex RegexObj = new Regex(@"[\{][\{]([a-zA-Z0-9\.]*)[\}][\}]");
                Match MatchResults = RegexObj.Match(htmlContent);
                while (MatchResults.Success)
                {
                    for (int i = 1; i < MatchResults.Groups.Count; i++)
                    {
                        Group GroupObj = MatchResults.Groups[i];
                        if (GroupObj.Success)
                        {
                            SendConsole("Group: "+GroupObj.Value);
                            try
                            {
                                //SendConsole("Properti:"+ Setting_variabel.GetPropValue<int>(data, GroupObj.Value)); //ini jika butuh cast
                                var val = (string.IsNullOrEmpty(""+Setting_variabel.GetPropValue(data, GroupObj.Value)))?"(Belum ada data)": Setting_variabel.GetPropValue(data, GroupObj.Value);
                                SendConsole("Properti:" + val);

                                //replace
                                tmphtmlContent = tmphtmlContent.Replace("{{"+ GroupObj.Value+"}}", ""+ val);
                            }
                            catch (Exception e) {
                                SendConsole(""+e.Message);
                            }
                        }                       
                    }
                    MatchResults = MatchResults.NextMatch();
                }
                htmlContent = tmphtmlContent;
            }
            catch (ArgumentException ex)
            {
            }

            FileDto filePdf = null;
            try
            {
                filePdf = CreatePdfPackage("PdfTest-"+DateTime.Now.ToString("yyyyMMddhhmmssfff")+".pdf", htmlContent);
            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            return filePdf;
        }

        #region debug console
        protected void SendConsole(string msg)
        {
            if (Setting_variabel.enable_tcp_debug == true)
            {
                if (Setting_variabel.Komunikasi_TCPListener == null)
                {
                    Setting_variabel.Komunikasi_TCPListener = new Visionet_Backend_NetCore.Komunikasi.Komunikasi_TCPListener(17000);
                    Task.Run(() => StartListenerLokal());
                }

                if (Setting_variabel.Komunikasi_TCPListener != null)
                {
                    if (Setting_variabel.Komunikasi_TCPListener.IsRunning())
                    {
                        if (Setting_variabel.ConsoleBayangan != null)
                        {
                            Setting_variabel.ConsoleBayangan.Send("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "] " + msg);
                        }
                    }
                }
            }

        }
        #endregion

        #region listener tcp debug
        protected void StartListenerLokal()
        {
            if (Setting_variabel.Komunikasi_TCPListener != null)
                Setting_variabel.Komunikasi_TCPListener.StartListener();
        }

        protected void StopListenerLokal()
        {
            if (Setting_variabel.Komunikasi_TCPListener != null)
                Setting_variabel.Komunikasi_TCPListener.StopListener();
        }
        #endregion
    }
}
