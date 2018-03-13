using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VDI.Demo.MasterPlan.Unit.MS_Details.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.Files;
using System.Text.RegularExpressions;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Data;
using Abp.AutoMapper;
using Newtonsoft.Json.Linq;
using Abp.Authorization;
using VDI.Demo.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using VDI.Demo.Configuration;

namespace VDI.Demo.MasterPlan.Unit.MS_Details
{
    public class MsDetailAppService : DemoAppServiceBase , IMsDetailAppService
    {
        private readonly IRepository<MS_Detail> _msDetailRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly FilesHelper _filesHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfigurationRoot _appConfiguration;

        public MsDetailAppService(IRepository<MS_Detail> msDetailRepo,
            IRepository<MS_Unit> msUnitRepo,
            FilesHelper filesHelper,
            IHostingEnvironment environment,
            IHttpContextAccessor httpContextAccessor)
        {
            _msDetailRepo = msDetailRepo;
            _msUnitRepo = msUnitRepo;
            _filesHelper = filesHelper;
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _appConfiguration = environment.GetAppConfiguration();

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

        private string MoveImage(string filename)
        {
            try
            {
                return _filesHelper.MoveFiles(filename, @"Temp\Downloads\DetailImage\", @"Assets\Upload\DetailImage\");
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
        }

        private void DeleteImage(string filename)
        {
            var imageToDelete = filename;
            var filenameToDelete = imageToDelete.Split(@"/");
            var nameFileToDelete = filenameToDelete[filenameToDelete.Count() - 1];
            var deletePath = @"\Assets\Upload\DetailImage\";

            var deleteImage = _hostingEnvironment.WebRootPath + deletePath + nameFileToDelete;

            if (File.Exists(deleteImage))
            {
                var file = new FileInfo(deleteImage);
                file.Delete();
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDetail_Create)]
        public void CreateMsDetail(CreateOrUpdateMsDetailInputDto input)
        {
            Logger.Info("CreateMsDetail() Started.");

            Logger.DebugFormat("CreateMsDetail() - Start checking existing code. Parameters sent:{0}" +
                            "detailCode     = {1}{0}" +
                            "detailName     = {2}"
                            , Environment.NewLine, input.detailCode, input.detailName);

            var checkCode = (from x in _msDetailRepo.GetAll()
                                 where x.detailCode == input.detailCode
                                 select x).Any();

            Logger.DebugFormat("CreateMsDetail() - End checking existing code. Result:{0}", checkCode);

            if (!checkCode)
            {
                string imageUrl;

                if (string.IsNullOrWhiteSpace(input.detailImage))
                {
                    imageUrl = null;
                }
                else
                {
                    imageUrl = MoveImage(input.detailImage);
                    GetURLWithoutHost(imageUrl, out imageUrl);
                }

                var data = new MS_Detail
                {
                    entityID = 1,
                    detailCode = input.detailCode,
                    detailName = input.detailName,
                    detailImage = imageUrl
                };
                Logger.DebugFormat("CreateMsDetail() - Start checking insert Detail. Parameters sent:{0}" +
                "	entityID	= {1}{0}" +
                "	detailCode	= {2}{0}" +
                "	detailName	= {3}{0}" +
                "	detailImage	= {4}"
                , Environment.NewLine, 1, input.detailCode, input.detailName, input.detailImage);

                _msDetailRepo.Insert(data);

                Logger.DebugFormat("CreateMsDetail() - End insert Detail.");
            }
            else
            {
                Logger.ErrorFormat("CreateMsDetail() ERROR. Result = {0}", "Detail Code Already Exist !");
                throw new UserFriendlyException("Detail Code Already Exist !");
            }
            Logger.Info("CreateMsDetail() Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDetail_Delete)]
        public void DeleteMsDetail(int Id)
        {
            Logger.Info("DeleteMsDetail() Started.");

            Logger.DebugFormat("DeleteMsDetail() - Start checking detail with coID: {0}.", Id);

            bool checkUnit = (from unit in _msUnitRepo.GetAll()
                                 where unit.detailID == Id
                                 select unit.detailID).Any();

            Logger.DebugFormat("DeleteMsDetail() - End checking detail. Result: {0}.", checkUnit);

            if (!checkUnit)
            {
                var getImage = (from x in _msDetailRepo.GetAll()
                                where x.Id == Id
                                select x.detailImage).FirstOrDefault();
                try
                {
                    Logger.DebugFormat("DeleteMsDetail() - Start delete Detail. Parameters sent: {0}", Id);
                    if (!string.IsNullOrWhiteSpace(getImage))
                    {
                        DeleteImage(getImage);
                    }
                    _msDetailRepo.Delete(Id);
                    Logger.DebugFormat("DeleteMsDetail() - End delete Detail");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("DeleteMsDetail() ERROR DbException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsDetail() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("DeleteMsDetail() ERROR Exception. Result = {0}", "This Detail is used by another master!");
                throw new UserFriendlyException("This Detail is used by another master!");
            }
            Logger.Info("DeleteMsDetail() Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDetail)]
        public ListResultDto<GetAllMsDetailListDto> GetAllMsDetail()
        {
            var result = (from x in _msDetailRepo.GetAll()
                          select new GetAllMsDetailListDto
                          {
                              Id = x.Id,
                              detailCode = x.detailCode,
                              detailName = x.detailName,
                              detailImage = (x != null && string.IsNullOrWhiteSpace(x.detailImage)) ? (!x.detailImage.Equals("-")) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(x.detailImage) : null : null,
                          })
                          .ToList();

            return new ListResultDto<GetAllMsDetailListDto>(result);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDetail_Edit)]
        public JObject UpdateMsDetail(CreateOrUpdateMsDetailInputDto input)
        {
            JObject obj = new JObject();

            Logger.DebugFormat("UpdateMsDetail() - Start checking existing code. Parameters sent:{0}" +
                "detailCode     = {1}{0}" +
                "detailName     = {2}{0}"
                , Environment.NewLine, input.detailCode, input.detailName);

            var checkCode = (from x in _msDetailRepo.GetAll()
                                 where x.Id != input.Id && (x.detailCode == input.detailCode)
                                 select x).Any();

            Logger.DebugFormat("UpdateMsDetail() - End checking existing code. Result: {0}", checkCode);

            var getMsDetail = (from x in _msDetailRepo.GetAll()
                                where x.Id == input.Id
                                select x).FirstOrDefault();
            if (!checkCode)
            {
                var getOldImage = getMsDetail.detailImage;//mark

                var updateMsDetail = getMsDetail.MapTo<MS_Detail>();

                if (input.imageStatus == "updated")
                {
                    var imageUrl = MoveImage(input.detailImage);
                    GetURLWithoutHost(imageUrl, out imageUrl);
                    updateMsDetail.detailImage = imageUrl;
                    if (!string.IsNullOrWhiteSpace(getOldImage))
                    {
                        DeleteImage(getOldImage);
                    }
                }
                else if (input.imageStatus == "removed")
                {
                    DeleteImage(getOldImage);
                    updateMsDetail.detailImage = null;
                }
                else
                {
                    updateMsDetail.detailImage = getMsDetail.detailImage;
                }

                var checkUnit = (from unit in _msUnitRepo.GetAll()
                                 where unit.detailID == input.Id
                                 select unit).Any();

                if (!checkUnit)
                {
                    updateMsDetail.detailCode = input.detailCode;
                    updateMsDetail.detailName = input.detailName;
                    obj.Add("message", "Edit Successfully");
                }
                else
                {
                    obj.Add("message", "Edit Successfully, but can't change Detail Code & Name");
                }

                Logger.DebugFormat("UpdateMsDetail() - Start update Detail. Parameters sent:{0}" +
                    "	entityID	= {1}{0}" +
                    "	detailCode	= {2}{0}" +
                    "	detailName	= {3}{0}" +
                    "	detailImage	= {4}"
                , Environment.NewLine, 1, input.detailCode, input.detailName, input.detailImage);

                _msDetailRepo.Update(updateMsDetail);

                Logger.DebugFormat("UpdateMsDetail() - End update Detail");
            }
            else
            {
                Logger.ErrorFormat("UpdateMsDetail() Detail Code already exist. Result = {0}", checkCode);
                throw new UserFriendlyException("Detail Code already exist!");
            }
            return obj;
        }
    }
}
