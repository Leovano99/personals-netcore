using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VDI.Demo.Authorization;
using VDI.Demo.Commission.MS_PPhRanges.Dto;
using VDI.Demo.NewCommDB;
using Abp.AutoMapper;

namespace VDI.Demo.Commission.MS_PPhRanges
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPPHRange)]
    public class MsPPhRangeAppService : DemoAppServiceBase, IMsPPhRangeAppService
    {
        private readonly IRepository<MS_PPhRange> _msPPhRangesRepo;

        public MsPPhRangeAppService(
            IRepository<MS_PPhRange> msPPhRangesRepo
        )
        {
            _msPPhRangesRepo = msPPhRangesRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPPHRange_Create)]
        public void CreateMsPPhRange(List<CreateOrUpdatePPhRangeListDto> input)
        {
            Logger.Info("CreateMsPPhRange() - Started.");
            foreach (var item in input)
            {
                var createPPhRange = new MS_PPhRange
                {
                    schemaID = item.schemaID,
                    pphYear = item.pphYear,
                    pphRangeHighBound = item.pphRangeHighBound,
                    pphRangePct = item.pphRangePct,
                    TAX_CODE = item.tax_code,
                    TAX_CODE_NON_NPWP = item.tax_code_non_npwp,
                    pphRangePct_NON_NPWP = item.pphRangePct_non_npwp,
                    isActive = item.isActive,
                    isComplete = true,
                    entityID = 1
                };

                try
                {
                    Logger.DebugFormat("CreateMsPPhRange() - Start insert PPh Range. Parameters sent:{0}" +
                        "schemaID = {1}{0}" +
                        "pphYear = {2}{0}" +
                        "pphRangeHighBound = {3}{0}" +
                        "pphRangePct = {4}{0}" +
                        "TAX_CODE = {5}{0}" +
                        "TAX_CODE_NON_NPWP = {6}{0}" +
                        "pphRangePct_NON_NPWP = {7}{0}" +
                        "isActive = {8}{0}" +
                        "isComplete = {9}{0}" +
                        "entityID = {10}{0}"
                        , Environment.NewLine, item.schemaID, item.pphYear, item.pphRangeHighBound, item.pphRangePct
                        , item.tax_code, item.tax_code_non_npwp, item.pphRangePct_non_npwp, item.isActive, true, 1);

                    _msPPhRangesRepo.Insert(createPPhRange);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("CreateMsPPhRange() - Ended insert PPh Range.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateMsPPhRange() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateMsPPhRange() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }

            }
            Logger.Info("CreateMsPPhRange() - Finished.");
        }

        public void UpdateMsPPhRange(CreateOrUpdatePPhRangeListDto input)
        {
            Logger.Info("UpdateMsPPhRange() - Started.");

            Logger.DebugFormat("UpdateMsPPhRange() - Start get data before update PPh Range. Parameters sent:{0}" +
                        "pphRangeID = {1}{0}"
                        , Environment.NewLine, input.pphRangeID);

            var getPPhRange = (from pphRanges in _msPPhRangesRepo.GetAll()
                               where input.pphRangeID == pphRanges.Id
                               select pphRanges).FirstOrDefault();

            Logger.DebugFormat("UpdateMsPPhRange() - Ended get data before update PPh Range. Result = {0}", getPPhRange);

            var updatePPhRange = getPPhRange.MapTo<MS_PPhRange>();

            updatePPhRange.pphYear = input.pphYear;
            updatePPhRange.pphRangeHighBound = input.pphRangeHighBound;
            updatePPhRange.pphRangePct = input.pphRangePct;
            updatePPhRange.TAX_CODE = input.tax_code;
            updatePPhRange.TAX_CODE_NON_NPWP = input.tax_code_non_npwp;
            updatePPhRange.pphRangePct_NON_NPWP = input.pphRangePct_non_npwp;
            updatePPhRange.isActive = input.isActive;

            try
            {
                Logger.DebugFormat("UpdateMsPPhRange() - Start update PPh Range. Parameters sent:{0}" +
                        "pphYear = {1}{0}" +
                        "pphRangeHighBound = {2}{0}" +
                        "pphRangePct = {3}{0}" +
                        "TAX_CODE = {4}{0}" +
                        "TAX_CODE_NON_NPWP = {5}{0}" +
                        "pphRangePct_NON_NPWP = {6}{0}" +
                        "isActive = {7}{0}"
                        , Environment.NewLine, input.pphYear, input.pphRangeHighBound, input.pphRangePct
                        , input.tax_code, input.tax_code_non_npwp, input.pphRangePct_non_npwp, input.isActive);


                _msPPhRangesRepo.Update(updatePPhRange);
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                Logger.DebugFormat("UpdateMsPPhRange() - Ended update PPh Range.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("UpdateMsPPhRange() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("UpdateMsPPhRange() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }

            Logger.Info("UpdateMsPPhRange() - Finished.");

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPPHRange_Delete)]
        public void DeleteMsPPhRange(int Id)
        {
            Logger.Info("DeleteMsPPhRange() - Started.");

            Logger.DebugFormat("DeleteMsPPhRange() - Start get data before delete PPh Range. Parameters sent:{0}" +
                        "pphRangeID = {1}{0}"
                        , Environment.NewLine, Id);

            var getPphRange = (from pphRange in _msPPhRangesRepo.GetAll()
                               where Id == pphRange.Id
                               select pphRange).FirstOrDefault();

            Logger.DebugFormat("DeleteMsPPhRange() - Ended get data before delete PPh Range. Result = {0}", getPphRange);

            var updatePphRange = getPphRange.MapTo<MS_PPhRange>();

            updatePphRange.isComplete = false;

            try
            {
                Logger.DebugFormat("DeleteMsPPhRange() - Start delete PPh Range. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                _msPPhRangesRepo.Update(updatePphRange);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("DeleteMsPPhRange() - Ended delete PPh Range.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("DeleteMsPPhRange() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("DeleteMsPPhRange() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.Info("DeleteMsPPhRange() - Finished.");
        }

        public ListResultDto<CreateOrUpdatePPhRangeListDto> GetMsPPhRangeBySchemaId(int schemaID)
        {
            var listResult = (from pphRanges in _msPPhRangesRepo.GetAll()
                              where pphRanges.schemaID == schemaID && pphRanges.isComplete == true
                              orderby pphRanges.Id descending
                              select new CreateOrUpdatePPhRangeListDto
                              {
                                  pphRangeID = pphRanges.Id,
                                  pphYear = pphRanges.pphYear,
                                  pphRangeHighBound = pphRanges.pphRangeHighBound,
                                  pphRangePct = pphRanges.pphRangePct,
                                  schemaID = pphRanges.schemaID,
                                  isActive = pphRanges.isActive,
                                  tax_code = pphRanges.TAX_CODE,
                                  tax_code_non_npwp = pphRanges.TAX_CODE_NON_NPWP,
                                  pphRangePct_non_npwp = pphRanges.pphRangePct_NON_NPWP

                              }).ToList();

            return new ListResultDto<CreateOrUpdatePPhRangeListDto>(listResult);
        }


    }
}
