using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using VDI.Demo.Authorization;
using VDI.Demo.Configuration;
using VDI.Demo.Files;
using VDI.Demo.MasterPlan.Unit.MS_Renovations.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.Pricing;
using Visionet_Backend_NetCore.Komunikasi;

namespace VDI.Demo.MasterPlan.Unit.MS_Renovations
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterRenovation)]
    public class MsRenovationAppService : DemoAppServiceBase, IMsRenovationAppService
    {
        private readonly IRepository<MS_Renovation> _msRenovRepo;
        private readonly IRepository<MS_UnitItem> _msUnitItemRepo;
        private readonly IRepository<LK_Item> _lkItemRepo;
        private readonly IRepository<LK_DPCalc> _lkDpCalcRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly FilesHelper _filesHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfigurationRoot _appConfiguration;

        public MsRenovationAppService(IRepository<MS_Renovation> msRenovRepo,
            IRepository<MS_UnitItem> msUnitItemRepo,
            IRepository<LK_Item> lkItemRepo,
            IRepository<LK_DPCalc> lkDpCalcRepo,
            IRepository<MS_Project> msProjectRepo,
            FilesHelper filesHelper,
            IHostingEnvironment environment,
            IHttpContextAccessor httpContextAccessor)
        {
            _msProjectRepo = msProjectRepo;
            _msRenovRepo = msRenovRepo;
            _msUnitItemRepo = msUnitItemRepo;
            _lkItemRepo = lkItemRepo;
            _lkDpCalcRepo = lkDpCalcRepo;
            _filesHelper = filesHelper;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = environment;
            _appConfiguration = environment.GetAppConfiguration();
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterRenovation_Create)]
        public void CreateMsRenovation(List<CreateMsRenovationInput> inputs)//passed
        {
            Logger.InfoFormat("CreateMsRenovation() Started.");

            Logger.DebugFormat("CreateMsRenovation() - Start get First projectID.");
            var projectID = (from x in inputs
                             select x.projectID).FirstOrDefault();
            Logger.DebugFormat("CreateMsRenovation() - End get First projectID.");

            Logger.DebugFormat("CreateMsRenovation() - Start get First renovationCode.");
            var renovCode = (from x in inputs
                             select x.renovationCode).FirstOrDefault();
            Logger.DebugFormat("CreateMsRenovation() - End get First renovationCode.");

            Logger.DebugFormat("CreateMsRenovation() - Start checking existing projectID. Parameters sent: {0} " +
                "   projectID = {1}{0}"
                , Environment.NewLine, projectID);
            var checkProject = (from x in _msRenovRepo.GetAll()
                                where x.projectID == projectID && x.renovationCode == renovCode
                                select x).Any();

            var checkAvailibilityProject = (from x in _msProjectRepo.GetAll()
                                            where x.Id == projectID
                                            select x).Any();

            Logger.DebugFormat("CreateMsRenovation() - End checking existing projectID. Result = {0}", checkProject);

            if (!checkAvailibilityProject) { throw new UserFriendlyException("Master Project Code Unavailable !"); }
            if (!checkProject)
            {
                Logger.DebugFormat("CreateMsRenovation() - Started Loop Data inputs = {0}", inputs);
                foreach (var input in inputs)
                {
                    string imageUrl = null;

                    if (input.detailName != null)
                    {
                        Logger.DebugFormat("CreateMsRenovation() - Start uploadFile.");
                        imageUrl = uploadFile(input.detailName); //TODO change tanpa ip host
                        GetURLWithoutHost(imageUrl, out imageUrl);
                    }

                    var createMsItem = new MS_Renovation
                    {
                        renovationCode = input.renovationCode,
                        renovationName = input.renovationName,
                        detail = imageUrl,
                        isActive = input.isActive,
                        projectID = input.projectID,
                        CreationTime = DateTime.Now
                    };
                    
                    try
                    {
                        Logger.DebugFormat("CreateMsRenovation() - Start Insert Renovation. Parameters sent: {0} " +
                    "   renovationCode = {1}{0}" +
                    "   renovationName = {2}{0}" +
                    "   detail = {3}{0}" +
                    "   isActive = {4}{0}" +
                    "   projectID = {5}{0}"
                    , Environment.NewLine, input.renovationCode, input.renovationName, imageUrl, input.isActive, input.projectID);
                        _msRenovRepo.Insert(createMsItem);
                        CurrentUnitOfWork.SaveChanges();
                        Logger.DebugFormat("CreateMsRenovation() - End Insert Renovation.");
                    }
                    // Handle data errors.
                    catch (DataException exDb)
                    {
                        Logger.DebugFormat("CreateMsRenovation() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.DebugFormat("CreateMsRenovation() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }
                }
                Logger.DebugFormat("CreateMsRenovation() - End Loop Data Inputs.");
            }
            else
            {
                Logger.DebugFormat("CreateMsRenovation() - ERROR. Result = {0}", "Project Code Already Exist !");
                throw new UserFriendlyException("Project Code Already Exist !");
            }
            Logger.InfoFormat("CreateMsRenovation() - Finished.");
        }

        public void DeleteFileTempRenovation(string filename)
        {
            Logger.InfoFormat("DeleteFileTempRenovation() - Started.");

            var deletePath = @"\Temp\Downloads\RenovationFile\";
            var deleteImage = _hostingEnvironment.WebRootPath + deletePath + filename;

            if (File.Exists(deleteImage))
            {
                Logger.DebugFormat("DeleteFileTempRenovation() - Start Delete Image. Params = {0}", deleteImage);
                var file = new FileInfo(deleteImage);
                file.Delete();
                Logger.DebugFormat("DeleteFileTempRenovation() - End Delete Image.");
            }
            else
            {
                Logger.DebugFormat("DeleteFileTempRenovation() - ERROR. Result = {0}", "File Not Found!");
                throw new UserFriendlyException("File Not Found !");
            }

            Logger.InfoFormat("DeleteFileTempRenovation() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterRenovation_Delete)]
        public void DeleteMsRenovation(int Id)
        {
            Logger.InfoFormat("DeleteMsRenovation() - Started.");
            try
            {
                Logger.DebugFormat("DeleteMsRenovation() - Start delete Renovation. Parameters sent: {0} " +
            "   renovID = {1}{0}", Environment.NewLine, Id);
                _msRenovRepo.Delete(Id);
                Logger.DebugFormat("DeleteMsRenovation() - End delete Renovation.");
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                Logger.DebugFormat("DeleteMsRenovation() - ERROR DataException. Result = {0}", exDb.Message);
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                Logger.DebugFormat("DeleteMsRenovation() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }

            Logger.InfoFormat("DeleteMsRenovation() - Finished.");
        }

        public ListResultDto<GetAllMsRenovationListDto> GetAllMsRenovation()
        {
            var listResult = (from x in _msRenovRepo.GetAll()
                              orderby x.Id descending
                              select new GetAllMsRenovationListDto
                              {
                                  Id = x.Id,
                                  renovationCode = x.renovationCode,
                                  renovationName = x.renovationName,
                                  detail = (x != null && x.detail != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(x.detail) : null, //TODO link + ip host
                                  isActive = x.isActive,
                                  projectID = x.projectID
                              }).ToList();

            return new ListResultDto<GetAllMsRenovationListDto>(listResult);
        }

        public ListResultDto<GetAllMsRenovationListDto> GetAllMsRenovationIsActive()
        {
            var listResult = (from x in _msRenovRepo.GetAll()
                              where x.isActive == true
                              orderby x.Id descending
                              select new GetAllMsRenovationListDto
                              {
                                  Id = x.Id,
                                  renovationCode = x.renovationCode,
                                  renovationName = x.renovationName,
                                  detail = (x != null && x.detail != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(x.detail) : null, //TODO link + ip host
                                  isActive = x.isActive,
                                  projectID = x.projectID
                              }).ToList();

            return new ListResultDto<GetAllMsRenovationListDto>(listResult);
        }


        public ListResultDto<GetLkItemListDto> GetAvailableItemDropdownByProject(int projectID, int? renovID) 
        {
            List<GetLkItemListDto> dataResult = null;
            var getUnavailabelRenovCode = (from x in _msRenovRepo.GetAll()
                                           where x.projectID == projectID
                                           select x.renovationCode).ToList();
            if (renovID == null)
            {
                dataResult = (from A in _lkItemRepo.GetAll()
                              where !getUnavailabelRenovCode.Contains(A.itemCode)
                              select new GetLkItemListDto
                              {
                                  Id = A.Id,
                                  itemCode = A.itemCode,
                                  shortName = A.shortName
                              }).ToList();
            }
            else
            {
                var getEditedRenovCode = (from x in _msRenovRepo.GetAll()
                                          where x.Id == renovID
                                          select x.renovationCode).FirstOrDefault();

                dataResult = (from A in _lkItemRepo.GetAll()
                              where !getUnavailabelRenovCode.Contains(A.itemCode) || A.itemCode == getEditedRenovCode
                              select new GetLkItemListDto
                              {
                                  Id = A.Id,
                                  itemCode = A.itemCode,
                                  shortName = A.shortName
                              }).ToList();
            }
            return new ListResultDto<GetLkItemListDto>(dataResult);
        }

        public ListResultDto<GetLkItemListDto> GetItemDropdown()
        {
            var dataResult = (from A in _lkItemRepo.GetAll()
                              select new GetLkItemListDto
                              {
                                  Id = A.Id,
                                  itemCode = A.itemCode,
                                  shortName = A.shortName
                              }).ToList();

            return new ListResultDto<GetLkItemListDto>(dataResult);
        }

        public ListResultDto<GetLkItemListDto> GetItemDropdownByProjectWithoutRenoveCode(int projectID, string renovCode)
        {
            var dataResult = (from B in _lkItemRepo.GetAll()
                              where !renovCode.Contains(B.itemCode)
                              select new GetLkItemListDto
                              {
                                  Id = B.Id,
                                  itemCode = B.itemCode,
                                  shortName = B.shortName
                              }).ToList();

            return new ListResultDto<GetLkItemListDto>(dataResult);
        }

        public ListResultDto<GetAllMsRenovationListDto> GetAllMsRenovationByProject(int projectID)
        {
            var listResult = (from x in _msRenovRepo.GetAll()
                              join project in _msProjectRepo.GetAll() on x.projectID equals project.Id
                              where x.projectID == projectID
                              && x.isActive == true
                              orderby x.Id descending
                              select new GetAllMsRenovationListDto
                              {
                                  Id = x.Id,
                                  renovationCode = x.renovationCode,
                                  renovationName = x.renovationName,
                                  detail = (x != null && x.detail != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(x.detail) : null, //TODO link + ip host
                                  isActive = x.isActive,
                                  projectID = x.projectID,
                                  projectName = project.projectName
                              }).ToList();

            return new ListResultDto<GetAllMsRenovationListDto>(listResult);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_GenerateUnit)]
        public ListResultDto<GetAllMsRenovationListDto> GetMsRenovationDropdown()
        {
            var listResult = (from x in _msRenovRepo.GetAll()
                              where x.isActive == true
                              select new GetAllMsRenovationListDto
                              {
                                  Id = x.Id,
                                  renovationCode = x.renovationCode,
                                  renovationName = x.renovationName
                              }).ToList();

            return new ListResultDto<GetAllMsRenovationListDto>(listResult);
        }

        private void GetAvailableFile(string detailUrl)
        {
            if (!string.IsNullOrWhiteSpace(detailUrl))
            {
                var filename = detailUrl.Split(@"/");
                var nameFile = filename[filename.Count() - 1];
                var Path = @"\Assets\Upload\RenovationFile\";

                var _file = _hostingEnvironment.WebRootPath + Path + nameFile;

                SendConsole("lokasi file:"+ _file);

                if (File.Exists(_file))
                {
                    SendConsole("File ada!!!");
                    throw new UserFriendlyException("File ada!!!");
                }
                else
                {
                    throw new UserFriendlyException("File tidak ada!!!");
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterRenovation_Edit)]
        public void UpdateMsRenovation(UpdateMsRenovationInput input)
        {
            Logger.InfoFormat("UpdateMsRenovation() Started.");

            Logger.DebugFormat("UpdateMsRenovation() - Start checking existing renovationCode, renovationName. Parameters sent: {0} " +
        "   renovationCode = {1}{0}" +
        "   renovationName = {2}{0}"
        , Environment.NewLine, input.renovationCode, input.renovationName);
            var checkCodeName = (from x in _msRenovRepo.GetAll()
                                 where x.Id != input.Id && x.projectID==input.projectID && (x.renovationCode == input.renovationCode || x.renovationName == input.renovationName)
                                 select x).Any();
            Logger.DebugFormat("UpdateMsRenovation() - End checking existing renovationCode, renovationName.");

            if (!checkCodeName)
            {
                Logger.DebugFormat("UpdateMsRenovation() - Start get renovation for update. Parameters sent: {0} " +
            "   renovationID = {1}{0}"
            , Environment.NewLine, input.Id);
                var getMsRenov = (from x in _msRenovRepo.GetAll()
                                  where input.Id == x.Id
                                  select x).FirstOrDefault();
                var updateMsRenov = getMsRenov.MapTo<MS_Renovation>();
                Logger.DebugFormat("UpdateMsRenovation() - End get renovation for update. Result = {0}", updateMsRenov);

                updateMsRenov.renovationCode = input.renovationCode;
                updateMsRenov.renovationName = input.renovationName;
                updateMsRenov.detail = input.detailNameToDelete;
                updateMsRenov.isActive = input.isActive;
                updateMsRenov.projectID = input.projectID;

                if (input.detailName == "updated")
                {
                    var fileToDelete = input.detailNameToDelete;

                    if (fileToDelete != null && fileToDelete != "")
                    {
                        var filenameToDelete = fileToDelete.Split(@"/");
                        var nameFileToDelete = filenameToDelete[filenameToDelete.Count() - 1];
                        var deletePath = @"\Assets\Upload\RenovationFile\";

                        var deleteImage = _hostingEnvironment.WebRootPath + deletePath + nameFileToDelete;

                        if (File.Exists(deleteImage))
                        {
                            Logger.DebugFormat("UpdateMsRenovation() - Start delete image. Params = {0}", deleteImage);
                            var file = new FileInfo(deleteImage);
                            file.Delete();
                            Logger.DebugFormat("UpdateMsRenovation() - End delete image.");
                        }
                    }

                    Logger.DebugFormat("UpdateMsRenovation() - Start upload image. Params = {0}", input.detailNameNew);
                    var imageUrl = uploadFile(input.detailNameNew);
                    GetURLWithoutHost(imageUrl, out imageUrl);
                    updateMsRenov.detail = imageUrl;
                    Logger.DebugFormat("UpdateMsRenovation() - End upload image. Result = {0}", imageUrl);

                }
                else if (input.detailName == "removed")
                {
                    updateMsRenov.detail = "";
                    var fileToDelete = input.detailNameToDelete;
                    if (fileToDelete != null && fileToDelete != "")
                    {
                        var filenameToDelete = fileToDelete.Split(@"/");
                        var nameFileToDelete = filenameToDelete[filenameToDelete.Count() - 1];
                        var deletePath = @"\Assets\Upload\RenovationFile\";

                        var deleteImage = _hostingEnvironment.WebRootPath + deletePath + nameFileToDelete;

                        if (File.Exists(deleteImage))
                        {
                            Logger.DebugFormat("UpdateMsRenovation() - Start delete image. Params = {0}", deleteImage);
                            var file = new FileInfo(deleteImage);
                            file.Delete();
                            Logger.DebugFormat("UpdateMsRenovation() - End delete image.");
                        }
                    }
                }
                else
                {
                    updateMsRenov.detail = getMsRenov.detail;
                }

                try
                {
                    Logger.DebugFormat("UpdateMsRenovation() - Start update Renovation. Parameters sent: {0} " +
                "   renovationCode = {1}{0}" +
                "   renovationName = {2}{0}" +
                "   detail = {3}{0}" +
                "   isActive = {4}{0}" +
                "   projectID = {5}{0}" +
                "   detail = {6}{0}"
                , Environment.NewLine, input.renovationCode, input.renovationName, updateMsRenov.detail,
                                input.isActive, input.projectID);
                    _msRenovRepo.Update(updateMsRenov);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("UpdateMsRenovation() - End update Renovation.");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.DebugFormat("UpdateMsRenovation() - ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.DebugFormat("UpdateMsRenovation() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("UpdateMsRenovation() - ERROR. Result = {0}", "Renovation Code or Renovation Name Already Exist !");
                throw new UserFriendlyException("Renovation Code or Renovation Name Already Exist !");
            }

            Logger.InfoFormat("UpdateMsRenovation() - Finished.");
        }

        private string uploadFile(string filename)
        {
            try
            {
                return _filesHelper.MoveFiles(filename, @"Temp\Downloads\RenovationFile\", @"Assets\Upload\RenovationFile\");
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Upload Error !", ex.Message);
            }
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
