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
using VDI.Demo.Pricing.MS_FinType.Dto;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.Pricing.MS_FinType
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterFinType)]
    public class MsFinTypeAppService : DemoAppServiceBase, IMsFinTypeAppService
    {
        private readonly IRepository<LK_FinType> _msFinTypeRepo;
        private readonly IRepository<MS_TermPmt> _msTermPmtRepo;

        public MsFinTypeAppService(
            IRepository<LK_FinType> msFinTypeRepo,
            IRepository<MS_TermPmt> msTermPmtRepo
            )
        {
            _msFinTypeRepo = msFinTypeRepo;
            _msTermPmtRepo = msTermPmtRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterFinType_Create)]
        public void CreateMsFinType(CreateMsFinTypeInputDto input)
        {
            Logger.Info("CreateMsFinType() - Started.");
            Logger.DebugFormat("CreateMsFinType() - Start checking before insert Fin Type. Parameters sent:{0}" +
                        "finTypeCode = {1}{0}"
                        , Environment.NewLine, input.finTypeCode);

            bool checkFinType = (from A in _msFinTypeRepo.GetAll()
                                 where A.finTypeCode == input.finTypeCode
                                 select A).Any();

            Logger.DebugFormat("CreateMsFinType() - Ended checking before insert Fin Type. Result = {0}", checkFinType);

            if (!checkFinType)
            {
                var createMsFinType = new LK_FinType
                {
                    finTypeCode = input.finTypeCode,
                    finTypeDesc = input.finTypeDesc,
                    finTimes = input.finTimes,
                    pctComm = input.pctComm,
                    isCommStd = true,
                    isCashStd = true,
                    oldFinTypeCode = "-",
                    pctCommLC = 10,
                    pctCommTB = 10
                };
                try
                {
                    Logger.DebugFormat("CreateMsFinType() - Start insert Fin Type. Parameters sent:{0}" +
                        "finTypeCode = {1}{0}" +
                        "finTypeDesc = {2}{0}" +
                        "finTimes = {3}{0}" +
                        "pctComm = {4}{0}" +
                        "isCommStd = {5}{0}" +
                        "isCashStd = {6}{0}" +
                        "oldFinTypeCode = {7}{0}" +
                        "pctCommLC = {8}{0}" +
                        "pctCommTB = {9}{0}"
                        , Environment.NewLine, input.finTypeCode, input.finTypeDesc, input.finTimes, input.pctComm, true, true
                        , "-", 10, 10);

                    _msFinTypeRepo.Insert(createMsFinType);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("CreateMsFinType() - Ended insert Fin Type.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateMsFinType() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateMsFinType() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }

            else
            {
                throw new UserFriendlyException("Data already exist");
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterFinType_Delete)]
        public void DeleteMsFinType(int finTypeID)
        {
            Logger.Info("DeleteMsFinType() - Started.");
            Logger.DebugFormat("DeleteMsFinType() - Start checking before delete Fin Type. Parameters sent:{0}" +
                        "finTypeID = {1}{0}"
                        , Environment.NewLine, finTypeID);

            bool check = _msTermPmtRepo.GetAll().Where(x => x.finTypeID == finTypeID).Any();

            Logger.DebugFormat("DeleteMsFinType() - Ended checking before delete Fin Type. Result = {0}", check);

            if (!check)
            {

                try
                {
                    Logger.DebugFormat("DeleteMsFinType() - Start checking before delete Category. Parameters sent:{0}" +
                        "finTypeID = {1}{0}"
                        , Environment.NewLine, finTypeID);

                    _msFinTypeRepo.Delete(finTypeID);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("DeleteMsFinType() - Ended delete Fin Type.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsFinType() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsFinType() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("DeleteMsFinType() - ERROR Exception.", "This finType is used in transaction !");
                throw new UserFriendlyException("This finType is used in transaction !");
            }
            Logger.Info("DeleteMsFinType() - Finished.");
        }

        public ListResultDto<string> GetAllFinType()
        {
            var listResult = (from x in _msFinTypeRepo.GetAll()
                              select x.finTypeDesc).ToList();

            return new ListResultDto<string>(listResult);
        }

        public ListResultDto<GetAllMsFinTypeListDto> GetAllMsFinType()
        {
            var listResult = (from finType in _msFinTypeRepo.GetAll()
                              orderby finType.Id descending
                              select new GetAllMsFinTypeListDto
                              {
                                  id = finType.Id,
                                  finTypeCode = finType.finTypeCode,
                                  finTypeDesc = finType.finTypeDesc,
                                  finTimes = finType.finTimes,
                                  pctComm = finType.pctComm
                              }).ToList();

            return new ListResultDto<GetAllMsFinTypeListDto>(listResult);

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterFinType_Edit)]
        public void UpdateMsFinType(UpdateMsFinTypeInputDto input)
        {
            Logger.Info("UpdateMsFinType() - Started.");
            Logger.DebugFormat("UpdateMsFinType() - Start checking before update Fin Type. Parameters sent:{0}" +
                        "finTypeCode = {1}{0}"
                        , Environment.NewLine, input.finTypeCode);

            bool checkCode = (from A in _msFinTypeRepo.GetAll()
                              where A.Id != input.fintypeID && A.finTypeCode == input.finTypeCode
                              select A).Any();

            Logger.DebugFormat("UpdateMsFinType() - Ended checking before update Fin Type. Result = {0}", checkCode);

            if (!checkCode)
            {
                Logger.DebugFormat("UpdateMsFinType() - Start get data before update Fin Type. Parameters sent:{0}" +
                        "finTypeCode = {1}{0}"
                        , Environment.NewLine, input.finTypeCode);

                var getMsFinType = (from A in _msFinTypeRepo.GetAll()
                                    where A.Id == input.fintypeID
                                    select A).FirstOrDefault();

                Logger.DebugFormat("UpdateMsFinType() - Ended get data before update Fin Type.");

                var update = getMsFinType.MapTo<LK_FinType>();

                update.finTypeDesc = input.finTypeDesc;
                update.finTimes = input.finTimes;
                update.pctComm = input.pctComm;
                try
                {
                    Logger.DebugFormat("UpdateMsFinType() - Start update Fin Type. Parameters sent:{0}" +
                        "finTypeDesc = {1}{0}" +
                        "finTimes = {2}{0}" +
                        "pctComm = {3}{0}"
                        , Environment.NewLine, input.finTypeDesc, input.finTimes, input.pctComm);

                    _msFinTypeRepo.Update(update);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("UpdateMsFinType() - Ended update Fin Type.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateMsFinType() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsFinType() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }


            }
            else
            {
                Logger.ErrorFormat("UpdateMsFinType() - ERROR Result = {0}.", "The FinType Already Exist!");
                throw new UserFriendlyException("The FinType Already Exist!");
            }
            Logger.Info("UpdateMsFinType() - Finished.");
        }
    }
}
