using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VDI.Demo.Chat.SignalR;
using VDI.Demo.DataExporting.Pdf.Exporter;
using VDI.Demo.Editions;
using VDI.Demo.Sessions.Dto;
using Visionet_Backend_NetCore.Komunikasi;

namespace VDI.Demo.Sessions
{
    public class SessionAppService : DemoAppServiceBase, ISessionAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ISession _session;
        private readonly IGeneratePdfExporter _generatePdfExporter;

        public SessionAppService(
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment environment,
            IGeneratePdfExporter generatePdfExporter)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = environment;
            _session = _httpContextAccessor.HttpContext.Session;
            _generatePdfExporter = generatePdfExporter;
        }

        #region session test
        public void SetSessionData(string key, string value)
        {
            _session.SetString(key, value);
        }

        public string GetSessionData(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return "Key Is Null !";
            var val = _session.GetString(key);            
            return val;
        }
        #endregion

        #region pdf untuk test        
        public Demo.Dto.FileDto GetOnlinePdf(string fileName, GetOnlinePdfDto data)
        {
            #region test work
            /*
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
            */
            #endregion

            var filePath = _hostingEnvironment.WebRootPath + @"\"+ fileName;
            string html = File.ReadAllText(filePath);

            return _generatePdfExporter.GeneratePdfSKLFinance(html, data);
        }        
        #endregion

        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>
                    {
                        {"SignalR", SignalRFeature.IsAvailable}
                    }
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper
                    .Map<TenantLoginInfoDto>(await TenantManager
                        .Tenants
                        .Include(t => t.Edition)
                        .FirstAsync(t => t.Id == AbpSession.GetTenantId()));
            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            if (output.Tenant == null)
            {
                return output;
            }

            if (output.Tenant.Edition != null)
            {
                output.Tenant.Edition.IsHighestEdition = IsEditionHighest(output.Tenant.Edition.Id);
            }

            output.Tenant.SubscriptionDateString = GetTenantSubscriptionDateString(output);
            {
                output.Tenant.CreationTimeString = output.Tenant.CreationTime.ToString("d");

                return output;
            }
        }

        private bool IsEditionHighest(int editionId)
        {
            var topEdition = GetHighestEditionOrNullByMonthlyPrice();
            if (topEdition == null)
            {
                return false;
            }

            return editionId == topEdition.Id;
        }

        private SubscribableEdition GetHighestEditionOrNullByMonthlyPrice()
        {
            var editions = TenantManager.EditionManager.Editions;
            if (editions == null || !editions.Any())
            {
                return null;
            }

            return ObjectMapper
                .Map<IEnumerable<SubscribableEdition>>(editions)
                .OrderByDescending(e => e.MonthlyPrice)
                .FirstOrDefault();
        }

        private string GetTenantSubscriptionDateString(GetCurrentLoginInformationsOutput output)
        {
            return output.Tenant.SubscriptionEndDateUtc == null
                ? L("Unlimited")
                : output.Tenant.SubscriptionEndDateUtc?.ToString("d");
        }

        public async Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken()
        {
            if (AbpSession.UserId <= 0)
            {
                throw new Exception(L("ThereIsNoLoggedInUser"));
            }

            var user = await UserManager.GetUserAsync(AbpSession.ToUserIdentifier());
            user.SetSignInToken();
            return new UpdateUserSignInTokenOutput
            {
                SignInToken = user.SignInToken,
                EncodedUserId = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id.ToString())),
                EncodedTenantId = user.TenantId.HasValue
                    ? Convert.ToBase64String(Encoding.UTF8.GetBytes(user.TenantId.Value.ToString()))
                    : ""
            };
        }        
    }
}