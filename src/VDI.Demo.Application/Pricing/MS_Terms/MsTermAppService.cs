using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using VDI.Demo.Authorization;
using VDI.Demo.Pricing.MS_Terms.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.Pricing.MS_Terms
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterTerm)]
    public class MsTermAppService : DemoAppServiceBase, IMsTermAppService
    {
        private readonly IRepository<MS_Term> _msTermRepo;
        private readonly IRepository<MS_Discount> _msDiscountRepo;
        private readonly IRepository<LK_FinType> _lkFinTypeRepo;
        private readonly IRepository<MS_TermAddDisc> _msTermAddDiscRepo;
        private readonly IRepository<MS_TermDP> _msTermDPRepo;
        private readonly IRepository<MS_TermPmt> _msTermPmtRepo;
        private readonly IRepository<MS_TermMain> _msTermMainRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<LK_DPCalc> _lkDpCalcRepo;

        public MsTermAppService(
            IRepository<MS_Term> msTermRepo,
            IRepository<MS_TermAddDisc> msTermAddDiscRepo,
            IRepository<MS_TermDP> msTermDPRepo,
            IRepository<MS_TermPmt> msTermPmtRepo,
            IRepository<MS_TermMain> msTermMainRepo,
            IRepository<MS_Discount> msDiscountRepo,
            IRepository<LK_FinType> lkFinTypeRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<LK_DPCalc> lkDpCalcRepo
            )
        {
            _msTermRepo = msTermRepo;
            _msTermAddDiscRepo = msTermAddDiscRepo;
            _msTermDPRepo = msTermDPRepo;
            _msTermPmtRepo = msTermPmtRepo;
            _msTermMainRepo = msTermMainRepo;
            _msDiscountRepo = msDiscountRepo;
            _lkFinTypeRepo = lkFinTypeRepo;
            _msProjectRepo = msProjectRepo;
            _msUnitRepo = msUnitRepo;
            _lkDpCalcRepo = lkDpCalcRepo;
        }
        public ListResultDto<GetAllMsTermListDto> GetTermCodeDropdown()
        {
            var listResult = (from x in _msTermMainRepo.GetAll()
                              orderby x.termCode ascending
                              select new GetAllMsTermListDto
                              {
                                  termMainID = x.Id,
                                  termCode = x.termCode
                              })
                              .ToList();

            return new ListResultDto<GetAllMsTermListDto>(listResult);
        }

        public ListResultDto<GetAllMsTermListDto> GetTermNoDropdownByProjectClusterCategoryProduct(GetTermNoDropdownByProjectClusterCategoryProductInputDto input)
        {
            var getUnitTerm = (from x in _msUnitRepo.GetAll()
                               where x.projectID == input.projectID && x.clusterID == input.clusterID
                               && x.categoryID == input.categoryID && x.productID == input.productID
                               select x.termMainID).FirstOrDefault();

            var listResult = (from x in _msTermRepo.GetAll()
                              where x.termMainID == getUnitTerm
                              orderby x.termCode ascending
                              select new GetAllMsTermListDto
                              {
                                  termID = x.Id,
                                  termNo = x.termNo,
                                  termMainID = x.termMainID,
                                  termCode = x.termCode,
                                  remarks = x.remarks,
                              })
                              .ToList();

            return new ListResultDto<GetAllMsTermListDto>(listResult);
        }

        public ListResultDto<GetAllMsTermListDto> GetMsTermByTermMainID(int Id)
        {
            var listResult = (from termMain in _msTermMainRepo.GetAll()
                              join term in _msTermRepo.GetAll() on termMain.Id equals term.termMainID
                              join project in _msProjectRepo.GetAll() on term.projectID equals project.Id
                              where termMain.Id == Id
                              select new GetAllMsTermListDto
                              {
                                  termMainID = termMain.Id,
                                  termID = term.Id,
                                  termCode = term.termCode,
                                  termNo = term.termNo,
                                  PPJBDue = term.PPJBDue,
                                  remarks = term.remarks,
                                  projectName = project.projectName,
                                  isActive = term.isActive
                              }).ToList();

            return new ListResultDto<GetAllMsTermListDto>(listResult);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterTerm_Edit)]
        public void UpdateMsTermStatus(UpdateMsTermStatusInputDto input)
        {
            Logger.InfoFormat("UpdateMsTermStatus() Started.");

            Logger.DebugFormat("UpdateMsTermStatus() - Start checking existing termMainID. Parameters sent: {0} " +
                "   termMainID = {1}{0}"
                , Environment.NewLine, input.termMainID);

            Logger.DebugFormat("UpdateMsTermStatus() - Start get data term for update. Parameters sent: {0} " +
                    "   termMainID = {1}{0}" +
                    "   termNo = {2}{0}"
                    , Environment.NewLine, input.termMainID, input.termNo);
            var getMsTerm = (from A in _msTermRepo.GetAll()
                             where input.termMainID == A.termMainID && input.termNo == A.termNo
                             select A).FirstOrDefault();
            var updateMsTerm = getMsTerm.MapTo<MS_Term>();
            Logger.DebugFormat("UpdateMsTermStatus() - End get data term for update. Result = {0}", getMsTerm);

            updateMsTerm.isActive = input.isActive;
            try
            {
                Logger.DebugFormat("UpdateMsTermStatus() - Start update term. Parameters sent: {0} " +
            "   isActive = {1}{0}", Environment.NewLine, input.isActive);
                _msTermRepo.Update(updateMsTerm);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("UpdateMsTermStatus() - End update term.");
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                Logger.DebugFormat("UpdateMsTermStatus() - ERROR DataException. Result = {0}", exDb.Message);
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                Logger.DebugFormat("UpdateMsTermStatus() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }

            Logger.InfoFormat("UpdateMsTermStatus() - Finished.");
        }

        //public void UpdateUniversalMsTerm(UpdateUniversalMsTermInput input)
        //{
        //    var getMsTerm = (from x in _msTermRepo.GetAll()
        //                        where x.termMainID == input.termMainID
        //                        select x).FirstOrDefault();

        //    var updateMsTerm = getMsTerm.MapTo<MS_Term>();

        //    updateMsTerm.projectID = input.projectID;
        //    updateMsTerm.PPJBDue = input.PPj;
        //    updateMsTerm.projectID = input.projectID;
        //    updateMsTerm.projectID = input.projectID;
        //    updateMsTerm.projectID = input.projectID;

        //            PPJBDue = term.PPJBDue,
        //            remarks = term.remarks,
        //            termInstallment = term.termInstallment,
        //            discBFCalcType = "1",
        //            DPCalcType = term.discBFCalcType,
        //            GroupTermCode = "1",
        //            projectID = input.projectID,
        //            termMainID = termMainID,
        //            isActive = term.isActive

        //    throw new NotImplementedException();
        //}

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterTerm_Create)]
        public void CreateUniversalMsTerm(CreateUniversalMsTermInput input)
        {
            Logger.InfoFormat("CreateUniversalMsTerm() Started.");
            var dataTermMain = new CreateMsTermMainInput
            {
                entityID = 1,
                termCode = input.termCode,
                termDesc = "-",
                famDiscCode = "-",
                BFAmount = input.BFAmount,
                remarks = input.remarks
            };
            Logger.DebugFormat("CreateUniversalMsTerm() - Start CreateMsTermMain.");
            var termMainID = CreateMsTermMain(dataTermMain);
            Logger.DebugFormat("CreateUniversalMsTerm() - End CreateMsTermMain.");

            Logger.DebugFormat("CreateUniversalMsTerm() - Start loop setValue");
            foreach (var term in input.setValue)
            {
                var dataTerm = new CreateMsTermInput
                {
                    entityID = 1, //hardcode for not null field
                    termCode = input.termCode,
                    termNo = term.termNo,
                    PPJBDue = term.PPJBDue,
                    remarks = term.remarks,
                    termInstallment = term.termInstallment,
                    discBFCalcType = "-",
                    DPCalcType = term.DPCalcType,
                    GroupTermCode = "-",
                    sortNo = term.sortNo,
                    projectID = input.projectID,
                    termMainID = termMainID,
                    isActive = term.isActive
                };
                Logger.DebugFormat("CreateUniversalMsTerm() - Start CreateMsTerm.");
                var termID = CreateMsTerm(dataTerm);
                Logger.DebugFormat("CreateUniversalMsTerm() - End CreateMsTerm.");

                var dataTermPmt = new CreateMsTermPmtInput
                {
                    entityID = 1,
                    termNo = term.termNo,
                    finTypeID = term.finTypeID, //hardcode for not null field
                    finStartDue = term.finStartDue,
                    termID = termID
                };
                Logger.DebugFormat("CreateUniversalMsTerm() - Start CreateMsTermPmt.");
                CreateMsTermPmt(dataTermPmt);
                Logger.DebugFormat("CreateUniversalMsTerm() - End CreateMsTermPmt.");

                Logger.DebugFormat("CreateUniversalMsTerm() - Start loop data DP");

                if (term.DtoDP.Any())
                {
                    foreach (var dp in term.DtoDP)
                    {
                        var dataTermDP = new CreateMsTermDPInput
                        {
                            termCode = input.termCode,
                            termNo = term.termNo,
                            daysDue = dp.daysDue,
                            DPNo = dp.DPNo,
                            DPPct = dp.DPPct,
                            DPAmount = dp.DPAmount,
                            daysDueNewKP = 0,
                            termID = termID
                        };

                        Logger.DebugFormat("CreateUniversalMsTerm() - Start CreateMsTermDP.");
                        CreateMsTermDP(dataTermDP);
                        Logger.DebugFormat("CreateUniversalMsTerm() - End CreateMsTermDP.");
                    }
                }
                else
                {
                    var dataTermDP = new CreateMsTermDPInput
                    {
                        termCode = input.termCode,
                        termNo = term.termNo,
                        daysDue = 0,
                        DPNo = 1,
                        DPPct = 0,
                        DPAmount = 0,
                        daysDueNewKP = 0,
                        termID = termID
                    };

                    Logger.DebugFormat("CreateUniversalMsTerm() - Start CreateMsTermDP.");
                    CreateMsTermDP(dataTermDP);
                    Logger.DebugFormat("CreateUniversalMsTerm() - End CreateMsTermDP.");
                }

                Logger.DebugFormat("CreateUniversalMsTerm() - End loop data DP");

                Logger.DebugFormat("CreateUniversalMsTerm() - Start loop data Disc");

                if (term.DtoDisc.Any())
                {
                    foreach (var disc in term.DtoDisc)
                    {
                        var dataTermAddDisc = new CreateMsTermAddDiscInput
                        {
                            entityCode = "1",
                            termCode = input.termCode,
                            termNo = term.termNo,
                            addDiscNo = disc.addDiscNo,
                            addDiscPct = disc.addDiscPct,
                            addDiscAmt = disc.addDiscAmt,
                            discountID = disc.discountID,
                            termID = termID
                        };
                        Logger.DebugFormat("CreateUniversalMsTerm() - Start CreateMsTermAddDisc.");
                        CreateMsTermAddDisc(dataTermAddDisc);
                        Logger.DebugFormat("CreateUniversalMsTerm() - End CreateMsTermAddDisc.");
                    }
                }
                else
                {
                    var getAllDisc = GetDiscountDropdownExcludeSalesLaunchDisc();
                    var discNo = 1;

                    foreach (var item in getAllDisc)
                    {

                        var dataTermAddDisc = new CreateMsTermAddDiscInput
                        {
                            entityCode = "1",
                            termCode = input.termCode,
                            termNo = term.termNo,
                            addDiscNo = Convert.ToByte(discNo),
                            addDiscPct = 0,
                            addDiscAmt = 0,
                            discountID = item.discountID,
                            termID = termID
                        };
                        Logger.DebugFormat("CreateUniversalMsTerm() - Start CreateMsTermAddDisc.");
                        CreateMsTermAddDisc(dataTermAddDisc);
                        Logger.DebugFormat("CreateUniversalMsTerm() - End CreateMsTermAddDisc.");

                        discNo++;
                    }

                    Logger.DebugFormat("CreateUniversalMsTerm() - Term Add Disc Is Empty.");
                }

                Logger.DebugFormat("CreateUniversalMsTerm() - End loop data Disc");
            }
            Logger.DebugFormat("CreateUniversalMsTerm() - Start loop setValue");

            Logger.InfoFormat("CreateUniversalMsTerm() - Finished");
        }

        public int CreateMsTermMain(CreateMsTermMainInput input)
        {
            Logger.InfoFormat("CreateMsTermMain() - Start");
            var dataTermMain = new MS_TermMain
            {
                entityID = 1,
                termCode = input.termCode,
                termDesc = input.termDesc,
                famDiscCode = input.famDiscCode,
                BFAmount = input.BFAmount,
                remarks = input.remarks
            };
            try
            {
                Logger.DebugFormat("CreateMsTermMain() - Start Insert termMain. Parameters sent: {0} " +
            "   entityID = {1}{0}" +
            "   termCode = {2}{0}" +
            "   termDesc = {3}{0}" +
            "   famDiscCode = {4}{0}" +
            "   BFAmount = {5}{0}" +
            "   remarks = {6}{0}"
            , Environment.NewLine, 1, input.termCode, input.termDesc, input.famDiscCode, input.BFAmount, input.remarks);

                CurrentUnitOfWork.SaveChanges();
                var termMainID = _msTermMainRepo.InsertAndGetId(dataTermMain);
                Logger.DebugFormat("CreateMsTermMain() - End Insert termMain. Result = {0}", termMainID);
                return termMainID;
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                Logger.DebugFormat("CreateMsTermMain() - ERROR DataException. Result = {0}", exDb.Message);
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                Logger.DebugFormat("CreateMsTermMain() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
            Logger.InfoFormat("CreateMsTermMain() - Finished");
        }

        public int CreateMsTerm(CreateMsTermInput input)
        {
            Logger.InfoFormat("CreateMsTerm() Started");
            var dataTerm = new MS_Term
            {
                entityID = input.entityID,
                termCode = input.termCode,
                termNo = input.termNo,
                sortNo = input.sortNo,
                PPJBDue = input.PPJBDue,
                remarks = input.remarks,
                termInstallment = input.termInstallment,
                discBFCalcType = input.discBFCalcType,
                DPCalcType = input.DPCalcType,
                GroupTermCode = input.GroupTermCode,
                projectID = input.projectID,
                termMainID = input.termMainID,
                isActive = input.isActive
            };
            try
            {
                Logger.DebugFormat("CreateMsTerm() - Start Insert Term. Parameters sent: {0} " +
            "   entityID = {1}{0}" +
            "   termCode = {2}{0}" +
            "   termNo = {3}{0}" +
            "   sortNo = {13}{0}" +
            "   PPJBDue = {4}{0}" +
            "   remarks = {5}{0}" +
            "   termInstallment = {6}{0}" +
            "   discBFCalcType = {7}{0}" +
            "   DPCalcType = {8}{0}" +
            "   GroupTermCode = {9}{0}" +
            "   projectID = {10}{0}" +
            "   termMainID = {11}{0}" +
            "   isActive = {12}{0}"
            , Environment.NewLine, input.entityID, input.termCode, input.termNo, input.PPJBDue, input.remarks,
            input.termInstallment, input.discBFCalcType, input.DPCalcType, input.GroupTermCode, input.projectID,
            input.termMainID, input.isActive, input.sortNo);

                CurrentUnitOfWork.SaveChanges();
                var termID = _msTermRepo.InsertAndGetId(dataTerm);
                Logger.DebugFormat("CreateMsTerm() - End Insert Term.");
                return termID;
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                Logger.DebugFormat("CreateMsTerm() - ERROR DataException. Result = {0}", exDb.Message);
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                Logger.DebugFormat("CreateMsTerm() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
            Logger.InfoFormat("CreateMsTerm() - Finished.");
        }

        public void CreateMsTermPmt(CreateMsTermPmtInput input)
        {
            Logger.InfoFormat("CreateMsTermPmt() Started");
            var dataTermPmt = new MS_TermPmt
            {
                entityID = 1,
                termNo = input.termNo,
                finTypeID = input.finTypeID,
                finStartDue = input.finStartDue,
                termID = input.termID
            };
            try
            {
                Logger.DebugFormat("CreateMsTermPmt() - Start Insert TermPmt. Parameters sent: {0} " +
            "   entityID = {1}{0}" +
            "   termNo = {2}{0}" +
            "   finTypeID = {3}{0}" +
            "   finStartDue = {4}{0}" +
            "   termID = {5}{0}"
            , Environment.NewLine, 1, input.termNo, input.finTypeID, input.finStartDue, input.termID);
                _msTermPmtRepo.Insert(dataTermPmt);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("CreateMsTermPmt() - End Insert TermPmt.");
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                Logger.DebugFormat("CreateMsTermPmt() - ERROR DataException. Result = {0}", exDb.Message);
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                Logger.DebugFormat("CreateMsTermPmt() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
            Logger.InfoFormat("CreateMsTermPmt() - Finished.");
        }

        public void CreateMsTermDP(CreateMsTermDPInput input)
        {
            Logger.InfoFormat("CreateMsTermDP() Started");
            var dataTermDP = new MS_TermDP
            {
                entityID = 1,
                termCode = input.termCode,
                termNo = input.termNo,
                daysDue = input.daysDue,
                DPNo = input.DPNo,
                DPPct = input.DPPct,
                DPAmount = input.DPAmount,
                daysDueNewKP = 0,
                termID = input.termID
            };
            try
            {
                Logger.DebugFormat("CreateMsTermDP() - Start Insert TermDP. Parameters sent: {0} " +
                       "   entityID = {1}{0}" +
                       "   termCode = {2}{0}" +
                       "   termNo = {3}{0}" +
                       "   daysDue = {4}{0}" +
                       "   DPNo = {5}{0}" +
                       "   DPPct = {6}{0}" +
                       "   DPAmount = {7}{0}" +
                       "   daysDueNewKP = {8}{0}" +
                       "   termID = {9}{0}"
                       , Environment.NewLine, 1, input.termCode, input.termNo, input.daysDue, input.DPNo, input.DPPct,
                        input.DPAmount, input.daysDueNewKP, input.termID);
                _msTermDPRepo.Insert(dataTermDP);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("CreateMsTermDP() - End Insert TermDP.");
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                Logger.DebugFormat("CreateMsTermDP() - ERROR DataException. Result = {0}", exDb.Message);
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                Logger.DebugFormat("CreateMsTermDP() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
            Logger.InfoFormat("CreateMsTermDP() - Finished.");
        }

        public void CreateMsTermAddDisc(CreateMsTermAddDiscInput input)
        {
            Logger.InfoFormat("CreateMsTermAddDisc() Started");
            var dataTermAddDisc = new MS_TermAddDisc
            {
                entityID = 1,
                termNo = input.termNo,
                addDiscNo = input.addDiscNo,
                addDiscAmt = input.addDiscAmt,
                addDiscPct = input.addDiscPct,
                discountID = input.discountID,
                termID = input.termID
            };
            try
            {
                Logger.DebugFormat("CreateMsTermAddDisc() - Start Insert TermAddDisc. Parameters sent: {0} " +
                       "   entityID = {1}{0}" +
                       "   termNo = {2}{0}" +
                       "   addDiscNo = {3}{0}" +
                       "   addDiscAmt = {4}{0}" +
                       "   addDiscPct = {5}{0}" +
                       "   addDiscPct = {6}{0}" +
                       "   termID = {7}{0}"
                       , Environment.NewLine, 1, input.termNo, input.addDiscNo, input.addDiscAmt,
                       input.addDiscPct, input.addDiscPct, input.termID);

                _msTermAddDiscRepo.Insert(dataTermAddDisc);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("CreateMsTermAddDisc() - End Insert TermAddDisc.");
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                Logger.DebugFormat("CreateMsTermAddDisc() - ERROR DataException. Result = {0}", exDb.Message);
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                Logger.DebugFormat("CreateMsTermAddDisc() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
            Logger.InfoFormat("CreateMsTermAddDisc() - Finished.");
        }

        public GetExistingTermDto GetExistingTermByTermCode(string termCode)
        {
            var result = new GetExistingTermDto();

            List<DtoGetTerm> setValue = new List<DtoGetTerm>();

            var getTerm = (from termMain in _msTermMainRepo.GetAll()
                           join term in _msTermRepo.GetAll() on termMain.Id equals term.termMainID
                           join pmt in _msTermPmtRepo.GetAll() on term.Id equals pmt.termID
                           join main in _msTermMainRepo.GetAll() on term.termMainID equals main.Id
                           join project in _msProjectRepo.GetAll() on term.projectID equals project.Id
                           join finType in _lkFinTypeRepo.GetAll() on pmt.finTypeID equals finType.Id
                           where term.termCode == termCode
                           select new DtoGetTerm
                           {
                               termMainID = termMain.Id,
                               termID = term.Id,
                               termCode = term.termCode,
                               termNo = term.termNo,
                               sortNo = term.sortNo,
                               PPJBDue = term.PPJBDue,
                               remarksMain = main.remarks,
                               remarks = term.remarks,
                               termInstallment = term.termInstallment,
                               discBFCalcType = term.discBFCalcType,
                               DPCalcType = term.DPCalcType,
                               GroupTermCode = term.GroupTermCode,
                               isActive = term.isActive,
                               termPmtID = pmt.Id,
                               finTypeID = pmt.finTypeID,
                               finTypeCode = finType.finTypeCode,
                               finStartDue = pmt.finStartDue,
                               BFAmount = main.BFAmount,
                               projectID = term.projectID,
                               projectName = project.projectName
                           }).ToList();

            if (getTerm.Any())
            {
                foreach (var term in getTerm)
                {
                    var getDP = (from dp in _msTermDPRepo.GetAll()
                                 where dp.termID == term.termID
                                 select new DtoGetDP
                                 {
                                     termDPID = dp.Id,
                                     DPNo = dp.DPNo,
                                     DPAmount = dp.DPAmount,
                                     DPPct = dp.DPPct,
                                     daysDue = dp.daysDue,
                                     daysDueNewKP = dp.daysDueNewKP
                                 }).ToList();

                    var getAddDisc = (from disc in _msTermAddDiscRepo.GetAll()
                                      where disc.termID == term.termID
                                      select new DtoGetDisc
                                      {
                                          termAddDiscID = disc.Id,
                                          addDiscNo = disc.addDiscNo,
                                          discountID = disc.discountID,
                                          addDiscPct = disc.addDiscPct,
                                          addDiscAmt = disc.addDiscAmt
                                      }).ToList();

                    var termTemp = term;
                    termTemp.DtoDP = getDP;
                    termTemp.DtoDisc = getAddDisc;
                    setValue.Add(termTemp);
                }
                result.entityCode = getTerm[0].entityCode;
                result.remarks = getTerm[0].remarksMain;
                result.termMainID = getTerm[0].termMainID;
                result.termCode = getTerm[0].termCode;
                result.BFAmount = getTerm[0].BFAmount;
                result.projectName = getTerm[0].projectName;
                result.projectID = getTerm[0].projectID;
                result.setValue = setValue;
                return result;
            }
            else
            {
                throw new UserFriendlyException("Empty Data");
            }
        }

        public List<GetMsDiscountDto> GetDiscountDropdown()
        {
            var listResult = (from x in _msDiscountRepo.GetAll()
                              where x.isActive == true
                              select new GetMsDiscountDto
                              {
                                  discountID = x.Id,
                                  discountCode = x.discountCode,
                                  discountName = x.discountName
                              }).ToList();
            return listResult;
        }

        public List<GetMsFinTypeDto> GetFinTypeDropdown()
        {
            var listResult = (from x in _lkFinTypeRepo.GetAll()
                              select new GetMsFinTypeDto
                              {
                                  finTypeID = x.Id,
                                  finTypeDesc = x.finTypeDesc,
                                  finTypeCode = x.finTypeCode,
                                  finTimes = x.finTimes
                              }).ToList();
            return listResult;
        }

        public JObject CheckAvailableTermCode(string termCode)
        {
            JObject obj = new JObject();

            var result = (from x in _msTermRepo.GetAll()
                          where x.termCode == termCode
                          select x.termCode).ToList();

            

            if (!result.Any())
            {
                obj.Add("message", termCode);
            }
            else
            {
                obj.Add("message", "Term Code is Existing");
            }
            return obj;
        }

        public List<GetMsDiscountDto> GetDiscountDropdownExcludeSalesLaunchDisc()
        {
            var listResult = (from x in _msDiscountRepo.GetAll()
                              where !x.discountCode.Contains("sales disc") && !x.discountCode.Contains("launching disc") && x.isActive
                              select new GetMsDiscountDto
                              {
                                  discountID = x.Id,
                                  discountCode = x.discountCode,
                                  discountName = x.discountName
                              }).ToList();
            return listResult;
        }

        public List<GetDpCalcListDto> GetDpCalc()
        {
            var dataResult = (from A in _lkDpCalcRepo.GetAll()
                              select new GetDpCalcListDto
                              {
                                  Id = A.Id,
                                  dpCalcDesc = A.DPCalcDesc,
                                  dpCalcType = A.DPCalcType
                              }).ToList();

            return dataResult;
        }

        public void UpdateMsTermMain(UpdateMsTermMainInput input)
        {
            Logger.InfoFormat("UpdateMsTermMain() Started");

            Logger.DebugFormat("UpdateMsTermMain() - Start get termMain for update. Parameters sent: {0} " +
        "   TermMainID = {1}{0}"
        , Environment.NewLine, input.Id);
            var getTermMain = (from x in _msTermMainRepo.GetAll()
                               where x.Id == input.Id
                               select x).FirstOrDefault();

            var dataTermMain = getTermMain.MapTo<MS_TermMain>();
            Logger.DebugFormat("UpdateMsTermMain() - End get termMain for update. Result = {0}", dataTermMain);

            dataTermMain.BFAmount = input.BFAmount;
            dataTermMain.remarks = input.remarks;

            try
            {
                Logger.DebugFormat("UpdateMsTermMain() - Start Update TermMain. Parameters sent: {0} " +
            "   TermMainID = {1}{0}" +
            "   BFAmount = {2}{0}" +
            "   remarks = {3}{0}"
            , Environment.NewLine, input.Id, input.BFAmount, input.remarks);

                _msTermMainRepo.Update(dataTermMain);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("UpdateMsTermMain() - End Update TermMain.");
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                Logger.DebugFormat("UpdateMsTermMain() - ERROR DataException. Result = {0}", exDb.Message);
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                Logger.DebugFormat("UpdateMsTermMain() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }

            Logger.InfoFormat("UpdateMsTermMain() - Finished");
        }

        public void UpdateMsTerm(UpdateMsTermInput input)
        {
            Logger.InfoFormat("UpdateMsTerm() Started");

            Logger.DebugFormat("UpdateMsTerm() - Start get term for update. Parameters sent: {0} " +
            "   TermID = {1}{0}", Environment.NewLine, input.id);
            var getTerm = (from x in _msTermRepo.GetAll()
                           where x.Id == input.id
                           select x).FirstOrDefault();
            Logger.DebugFormat("UpdateMsTerm() - End get term for update. Result = {0}", getTerm);
            var dataTerm = getTerm.MapTo<MS_Term>();

            dataTerm.termNo = input.termNo;
            dataTerm.sortNo = input.sortNo;
            dataTerm.PPJBDue = input.PPJBDue;
            dataTerm.remarks = input.remarks;
            dataTerm.termInstallment = input.termInstallment;
            dataTerm.DPCalcType = input.DPCalcType;
            dataTerm.isActive = input.isActive;

            try
            {
                Logger.DebugFormat("UpdateMsTerm() - Start update term. Parameters sent: {0} " +
                    "termNo = {1}{0}" +
                    "sortNo = {7}{0}" +
                    "PPJBDue = {2}{0}" +
                    "remarks = {3}{0}" +
                    "termInstallment = {4}{0}" +
                    "DPCalcType = {5}{0}" +
                    "isActive = {6}{0}"
                , Environment.NewLine, input.termNo, input.PPJBDue, input.remarks,
                            input.termInstallment, input.DPCalcType, input.isActive, input.sortNo);
                _msTermRepo.Update(dataTerm);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("UpdateMsTerm() - End update term.");
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                Logger.DebugFormat("UpdateMsTerm() - ERROR DataException. Result = {0}", exDb.Message);
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                Logger.DebugFormat("UpdateMsTerm() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
            Logger.InfoFormat("UpdateMsTerm() - Finished.");
        }

        public void UpdateMsTermPmt(UpdateMsTermPmtInput input)
        {
            Logger.InfoFormat("UpdateMsTermPmt() Started.");

            Logger.DebugFormat("UpdateMsTerm() - Start get termPmt for update. Parameters sent: {0} " +
                "TermPmtID = {1}{0}", Environment.NewLine, input.Id);
            var getTermPmt = (from x in _msTermPmtRepo.GetAll()
                              where x.Id == input.Id
                              select x).FirstOrDefault();

            var dataTermPmt = getTermPmt.MapTo<MS_TermPmt>();
            Logger.DebugFormat("UpdateMsTerm() - End get termPmt for update. Result: {0} ", dataTermPmt);

            dataTermPmt.termNo = input.termNo;
            dataTermPmt.finTypeID = input.finTypeID;
            dataTermPmt.finStartDue = input.finStartDue;

            try
            {
                Logger.DebugFormat("UpdateMsTerm() - Start Update termPmt. Parameters sent: {0} " +
                    "termNo = {1}{0}",
                    "finTypeID = {2}{0}" +
                    "finStartDue = {3}{0}" +
                    Environment.NewLine, input.Id, input.termNo, input.finTypeID);
                _msTermPmtRepo.Update(dataTermPmt);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("UpdateMsTerm() - End Update termPmt.");
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                Logger.DebugFormat("UpdateMsTerm() - ERROR DataException. Result = {0}", exDb.Message);
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                Logger.DebugFormat("UpdateMsTerm() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }

            Logger.InfoFormat("UpdateMsTerm() - Finished.");
        }

        public void CreateOrUpdateMsTermDP(UpdateMsTermDPInput input)
        {
            Logger.InfoFormat("CreateOrUpdateMsTermDP() Started");
            if (input.Id != 0)
            {
                Logger.InfoFormat("UpdateMsTermDP() Started");

                Logger.DebugFormat("UpdateMsTermDP() - Start get data TermDP for update. Parameters sent: {0} " +
                    "   termDPID = {1}{0}"
                    , Environment.NewLine, input.Id);

                var getTermDP = (from A in _msTermDPRepo.GetAll()
                                 where A.Id == input.Id
                                 select A).FirstOrDefault();

                var updateMsTermDP = getTermDP.MapTo<MS_TermDP>();
                Logger.DebugFormat("UpdateMsTermDP() - End  get data TermDP for update. Result = {0}", updateMsTermDP);

                updateMsTermDP.DPNo = input.DPNo;
                updateMsTermDP.daysDue = input.daysDue;
                updateMsTermDP.DPPct = input.DPPct;
                updateMsTermDP.DPAmount = input.DPAmount;
                updateMsTermDP.daysDueNewKP = input.daysDueNewKP;

                try
                {
                    Logger.DebugFormat("UpdateMsTermDP() - Start update TermDP. Parameters sent: {0} " +
                       "DPNo = {1}{0}" +
                       "daysDue = {2}{0}" +
                       "DPPct = {3}{0}" +
                       "DPAmount = {4}{0}" +
                       "daysDueNewKP = {5}{0}"
                       , Environment.NewLine, input.DPNo, input.daysDue, input.DPPct, input.DPAmount, input.daysDueNewKP);

                    _msTermDPRepo.Update(updateMsTermDP);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("UpdateMsTermDP() - End  getupdate TermDP");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.DebugFormat("UpdateMsTermDP() - ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.DebugFormat("UpdateMsTermDP() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }

                Logger.InfoFormat("UpdateMsTermDP() - Finished");
            }
            else
            {
                Logger.InfoFormat("CreateMsTermDP() Started");
                var dataCreateMsTermDP = new CreateMsTermDPInput
                {
                    entityCode = "1",
                    termCode = input.termCode,
                    termNo = input.termNo,
                    daysDue = input.daysDue,
                    DPNo = input.DPNo,
                    DPPct = input.DPPct,
                    DPAmount = input.DPAmount,
                    daysDueNewKP = input.daysDueNewKP,
                    termID = input.termID
                };

                try
                {
                    Logger.DebugFormat("CreateMsTermDP() - Start Insert TermDP. Parameters sent: {0} " +
                        "entityCode = {1}{0}" +
                        "termCode = {2}{0}" +
                        "termNo = {3}{0}" +
                        "daysDue = {4}{0}" +
                        "DPNo = {5}{0}" +
                        "DPPct = {6}{0}" +
                        "DPAmount = {7}{0}" +
                        "daysDueNewKP = {8}{0}" +
                        "termID = {9}{0}"
                        , Environment.NewLine, 1, input.termCode, input.termNo, input.daysDue, input.DPNo, input.DPPct,
                        input.DPAmount, input.daysDueNewKP, input.termID);
                    CreateMsTermDP(dataCreateMsTermDP);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("CreateMsTermDP() - End Insert TermDP.");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.DebugFormat("CreateMsTermDP() - ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.DebugFormat("CreateMsTermDP() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }

                Logger.InfoFormat("CreateMsTermDP() - Finished");
            }
            Logger.InfoFormat("CreateOrUpdateMsTermDP() - Finished");

        }

        public void UpdateMsTermAddDisc(UpdateMsTermAddDiscInput input)
        {
            Logger.InfoFormat("UpdateMsTermAddDisc()  Started");

            Logger.DebugFormat("UpdateMsTermAddDisc() - Start get data TermAddDisc for update. Parameters sent: {0} " +
                "entityTermAddDiscID = {1}{0}", Environment.NewLine, 1, input.Id);
            var getTermAddDisc = (from A in _msTermAddDiscRepo.GetAll()
                                  where A.Id == input.Id
                                  select A).FirstOrDefault();
            var updateMsTermAddDisc = getTermAddDisc.MapTo<MS_TermAddDisc>();
            Logger.DebugFormat("UpdateMsTermAddDisc() - End  get data TermAddDisc for update. Result = {0}", updateMsTermAddDisc);

            updateMsTermAddDisc.addDiscNo = input.addDiscNo;
            updateMsTermAddDisc.addDiscAmt = input.addDiscAmt;
            updateMsTermAddDisc.addDiscPct = input.addDiscPct;
            updateMsTermAddDisc.discountID = input.discountID;

            try
            {
                Logger.DebugFormat("UpdateMsTermAddDisc() - Start update TermAddDisc. Parameters sent: {0} " +
                "addDiscNo  = {1}{0}" +
                "addDiscAmt = {2}{0}" +
                "addDiscPct = {3}{0}" +
                "discountID = {4}{0}"
                , Environment.NewLine, 1, input.addDiscNo, input.addDiscAmt, input.addDiscPct, input.discountID);
                _msTermAddDiscRepo.Update(updateMsTermAddDisc);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("UpdateMsTermAddDisc() - End update TermAddDisc.");
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                Logger.DebugFormat("UpdateMsTermAddDisc() - ERROR DataException. Result = {0}", exDb.Message);
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                Logger.DebugFormat("UpdateMsTermAddDisc() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
            Logger.InfoFormat("UpdateMsTermAddDisc() - Finished");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterTerm_Edit)]
        public void UpdateUniversalMsTerm(UpdateUniversalMsTermInput input)
        {
            Logger.InfoFormat("UpdateUniversalMsTerm() Started.");

            Logger.DebugFormat("UpdateUniversalMsTerm() - Start get data term. Parameters sent: {0} " +
                "termMainID = {1}{0}", Environment.NewLine, input.termMainID);
            var dataMsTerm = (from A in _msTermMainRepo.GetAll()
                              where A.Id == input.termMainID
                              select A).FirstOrDefault();
            Logger.DebugFormat("UpdateUniversalMsTerm() - End get data term. Result = {0}", dataMsTerm);

            var dataTermMain = new UpdateMsTermMainInput
            {
                Id = input.termMainID,
                BFAmount = input.BFAmount,
                remarks = input.remarks
            };
            Logger.DebugFormat("UpdateUniversalMsTerm() - Start update termMain. Parameters sent: {0} " +
                "termMainID = {1}{0}" +
                "BFAmount = {2}{0}" +
                "remarks = {3}{0}", Environment.NewLine, input.termMainID, input.BFAmount, input.remarks);
            UpdateMsTermMain(dataTermMain);
            Logger.DebugFormat("UpdateUniversalMsTerm() - End update termMain.");

            foreach (var term in input.setValue)
            {
                foreach (var dp in term.DtoDP)
                {
                    if (dp.termDPID == 0)
                    {
                        Logger.DebugFormat("UpdateUniversalMsTerm() - Start get TermDPID. Parameters sent: {0} " +
                            "termID = {1}{0}", Environment.NewLine, term.termID);
                        var getTermDPID = (from x in _msTermDPRepo.GetAll()
                                           where x.termID == term.termID
                                           select x.Id).ToList();
                        Logger.DebugFormat("UpdateUniversalMsTerm() - End get TermDPID.");

                        foreach (var item in getTermDPID)
                        {
                            Logger.DebugFormat("UpdateUniversalMsTerm() - Start delete TermDPID. Parameters sent: {0} " +
                                 "TermDPID = {1}{0}", Environment.NewLine, item);
                            _msTermDPRepo.Delete(item);
                            Logger.DebugFormat("UpdateUniversalMsTerm() - End delete TermDPID.");
                        }

                    }
                }
            }

            foreach (var term in input.setValue)
            {
                var dataTerm = new UpdateMsTermInput
                {
                    id = term.termID,
                    termNo = term.termNo,
                    sortNo = term.sortNo,
                    DPCalcType = term.DPCalcType,
                    remarks = term.remarks,
                    termInstallment = term.termInstallment,
                    PPJBDue = term.PPJBDue,
                    projectID = input.projectID,
                    isActive = term.isActive,
                    termMainID = input.termMainID
                };
                Logger.DebugFormat("UpdateUniversalMsTerm() - Start Update MsTerm. Parameters sent: {0} " +
                    "id = {1}{0}" +
                    "termNo = {2}{0}" +
                    "sortNo = {10}{0}" +
                    "DPCalcType = {3}{0}" +
                    "remarks = {4}{0}" +
                    "termInstallment = {5}{0}" +
                    "PPJBDue = {6}{0}" +
                    "projectID = {7}{0}" +
                    "isActive = {8}{0}" +
                    "termMainID = {9}{0}",
                    Environment.NewLine, term.termID, term.termNo, term.DPCalcType, term.remarks, term.termInstallment, term.PPJBDue,
                     input.projectID, term.isActive, input.termMainID, term.sortNo);
                UpdateMsTerm(dataTerm);
                Logger.DebugFormat("UpdateUniversalMsTerm() - End Update MsTerm.");

                var dataTermPmt = new UpdateMsTermPmtInput
                {
                    Id = term.termPmtID,
                    termNo = term.termNo,
                    finTypeID = term.finTypeID,
                    finTypeCode = term.finTypeCode,
                    finStartDue = term.finStartDue
                };
                Logger.DebugFormat("UpdateUniversalMsTerm() - Start Update MsTermPmt. Parameters sent: {0} " +
                    "Id = {1}{0}" +
                    "termNo = {2}{0}" +
                    "finTypeID = {3}{0}" +
                    "finTypeCode = {4}{0}" +
                    "finStartDue = {5}{0}",
                    Environment.NewLine, term.termPmtID, term.termNo, term.finTypeID, term.finTypeCode, term.finStartDue);
                UpdateMsTermPmt(dataTermPmt);
                Logger.DebugFormat("UpdateUniversalMsTerm() - End Update MsTermPmt.");

                foreach (var dp in term.DtoDP)
                {
                    var dataTermDP = new UpdateMsTermDPInput
                    {
                        entityCode = "1",
                        termCode = dataMsTerm.termCode,
                        termNo = term.termNo,
                        termID = term.termID,
                        Id = dp.termDPID,
                        daysDue = dp.daysDue,
                        DPNo = dp.DPNo,
                        DPPct = dp.DPPct,
                        DPAmount = dp.DPAmount,
                        daysDueNewKP = dp.daysDueNewKP
                    };
                    Logger.DebugFormat("UpdateUniversalMsTerm() - Start Create Or Update MsTermDP. Parameters sent: {0} " +
                        "entityCode = {1}{0}" +
                        "termCode = {2}{0}" +
                        "termNo = {3}{0}" +
                        "termID = {4}{0}" +
                        "Id = {5}{0}" +
                        "daysDue = {6}{0}" +
                        "DPNo = {7}{0}" +
                        "DPPct = {8}{0}" +
                        "DPAmount = {9}{0}" +
                        "daysDueNewKP = {10}{0}"
                        , Environment.NewLine, "1", dataMsTerm.termCode, term.termNo, term.termID, dp.termDPID,
                         dp.daysDue, dp.DPNo, dp.DPPct, dp.DPAmount, dp.daysDueNewKP);
                    CreateOrUpdateMsTermDP(dataTermDP);
                    Logger.DebugFormat("UpdateUniversalMsTerm() - End Create Or Update MsTermDP.");
                }

                foreach (var disc in term.DtoDisc)
                {
                    if (disc != null)
                    {
                        var dataTermAddDisc = new UpdateMsTermAddDiscInput
                        {
                            Id = disc.termAddDiscID,
                            addDiscNo = disc.addDiscNo,
                            addDiscPct = disc.addDiscPct,
                            addDiscAmt = disc.addDiscAmt,
                            discountID = disc.discountID
                        };
                        Logger.DebugFormat("UpdateUniversalMsTerm() - Start Update MsTermAddDisc. Parameters sent: {0} " +
                            "Id = {1}{0}" +
                            "addDiscNo = {2}{0}" +
                            "addDiscPct = {3}{0}" +
                            "addDiscAmt = {4}{0}" +
                            "discountID = {5}{0}"
                            , Environment.NewLine, disc.termAddDiscID, disc.addDiscNo, disc.addDiscPct, disc.addDiscAmt, disc.discountID);
                        UpdateMsTermAddDisc(dataTermAddDisc);
                        Logger.DebugFormat("UpdateUniversalMsTerm() - End Update MsTermAddDisc.");
                    }
                    else
                    {
                        Logger.DebugFormat("UpdateUniversalMsTerm() - Term Add Disc Is Empty.");
                    }
                }
            }

            Logger.InfoFormat("UpdateUniversalMsTerm() - Finished.");
        }
        //public async Task<PagedResultDto<GetAllMsTermListDto>> GetAllMsTerm(GetTermListInput input)
        //{
        //var listResult = (from term in _msTermRepo.GetAll()
        //                  join project in _msProjectRepo.GetAll() on term.projectCode equals project.projectCode
        //                  select new GetAllMsTermListDto
        //                  {
        //                      termId = term.Id,
        //                      termName = term.termName,
        //                      termCode = term.termCode,
        //                      projectName = project.projectName,
        //                      projectCode = project.projectCode,
        //                      installmentTerm = term.installmentTerm,
        //                      isActive = term.isActive
        //                  });

        //int dataCount = await listResult.AsQueryable().CountAsync();

        //var resultList = await listResult.AsQueryable()
        //    .OrderBy(input.Sorting)
        //    .PageBy(input)
        //    .ToListAsync();

        //return new PagedResultDto<GetAllMsTermListDto>(
        //    dataCount,
        //    resultList);

        //}

        //public void DeleteMsTerm(int Id)
        //{
        //    //var check = _msCityRepo.GetAll().Where(x => x.TerritoryID == ID).FirstOrDefault();

        //    if (true)
        //    {
        //        _msTermRepo.Delete(Id);
        //    }
        //    else
        //    {
        //        throw new UserFriendlyException("This term is used in transaction !");
        //    }
        //}

        //public void UpdateMsTerm(UpdateMsTermInputDto input)
        //{
        //        bool checkCode = (from A in _msTermRepo.GetAll()
        //                          where A.Id != input.termID
        //                          && A.termCode == input.termCode
        //                          select A).Any();

        //        if (!checkCode)
        //        {
        //            var getMsTerm = (from A in _msTermRepo.GetAll()
        //                             where input.termID == A.Id
        //                             select A).FirstOrDefault();

        //            var update = getMsTerm.MapTo<MS_Term>();

        //            //update.termName = input.termName;
        //            update.termCode = input.termCode;
        //            update.isActive = input.isActive;
        //            //update.installmentTerm = input.installmentTerm;
        //            //update.projectCode = input.projectCode;

        //            _msTermRepo.Update(update);
        //        }
        //        else
        //        {
        //            throw new UserFriendlyException("The Term Already Inserted !");
        //        }
        //}
    }
}
