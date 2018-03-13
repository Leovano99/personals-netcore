using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using VDI.Demo.Files;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PSAS.LegalDocument.MappingTemplate.Dto;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Data;
using Abp.UI;
using Abp.Authorization;
using VDI.Demo.Authorization;

namespace VDI.Demo.PSAS.LegalDocument.MappingTemplate
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MappingTemplate)]
    public class PSASMappingTemplateAppService : DemoAppServiceBase, IPSASMappingTemplateAppService
    {
        private readonly IRepository<MS_MappingTemplate> _msMappingTemplateRepo;
        private readonly IRepository<MS_DocumentPS> _msDocumentPsRepo;
        private readonly IRepository<MS_DocTemplate> _msDocTemplateRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IFilesHelper _iFilesHelper;

        public PSASMappingTemplateAppService(
            IRepository<MS_MappingTemplate> msMappingTemplateRepo,
            IRepository<MS_DocumentPS> msDocumentPsRepo,
            IRepository<MS_DocTemplate> msDocTemplateRepo,
            IRepository<MS_Project> msProjectRepo,
            IFilesHelper iFilesHelper
            )
        {
            _msMappingTemplateRepo = msMappingTemplateRepo;
            _msDocumentPsRepo = msDocumentPsRepo;
            _msDocTemplateRepo = msDocTemplateRepo;
            _msProjectRepo = msProjectRepo;
            _iFilesHelper = iFilesHelper;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MappingTemplate_Create)]
        public void CreateMappingTemplate(CreateMappingTemplateInputDto input)
        {
            Logger.InfoFormat("CreateMappingTemplate() Started.");

            Logger.DebugFormat("CreateMappingTemplate() - Start Preparation Data For Insert MS_MappingTemplate. Parameters sent: {0} " +
            "   entityID = 1" +
            "   docID = {1}{0}" +
            "   docTemplateID = {2}{0}" +
            "   activeFrom = {3}{0}" +
            "   activeTo = {4}{0}" +
            "   isActive = {5}{0}" +
            "   projectID = {6}{0}"
            , Environment.NewLine, input.docID, input.docTemplateID, input.activeFrom, input.activeTo, input.isActive, input.projectID);
            var dataMappingTemplate = new MS_MappingTemplate
            {
                entityID = 1,
                projectID = input.projectID,
                docID = input.docID,
                docTemplateID = input.docTemplateID,
                activeFrom = input.activeFrom,
                activeTo = input.activeTo,
                isActive = input.isActive,
                isTandaTerima = input.isTandaTerima
            };
            Logger.DebugFormat("CreateMappingTemplate() - End Preparation Data For Insert MS_MappingTemplate.");

            Logger.InfoFormat("CheckDataIsExist() Start check Data is exist.");

            Logger.DebugFormat("CheckDataInsertIsExist() - Start Checking Data. ");
            var dataIsExist = CheckDataInsertIsExist(input.projectID, input.docID, input.docTemplateID, input.activeFrom, input.activeTo);
            Logger.DebugFormat("CheckDataInsertIsExist() - End Checking Data. ");

            if (dataIsExist.isExist)
            {
                if (dataIsExist.mappingTemplateID != null)
                {
                    UpdateDataIsExist(dataIsExist.mappingTemplateID.GetValueOrDefault(), dataIsExist.activeToOldData.GetValueOrDefault());
                }

                if (!input.activeTo.HasValue)
                {
                    dataMappingTemplate.activeTo = dataIsExist.activeTo;
                }
            }

            Logger.DebugFormat("CreateMappingTemplate() - Start Insert MS_MappingTemplate. ");
            _msMappingTemplateRepo.Insert(dataMappingTemplate);
            Logger.DebugFormat("CreateMappingTemplate() - End Insert MS_MappingTemplate. ");

            Logger.InfoFormat("CreateMappingTemplate() Finished.");
        }

        private CheckDataUpdateIsExistListDto CheckDataInsertIsExist(int projectID, int docID, int docTemplateID, DateTime activeFrom, DateTime? activeTo)
        {
            CheckDataUpdateIsExistListDto returnDataUpdate = new CheckDataUpdateIsExistListDto();

            var GetMappingTemplate = (from A in _msMappingTemplateRepo.GetAll()
                                      join B in _msDocTemplateRepo.GetAll() on A.docTemplateID equals B.Id
                                      join C in _msProjectRepo.GetAll() on A.projectID equals C.Id
                                      join D in _msDocumentPsRepo.GetAll() on A.docID equals D.Id
                                      where A.projectID == projectID && A.docID == docID
                                      && A.docTemplateID == docTemplateID
                                      && A.isActive
                                      select new UpdateMappingTemplateInputDto
                                      {
                                          entityID = A.entityID,
                                          isTandaTerima = A.isTandaTerima,
                                          mappingTemplateID = A.Id,
                                          docID = A.docID,
                                          docTemplateID = B.Id,
                                          projectID = A.projectID,
                                          isActive = A.isActive,
                                          activeFrom = A.activeFrom,
                                          activeTo = A.activeTo
                                      })
                                      .OrderByDescending(x => x.activeFrom)
                                      .ThenByDescending(x => x.activeTo);

            if (GetMappingTemplate.Any())
            {
                //Validation
                if (!activeTo.HasValue)
                {
                    if (GetMappingTemplate.Any(x => x.activeTo == null && x.activeFrom >= activeFrom))
                    {
                        throw new UserFriendlyException("Newer period is exist!");
                    }

                    if (GetMappingTemplate.Any(x => x.activeTo != null && (x.activeFrom <= activeFrom && x.activeTo >= activeFrom)))
                    {
                        throw new UserFriendlyException("Newer period is exist!");
                    }
                }
                else
                {
                    if (GetMappingTemplate.Any(x => x.activeTo == null && x.activeFrom <= activeFrom && x.activeFrom >= activeTo))
                    {
                        throw new UserFriendlyException("Newer period is exist!");
                    }

                    if (GetMappingTemplate.Any(x => x.activeTo != null && (x.activeFrom <= activeFrom && x.activeTo >= activeFrom) && (x.activeFrom <= activeTo && x.activeTo >= activeTo)))
                    {
                        throw new UserFriendlyException("Newer period is exist!");
                    }
                }

                //Return Value
                var oldDataMapping = GetMappingTemplate.Any(x => x.activeTo == null && x.activeFrom < activeFrom);
                if (oldDataMapping)
                {
                    var singleData = GetMappingTemplate.Where(x => x.activeTo == null && x.activeFrom < activeFrom).OrderByDescending(x => x.activeFrom).FirstOrDefault();
                    returnDataUpdate.isExist = true;
                    returnDataUpdate.mappingTemplateID = singleData.mappingTemplateID;
                    returnDataUpdate.activeToOldData = activeFrom.AddDays(-1);
                }

                if (!activeTo.HasValue && GetMappingTemplate.Any(x => x.activeFrom > activeFrom))
                {
                    returnDataUpdate.isExist = true;
                    returnDataUpdate.activeTo = GetMappingTemplate.Where(x => x.activeFrom > activeFrom).Select(x =>x.activeFrom.AddDays(-1)).OrderBy(x => x).FirstOrDefault();                    
                }
            }

            return returnDataUpdate;
        }

        private void UpdateDataIsExist(int mappingTemplateID, DateTime activeTo)
        {
            Logger.InfoFormat("UpdateDataIsExist() - Started.");
            Logger.DebugFormat("UpdateDataIsExist() - Start get MS_MappingTemplate for update. Parameters sent: {0} " +
                "mappingTemplateID = {1}{0}", Environment.NewLine, mappingTemplateID);
            var getMappingTemplate = (from x in _msMappingTemplateRepo.GetAll()
                                      where x.Id == mappingTemplateID
                                      select x).FirstOrDefault();
            Logger.DebugFormat("UpdateDataIsExist() - End get MS_MappingTemplate for update. Result = {0} ", getMappingTemplate);

            var updateMappingTemplate = getMappingTemplate.MapTo<MS_MappingTemplate>();
            updateMappingTemplate.activeTo = activeTo;
            
            Logger.DebugFormat("UpdateDataIsExist() - Start Update MS_MappingTemplate. Parameters sent: {0} " +
                "projectID = {1}{0}" +
                "docID = {2}{0}" +
                "docTemplateID = {3}{0}" +
                "activeFrom = {4}{0}" +
                "activeTo = {5}{0}" +
                "isTandaTerima = {6}{0}" +
                "isActive = {7}{0}"
                , Environment.NewLine, updateMappingTemplate.projectID, updateMappingTemplate.docID, updateMappingTemplate.docTemplateID, updateMappingTemplate.activeFrom, activeTo
                , updateMappingTemplate.isTandaTerima, updateMappingTemplate.isActive);
            _msMappingTemplateRepo.Update(updateMappingTemplate);
            Logger.DebugFormat("UpdateDataIsExist() - End Update MS_MappingTemplate.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MappingTemplate_Delete)]
        public void DeleteMappingTemplate(int mappingTemplateID)
        {
            Logger.InfoFormat("DeleteMappingTemplate() Started.");

            var getMappingTemplate = (from x in _msMappingTemplateRepo.GetAll()
                                         where x.Id == mappingTemplateID
                                         select x).FirstOrDefault();
            var updateMappingTemplate = getMappingTemplate.MapTo<MS_MappingTemplate>();
            updateMappingTemplate.isActive = false;

            Logger.DebugFormat("DeleteMappingTemplate() - Start Update isActive MS_MappingTemplate ");
            _msMappingTemplateRepo.Update(updateMappingTemplate);
            Logger.DebugFormat("DeleteMappingTemplate() - Start Update isActive MS_MappingTemplate ");

            Logger.InfoFormat("DeleteMappingTemplate() Finished.");
        }

        public PagedResultDto<GetMappingTemplateListDto> GetMappingTemplate(GetMappingTemplateInputDto input)
        {
            var queryMappingTemplate = (from A in _msMappingTemplateRepo.GetAll()
                              join B in _msDocTemplateRepo.GetAll() on A.docTemplateID equals B.Id
                              join C in _msProjectRepo.GetAll() on A.projectID equals C.Id
                              join D in _msDocumentPsRepo.GetAll() on A.docID equals D.Id
                              select new GetMappingTemplateListDto
                              {
                                  entityID = A.Id,
                                  mappingTemplateID = A.Id,
                                  mappingTemplateCode = _iFilesHelper.ConvertIdToCode(A.Id),
                                  docID = A.docID,
                                  docCode = D.docCode,
                                  docTemplateID = B.Id,
                                  docTemplateName = B.templateName,
                                  projectID = A.projectID,
                                  projectName = C.projectName,
                                  isActive = A.isActive,
                                  isTandaTerima = A.isTandaTerima,
                                  activeFrom = A.activeFrom,
                                  activeTo = A.activeTo,
                                  period = A.activeTo != null ? A.activeFrom.Date.ToString() + " - " + A.activeTo.Value.Date.ToString() : A.activeFrom.Date.ToString()
                              })
                              .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                                            u =>
                                                u.projectName.ToLower().Contains(input.Filter.ToLower()) ||
                                                u.docCode.ToLower().Contains(input.Filter.ToLower()) ||
                                                u.mappingTemplateCode.ToLower().Contains(input.Filter.ToLower()) ||
                                                u.activeFrom.ToString().Contains(input.Filter) ||
                                                u.activeTo.ToString().Contains(input.Filter)
                                        );

            var dataCount = queryMappingTemplate.Count();
            List<GetMappingTemplateListDto> getMappingTemplate = new List<GetMappingTemplateListDto>();
            if (dataCount != 0)
            {
                getMappingTemplate = queryMappingTemplate
                                    .OrderBy(input.Sorting)
                                    .PageBy(input)
                                    .ToList();
            }

            return new PagedResultDto<GetMappingTemplateListDto>(
                dataCount,
                getMappingTemplate
                );
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MappingTemplate_Edit)]
        public void UpdateMappingTemplate(CreateMappingTemplateInputDto input)
        {
            Logger.InfoFormat("UpdateMappingTemplate() - Started.");
            if (input.mappingTemplateID.HasValue)
            {
                Logger.DebugFormat("UpdateMappingTemplate() - Start get MS_MappingTemplate for update. Parameters sent: {0} " +
                    "mappingTemplateID = {1}{0}", Environment.NewLine, input.mappingTemplateID);
                var getMappingTemplate = (from x in _msMappingTemplateRepo.GetAll()
                                          where x.Id == input.mappingTemplateID
                                          select x).FirstOrDefault();
                Logger.DebugFormat("UpdateMappingTemplate() - End get MS_MappingTemplate for update. Result = {0} ", getMappingTemplate);

                Logger.DebugFormat("CheckDataInsertIsExist() - Start Checking Data. ");
                var checkDataUpdateIsExist = CheckDataUpdateIsExist(input.mappingTemplateID.GetValueOrDefault(), input.projectID, input.docID, input.docTemplateID, input.activeFrom, input.activeTo);
                Logger.DebugFormat("CheckDataInsertIsExist() - End Checking Data. ");

                var updateMappingTemplate = getMappingTemplate.MapTo<MS_MappingTemplate>();

                updateMappingTemplate.projectID = input.projectID;
                updateMappingTemplate.docID = input.docID;
                updateMappingTemplate.docTemplateID = input.docTemplateID;
                updateMappingTemplate.activeFrom = input.activeFrom;
                updateMappingTemplate.activeTo = input.activeTo;
                updateMappingTemplate.isTandaTerima = input.isTandaTerima;
                updateMappingTemplate.isActive = input.isActive;

                if (checkDataUpdateIsExist.isExist)
                {
                    if (checkDataUpdateIsExist.mappingTemplateID != null)
                    {
                        UpdateDataIsExist(checkDataUpdateIsExist.mappingTemplateID.GetValueOrDefault(), checkDataUpdateIsExist.activeToOldData.GetValueOrDefault());
                    }

                    if (!input.activeTo.HasValue)
                    {
                        updateMappingTemplate.activeTo = checkDataUpdateIsExist.activeTo;
                    }
                }

                Logger.DebugFormat("UpdateMappingTemplate() - Start Update MS_MappingTemplate. Parameters sent: {0} " +
                        "projectID = {1}{0}" +
                        "docID = {2}{0}" +
                        "docTemplateID = {3}{0}" +
                        "activeFrom = {4}{0}" +
                        "activeTo = {5}{0}" +
                        "isTandaTerima = {6}{0}" +
                        "isActive = {7}{0}"
                        , Environment.NewLine, input.projectID, input.docID, input.docTemplateID, input.activeFrom, input.activeTo
                        , input.isTandaTerima, input.isActive);
                _msMappingTemplateRepo.Update(updateMappingTemplate);
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try
            }
            else
            {
                throw new UserFriendlyException("Data not found !");
            }
            Logger.DebugFormat("UpdateMappingTemplate() - End Update MS_MappingTemplate.");
        }

        private CheckDataUpdateIsExistListDto CheckDataUpdateIsExist(int mappingTemplateID, int projectID, int docID, int docTemplateID, DateTime activeFrom, DateTime? activeTo)
        {
            CheckDataUpdateIsExistListDto returnDataUpdate = new CheckDataUpdateIsExistListDto();

            var GetMappingTemplate = (from A in _msMappingTemplateRepo.GetAll()
                                      join B in _msDocTemplateRepo.GetAll() on A.docTemplateID equals B.Id
                                      join C in _msProjectRepo.GetAll() on A.projectID equals C.Id
                                      join D in _msDocumentPsRepo.GetAll() on A.docID equals D.Id
                                      where A.Id != mappingTemplateID && A.projectID == projectID && A.docID == docID
                                      && A.docTemplateID == docTemplateID
                                      && A.isActive
                                      select new UpdateMappingTemplateInputDto
                                      {
                                          entityID = A.entityID,
                                          isTandaTerima = A.isTandaTerima,
                                          mappingTemplateID = A.Id,
                                          docID = A.docID,
                                          docTemplateID = B.Id,
                                          projectID = A.projectID,
                                          isActive = A.isActive,
                                          activeFrom = A.activeFrom,
                                          activeTo = A.activeTo
                                      })
                                      .OrderByDescending(x => x.activeFrom)
                                      .ThenByDescending(x => x.activeTo)
                                      .ToList();

            if (GetMappingTemplate.Any())
            {
                //Validation
                if (!activeTo.HasValue)
                {
                    if (GetMappingTemplate.Any(x => x.activeTo == null && x.activeFrom >= activeFrom))
                    {
                        throw new UserFriendlyException("Newer period is exist!");
                    }

                    if (GetMappingTemplate.Any(x => x.activeTo != null && (x.activeFrom <= activeFrom && x.activeTo >= activeFrom)))
                    {
                        throw new UserFriendlyException("Newer period is exist!");
                    }
                }
                else
                {
                    if (GetMappingTemplate.Any(x => x.activeTo == null && x.activeFrom <= activeFrom && x.activeFrom >= activeTo))
                    {
                        throw new UserFriendlyException("Newer period is exist!");
                    }

                    if (GetMappingTemplate.Any(x => x.activeTo != null && (x.activeFrom <= activeFrom && x.activeTo >= activeFrom) && (x.activeFrom <= activeTo && x.activeTo >= activeTo)))
                    {
                        throw new UserFriendlyException("Newer period is exist!");
                    }
                }

                //Return Value
                var oldDataMapping = GetMappingTemplate.Any(x => x.activeTo == null && x.activeFrom < activeFrom);
                if (oldDataMapping)
                {
                    var singleData = GetMappingTemplate.Where(x => x.activeTo == null && x.activeFrom < activeFrom).OrderByDescending(x => x.activeFrom).FirstOrDefault();
                    returnDataUpdate.isExist = true;
                    returnDataUpdate.mappingTemplateID = singleData.mappingTemplateID;
                    returnDataUpdate.activeToOldData = activeFrom.AddDays(-1);
                }

                if (!activeTo.HasValue && GetMappingTemplate.Any(x => x.activeFrom > activeFrom))
                {
                    returnDataUpdate.isExist = true;
                    returnDataUpdate.activeTo = GetMappingTemplate.Where(x => x.activeFrom > activeFrom).Select(x => x.activeFrom.AddDays(-1)).OrderBy(x => x).FirstOrDefault();
                }
            }

            return returnDataUpdate;
        }
    }
}
