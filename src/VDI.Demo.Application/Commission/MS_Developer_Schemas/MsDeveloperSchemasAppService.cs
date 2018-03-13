using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using VDI.Demo.Authorization;
using VDI.Demo.Commission.MS_Developer_Schemas.Dto;
using VDI.Demo.NewCommDB;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.Commission.MS_Developer_Schemas
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDeveloperSchema)]
    public class MsDeveloperSchemasAppService : DemoAppServiceBase, IMsDeveloperSchemasAppService
    {
        private readonly IRepository<MS_Developer_Schema> _msDeveloperSchemasRepo;
        private readonly IRepository<MS_Schema> _msSchemaRepo;
        private readonly IRepository<MS_Property> _msPropertyRepo;
        private readonly IRepository<MS_Company> _msCompanyRepo;

        public MsDeveloperSchemasAppService(
            IRepository<MS_Developer_Schema> msDeveloperSchemasRepo,
            IRepository<MS_Schema> msSchemaRepo,
            IRepository<MS_Property> msPropertyRepo,
            IRepository<MS_Company> msCompanyRepo
            )
        {
            _msDeveloperSchemasRepo = msDeveloperSchemasRepo;
            _msSchemaRepo = msSchemaRepo;
            _msPropertyRepo = msPropertyRepo;
            _msCompanyRepo = msCompanyRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDeveloperSchema_Delete)]
        public void DeleteMsDeveloperSchemas(int Id)
        {
            Logger.InfoFormat("DeleteMsDeveloperSchemas() Started.");

            Logger.DebugFormat("DeleteMsBobotComm() - Start get data Developer Schemas for update. Parameters sent: {0} " +
                    "   devSchemaID = {1}{0}"
                    , Environment.NewLine, Id);
            var getDeveloperSchema = (from devSchema in _msDeveloperSchemasRepo.GetAll()
                                      where Id == devSchema.Id
                                      select devSchema).FirstOrDefault();

            var updateDevSchema = getDeveloperSchema.MapTo<MS_Developer_Schema>();

            updateDevSchema.isComplete = false;
            Logger.DebugFormat("DeleteMsDeveloperSchemas() - End get data Developer Schemas  for update. Result = {0}", updateDevSchema);

            try
            {
                Logger.DebugFormat("DeleteMsDeveloperSchemas() - Start Update Developer Schemas. Parameters sent: {0} " +
                "   devSchemaID = {1}{0}" +
                "   isComplete = {2}{0}"
                , Environment.NewLine, Id, false);
                _msDeveloperSchemasRepo.Update(updateDevSchema);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("DeleteMsDeveloperSchemas() - End Update Developer Schemas.");
            }
            catch (DataException ex)
            {
                Logger.DebugFormat("DeleteMsDeveloperSchemas() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("DeleteMsDeveloperSchemas() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.InfoFormat("DeleteMsDeveloperSchemas() - Finished.");
        }

        public ListResultDto<GetDeveloperSchemasListDto> GetMsDeveloperSchemas()
        {
            var listResult = (from DeveloperSchemas in _msDeveloperSchemasRepo.GetAll()
                              join schema in _msSchemaRepo.GetAll() on DeveloperSchemas.schemaID equals schema.Id
                              join property in _msPropertyRepo.GetAll() on DeveloperSchemas.propertyID equals property.Id
                              where DeveloperSchemas.isComplete == true
                              orderby DeveloperSchemas.Id descending
                              select new GetDeveloperSchemasListDto
                              {
                                  Id = DeveloperSchemas.Id,
                                  propCode = property.propCode,
                                  devCode = DeveloperSchemas.devCode,
                                  devName = DeveloperSchemas.devName,
                                  schemaID = DeveloperSchemas.schemaID,
                                  propertyID = property.Id,
                                  schemaName = schema.scmName,
                                  propName = property.propName,
                                  isActive = DeveloperSchemas.isActive
                              }).ToList();

            return new ListResultDto<GetDeveloperSchemasListDto>(listResult);
        }

        public ListResultDto<GetDeveloperSchemasListDto> GetMsDeveloperSchemasBySchema(int SchemaID)
        {
            var listResult = (from x in _msDeveloperSchemasRepo.GetAll()
                              where x.schemaID == SchemaID
                              join schema in _msSchemaRepo.GetAll() on x.schemaID equals schema.Id
                              join property in _msPropertyRepo.GetAll() on x.propertyID equals property.Id
                              where x.isComplete == true
                              orderby x.schemaID descending
                              select new GetDeveloperSchemasListDto
                              {
                                  Id = x.Id,
                                  propCode = property.propCode,
                                  devCode = x.devCode,
                                  devName = x.devName,
                                  schemaID = x.schemaID,
                                  propertyID = x.propertyID,
                                  schemaName = schema.scmName,
                                  propName = property.propName,
                                  bankCode = x.bankCode,
                                  bankAccountName = x.bankAccountName,
                                  bankBranchName = x.bankBranchName,
                                  isActive = x.isActive
                              }).ToList();

            return new ListResultDto<GetDeveloperSchemasListDto>(listResult);
        }

        public GetDeveloperSchemasListDto GetDetailMsDeveloperSchemas(int Id)
        {
            var listResult = (from x in _msDeveloperSchemasRepo.GetAll()
                              join schema in _msSchemaRepo.GetAll() on x.schemaID equals schema.Id
                              join property in _msPropertyRepo.GetAll() on x.propertyID equals property.Id
                              orderby x.Id descending
                              where x.Id == Id
                              select new GetDeveloperSchemasListDto
                              {
                                  Id = x.Id,
                                  propCode = property.propCode,
                                  devName = x.devName,
                                  devCode = x.devCode,
                                  schemaID = x.schemaID,
                                  propertyID = x.propertyID,
                                  schemaName = schema.scmName,
                                  propName = property.propName,
                                  bankCode = x.bankCode,
                                  bankAccountName = x.bankAccountName,
                                  bankBranchName = x.bankBranchName,
                                  isActive = x.isActive
                              }).FirstOrDefault();
            return listResult;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDeveloperSchema_Create)]
        public void CreateMsDeveloperSchemas(CreateMsDeveloperSchemasInputDto input)
        {
            Logger.InfoFormat("CreateMsDeveloperSchemas() Started.");

            Logger.DebugFormat("CreateMsDeveloperSchemas() - Start Get property Id.  Parameters sent: {0} " +
                    "   isComplete = {1}{0}" +
                    "   propCode = {2}{0}" +
                    "   schemaID = {3}{0}"
                    , Environment.NewLine, true, input.propCode, input.schemaID);
            var propertyID = (from x in _msPropertyRepo.GetAll()
                              where x.isComplete == true && x.propCode == input.propCode && x.schemaID == input.schemaID
                              select x.Id).FirstOrDefault();
            Logger.DebugFormat("CreateMsDeveloperSchemas() - End Get property Id. Result = {0}", propertyID);

            Logger.DebugFormat("CreateMsDeveloperSchemas() - Start checking existing Property. Parameters sent: {0} " +
                "   isComplete = {1}{0}" +
                "   propCode = {2}{0}" +
                "   schemaID = {3}"
                , Environment.NewLine, true, input.propCode, input.schemaID);
            var checkProp = (from x in _msPropertyRepo.GetAll()
                             where x.isComplete == true && x.propCode == input.propCode && x.schemaID == input.schemaID
                             select x).Any();
            Logger.DebugFormat("CreateMsDeveloperSchemas() - End checking existing Property. Result = {0}", checkProp);


            if (!checkProp)
            {
                var dataProperty = new MS_Property
                {
                    schemaID = input.schemaID,
                    propCode = input.propCode,
                    propName = input.propName,
                    propDesc = "desc",
                    isComplete = true,
                    entityID = 1
                };

                try
                {
                    Logger.DebugFormat("CreateMsDeveloperSchemas() - Start insert property. Parameters sent: {0} " +
                    "   schemaID = {1}{0}" +
                    "   propCode = {2}{0}" +
                    "   propName = {3}{0}" +
                    "   propDesc = {4}{0}" +
                    "   isComplete = {5}{0}" +
                    "   entityID = {6}"
                    , Environment.NewLine, input.schemaID, input.propCode, input.propName, "desc", true, 1);
                    propertyID = _msPropertyRepo.InsertAndGetId(dataProperty);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("CreateMsDeveloperSchemas() - End insert property. Result = {0}", propertyID);
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("CreateMsDeveloperSchemas() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("CreateMsDeveloperSchemas() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }

            Logger.DebugFormat("CreateMsDeveloperSchemas() - Started Loop Data = {0}", input.setDataDev);
            foreach (var item in input.setDataDev)
            {
                Logger.DebugFormat("CreateMsDeveloperSchemas() - Start checking existing Developer Schema. Parameters sent: {0} " +
                    "   isComplete = {1}{0}" +
                    "   propertyID = {2}{0}" +
                    "   schemaID = {3}{0}" +
                    "   devCode = {4}"
                    , Environment.NewLine, true, propertyID, input.schemaID, item.devCode);
                var checkDev = (from x in _msDeveloperSchemasRepo.GetAll()
                                where x.isComplete == true && x.propertyID == propertyID && x.schemaID == input.schemaID && x.devCode == item.devCode
                                select x).Any();
                Logger.DebugFormat("CreateMsDeveloperSchemas() - End checking existing Developer Schema. Result = {0}", checkDev);

                if (!checkDev)
                {
                    var data = new MS_Developer_Schema
                    {
                        devCode = item.devCode,
                        devName = item.devName,
                        schemaID = input.schemaID,
                        propertyID = propertyID,
                        bankCode = item.bankCode,
                        isActive = item.isActive,
                        bankAccountName = item.bankAccountName,
                        bankBranchName = item.bankBranchName,
                        isComplete = true
                    };
                    try
                    {
                        Logger.DebugFormat("CreateMsDeveloperSchemas() - Start insert developerSchema. Parameters sent: {0} " +
                        "   devCode = {1}{0}" +
                        "   devName = {2}{0}" +
                        "   schemaID = {3}{0}" +
                        "   propertyID = {4}{0}" +
                        "   bankCode = {5}{0}" +
                        "   isActive = {6}{0}" +
                        "   bankAccountName = {7}{0}" +
                        "   bankBranchName = {8}{0}" +
                        "   isComplete = {9}"
                        , Environment.NewLine, item.devCode, item.devName, input.schemaID, propertyID,
                            item.bankCode, item.isActive, item.bankAccountName, item.bankBranchName, true);
                        _msDeveloperSchemasRepo.Insert(data);
                        CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                        Logger.DebugFormat("CreateMsDeveloperSchemas() - End insert developerSchema.");
                    }
                    catch (DataException ex)
                    {
                        Logger.DebugFormat("CreateMsDeveloperSchemas() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.DebugFormat("CreateMsDeveloperSchemas() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
                else
                {
                    Logger.DebugFormat("CreateMsDeveloperSchemas() - ERROR. Result = {0}", "Already Exist! You could edit the value.");
                    throw new UserFriendlyException("Already Exist! You could edit the value.");
                }

            }

            Logger.InfoFormat("CreateMsDeveloperSchemas() - End Loop Data.");
            Logger.InfoFormat("CreateMsDeveloperSchemas() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDeveloperSchema_Edit)]
        public void UpdateMsDeveloperSchemas(UpdateMsDeveloperSchemasInputDto input)
        {
            Logger.InfoFormat("UpdateMsDeveloperSchemas() Started.");

            Logger.DebugFormat("UpdateMsDeveloperSchemas() - Start checking existing Code. Parameters sent: {0} " +
                "   devCode = {1}{0}" +
                "   Id = {2}{0}" +
                "   isComplete = {3}{0}" +
                "   schemaID = {4}{0}" +
                "   propertyID = {5}{0}"
                , Environment.NewLine, input.devCode, input.Id, true, input.schemaID, input.propertyID);

            var checkCode = (from x in _msDeveloperSchemasRepo.GetAll()
                             where x.devCode == input.devCode && x.Id != input.Id && x.isComplete == true && x.schemaID == input.schemaID && x.propertyID == input.propertyID
                             select x).Any();
            Logger.DebugFormat("UpdateMsDeveloperSchemas() - End checking existing Code. Result = {0}", checkCode);


            Logger.DebugFormat("UpdateMsDeveloperSchemas() - Start checking code in company. Parameters sent: {0} " +
                "   coCode = {1}{0}"
                , Environment.NewLine, input.devCode);
            var checkCodeInCompany = (from x in _msCompanyRepo.GetAll()
                                      where x.coCode == input.devCode
                                      select x).Any();
            Logger.DebugFormat("UpdateMsDeveloperSchemas() - End checking code in company. Result = {0}", checkCodeInCompany);

            if (!checkCode || checkCodeInCompany)
            {
                Logger.DebugFormat("UpdateMsDeveloperSchemas() - Start get developerSchema for update. Parameters sent: {0} " +
                    "   Id = {1}{0}", Environment.NewLine, input.Id);
                var getDeveloperSchemas = (from x in _msDeveloperSchemasRepo.GetAll()
                                           where x.Id == input.Id
                                           select x).FirstOrDefault();

                var updateDeveloperSchemas = getDeveloperSchemas.MapTo<MS_Developer_Schema>();
                updateDeveloperSchemas.devCode = input.devCode;
                updateDeveloperSchemas.devName = input.devName;
                updateDeveloperSchemas.bankCode = input.bankCode;
                updateDeveloperSchemas.bankAccountName = input.bankAccountName;
                updateDeveloperSchemas.bankBranchName = input.bankBranchName;
                updateDeveloperSchemas.isActive = input.isActive;
                Logger.DebugFormat("UpdateMsDeveloperSchemas() - End get developerSchema for update. Result = {0}", getDeveloperSchemas);

                try
                {
                    Logger.DebugFormat("UpdateMsDeveloperSchemas() - Start update developerSchema. Parameters sent: {0} " +
                "   devCode = {1}{0}" +
                "   devName = {2}{0}" +
                "   bankCode = {3}{0}" +
                "   bankAccountName = {4}{0}" +
                "   bankBranchName = {5}{0}" +
                "   isActive = {6}{0}"
                , Environment.NewLine, input.devCode, input.devName, input.bankCode, input.bankAccountName, input.bankBranchName, input.isActive);

                    _msDeveloperSchemasRepo.Update(updateDeveloperSchemas);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("UpdateMsDeveloperSchemas() - End update developerSchema.");
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("UpdateMsDeveloperSchemas() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("UpdateMsDeveloperSchemas() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }

            }
            else
            {
                Logger.DebugFormat("UpdateMsDeveloperSchemas() - ERROR. Result = {0}", "The Code Already Exist or Not Exist in Company!");
                throw new UserFriendlyException("The Code Already Exist or Not Exist in Company!");
            }

            Logger.InfoFormat("UpdateMsDeveloperSchemas() - Finished.");
        }

        public ListResultDto<GetPropCodeListDto> GetPropCodeBySchemaID(int schemaID)
        {
            var listResult = (from property in _msPropertyRepo.GetAll()
                              where property.schemaID == schemaID && property.isComplete == true
                              orderby property.Id descending
                              select new GetPropCodeListDto
                              {
                                  Id = property.Id,
                                  propCode = property.propCode,
                                  propName = property.propName
                              }).ToList();

            return new ListResultDto<GetPropCodeListDto>(listResult);
        }

        public string GetPropNameByPropCode(string propCode)
        {
            var propName = (from property in _msPropertyRepo.GetAll()
                            where property.propCode == propCode
                            select property.propName).FirstOrDefault();
            return propName;
        }

        public ListResultDto<GetDeveloperSchemasListDto> GetAllMsDeveloperSchemaPaging()
        {
            var listResult = (from DeveloperSchemas in _msDeveloperSchemasRepo.GetAll()
                              join schema in _msSchemaRepo.GetAll() on DeveloperSchemas.schemaID equals schema.Id
                              join property in _msPropertyRepo.GetAll() on DeveloperSchemas.propertyID equals property.Id
                              where DeveloperSchemas.isComplete == true
                              orderby DeveloperSchemas.CreationTime descending
                              select new GetDeveloperSchemasListDto
                              {
                                  Id = DeveloperSchemas.Id,
                                  propCode = property.propCode,
                                  devCode = DeveloperSchemas.devCode,
                                  devName = DeveloperSchemas.devName,
                                  schemaID = DeveloperSchemas.schemaID,
                                  propertyID = property.Id,
                                  schemaName = schema.scmName,
                                  propName = property.propName,
                                  isActive = DeveloperSchemas.isActive
                              }).ToList();
            return new ListResultDto<GetDeveloperSchemasListDto>(listResult);
        }

        public ListResultDto<GetDropDownDeveloperSchemasListDto> GetDropDownMsDeveloperSchemasBySchema(int SchemaID)
        {
            var listResult = (from x in _msDeveloperSchemasRepo.GetAll()
                              where x.schemaID == SchemaID && x.isComplete == true && x.isActive == true
                              orderby x.devCode descending
                              select new GetDropDownDeveloperSchemasListDto
                              {
                                  Id = x.Id,
                                  devCode = x.devCode,
                                  devName = x.devName
                              }).ToList();

            return new ListResultDto<GetDropDownDeveloperSchemasListDto>(listResult);
        }

        public List<GetDropDownDeveloperSchemasListDto> GetDataDeveloperSchemaByProperty(int propertyID)
        {
            var getDev = (from dev in _msDeveloperSchemasRepo.GetAll()
                          where dev.propertyID == propertyID && dev.isActive == true && dev.isComplete == true
                          select new GetDropDownDeveloperSchemasListDto
                          {
                              Id = dev.Id,
                              devCode = dev.devCode,
                              devName = dev.devName
                          }).ToList();

            return getDev;
        }
    }
}
