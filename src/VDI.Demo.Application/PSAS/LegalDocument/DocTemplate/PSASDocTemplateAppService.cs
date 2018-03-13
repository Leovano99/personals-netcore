using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Abp.Extensions;
using Abp.Linq.Extensions;
using VDI.Demo.Helper;
using System.Linq.Dynamic.Core;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PSAS.LegalDocument.DocTemplate.Dto;
using VDI.Demo.Files;
using Abp.Authorization;
using VDI.Demo.Authorization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using VDI.Demo.Configuration;
using Microsoft.Extensions.Configuration;

namespace VDI.Demo.PSAS.LegalDocument.DocTemplate
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MasterTemplate)]
    public class PSASDocTemplateAppService : DemoAppServiceBase, IPSASDocTemplateAppService
    {
        private readonly IRepository<MS_DocumentPS> _msDocumentRepo;
        private readonly IRepository<MS_DocTemplate> _msDocTemplateRepo;
        private readonly IRepository<MS_MappingTemplate> _msMappingTemplateRepo;
        private readonly IFilesHelper _iFilesHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IConfigurationRoot _appConfiguration;

        public PSASDocTemplateAppService(
            IRepository<MS_DocTemplate> msDocTemplateRepo,
            IRepository<MS_MappingTemplate> msMappingTemplateRepo,
            IRepository<MS_DocumentPS> msDocumentRepo,
            IFilesHelper iFilesHelper,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment
            )
        {
            _msDocTemplateRepo = msDocTemplateRepo;
            _msMappingTemplateRepo = msMappingTemplateRepo;
            _msDocumentRepo = msDocumentRepo;
            _iFilesHelper = iFilesHelper;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;

            _appConfiguration = hostingEnvironment.GetAppConfiguration();
        }

        public List<GetDocTemplateListDto> GetDocTemplate()
        {
            var getDocTemplate = (from A in _msDocTemplateRepo.GetAll()
                                  join B in _msDocumentRepo.GetAll() on A.docID equals B.Id
                                  select new GetDocTemplateListDto
                                  {
                                      docTemplateID = A.Id,
                                      docTemplateCode = _iFilesHelper.ConvertIdToCode(A.Id),
                                      docTemplateName = A.templateName,
                                      linkFileDoc = (A != null && A.templateFile != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(A.templateFile) : null,
                                      docCode = B.docCode
                                  }).ToList();

            return getDocTemplate;
        }

        public List<GetDocTemplateListDto> GetDocTemplateByDocID(int docID)
        {
            var getDocTemplate = (from A in _msDocTemplateRepo.GetAll()
                                  join B in _msDocumentRepo.GetAll() on A.docID equals B.Id
                                  where A.docID == docID
                                  select new GetDocTemplateListDto
                                  {
                                      docTemplateID = A.Id,
                                      docTemplateCode = _iFilesHelper.ConvertIdToCode(A.Id),
                                      docTemplateName = A.templateName,
                                      linkFileDoc = (A != null && A.templateFile != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(A.templateFile) : null,
                                      docCode = B.docCode
                                  }).ToList();

            return getDocTemplate;
        }

        private void GetURLWithoutHost(string path, out string finalpath)
        {
            finalpath = path;
            try
            {
                Regex RegexObj = new Regex("[\\w\\W]*([\\/]Assets[\\w\\W\\s]*)");
                if (RegexObj.IsMatch(path))
                {
                    finalpath = RegexObj.Match(path).Groups[1].Value;
                }
            }
            catch (ArgumentException ex)
            {
            }
        }

        private string GetURLWithoutHost(string path)
        {
            string finalpath = path;
            try
            {
                Regex RegexObj = new Regex("[\\w\\W]*([\\/]Assets[\\w\\W\\s]*)");
                if (RegexObj.IsMatch(path))
                {
                    finalpath = RegexObj.Match(path).Groups[1].Value;
                }
            }
            catch (ArgumentException ex)
            {
            }
            return finalpath;
        }

        private string getAbsoluteUriWithoutTail()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.ToString();
            var test = uriBuilder.ToString();
            var result = test.Replace("[", "").Replace("]", "");
            int position = result.LastIndexOf('/');
            if (position > -1)
                result = result.Substring(0, result.Length - 1);
            return result + _appConfiguration["App:VirtualDirectory"];
        }
    }
}
