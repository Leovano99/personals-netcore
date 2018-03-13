using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Project.MS_Companies.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using Abp.UI;
using System.IO;
using Abp.AutoMapper;
using System.Data;
using Microsoft.AspNetCore.Http;
using VDI.Demo.Files;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using VDI.Demo.Configuration;

namespace VDI.Demo.MasterPlan.Project.MS_Companies
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCompany)]
    public class MsCompanyAppService : DemoAppServiceBase, IMsCompanyAppService
    {
        private readonly IRepository<MS_Company> _msCompanyRepo;
        private readonly IRepository<MS_Country> _msCountryRepo;
        private readonly IRepository<MS_Town> _msTownRepo;
        private readonly IRepository<MS_PostCode> _msPostCodeRepo;
        private readonly IRepository<MS_Bank> _msBankRepo;
        private readonly IRepository<MS_BankBranch> _msBankBranchRepo;
        private readonly IRepository<MS_Account> _msAccountRepo;
        private readonly IAppFolders _appFolders;
        private static IHttpContextAccessor _HttpContextAccessor;
        private readonly FilesHelper _filesHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfigurationRoot _appConfiguration;

        public MsCompanyAppService(
            IRepository<MS_Company> msCompanyRepo,
            IRepository<MS_Country> msCountryRepo,
            IRepository<MS_Town> msTownRepo,
            IRepository<MS_PostCode> msPostCodeRepo,
            IRepository<MS_Bank> msBankRepo,
            IRepository<MS_BankBranch> msBankBranchRepo,
            IRepository<MS_Account> msAccountRepo,
            IAppFolders appFolders,
            IHttpContextAccessor httpContextAccessor,
            FilesHelper filesHelper,
            IHostingEnvironment environment
            )
        {
            _msCompanyRepo = msCompanyRepo;
            _msCountryRepo = msCountryRepo;
            _msTownRepo = msTownRepo;
            _msPostCodeRepo = msPostCodeRepo;
            _msBankRepo = msBankRepo;
            _msBankBranchRepo = msBankBranchRepo;
            _msAccountRepo = msAccountRepo;
            _appFolders = appFolders;
            _HttpContextAccessor = httpContextAccessor;
            _filesHelper = filesHelper;
            _hostingEnvironment = environment;
            _appConfiguration = environment.GetAppConfiguration();
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCompany_Create)]
        public void CreateMsCompany(CreateMsCompanyInput input)
        {
            Logger.Info("CreateMsCompany() Started.");

            Logger.DebugFormat("CreateMsCompany() - Start checking existing code and name. Parameters sent:{0}" +
                            "coCode     = {1}{0}" +
                            "coName     = {2}"
                            , Environment.NewLine, input.coCode, input.coName);
            var checkNameCode = (from x in _msCompanyRepo.GetAll()
                                 where x.coCode == input.coCode || x.coName == input.coName
                                 select x).Count();
            Logger.DebugFormat("CreateMsCompany() - End checking existing code and name. Result:{0}", checkNameCode);

            if (checkNameCode == 0)
            {
                string imageUrl;
                if (input.image == null)
                {
                    imageUrl = "-";
                }
                else
                {
                    imageUrl = UploadImage(input.image);
                    GetURLWithoutHost(imageUrl, out imageUrl);
                }

                var data = new MS_Company
                {
                    entityID = 1,
                    coCode = input.coCode,
                    coName = input.coName,
                    address = input.address,
                    city = "-", //hardcode for not null field
                    NPWP = input.npwp,
                    PKP = input.pkp,
                    PKPDate = input.pkpDate,
                    image = imageUrl,
                    accountNo = "673434736", //hardcode for not null field
                    bankName = "CIMB",
                    bankBranch = "CIMB1",
                    mailAddress = input.email,
                    phoneNo = input.phoneNo,
                    faxNo = string.IsNullOrEmpty(input.faxNo) ? "-" : input.faxNo,
                    postCodeID = input.postCodeID,
                    NPWPAddress = input.npwpAddress,
                    KPP_Name = string.IsNullOrEmpty(input.kppName) ? "-" : input.kppName,
                    KPP_TTD = string.IsNullOrEmpty(input.kppTTD) ? "-" : input.kppTTD,
                    coCodeParent = "-", //hardcode for not null field
                    APServer = "-",
                    APcoCode = "-",
                    APLogin = "-",
                    isActive = input.isActive,
                    isCA = true // what is this?
                };
                Logger.DebugFormat("CreateMsCompany() - Start checking insert company. Parameters sent:{0}" +
                "	entityID	= {1}{0}" +
                "	coCode	    = {2}{0}" +
                "	coName	    = {3}{0}" +
                "	address	    = {4}{0}" +
                "	city	    = {5}{0}" +
                "	NPWP	    = {6}{0}" +
                "	PKP	        = {7}{0}" +
                "	PKPDate	    = {8}{0}" +
                "	image	    = {9}{0}" +
                "	accountNo	= {10}{0}" +
                "	bankName	= {11}{0}" +
                "	bankBranch	= {12}{0}" +
                "	mailAddress	= {13}{0}" +
                "	phoneNo	    = {14}{0}" +
                "	faxNo	    = {15}{0}" +
                "	postCodeID	= {16}{0}" +
                "	NPWPAddress	= {17}{0}" +
                "	KPP_Name	= {18}{0}" +
                "	KPP_TTD	    = {19}{0}" +
                "	coCodeParent= {20}{0}" +
                "	APServer	= {21}{0}" +
                "	APcoCode	= {22}{0}" +
                "	APLogin	    = {23}{0}" +
                "	isActive	= {24}{0}" +
                "	isCA	    = {25}"
                , Environment.NewLine, 1, input.coCode, input.coName, input.address, "-", input.npwp, input.pkp, input.pkpDate, imageUrl
                , "673434736", "CIMB", "CIMB1", input.email, input.phoneNo, input.faxNo, input.postCodeID, input.npwpAddress, input.kppName
                , input.kppTTD, "-t", "-", "-", "-", input.isActive, true);

                _msCompanyRepo.Insert(data);

                Logger.DebugFormat("CreateMsCompany() - End insert company.");
            }
            else
            {
                Logger.ErrorFormat("CreateMsCompany() ERROR. Result = {0}", "Company Code or Company Name Already Exist !");
                throw new UserFriendlyException("Company Code or Company Name Already Exist !");
            }
            Logger.Info("CreateMsCompany() Finished.");
        }

        private static Uri getAbsolutUri()
        {
            var request = _HttpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.ToString();
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri;
        }

        private string UploadImage(string filename)
        {
            try
            {
                return _filesHelper.MoveFiles(filename, @"Temp\Downloads\CompanyImage\", @"Assets\Upload\CompanyImage\");
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCompany_Detail)]
        public GetDetailMsCompanyListDto GetDetailMsCompany(int companyId)
        {
            var compInfo = (from x in _msCompanyRepo.GetAll()
                            join w in _msPostCodeRepo.GetAll() on x.postCodeID equals w.Id
                            join z in _msTownRepo.GetAll() on w.townID equals z.Id
                            join y in _msCountryRepo.GetAll() on z.countryID equals y.Id
                            where x.Id == companyId
                            select new CompanyInformationDto
                            {
                                coName = x.coName,
                                coCode = x.coCode,
                                address = x.address,
                                countryName = y.countryName,
                                townName = z.townName,
                                postCode = w.postCode,
                                countryID = y.Id,
                                townID = z.Id,
                                postCodeID = w.Id,
                                email = x.mailAddress,
                                phoneNo = x.phoneNo,
                                faxNo = x.faxNo,
                                isActive = x.isActive,
                                image = (x != null && x.image != null) ? (!x.image.Equals("-")) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(x.image) : null : null, //TODO link + ip host
                            }).FirstOrDefault();

            var docInfo = (from x in _msCompanyRepo.GetAll()
                           where x.Id == companyId
                           select new DocumentInformationDto
                           {
                               npwp = x.NPWP,
                               npwpAddress = x.NPWPAddress,
                               pkp = x.PKP,
                               pkpDate = x.PKPDate,
                               kppName = x.KPP_Name,
                               kppTTD = x.KPP_TTD
                           }).FirstOrDefault();

            var bankInfo = (from x in _msCompanyRepo.GetAll()
                                //join y in _msBankBranchRepo.GetAll() on x.bankBranchID equals y.Id
                                //join z in _msBankRepo.GetAll() on y.bankID equals z.Id
                                //where x.companyID == companyId
                            select new BankInformationDto
                            {
                                //bankName = x.bankName,
                                //branchName = x.bankBranchName,
                                //accName = x.accName,
                                //accNo = x.accNo
                            }).ToList();

            var listResult = new GetDetailMsCompanyListDto
            {
                CompanyInformationDto = compInfo,
                DocumentInformationDto = docInfo,
                BankInformationDto = bankInfo
            };

            return listResult;
        }

        public ListResultDto<GetMsCompanyListDto> GetMsCompany()
        {
            var listResult = (from x in _msCompanyRepo.GetAll()
                              orderby x.Id descending
                              select new GetMsCompanyListDto
                              {
                                  Id = x.Id,
                                  coName = x.coName,
                                  address = x.address,
                                  email = x.mailAddress,
                                  phoneNo = x.phoneNo,
                                  isActive = x.isActive,
                              }).ToList();

            return new ListResultDto<GetMsCompanyListDto>(listResult);
        }

        public ListResultDto<GetCountryListDto> GetMsCountry()
        {
            var listResult = (from x in _msCountryRepo.GetAll()
                              select new GetCountryListDto
                              {
                                  Id = x.Id,
                                  countryName = x.countryName
                              }).ToList();

            return new ListResultDto<GetCountryListDto>(listResult);
        }

        public ListResultDto<GetTownListDto> GetMsTownByCountry(int countryId)
        {
            var listResult = (from x in _msTownRepo.GetAll()
                              where x.countryID == countryId
                              select new GetTownListDto
                              {
                                  Id = x.Id,
                                  townName = x.townName
                              }).ToList();

            return new ListResultDto<GetTownListDto>(listResult);
        }

        public ListResultDto<GetPostCodeListDto> GetMsPostCodeByTown(int townId)
        {
            var listResult = (from x in _msPostCodeRepo.GetAll()
                              where x.townID == townId
                              select new GetPostCodeListDto
                              {
                                  Id = x.Id,
                                  postCode = x.postCode,
                                  regency = x.regency,
                                  village = x.village
                              }).ToList();

            return new ListResultDto<GetPostCodeListDto>(listResult);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCompany_Edit)]
        public JObject UpdateMsCompany(UpdateMsCompanyInput input)
        {
            JObject obj = new JObject();
            Logger.DebugFormat("UpdateMsCompany() - Start checking existing code and name. Parameters sent:{0}" +
                            "coCode     = {1}{0}" +
                            "coName     = {2}{0}"
                            , Environment.NewLine, input.coCode, input.coName);

            var checkAccount = (from acc in _msAccountRepo.GetAll()
                                where acc.coID == input.Id
                                select acc).Any();

            var checkNameCode = (from x in _msCompanyRepo.GetAll()
                                 where x.Id != input.Id && (x.coCode == input.coCode || x.coName == input.coName)
                                 select x).Any();

            Logger.DebugFormat("UpdateMsCompany() - End checking existing code and name. Result: {0}", checkNameCode);

            var getMsCompany = (from x in _msCompanyRepo.GetAll()
                                where x.Id == input.Id
                                select x).FirstOrDefault();
            if (!checkNameCode)
            {
                var getOldImage = getMsCompany.image;//mark
                var updateMsCompany = getMsCompany.MapTo<MS_Company>();
                
                updateMsCompany.address = input.address;
                updateMsCompany.mailAddress = input.email;
                updateMsCompany.phoneNo = input.phoneNo;
                updateMsCompany.faxNo = string.IsNullOrEmpty(input.faxNo) ? "-" : input.faxNo;
                updateMsCompany.NPWP = input.npwp;
                updateMsCompany.NPWPAddress = input.npwpAddress;
                updateMsCompany.KPP_Name = string.IsNullOrEmpty(input.kppName) ? "-" : input.kppName;
                updateMsCompany.KPP_TTD = string.IsNullOrEmpty(input.kppTTD) ? "-" : input.kppTTD;
                updateMsCompany.PKP = input.pkp;
                updateMsCompany.PKPDate = input.pkpDate;
                updateMsCompany.isActive = input.isActive;
                updateMsCompany.postCodeID = input.postCodeID;

                if (input.fileName == "updated")
                {
                    var imageUrl = UploadImage(input.image);
                    GetURLWithoutHost(imageUrl, out imageUrl);
                    updateMsCompany.image = imageUrl;
                    DeleteImage(getOldImage);
                }
                else if (input.fileName == "removed")
                {
                    updateMsCompany.image = "-";
                    DeleteImage(getOldImage);
                }
                else
                {
                    updateMsCompany.image = getMsCompany.image;
                }

                if (!checkAccount)
                {
                    updateMsCompany.coCode = input.coCode;
                    updateMsCompany.coName = input.coName;
                    obj.Add("message", "Edit Successfully");
                }
                else
                {
                    obj.Add("message", "Edit Successfully, but can't change Company Code & Name");
                }

                Logger.DebugFormat("UpdateMsCompany() - Start update company. Parameters sent:{0}" +
                             "	coCode	    = {1}{0}" +
                             "	coName	    = {2}{0}" +
                             "	address	    = {3}{0}" +
                             "	mailAddress	= {4}{0}" +
                             "	phoneNo	    = {5}{0}" +
                             "	faxNo	    = {6}{0}" +
                             "	NPWP	    = {7}{0}" +
                             "	NPWPAddress	= {8}{0}" +
                             "	KPP_Name	= {9}{0}" +
                             "	KPP_TTD	    = {10}{0}" +
                             "	PKP	        = {11}{0}" +
                             "	PKPDate	    = {12}{0}" +
                             "  isActive    = {13}{0}" +
                             "  postCodeID  = {14}"
                             , Environment.NewLine, input.coCode, input.coName, input.address, input.email, input.phoneNo, input.faxNo, input.npwp
                             , input.npwpAddress, input.kppName, input.kppTTD, input.pkp, input.pkpDate, input.isActive, input.postCodeID);

                _msCompanyRepo.Update(updateMsCompany);

                Logger.DebugFormat("UpdateMsCompany() - End update company");
            }
            else
            {
                Logger.ErrorFormat("UpdateMsCompany() Company Code or Name already exist. Result = {0}", checkNameCode);
                throw new UserFriendlyException("Company Code or Name already exist!");
            }
            return obj;
        }

        private void DeleteImage(string filename)
        {
            var imageToDelete = filename;
            var filenameToDelete = imageToDelete.Split(@"/");
            var nameFileToDelete = filenameToDelete[filenameToDelete.Count() - 1];
            var deletePath = @"\Assets\Upload\CompanyImage\";

            var deleteImage = _hostingEnvironment.WebRootPath + deletePath + nameFileToDelete;

            if (File.Exists(deleteImage))
            {
                var file = new FileInfo(deleteImage);
                file.Delete();
            }
        }

        public ListResultDto<GetMsCompanyDropDownListDto> GetMsCompanyDropDown()
        {
            var listResult = (from x in _msCompanyRepo.GetAll()
                                  //where x.isActive == true
                              select new GetMsCompanyDropDownListDto
                              {
                                  Id = x.Id,
                                  coName = x.coName,
                                  address = x.address,
                                  coCode = x.coCode
                              }).ToList();

            return new ListResultDto<GetMsCompanyDropDownListDto>(listResult);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCompany_Delete)]
        public void DeleteMsCompany(int Id)
        {
            Logger.Info("DeleteMsCompany() Started.");

            Logger.DebugFormat("DeleteMsCompany() - Start checking account with coID: {0}.", Id);
            bool checkAccount = (from acc in _msAccountRepo.GetAll()
                                 where acc.coID == Id
                                 select acc).Any();
            Logger.DebugFormat("DeleteMsCompany() - End checking account. Result: {0}.", checkAccount);
            if (!checkAccount)
            {

                var getImage = (from x in _msCompanyRepo.GetAll()
                                where x.Id == Id
                                select x.image).FirstOrDefault();
                try
                {
                    Logger.DebugFormat("DeleteMsCompany() - Start delete company. Parameters sent: {0}", Id);
                    DeleteImage(getImage);
                    _msCompanyRepo.Delete(Id);
                    Logger.DebugFormat("DeleteMsCompany() - End delete company");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("DeleteMsCompany() ERROR DbException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsCompany() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("DeleteMsCompany() ERROR Exception. Result = {0}", "This Company is used by another master!");
                throw new UserFriendlyException("This Company is used by another master!");
            }
            Logger.Info("DeleteMsCompany() Finished.");
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
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
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
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
            return finalpath;
        }

        private string getAbsoluteUriWithoutTail()
        {
            var request = _HttpContextAccessor.HttpContext.Request;
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
