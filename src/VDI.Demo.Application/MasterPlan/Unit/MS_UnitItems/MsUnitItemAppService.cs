using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_UnitItems.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.MS_UnitItems
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MappingItem)]
    public class MsUnitItemAppService : DemoAppServiceBase
    {
        private readonly IRepository<LK_Item> _lkItemRepo;
        private readonly IRepository<MS_UnitItem> _msUnitItemRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<MS_Company> _msCompanyRepo;

        public MsUnitItemAppService(
            IRepository<LK_Item> lkItemRepo,
            IRepository<MS_UnitItem> msUnitItemRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<MS_Cluster> msClusterRepo,
            IRepository<MS_Company> msCompanyRepo)
        {
            _lkItemRepo = lkItemRepo;
            _msUnitItemRepo = msUnitItemRepo;
            _msUnitRepo = msUnitRepo;
            _msClusterRepo = msClusterRepo;
            _msCompanyRepo = msCompanyRepo;
        }

        public ListResultDto<GetUnitItemListDto> GetDataMsUnitItem(GetUnitItemInputDto input)
        {
            var getData = (from A in _msUnitItemRepo.GetAll()
                           join B in _msUnitRepo.GetAll() on A.unitID equals B.Id
                           join C in _msClusterRepo.GetAll() on B.clusterID equals C.Id
                           join D in _msCompanyRepo.GetAll() on A.coCode equals D.coCode into L1
                           from D in L1.DefaultIfEmpty()
                           join E in _lkItemRepo.GetAll() on A.itemID equals E.Id
                           where input.itemID.Contains(A.itemID) && input.unitID.Contains(A.unitID)
                           select new GetUnitItemListDto
                           {
                               coID = D == null ? 0 : D.Id,
                               coCode = D == null ? null : D.coCode,
                               coName = D == null ? null : D.coName,
                               item = E.itemName,
                               itemID = A.Id,
                               unitID = A.unitID,
                               unitNo = B.unitNo,
                               unitItemID = A.Id,
                               clusterCode = C.clusterCode
                           }).ToList();

            return new ListResultDto<GetUnitItemListDto>(getData);
        }

        public void UpdateCompanyMsUnitItem(List<UpdateCompanyMsUnitItemInputDto> input)
        {
            Logger.Info("UpdateCompanyMsUnitItem() - Started.");

            foreach (var getInputToUpdate in input)
            {
                var getDataToUpdate = (from A in _msUnitItemRepo.GetAll()
                                       where A.Id == getInputToUpdate.unitItemId
                                       select A).FirstOrDefault();

                var update = getDataToUpdate.MapTo<MS_UnitItem>();

                update.coCode = getInputToUpdate.coCode;

                try
                {
                    Logger.DebugFormat("UpdateCompanyMsUnitItem() - Start update unit item. Params sent:{0}" +
                        "unitItemID = {1}{0}" +
                        "coCode = {2}{0}"
                        , Environment.NewLine, getInputToUpdate.unitItemId, getInputToUpdate.coCode);

                    _msUnitItemRepo.Update(update);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("UpdateCompanyMsUnitItem() - End update data unitItem.");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("UpdateCompanyMsUnitItem() ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateCompanyMsUnitItem() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }

            Logger.Info("UpdateCompanyMsUnitItem() - Finished.");
        }
    }
}
