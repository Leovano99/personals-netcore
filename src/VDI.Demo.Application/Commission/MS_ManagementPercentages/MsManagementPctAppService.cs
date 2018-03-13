using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Commission.MS_ManagementPercentages.Dto;
using VDI.Demo.NewCommDB;
using Abp.UI;
using System.Data;
using Abp.Authorization;
using VDI.Demo.Authorization;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace VDI.Demo.Commission.MS_ManagementPercentages
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterManagementFee)]
    public class MsManagementPctAppService : DemoAppServiceBase, IMsManagementPctAppService
    {
        private readonly IRepository<MS_ManagementPct> _msManagementPctRepo;
        private readonly IRepository<MS_Schema> _msSchemaRepo;
        private readonly IRepository<MS_Developer_Schema> _msDeveloperSchemaRepo;

        public MsManagementPctAppService(
            IRepository<MS_ManagementPct> msManagementPctRepo,
            IRepository<MS_Schema> msSchemaRepo,
            IRepository<MS_Developer_Schema> msDeveloperSchemaRepo
        )
        {
            _msManagementPctRepo = msManagementPctRepo;
            _msSchemaRepo = msSchemaRepo;
            _msDeveloperSchemaRepo = msDeveloperSchemaRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterManagementFee_Create)]
        public void CreateMsManagementPct(List<InputManagementPctDto> input)
        {
            Logger.InfoFormat("CreateMsManagementPct() Started.");

            Logger.DebugFormat("CreateMsManagementPct() - Started Loop Data = {0}", input);
            foreach (var item in input)
            {
                Logger.DebugFormat("CreateMsManagementPct() - Start checking existing SchemaID, developerSchemaID, managementPct . Parameters sent: {0} " +
                    "   isComplete = {1}{0}" +
                    "   schemaID = {2}{0}" +
                    "   developerSchemaID = {3}{0}" +
                    "   managementPct = {4}"
                    , Environment.NewLine, true, item.developerSchemaID, item.managementPct, item.managementPct);
                var check = (from x in _msManagementPctRepo.GetAll()
                             where x.isComplete == true && item.schemaID == x.schemaID && item.developerSchemaID == x.developerSchemaID && item.managementPct == x.managementPct
                             select x).Any();
                Logger.DebugFormat("CreateMsManagementPct() - End checking existing checking existing SchemaID, developerSchemaID, managementPct. Result = {0}", check);

                if (!check)
                {
                    Logger.DebugFormat("CreateMsManagementPct() - Start get developer by developerSchemaID. Parameters sent: {0} " +
                        "   developerSchemaID = {1}{0}"
                        , Environment.NewLine, true, item.developerSchemaID);
                    var getDev = (from x in _msDeveloperSchemaRepo.GetAll()
                                  where x.Id == item.developerSchemaID
                                  select x).FirstOrDefault();
                    Logger.DebugFormat("CreateMsManagementPct() - End get developer by developerSchemaID. Result = {0}", getDev);

                    var create = new MS_ManagementPct
                    {
                        schemaID = item.schemaID,
                        developerSchemaID = item.developerSchemaID,
                        managementPct = item.managementPct,
                        bankCode = getDev.bankCode,
                        bankAccountName = getDev.bankAccountName,
                        bankBranchName = getDev.bankBranchName,
                        isActive = item.isActive,
                        isComplete = true
                    };

                    try
                    {
                        Logger.DebugFormat("CreateMsManagementPct() - Start insert managementPct. Parameters sent: {0} " +
                        "   schemaID = {1}{0}" +
                        "   developerSchemaID = {2}{0}" +
                        "   managementPct = {3}{0}" +
                        "   bankCode = {4}{0}" +
                        "   bankAccountName = {5}{0}" +
                        "   bankBranchName = {6}{0}" +
                        "   isActive = {7}{0}" +
                        "   isComplete = {8}"
                        , Environment.NewLine, item.schemaID, item.developerSchemaID, item.managementPct, getDev.bankCode,
                            getDev.bankAccountName, getDev.bankBranchName, item.isActive, true);
                        _msManagementPctRepo.Insert(create);
                        CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                        Logger.DebugFormat("CreateMsManagementPct() - End insert managementPct.");
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
                    Logger.DebugFormat("CreateMsDeveloperSchemas() - ERROR. Result = {0}", "Already Exist!");
                    throw new UserFriendlyException("Already Exist!");
                }
            }
            Logger.DebugFormat("CreateMsManagementPct() - End Loop Data.");
            Logger.InfoFormat("CreateMsManagementPct() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterManagementFee_Delete)]
        public void DeleteMsManagementPct(int Id)
        {
            Logger.InfoFormat("DeleteMsManagementPct() Started.");

            Logger.DebugFormat("DeleteMsManagementPct() - Start get data ManagementPct for update. Parameters sent: {0} " +
                    "   managementPctID = {1}{0}"
                    , Environment.NewLine, Id);

            var getManagementPct = (from x in _msManagementPctRepo.GetAll()
                                    where Id == x.Id
                                    select x).FirstOrDefault();

            var update = getManagementPct.MapTo<MS_ManagementPct>();
            Logger.DebugFormat("DeleteMsManagementPct() - End get data ManagementPct  for update. Result = {0}", update);
            update.isComplete = false;

            try
            {
                Logger.DebugFormat("DeleteMsManagementPct() - Start Update ManagementPct. Parameters sent: {0} " +
                "   managementPctID = {1}{0}" +
                "   isComplete = {2}{0}"
                , Environment.NewLine, Id, false);
                _msManagementPctRepo.Update(update);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("DeleteMsManagementPct() - End Update ManagementPct.");
            }
            catch (DataException ex)
            {
                Logger.DebugFormat("DeleteMsManagementPct() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("DeleteMsManagementPct() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }

            Logger.InfoFormat("DeleteMsManagementPct() - Finished.");
        }

        public ListResultDto<GetAllManagementPctListDto> GetMsManagementPctBySchemaId(int schemaID)
        {
            var listResult = (from x in _msManagementPctRepo.GetAll()
                              join dev in _msDeveloperSchemaRepo.GetAll() on x.developerSchemaID equals dev.Id
                              where x.schemaID == schemaID && x.isComplete == true
                              orderby x.managementPct descending
                              select new GetAllManagementPctListDto
                              {
                                  managementPctID = x.Id,
                                  schemaID = x.schemaID,
                                  managementPct = x.managementPct,
                                  developerSchemaID = x.developerSchemaID,
                                  developerName = dev.devName,
                                  isActive = x.isActive
                              }).ToList();

            return new ListResultDto<GetAllManagementPctListDto>(listResult);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterManagementFee_Edit)]
        public void UpdateMsManagementPct(InputManagementPctDto input)
        {
            var check = (from x in _msManagementPctRepo.GetAll()
                         where input.managementPctID != x.Id && x.isComplete == true && input.schemaID == x.schemaID && input.developerSchemaID == x.developerSchemaID
                         select x).Any();
            if (!check)
            {
                Logger.DebugFormat("UpdateMsManagementPct() - Start get data ManagementPct for update. Parameters sent: {0} " +
                        "   managementPctID = {1}{0}"
                        , Environment.NewLine, input.managementPctID);
                var getManagementPct = (from x in _msManagementPctRepo.GetAll()
                                        where input.managementPctID == x.Id
                                        select x).FirstOrDefault();
                Logger.DebugFormat("UpdateMsManagementPct() - End get data ManagementPct  for update. Result = {0}", getManagementPct);

                Logger.DebugFormat("UpdateMsManagementPct() - Start get data DevSchema for update. Parameters sent: {0} " +
                        "   developerSchemaID = {1}{0}"
                        , Environment.NewLine, input.developerSchemaID);
                var getDev = (from x in _msDeveloperSchemaRepo.GetAll()
                              where input.developerSchemaID == x.Id
                              select x).FirstOrDefault();
                Logger.DebugFormat("UpdateMsManagementPct() - End get data DevSchema  for update. Result = {0}", getDev);

                var update = getManagementPct.MapTo<MS_ManagementPct>();

                if (input.developerSchemaID != getManagementPct.developerSchemaID)
                {
                    update.developerSchemaID = input.developerSchemaID;
                    update.bankCode = getDev.bankCode;
                    update.bankAccountName = getDev.bankAccountName;
                    update.bankBranchName = getDev.bankBranchName;
                }

                update.schemaID = input.schemaID;
                update.managementPct = input.managementPct;
                update.isActive = input.isActive;

                try
                {
                    Logger.DebugFormat("UpdateMsDeveloperSchemas() - Start update managementPct. Parameters sent: {0} " +
                "   developerSchemaID = {1}{0}" +
                "   bankCode = {2}{0}" +
                "   bankAccountName = {3}{0}" +
                "   bankBranchName = {4}{0}" +
                "   schemaID = {5}{0}" +
                "   managementPct = {6}{0}" +
                "   isActive = {7}{0}"
                , Environment.NewLine, input.developerSchemaID, getDev.bankCode, getDev.bankAccountName, getDev.bankBranchName,
                input.schemaID, input.managementPct, input.isActive);
                    _msManagementPctRepo.Update(update);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("UpdateMsManagementPct() - End update managementPct.");
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("UpdateMsManagementPct() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("UpdateMsManagementPct() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("UpdateMsManagementPct() - ERROR. Result = {0}", "Already Exist!");
                throw new UserFriendlyException("Already Exist!");
            }

            Logger.InfoFormat("UpdateMsManagementPct() - Finished.");
        }
    }
}
