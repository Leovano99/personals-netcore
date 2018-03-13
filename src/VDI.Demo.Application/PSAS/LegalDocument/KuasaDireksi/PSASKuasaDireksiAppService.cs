using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PSAS.LegalDocument.KuasaDireksi.Dto;
using VDI.Demo.Files;
using Abp.UI;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Abp.AutoMapper;
using Abp.Authorization;
using VDI.Demo.Authorization;
using Microsoft.Extensions.Configuration;
using VDI.Demo.Configuration;
using VDI.Demo.PSAS.Price;
using VDI.Demo.PSAS.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.PSAS.LegalDocument.KuasaDireksi
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi)]
    public class PSASKuasaDireksiAppService : DemoAppServiceBase, IPSASMsKuasaDireksiAppService
    {
        private readonly IRepository<MS_KuasaDireksi> _msKuasaDireksiRepo;
        private readonly IRepository<MS_KuasaDireksiPeople> _msKuasaDireksiPeopleRepo;
        private readonly IRepository<MS_DocumentPS> _msDocumentRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Officer> _msOfficerRepo;
        private readonly IRepository<MS_Position> _msPositionRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IFilesHelper _iFilesHelper;
        private readonly IPSASPriceAppService _ipriceAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfigurationRoot _appConfiguration;

        public PSASKuasaDireksiAppService(
            IRepository<MS_KuasaDireksi> msKuasaDireksiRepo,
            IRepository<MS_KuasaDireksiPeople> msKuasaDireksiPeopleRepo,
            IRepository<MS_DocumentPS> msDocumentRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<MS_Officer> msOfficerRepo,
            IRepository<MS_Position> msPositionRepo,
            IRepository<MS_Unit> msUnitRepo,
            IFilesHelper iFilesHelper,
            IPSASPriceAppService ipriceAppService,
            IHostingEnvironment hostingEnvironment,
             IHttpContextAccessor httpContextAccessor
            )
        {
            _msKuasaDireksiRepo = msKuasaDireksiRepo;
            _msKuasaDireksiPeopleRepo = msKuasaDireksiPeopleRepo;
            _msDocumentRepo = msDocumentRepo;
            _msProjectRepo = msProjectRepo;
            _msOfficerRepo = msOfficerRepo;
            _msPositionRepo = msPositionRepo;
            _msUnitRepo = msUnitRepo;
            _iFilesHelper = iFilesHelper;
            _ipriceAppService = ipriceAppService;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _appConfiguration = hostingEnvironment.GetAppConfiguration();
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi_Create)]
        public void CreateUniversalKuasaDireksi(CreateKuasaDireksiInputDto input)
        {
            Logger.InfoFormat("CreateKuasaDireksi() Started.");

            Logger.DebugFormat("CreateKuasaDireksi() - Start Preparation Data For Insert MS_KuasaDireksi. Parameters sent: {0} " +
            "   entityID = 1" +
            "   docID = {1}{0}" +
            "   isActive = {2}{0}" +
            "   suratKuasaImage = {3}{0}" +
            "   projectID = {4}{0}" +
            "   remarks = {5}{0}"
            , Environment.NewLine, input.docID, input.isActive, input.linkFileKuasaDireksi, input.projectID, input.remarks);

            //Logger.InfoFormat("CreateKuasaDireksi() Start Check ProjectId and DocId.");
            //var cekKuasaDireksi = (from A in _msKuasaDireksiRepo.GetAll()
            //                       where A.entityID == 1 && A.projectID == input.projectID && A.docID == input.docID && A.isActive
            //                       select A).Any();
            //Logger.InfoFormat("CreateKuasaDireksi() End Check ProjectId and DocId.");
            //if (cekKuasaDireksi)
            //{
            //    throw new UserFriendlyException("Data with ProjectID: " + input.projectID + " and DocID: " + input.docID + " is exist!");
            //}
            //else
            //{
                if (input.kuasaDireksiID != null)
                {
                    DeleteKuasaDireksi(input.kuasaDireksiID.GetValueOrDefault());
                }

                if (input.linkFileKuasaDireksi != null && input.linkFileKuasaDireksi != "")
                {
                    var fileKuasaDireksi = uploadFile(input.linkFileKuasaDireksi, @"Temp\Downloads\LegalDocument\KuasaDireksi\", @"Assets\Upload\LegalDocument\KuasaDireksi\");
                    GetURLWithoutHost(fileKuasaDireksi, out fileKuasaDireksi);
                    input.linkFileKuasaDireksi = fileKuasaDireksi;
                }

                var dataKuasaDireksi = new MS_KuasaDireksi
                {
                    entityID = 1,
                    docID = input.docID,
                    isActive = input.isActive,
                    suratKuasaImage = input.linkFileKuasaDireksi,
                    projectID = input.projectID,
                    remarks = input.remarks
                };
                Logger.DebugFormat("CreateKuasaDireksi() - End Preparation Data For Insert MS_KuasaDireksi.");

                Logger.DebugFormat("CreateKuasaDireksi() - Start Insert MS_KuasaDireksi entityID = 1" +
                "   docID = {1}{0}" +
                "   isActive = {2}{0}" +
                "   suratKuasaImage = {3}{0}" +
                "   projectID = {4}{0}" +
                "   remarks = {5}{0}"
                , Environment.NewLine, input.docID, input.isActive, input.linkFileKuasaDireksi, input.projectID, input.remarks);
                var kuasaDireksiID = _msKuasaDireksiRepo.InsertAndGetId(dataKuasaDireksi);
                Logger.DebugFormat("CreateKuasaDireksi() - End Insert MS_KuasaDireksi. KuasaDireksiID: " + kuasaDireksiID);

                Logger.DebugFormat("CreateKuasaDireksiPeople() - Start Insert MS_KuasaDireksiPeople");
                var kuasaDireksiPeople = input.kuasaDireksiPeople.Select(x => new CreateKuasaDireksiPeopleInputDto
                {
                    kuasaDireksiID = kuasaDireksiID,
                    entityID = x.entityID,
                    isActive = x.isActive,
                    officerID = x.officerID,
                    name = x.name,
                    email = x.email,
                    noTelp = x.noTelp,
                    position = x.position,
                    signatureImage = x.signatureImage
                }).ToList();
                CreateKuasaDireksiPeople(kuasaDireksiPeople);
            //}
            Logger.DebugFormat("CreateKuasaDireksiPeople() - End Insert MS_KuasaDireksiPeople");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi_Create)]
        public void CreateKuasaDireksiPeople(List<CreateKuasaDireksiPeopleInputDto> dataKuasaDireksiPeople)
        {
            var data = dataKuasaDireksiPeople.Select(x => new MS_KuasaDireksiPeople
            {
                entityID = x.entityID,
                kuasaDireksiID = x.kuasaDireksiID,
                isActive = x.isActive,
                officerID = x.officerID,
                signeeName = x.name,
                signeeEmail = x.email,
                signeePhone = x.noTelp,
                signeePosition = x.position,
                signeeSignImage = x.signatureImage
            }).ToList();

            int inc = 1;
            foreach (var kuasaDireksi in data)
            {
                if (kuasaDireksi.signeeSignImage != null && kuasaDireksi.signeeSignImage != "")
                {
                    if (!kuasaDireksi.signeeSignImage.Contains("Assets"))
                    {
                        var signImage = uploadFile(kuasaDireksi.signeeSignImage, @"Temp\Downloads\LegalDocument\KuasaDireksiPeople\", @"Assets\Upload\LegalDocument\KuasaDireksiPeople\");
                        GetURLWithoutHost(signImage, out signImage);
                        kuasaDireksi.signeeSignImage = signImage;
                    }
                }

                Logger.DebugFormat("CreateKuasaDireksi() - Start Insert MS_KuasaDireksiPeople " + inc + " of " + dataKuasaDireksiPeople.Count + " entityID = 1" +
                "   kuasaDireksiID = {1}{0}" +
                "   isActive = {2}{0}" +
                "   officerID = {3}{0}" +
                "   signeeName = {4}{0}" +
                "   signeeEmail = {5}{0}" +
                "   signeePhone = {6}{0}" +
                "   signeePosition = {7}{0}" +
                "   signeeSignImage = {8}{0}"
                , Environment.NewLine, kuasaDireksi.kuasaDireksiID, kuasaDireksi.isActive, kuasaDireksi.officerID, kuasaDireksi.signeeName,
                kuasaDireksi.signeeEmail, kuasaDireksi.signeePhone, kuasaDireksi.signeePosition, kuasaDireksi.signeeSignImage);
                _msKuasaDireksiPeopleRepo.Insert(kuasaDireksi);
                inc++;
                Logger.DebugFormat("CreateKuasaDireksi() - End Insert MS_KuasaDireksiPeople.");
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MasterKuasaDireksi_Delete)]
        public void DeleteKuasaDireksi(int kuasaDireksiID)
        {
            Logger.DebugFormat("DeleteKuasaDireksi() - Start Update IsActive MS_KuasaDireksi " +
                "   kuasaDireksiID = {1}{0}"
                , Environment.NewLine, kuasaDireksiID);
            
            var getKuasaDireksi = (from x in _msKuasaDireksiRepo.GetAll()
                                      where x.Id == kuasaDireksiID
                                      select x).FirstOrDefault();
            var updateKuasaDireksi = getKuasaDireksi.MapTo<MS_KuasaDireksi>();
            updateKuasaDireksi.isActive = false;
            Logger.DebugFormat("DeleteKuasaDireksi() - Start Update isActive MS_KuasaDireksi ");
            _msKuasaDireksiRepo.Update(updateKuasaDireksi);
            Logger.DebugFormat("DeleteKuasaDireksi() - End Update isActive MS_KuasaDireksi ");

            var getDataKuasaDireksiPeople = GetPSASKuasaDireksiPeopleByKuasaDireksiID(kuasaDireksiID);
            foreach (var data in getDataKuasaDireksiPeople)
            {
                var getKuasaDireksiPeople = (from x in _msKuasaDireksiPeopleRepo.GetAll()
                                       where x.Id == data.kuasaDireksiPeopleID
                                       select x).FirstOrDefault();
                var updateKuasaDireksiPeople = getKuasaDireksiPeople.MapTo<MS_KuasaDireksiPeople>();
                updateKuasaDireksiPeople.isActive = false;
                Logger.DebugFormat("DeleteKuasaDireksiPeople() - Start Update isActive MS_KuasaDireksiPeople ");
                _msKuasaDireksiPeopleRepo.Update(updateKuasaDireksiPeople);
                Logger.DebugFormat("DeleteKuasaDireksiPeople() - Start Update isActive MS_KuasaDireksiPeople ");
            }

            Logger.DebugFormat("DeleteKuasaDireksi() - End Delete MS_KuasaDireksiPeople ");
        }

        public PagedResultDto<GetPSASListOfKuasaDireksiListDto> GetPSASListOfKuasaDireksi(PSASListOfKuasaDireksiInputDto input)
        {
            var queryMsKuasaDireksi = (from A in _msKuasaDireksiRepo.GetAll()
                                       join B in _msDocumentRepo.GetAll() on A.docID equals B.Id
                                       join C in _msProjectRepo.GetAll() on A.projectID equals C.Id
                                       select new GetPSASListOfKuasaDireksiListDto
                                       {
                                           entityID = A.entityID,
                                           kuasaDireksiID = A.Id,
                                           kuasaDireksiCode = _iFilesHelper.ConvertIdToCode(A.Id),
                                           docID = A.docID,
                                           docCode = B.docCode,
                                           projectID = A.projectID,
                                           projectCode = C.projectCode,
                                           projectName = C.projectName,
                                           suratKuasaImage = (A != null && A.suratKuasaImage != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(A.suratKuasaImage) : null, //TODO link + ip host,
                                           remarks = A.remarks,
                                           isActive = A.isActive
                                       })
                                       .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                                            u =>
                                                u.projectName.ToLower().Contains(input.Filter.ToLower()) ||
                                                u.docCode.ToLower().Contains(input.Filter.ToLower()) ||
                                                u.kuasaDireksiCode.ToLower().Contains(input.Filter.ToLower()) ||
                                                u.remarks.ToLower().Contains(input.Filter.ToLower())
                                        );

            var dataCount = queryMsKuasaDireksi.Count();

            var getListOfKuasaDireksi = queryMsKuasaDireksi
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToList();

            return new PagedResultDto<GetPSASListOfKuasaDireksiListDto>(
                dataCount,
                getListOfKuasaDireksi
                );
        }

        public GetPSASKuasaDireksiAndPeopleListDto GetPSASKuasaDireksiAndPeople(int id)
        {
            GetPSASKuasaDireksiAndPeopleListDto GetPSASKuasaDireksi = new GetPSASKuasaDireksiAndPeopleListDto();
            var queryMsKuasaDireksi = GetPSASListOfKuasaDireksiByDireksiID(id);
            var queryKuasaDireksiPeople = GetPSASKuasaDireksiPeopleByKuasaDireksiID(id);

            GetPSASKuasaDireksi.GetDetailKuasaDireksi = queryMsKuasaDireksi;
            GetPSASKuasaDireksi.GetDetailKuasaDireksiPeople = queryKuasaDireksiPeople;
 
            return GetPSASKuasaDireksi;
        }

        public GetDetailPSASListOfKuasaDireksiListDto GetPSASListOfKuasaDireksiByDireksiID(int id)
        {
            var queryMsKuasaDireksi = (from A in _msKuasaDireksiRepo.GetAll()
                                       join B in _msDocumentRepo.GetAll() on A.docID equals B.Id
                                       join C in _msProjectRepo.GetAll() on A.projectID equals C.Id
                                       where A.Id == id
                                       select new GetDetailPSASListOfKuasaDireksiListDto
                                       {
                                           entityID = A.entityID,
                                           kuasaDireksiID = A.Id,
                                           kuasaDireksiCode = _iFilesHelper.ConvertIdToCode(A.Id),
                                           linkFileKuasaDireksi = (A != null && A.suratKuasaImage != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(A.suratKuasaImage) : null,
                                           docID = A.docID,
                                           isActive = A.isActive,
                                           projectID = A.projectID,
                                           remarks = A.remarks
                                       }).FirstOrDefault();

            return queryMsKuasaDireksi;
        }

        public List<GetPSASListOfKuasaDireksiPeopleListDto> GetPSASKuasaDireksiPeopleByKuasaDireksiID(int kuasaDireksiID)
        {
            List<GetPSASListOfKuasaDireksiPeopleListDto> GetKuasaDireksiPeople = new List<GetPSASListOfKuasaDireksiPeopleListDto>();

            var queryMsKuasaDireksiPeople = (from A in _msKuasaDireksiPeopleRepo.GetAll()
                                             where A.kuasaDireksiID == kuasaDireksiID
                                             select new GetPSASListOfKuasaDireksiPeopleListDto
                                             {
                                                 entityID = A.entityID,
                                                 kuasaDireksiPeopleID = A.Id,
                                                 kuasaDireksiID = A.kuasaDireksiID,
                                                 email = A.signeeEmail,
                                                 name = A.signeeName,
                                                 noTelp = A.signeePhone,
                                                 officerID = A.officerID,
                                                 position = A.signeePosition,
                                                 signatureImage = A.signeeSignImage,
                                                 isActive = A.isActive
                                             }).ToList();

            var kuasaDireksiOfficer = queryMsKuasaDireksiPeople.Where(x => x.officerID != null).ToList() != null;
            if (kuasaDireksiOfficer)
            {
                var queryOfficer = (from A in _msKuasaDireksiPeopleRepo.GetAll()
                                    join B in _msOfficerRepo.GetAll() on A.officerID equals B.Id
                                    join C in _msPositionRepo.GetAll() on B.positionID equals C.Id
                                    where A.officerID != null && A.kuasaDireksiID == kuasaDireksiID
                                    select new GetPSASListOfKuasaDireksiPeopleListDto
                                    {
                                        entityID = A.entityID,
                                        kuasaDireksiPeopleID = A.Id,
                                        kuasaDireksiID = A.kuasaDireksiID,
                                        officerID = B.Id,
                                        name = B.officerName,
                                        noTelp = B.officerPhone,
                                        position = C.positionName,
                                        email = B.officerEmail,
                                        isActive = A.isActive,
                                        signatureImage = (A != null && A.signeeSignImage != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(A.signeeSignImage) : null
                                    }).ToList();

                GetKuasaDireksiPeople.AddRange(queryOfficer);
            }

            var kuasaDireksiNonOfficer = queryMsKuasaDireksiPeople.Where(x => x.officerID == null).ToList() != null;
            if (kuasaDireksiNonOfficer)
            {
                var queryNonOfficer = queryMsKuasaDireksiPeople.Select(x => new GetPSASListOfKuasaDireksiPeopleListDto
                {
                    entityID = x.entityID,
                    kuasaDireksiPeopleID = x.kuasaDireksiPeopleID,
                    kuasaDireksiID = x.kuasaDireksiID,
                    officerID = x.officerID,
                    name = x.name,
                    noTelp = x.noTelp,
                    position = x.position,
                    email = x.email,
                    isActive = x.isActive,
                    signatureImage = (x.signatureImage != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(x.signatureImage) : null
                }).Where(x => x.officerID == null).ToList();

                GetKuasaDireksiPeople.AddRange(queryNonOfficer);
            }
            
            return GetKuasaDireksiPeople;
        }

        private string uploadFile(string filename, string pathTemp, string pathAsset)
        {
            try
            {
                return _iFilesHelper.MoveFilesLegalDoc(filename, pathTemp, pathAsset);
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

        private void DeleteFileOrImage(string fileToDelete, string path)
        {
            var filenameToDelete = fileToDelete.Split(@"/");
            var nameFileToDelete = filenameToDelete[filenameToDelete.Count() - 1];
            var deletePath = path;

            var deleteImage = _hostingEnvironment.WebRootPath + deletePath + nameFileToDelete;

            if (File.Exists(deleteImage))
            {
                Logger.DebugFormat("DeleteFileOrImage() - Start delete file or image. Params = {0}", deleteImage);
                var file = new FileInfo(deleteImage);
                file.Delete();
                Logger.DebugFormat("DeleteFileOrImage() - End delete file or image.");
            }
        }

        public List<GetDetailPSASListOfKuasaDireksiListDto> GetDropdownPSASListOfKuasaDireksi(string bookCode, int docID)
        {
            GetPSASParamsDto inputUnitID = new GetPSASParamsDto();
            inputUnitID.bookCode = bookCode;
            var getUnitID = _ipriceAppService.GetParameter(inputUnitID);
            var getProjectID = (from A in _msUnitRepo.GetAll()
                                where A.entityID == 1 &&
                                A.Id == getUnitID.unitID
                                select A.projectID).FirstOrDefault();

            var getKuasaDireksi = (from A in _msKuasaDireksiRepo.GetAll()
                                   join B in _msKuasaDireksiPeopleRepo.GetAll() on A.Id equals B.kuasaDireksiID
                                   where A.entityID == 1 &&
                                   A.docID == docID &&
                                   A.projectID == getProjectID &&
                                   A.isActive
                                   select new GetDetailPSASListOfKuasaDireksiListDto
                                   {
                                       namePosition = B.signeeName + "-" + B.signeePosition,
                                       docID = A.docID,
                                       kuasaDireksiPeopleID = B.Id,
                                       kuasaDireksiID = A.Id,
                                       entityID = A.entityID,
                                       isActive = A.isActive,
                                       projectID = A.projectID,
                                       remarks = A.remarks,
                                       linkSignature = B.signeeSignImage
                                   }).ToList();

            return getKuasaDireksi;
        }

        public List<GetPSASListOfKuasaDireksiListDto> GetAllPSASListOfKuasaDireksi()
        {
            var queryMsKuasaDireksi = (from A in _msKuasaDireksiRepo.GetAll()
                                       join B in _msDocumentRepo.GetAll() on A.docID equals B.Id
                                       join C in _msProjectRepo.GetAll() on A.projectID equals C.Id
                                       select new GetPSASListOfKuasaDireksiListDto
                                       {
                                           entityID = A.entityID,
                                           kuasaDireksiID = A.Id,
                                           kuasaDireksiCode = _iFilesHelper.ConvertIdToCode(A.Id),
                                           docID = A.docID,
                                           docCode = B.docCode,
                                           projectID = A.projectID,
                                           projectCode = C.projectCode,
                                           projectName = C.projectName,
                                           suratKuasaImage = (A != null && A.suratKuasaImage != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(A.suratKuasaImage) : null, //TODO link + ip host,
                                           remarks = A.remarks,
                                           isActive = A.isActive
                                       }).ToList();

            return queryMsKuasaDireksi;
        }
    }

}
