using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VDI.Demo.Authorization;
using VDI.Demo.Commission.MS_PPhRangesInst.Dto;
using VDI.Demo.NewCommDB;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace VDI.Demo.Commission.MS_PPhRangesInst
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPPHRangeInst)]
    public class MsPPhRangeInstAppService : DemoAppServiceBase, IMsPPhRangeInstAppService
    {
        private readonly IRepository<MS_PPhRangeIns> _msPPhRangesInstRepo;

        public MsPPhRangeInstAppService(IRepository<MS_PPhRangeIns> msPPhRangeInstRepo)
        {
            _msPPhRangesInstRepo = msPPhRangeInstRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPPHRangeInst_Create)]
        public void CreateMsPPhRangeInst(List<CreateOrUpdatePPhRangeInstListDto> input)
        {
            Logger.InfoFormat("CreateMsPPhRangeInst() Started.");

            Logger.DebugFormat("CreateMsPPhRangeInst() - Started Loop Data = {0}", input);
            foreach (var item in input)
            {
                var createPPhRangeInst = new MS_PPhRangeIns
                {
                    schemaID = item.schemaID,
                    pphRangePct = item.pphRangePct,
                    validDate = item.validDate,
                    TAX_CODE = item.taxCode,
                    isComplete = true,
                    isActive = true,
                    entityID = 1
                };

                try
                {
                    Logger.DebugFormat("CreateMsPPhRangeInst() - Start insert PPhRangeInst. Parameters sent: {0} " +
                    "   schemaID = {1}{0}" +
                    "   pphRangePct = {2}{0}" +
                    "   validDate = {3}{0}" +
                    "   TAX_CODE = {4}{0}" +
                    "   isComplete = {5}{0}" +
                    "   isActive = {6}{0}" +
                    "   entityID = {7}{0}"
                    , Environment.NewLine, item.schemaID, item.pphRangePct, item.validDate, item.taxCode, true, true, 1);
                    _msPPhRangesInstRepo.Insert(createPPhRangeInst);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("CreateMsPPhRangeInst() - End insert PPhRangeInst.");
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("CreateMsPPhRangeInst() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("CreateMsPPhRangeInst() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.DebugFormat("CreateMsPPhRangeInst() - End Loop Data.");
            Logger.InfoFormat("CreateMsPPhRangeInst() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPPHRangeInst_Delete)]
        public void DeleteMsPPhRange(int Id)
        {
            Logger.InfoFormat("DeleteMsPPhRangeInst() Started.");

            Logger.DebugFormat("DeleteMsPPhRangeInst() - Start get data PPhRangeInst for update. Parameters sent: {0} " +
                    "   pphRangeInstID = {1}{0}"
                    , Environment.NewLine, Id);

            var getPPhRangeInst = (from pphRangeInst in _msPPhRangesInstRepo.GetAll()
                                   where Id == pphRangeInst.Id
                                   select pphRangeInst).FirstOrDefault();
            var updatePPhRangeInst = getPPhRangeInst.MapTo<MS_PPhRangeIns>();
            Logger.DebugFormat("DeleteMsPPhRangeInst() - End get data PPhRangeInst  for update. Result = {0}", updatePPhRangeInst);
            updatePPhRangeInst.isComplete = false;

            try
            {
                Logger.DebugFormat("DeleteMsPPhRangeInst() - Start Update PPhRangeInst. Parameters sent: {0} " +
                "   pphRangeInstID = {1}{0}" +
                "   isComplete = {2}{0}"
                , Environment.NewLine, Id, false);

                _msPPhRangesInstRepo.Update(updatePPhRangeInst);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("DeleteMsPPhRangeInst() - End Update PPhRangeInst.");
            }
            catch (DataException ex)
            {
                Logger.DebugFormat("DeleteMsPPhRangeInst() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("DeleteMsPPhRangeInst() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.InfoFormat("DeleteMsPPhRangeInst() - Finished.");
        }

        public ListResultDto<CreateOrUpdatePPhRangeInstListDto> GetMsPPhRangeInstBySchemaId(int schemaID)
        {
            var listResult = (from pphRangesInst in _msPPhRangesInstRepo.GetAll()
                              where pphRangesInst.schemaID == schemaID && pphRangesInst.isComplete == true
                              orderby pphRangesInst.Id descending
                              select new CreateOrUpdatePPhRangeInstListDto
                              {
                                  pphRangeIDInst = pphRangesInst.Id,
                                  pphRangePct = pphRangesInst.pphRangePct,
                                  schemaID = pphRangesInst.schemaID,
                                  validDate = pphRangesInst.validDate,
                                  taxCode = pphRangesInst.TAX_CODE,
                                  isActive = pphRangesInst.isActive
                              }).ToList();
            return new ListResultDto<CreateOrUpdatePPhRangeInstListDto>(listResult);
        }

        public void UpdateMsPPhRangeInst(CreateOrUpdatePPhRangeInstListDto input)
        {
            Logger.InfoFormat("UpdateMsPPhRangeInst() - Started.");

            Logger.DebugFormat("UpdateMsPPhRangeInst() - Start get data PPhRangeInst for update. Parameters sent: {0} " +
                    "   pphRangeIDInst = {1}{0}"
                    , Environment.NewLine, input.pphRangeIDInst);
            var getPPhRangeInst = (from pphRangeInst in _msPPhRangesInstRepo.GetAll()
                                   where input.pphRangeIDInst == pphRangeInst.Id
                                   select pphRangeInst).FirstOrDefault();
            var updatePPhRangeInst = getPPhRangeInst.MapTo<MS_PPhRangeIns>();
            Logger.DebugFormat("UpdateMsPPhRangeInst() - End get data PPhRangeInst  for update. Result = {0}", updatePPhRangeInst);

            updatePPhRangeInst.schemaID = input.schemaID;
            updatePPhRangeInst.pphRangePct = input.pphRangePct;
            updatePPhRangeInst.TAX_CODE = input.taxCode;
            updatePPhRangeInst.isActive = input.isActive;

            try
            {
                Logger.DebugFormat("UpdateMsPPhRangeInst() - Start update pphRangeInst. Parameters sent: {0} " +
            "   schemaID = {1}{0}" +
            "   pphRangePct = {2}{0}" +
            "   TAX_CODE = {3}{0}" +
            "   isActive = {4}{0}"
            , Environment.NewLine, input.schemaID, input.pphRangePct, input.taxCode, input.isActive);

                _msPPhRangesInstRepo.Update(updatePPhRangeInst);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("UpdateMsPPhRangeInst() - End update PPhRangeInst.");
            }
            catch (DataException ex)
            {
                Logger.DebugFormat("UpdateMsPPhRangeInst() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("UpdateMsPPhRangeInst() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }

            Logger.InfoFormat("UpdateMsPPhRangeInst() - Finished.");
        }
    }
}
