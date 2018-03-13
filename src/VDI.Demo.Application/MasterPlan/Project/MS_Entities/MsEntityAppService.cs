using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Project.MS_Entities.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using System.Data;
using Abp.UI;
using Abp.AutoMapper;
using VDI.Demo.EntityFrameworkCore;

namespace VDI.Demo.MasterPlan.Project.MS_Entities
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterEntity)]
    public class MsEntityAppService : DemoAppServiceBase, IMsEntityAppService
    {
        private readonly IRepository<MS_Entity> _msEntityRepo;
        private readonly IRepository<MS_Account> _msAccountRepo;
        private readonly IRepository<MS_Bank> _msBankRepo;
        private readonly IRepository<MS_BankBranch> _msBankBranchRepo;
        private readonly IRepository<MS_Company> _msCompanyRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;
        private readonly IRepository<MS_Zoning> _msZoningRepo;
        private readonly PropertySystemDbContext _contextProp;

        public MsEntityAppService(
            IRepository<MS_Entity> msEntityRepo,
            IRepository<MS_Account> msAccountRepo,
            IRepository<MS_Bank> msBankRepo,
            IRepository<MS_BankBranch> msBankBranch,
            IRepository<MS_Company> msCompany,
            IRepository<MS_Project> msProject,
            IRepository<MS_Cluster> msCluster,
            IRepository<MS_Unit> msUnit,
            IRepository<MS_UnitCode> msUnitCode,
            IRepository<MS_Zoning> msZoning,
            PropertySystemDbContext contextProp
        )
        {
            _msEntityRepo = msEntityRepo;
            _msAccountRepo = msAccountRepo;
            _msBankRepo = msBankRepo;
            _msBankBranchRepo = msBankBranch;
            _msCompanyRepo = msCompany;
            _msProjectRepo = msProject;
            _msClusterRepo = msCluster;
            _msUnitRepo = msUnit;
            _msUnitCodeRepo = msUnitCode;
            _msZoningRepo = msZoning;
            _contextProp = contextProp;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterEntity_Create)]
        public void CreateMsEntity(GetAllMsEntityListDto input)
        {
            Logger.InfoFormat("CreateMsEntity() Started.");

            Logger.DebugFormat("CreateMsEntity() - Start checking existing entityCode, entityName. Parameters sent: {0} " +
                "entityCode = {1}{0}" +
                "entityName = {2}{0}"
                , Environment.NewLine, input.entityCode, input.entityName);
            var checkEntity = (from A in _msEntityRepo.GetAll()
                               where A.entityCode == input.entityCode || A.entityName == input.entityName
                               select A).Count();
            Logger.DebugFormat("CreateMsEntity() - End checking existing entityCode, entityName. Result = {0}", checkEntity);

            if (checkEntity == 0)
            {
                var createMsEntity = new MS_Entity
                {
                    entityName = input.entityName,
                    entityCode = input.entityCode
                };

                try
                {
                    Logger.DebugFormat("CreateMsEntity() - Start Insert Entity. Parameters sent: {0} " +
                        "entityName = {1}{0}" +
                        "entityCode = {2}{0}"
                        , Environment.NewLine, input.entityName, input.entityCode);
                    _msEntityRepo.Insert(createMsEntity);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("CreateMsEntity() - End Insert Entity. Result = {0}", checkEntity);
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("CreateMsEntity() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("CreateMsEntity() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("CreateMsEntity() - ERROR. Result = {0}", "Entity Code or Entity Name Already Exist !");
                throw new UserFriendlyException("Entity Code or Entity Name Already Exist !");
            }
            Logger.InfoFormat("CreateMsEntity() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterEntity_Delete)]
        public void DeleteMsEntity(int Id)
        {
            Logger.InfoFormat("DeleteMsEntity() Started.");

            Logger.DebugFormat("DeleteMsEntity() - Start checking existing entityID In msAccount, msBank, {0}" +
                "msBankBranch, msCompany, msProject, msCluster, msUnit, msUnitCode, msZoning . Parameters sent: {0} " +
                "entityID = {1}{0}"
                , Environment.NewLine, Id);
            bool checkAccount = _msAccountRepo.GetAll().Where(x => x.entityID == Id).Any();
            bool checkBank = _msBankRepo.GetAll().Where(x => x.entityID == Id).Any();
            bool checkBankBranch = _msBankBranchRepo.GetAll().Where(x => x.entityID == Id).Any();
            bool checkCompany = _msCompanyRepo.GetAll().Where(x => x.entityID == Id).Any();
            bool checkProject = _msProjectRepo.GetAll().Where(x => x.entityID == Id).Any();
            bool checkCluster = _msClusterRepo.GetAll().Where(x => x.entityID == Id).Any();
            bool checkUnit = _msUnitRepo.GetAll().Where(x => x.entityID == Id).Any();
            bool checkUnitCode = _msUnitCodeRepo.GetAll().Where(x => x.entityID == Id).Any();
            bool checkZoning = _msZoningRepo.GetAll().Where(x => x.entityID == Id).Any();
            Logger.DebugFormat("DeleteMsEntity() - End checking existing entityID In msAccount, msBank, {0}" +
                "msBankBranch, msCompany, msProject, msCluster, msUnit, msUnitCode, msZoning.", Environment.NewLine);

            if (checkAccount || checkBank || checkBankBranch || checkCompany
               || checkProject || checkCluster || checkUnit || checkUnitCode || checkZoning)
            {
                Logger.DebugFormat("DeleteMsEntity() - ERROR. Result = {0}", "This Entity is used by another master!");
                throw new UserFriendlyException("This Entity is used by another master!");
            }
            else
            {
                try
                {
                    Logger.DebugFormat("DeleteMsEntity() - Start delete entity. Parameters sent: {0} " +
                        "entityID = {1}{0}", Environment.NewLine, Id);
                    _msEntityRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("DeleteMsEntity() - End delete entity");
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("DeleteMsEntity() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("DeleteMsEntity() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            
            Logger.InfoFormat("DeleteMsEntity() - Finished.");
        }

        public ListResultDto<GetAllMsEntityListDto> GetAllMsEntity()
        {
            var getAllEntity = (from A in _msEntityRepo.GetAll()
                                orderby A.Id descending
                                select new GetAllMsEntityListDto
                                {
                                    Id = A.Id,
                                    entityName = A.entityName,
                                    entityCode = A.entityCode
                                }).ToList();
            return new ListResultDto<GetAllMsEntityListDto>(getAllEntity);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProject)]
        public ListResultDto<GetAllMsEntityListDto> GetMsEntityDropdown()
        {
            var listResult = (from x in _msEntityRepo.GetAll()
                              select new GetAllMsEntityListDto
                              {
                                  Id = x.Id,
                                  entityName = x.entityName,
                                  entityCode = x.entityCode
                              }).ToList();

            return new ListResultDto<GetAllMsEntityListDto>(listResult);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterEntity_Edit)]
        public void UpdateMsEntity(GetAllMsEntityListDto input)
        {
            Logger.InfoFormat("UpdateMsEntity() - Started.");

            Logger.DebugFormat("UpdateMsEntity() - Start checking existing entityCode, entityName. Parameters sent: {0} " +
                "enityID = {1}{0}" +
                "entityCode = {2}{0}" +
                "entityName = {3}{0}"
                , Environment.NewLine, input.Id, input.entityCode, input.entityName);
            var checkEntity = (from entity in _msEntityRepo.GetAll()
                               where entity.Id != input.Id && (entity.entityCode == input.entityCode || entity.entityName == input.entityName)
                               select entity).Any();
            Logger.DebugFormat("UpdateMsEntity() - End checking existing entityCode, entityName. Result = {0}", checkEntity);

            var checkMsAccount = (from account in _contextProp.MS_Account
                                  where account.entityID == input.Id
                                  select account).Any();
            var checkMsArea = (from area in _contextProp.MS_Area
                               where area.entityID == input.Id
                               select area).Any();
            var checkMsBank = (from bank in _contextProp.MS_Bank
                               where bank.entityID == input.Id
                               select bank).Any();
            var checkMsBankBranch = (from bankBranch in _contextProp.MS_BankBranch
                                     where bankBranch.entityID == input.Id
                                     select bankBranch).Any();
            var checkMsCategory = (from category in _contextProp.MS_Category
                                   where category.entityID == input.Id
                                   select category).Any();
            var checkMsCluster = (from cluster in _contextProp.MS_Cluster
                                  where cluster.entityID == input.Id
                                  select cluster).Any();
            var checkMsCompany = (from company in _contextProp.MS_Company
                                  where company.entityID == input.Id
                                  select company).Any();
            var checkMsDetail = (from detail in _contextProp.MS_Detail
                                 where detail.entityID == input.Id
                                 select detail).Any();
            var checkMsFacade = (from facade in _contextProp.MS_Facade
                                 where facade.entityID == input.Id
                                 select facade).Any();
            var checkMsGroupTerm = (from groupTerm in _contextProp.MS_GroupTerm
                                    where groupTerm.entityID == input.Id
                                    select groupTerm).Any();
            var checkMsProduct = (from product in _contextProp.MS_Product
                                  where product.entityID == input.Id
                                  select product).Any();
            var checkMsProject = (from project in _contextProp.MS_Project
                                  where project.entityID == input.Id
                                  select project).Any();
            var checkMsTerm = (from term in _contextProp.MS_Term
                               where term.entityID == input.Id
                               select term).Any();
            var checkMsTermAddDisc = (from termAdd in _contextProp.MS_TermAddDisc
                                      where termAdd.entityID == input.Id
                                      select termAdd).Any();
            var checkMsTermDp = (from termDp in _contextProp.MS_TermDP
                                 where termDp.entityID == input.Id
                                 select termDp).Any();
            var checkMsTermMain = (from termMain in _contextProp.MS_TermMain
                                   where termMain.entityID == input.Id
                                   select termMain).Any();
            var checkMsTermPmt = (from termPmt in _contextProp.MS_TermPmt
                                  where termPmt.entityID == input.Id
                                  select termPmt).Any();
            var checkMsUnit = (from unit in _contextProp.MS_Unit
                               where unit.entityID == input.Id
                               select unit).Any();
            var checkMsUnitCode = (from unitCode in _contextProp.MS_UnitCode
                                   where unitCode.entityID == input.Id
                                   select unitCode).Any();
            var checkMsUnitItem = (from unitItem in _contextProp.MS_UnitItem
                                   where unitItem.entityID == input.Id
                                   select unitItem).Any();
            var checkMsUnitItemPrice = (from unitItemPrice in _contextProp.MS_UnitItemPrice
                                        where unitItemPrice.entityID == input.Id
                                        select unitItemPrice).Any();
            var checkMsZoning = (from zoning in _msZoningRepo.GetAll()
                                 where zoning.entityID == input.Id
                                 select zoning).Any();

            if (!checkEntity)
            {
                Logger.DebugFormat("UpdateMsEntity() - Start get entity for update. Parameters sent: {0} " +
                    "enityID = {1}{0}", Environment.NewLine, input.Id);
                var getMsEntity = (from A in _msEntityRepo.GetAll()
                                   where input.Id == A.Id
                                   select A).FirstOrDefault();
                Logger.DebugFormat("UpdateMsEntity() - End get entity for update. Result = {0}", getMsEntity);

                var updateMsentity = getMsEntity.MapTo<MS_Entity>();
                
                if (!checkMsAccount && !checkEntity && !checkMsArea && !checkMsBank && !checkMsBankBranch && !checkMsCategory
                && !checkMsCluster && !checkMsCompany && !checkMsDetail && !checkMsFacade && !checkMsGroupTerm
                && !checkMsProduct && !checkMsProject && !checkMsTerm && !checkMsTermAddDisc && !checkMsTermDp
                && !checkMsTermMain && !checkMsTermPmt && !checkMsUnit && !checkMsUnitCode && !checkMsUnitItem
                && !checkMsUnitItemPrice && !checkMsZoning)
                {
                    updateMsentity.entityName = input.entityName;
                    updateMsentity.entityCode = input.entityCode;
                }
                else
                {
                    Logger.DebugFormat("UpdateMsEntity() - This Entity is used in another table. entityId = {0}", input.Id);
                    throw new UserFriendlyException("This Entity is used !");
                }

                try
                {
                    Logger.DebugFormat("UpdateMsEntity() - Start update msEntity. Parameters sent: {0} " +
                        "entityName = {1}{0}" +
                        "entityCode = {2}{0}", Environment.NewLine, input.entityName, input.entityCode);
                    _msEntityRepo.Update(updateMsentity);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("UpdateMsEntity() - End update msEntity.");
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("UpdateMsEntity() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("UpdateMsEntity() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("UpdateMsEntity() - ERROR Exception. Result = {0}", "Entity Code or Entity Name Already Exist !");
                throw new UserFriendlyException("Entity Code or Entity Name Already Exist !");
            }

            Logger.InfoFormat("UpdateMsEntity() - Finished.");
        }
    }
}
