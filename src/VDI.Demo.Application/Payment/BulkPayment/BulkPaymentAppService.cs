using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.Payment.BulkPayment.Dto;
using VDI.Demo.Payment.InputPayment;
using VDI.Demo.Payment.InputPayment.Dto;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PSAS.Schedule;
using VDI.Demo.PSAS.Schedule.Dto;
using VDI.Demo.TAXDB;

namespace VDI.Demo.Payment.BulkPayment
{
    public class BulkPaymentAppService : DemoAppServiceBase, IBulkPaymentAppService
    {
        private readonly IRepository<LK_Alloc> _lkAllocRepo;
        private readonly IRepository<LK_PayFor> _lkPayForRepo;
        private readonly IRepository<LK_PayType> _lkPayTypeRepo;
        private readonly IRepository<LK_OthersType> _lkOthersTypeRepo;
        private readonly IRepository<SYS_RolesPayFor> _sysRolesPayForRepo;
        private readonly IRepository<SYS_RolesPayType> _sysRolesPayTypeRepo;
        private readonly IRepository<SYS_RolesOthersType> _sysRolesOthersTypeRepo;
        private readonly PropertySystemDbContext _contextPropertySystem;
        private readonly AccountingDbContext _contextAccounting;
        private readonly TAXDbContext _contextTAX;
        private readonly PersonalsNewDbContext _contextPersonals;
        private readonly DemoDbContext _contextEngine3;
        private readonly IRepository<TR_PaymentHeader> _trPaymentHeaderRepo;
        private readonly IRepository<TR_BookingHeader> _trBookingHeaderRepo;
        private readonly IRepository<TR_PaymentDetail> _trPaymentDetailRepo;
        private readonly IRepository<TR_PaymentDetailAlloc> _trPaymentDetailAllocRepo;
        private readonly IRepository<TR_PaymentBulk> _trPaymentBulkRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Area> _msAreaRepo;
        private readonly IRepository<MS_Category> _msCategoryRepo;
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<TR_BookingDetail> _trBookingDetailRepo;
        private readonly IRepository<TR_BookingDetailSchedule> _trBookingDetailScheduleRepo;
        private readonly IRepository<MS_Company> _msCompanyRepo;
        private readonly IRepository<MS_Account> _msAccountRepo;
        private readonly IInputPaymentAppService _iInputPaymentAppService;
        private readonly IPSASScheduleAppService _iPSASScheduleAppService;


        public BulkPaymentAppService(
            IRepository<LK_Alloc> lkAllocRepo,
            IRepository<LK_PayFor> lkPayForRepo,
            IRepository<LK_PayType> lkPayTypeRepo,
            IRepository<LK_OthersType> lkOthersTypeRepo,
            IRepository<SYS_RolesPayFor> sysRolesPayForRepo,
            IRepository<SYS_RolesPayType> sysRolesPayTypeRepo,
            IRepository<SYS_RolesOthersType> sysRolesOthersTypeRepo,
            PropertySystemDbContext contextPropertySystem,
            AccountingDbContext contextAccounting,
            TAXDbContext contextTAX,
            PersonalsNewDbContext contextPersonals,
            DemoDbContext contextEngine3, 
            IRepository<TR_PaymentHeader> trPaymentHeaderRepo,
            IRepository<TR_BookingHeader> trBookingHeaderRepo,
            IRepository<TR_PaymentDetail> trPaymentDetailRepo,
            IRepository<TR_PaymentDetailAlloc> trPaymentDetailAllocRepo,
            IRepository<TR_PaymentBulk> trPaymentBulkRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<MS_UnitCode> msUnitCodeRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<MS_Area> msAreaRepo,
            IRepository<MS_Category> msCategoryRepo,
            IRepository<MS_Cluster> msClusterRepo,
            IRepository<TR_BookingDetail> trBookingDetailRepo,
            IRepository<TR_BookingDetailSchedule> trBookingDetailScheduleRepo,
            IRepository<MS_Company> msCompanyRepo,
            IRepository<MS_Account> msAccountRepo,
            IInputPaymentAppService iInputPaymentAppService,
            IPSASScheduleAppService iPSASScheduleAppService
            )
        {
            _lkAllocRepo = lkAllocRepo;
            _lkPayForRepo = lkPayForRepo;
            _lkPayTypeRepo = lkPayTypeRepo;
            _lkOthersTypeRepo = lkOthersTypeRepo;
            _sysRolesPayForRepo = sysRolesPayForRepo;
            _sysRolesPayTypeRepo = sysRolesPayTypeRepo;
            _sysRolesOthersTypeRepo = sysRolesOthersTypeRepo;
            _contextPropertySystem = contextPropertySystem;
            _contextAccounting = contextAccounting;
            _contextTAX = contextTAX;
            _contextPersonals = contextPersonals;
            _contextEngine3 = contextEngine3;
            _trPaymentHeaderRepo = trPaymentHeaderRepo;
            _trBookingHeaderRepo = trBookingHeaderRepo;
            _trPaymentDetailRepo = trPaymentDetailRepo;
            _trPaymentDetailAllocRepo = trPaymentDetailAllocRepo;
            _trPaymentBulkRepo = trPaymentBulkRepo;
            _msUnitRepo = msUnitRepo;
            _msUnitCodeRepo = msUnitCodeRepo;
            _msProjectRepo = msProjectRepo;
            _msAreaRepo = msAreaRepo;
            _msCategoryRepo = msCategoryRepo;
            _msClusterRepo = msClusterRepo;
            _trBookingDetailRepo = trBookingDetailRepo;
            _trBookingDetailScheduleRepo = trBookingDetailScheduleRepo;
            _msCompanyRepo = msCompanyRepo;
            _msAccountRepo = msAccountRepo;
            _iInputPaymentAppService = iInputPaymentAppService;
            _iPSASScheduleAppService = iPSASScheduleAppService;
        }

        public List<GetDataCheckUploadExcelListDto> CheckDataUploadExcelBulk(List<CheckDataUploadExcelBulkInputDto> input)
        {
            List<GetDataCheckUploadExcelListDto> listResult = new List<GetDataCheckUploadExcelListDto>();
            var result = new GetDataCheckUploadExcelListDto();

            foreach (var dataInput in input)
            {
                var getCoCode = (from a in _msAccountRepo.GetAll()
                                 where a.Id == dataInput.accID
                                 select a.devCode).FirstOrDefault();

                //check input an bookCode ada di tabel apa nggak
                var checkBookCode = (from a in _trBookingHeaderRepo.GetAll()
                                     join b in _trBookingDetailRepo.GetAll() on a.Id equals b.bookingHeaderID
                                     where a.bookCode == dataInput.bookCode && b.coCode == getCoCode
                                     group b by new
                                     {
                                         b.bookingHeaderID,
                                         a.bookCode,
                                         b.pctTax,
                                         a.psCode,
                                         a.unitID
                                     } into G
                                     select new
                                     {
                                         G.Key.bookingHeaderID,
                                         G.Key.bookCode,
                                         G.Key.pctTax,
                                         G.Key.psCode,
                                         G.Key.unitID
                                     }).FirstOrDefault();

                if (checkBookCode != null)
                {

                    //get personal by bookCode
                    var getPersonal = (from a in _contextPersonals.PERSONAL.ToList()
                                       where a.psCode == checkBookCode.psCode
                                       select new
                                       {
                                           a.name
                                       }).FirstOrDefault();

                    //get role by user id
                    var getRoleId = (from A in _contextEngine3.UserRoles.ToList()
                                     where A.UserId == dataInput.userID
                                     select A.RoleId).ToList();

                    //get list pay for by role
                    var getPayForCheckRole = (from A in _sysRolesPayForRepo.GetAll()
                                              join B in _lkPayForRepo.GetAll() on A.payForID equals B.Id
                                              where B.isActive == true && getRoleId.Contains(A.rolesID)
                                              orderby B.payForCode
                                              select B.payForCode).ToList();

                    //get list pay type by role
                    var getPayTypeCheckRole = (from A in _sysRolesPayTypeRepo.GetAll()
                                               join B in _lkPayTypeRepo.GetAll() on A.payTypeID equals B.Id
                                               where B.isActive == true && getRoleId.Contains(A.rolesID)
                                               orderby B.payTypeCode
                                               select B.payTypeCode).ToList();

                    //get list others type by role
                    var getOthersTypeCheckRole = (from a in _sysRolesOthersTypeRepo.GetAll()
                                                  join b in _lkOthersTypeRepo.GetAll() on a.othersTypeID equals b.Id
                                                  where getRoleId.Contains(a.rolesID) && b.isActive == true
                                                  select new
                                                  {
                                                      b.Id,
                                                      b.isAdjSAD,
                                                      b.isOthers,
                                                      b.isOTP,
                                                      b.isPayment,
                                                      b.isSDH,
                                                      b.othersTypeCode
                                                  })
                                      .WhereIf(dataInput.payForCode.Contains("PMT"), item => item.isPayment == true)
                                      .WhereIf(dataInput.payForCode.Contains("ADJ"), item => item.isAdjSAD == true)
                                      .WhereIf(dataInput.payForCode.Contains("OTP"), item => item.isOTP == true)
                                      .WhereIf(dataInput.payForCode.Contains("OTH"), item => item.isOthers == true)
                                      .Select(x => x.othersTypeCode)
                                      .ToList();

                    if (getPayForCheckRole.Contains(dataInput.payForCode) && getPayTypeCheckRole.Contains(dataInput.payTypeCode) && getOthersTypeCheckRole.Contains(dataInput.othersTypeCode))
                    {

                        //get pay for id
                        var getPayForId = (from a in _lkPayForRepo.GetAll()
                                           where a.payForCode == dataInput.payForCode
                                           select a.Id).FirstOrDefault();

                        //get pay type id
                        var getPayTypeId = (from a in _lkPayTypeRepo.GetAll()
                                            where a.payTypeCode == dataInput.payTypeCode
                                            select a.Id).FirstOrDefault();

                        //get others type id
                        var getOthersTypeId = (from a in _lkOthersTypeRepo.GetAll()
                                               where a.othersTypeCode == dataInput.othersTypeCode
                                               select a.Id).FirstOrDefault();

                        //get unit code dan unit no
                        var getUnit = (from a in _msUnitRepo.GetAll()
                                       join b in _msUnitCodeRepo.GetAll() on a.unitCodeID equals b.Id
                                       where a.Id == checkBookCode.unitID
                                       select new
                                       {
                                           b.unitCode,
                                           a.unitNo
                                       }).FirstOrDefault();

                        //get booking detail schedule
                        var getDataSceduleInput = new GetDataSchedulePaymentInputDto
                        {
                            accountID = dataInput.accID,
                            bookingHeaderID = checkBookCode.bookingHeaderID,
                            payForID = getPayForId
                        };

                        var getTrBookingDetailSchedule = _iInputPaymentAppService.GetDataSchedulePayment(getDataSceduleInput);

                        //get schedule dengan outstanding != 0
                        var getDataScheduleOutNoNol = (from a in getTrBookingDetailSchedule
                                                       where a.netOutstanding != 0 && a.VATOutstanding != 0
                                                       select new GetDataSchedule
                                                       {
                                                           schedNo = a.schedNo,
                                                           allocDesc = a.allocCode,
                                                           allocID = a.allocID,
                                                           amount = a.netOutstanding + a.VATOutstanding
                                                       }).ToList();

                        //hasil

                        result = new GetDataCheckUploadExcelListDto
                        {
                            bookingHeaderID = checkBookCode.bookingHeaderID,
                            bookCode = checkBookCode.bookCode,
                            pctTax = checkBookCode.pctTax,
                            psCode = checkBookCode.psCode,
                            name = getPersonal.name,
                            payForID = getPayForId,
                            payForCode = dataInput.payForCode,
                            payTypeID = getPayTypeId,
                            payTypeCode = dataInput.payTypeCode,
                            othersTypeCode = dataInput.othersTypeCode,
                            othersTypeID = getOthersTypeId,
                            unitID = checkBookCode.unitID,
                            unitCode = getUnit.unitCode,
                            unitNo = getUnit.unitNo,
                            dataSchedule = getDataScheduleOutNoNol,
                            message = null
                        };
                    }
                    else
                    {
                        result = new GetDataCheckUploadExcelListDto
                        {
                            bookCode = dataInput.bookCode,
                            payForCode = dataInput.payForCode,
                            payTypeCode = dataInput.payTypeCode,
                            othersTypeCode = dataInput.othersTypeCode,
                            message = "You don't have access in your PayForCode or PayTypeCode or OthersTypeCode"
                        };
                    }

                }
                else
                {
                    result = new GetDataCheckUploadExcelListDto
                    {
                        bookCode = dataInput.bookCode,
                        payForCode = dataInput.payForCode,
                        payTypeCode = dataInput.payTypeCode,
                        othersTypeCode = dataInput.othersTypeCode,
                        message = "BookCode Not Found"
                    };
                }
                listResult.Add(result);
            }
            return listResult;
        }

        public void CreateTrPaymentBulk(CreateTrPaymentBulkInputDto input)
        {
            Logger.Info("CreateTrPaymentBulk() - Started.");

            var data = new TR_PaymentBulk
            {
                bulkPaymentKey      = input.bulkPaymentKey,
                bookingHeaderID     = input.bookingHeaderID,
                unitID              = input.unitID,
                payForID            = input.payForID,
                payTypeID           = input.payTypeID,
                othersTypeID        = input.othersTypeID,
                psCode              = input.psCode,
                name                = input.name,
                clearDate           = input.clearDate,
                amount              = input.amount
            };

            try
            {
                Logger.DebugFormat("CreateTrPaymentBulk() - Start insert TR Payment Bulk. Parameters sent:{0}" +
                    "bulkPaymentKey     = {1}{0}" +
                    "bookingHeaderID    = {2}{0}" +
                    "unitID             = {3}{0}" +
                    "payForID           = {4}{0}" +
                    "payTypeID          = {5}{0}" +
                    "othersTypeID       = {6}{0}" +
                    "psCode             = {7}{0}" +
                    "name               = {8}{0}" +
                    "clearDate          = {9}{0}" +
                    "amount             = {10}{0}"
                    , Environment.NewLine, input.bulkPaymentKey, input.bookingHeaderID, input.unitID, input.payForID, input.payTypeID, input.othersTypeID
                    , input.psCode, input.name, input.clearDate, input.amount);

                _trPaymentBulkRepo.Insert(data);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("CreateTrPaymentBulk() - Ended insert TR Payment Bulk.");

                Logger.Info("CreateTrPaymentBulk() - Finished.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreateTrPaymentBulk() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateTrPaymentBulk() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        public void CreateUniversalBulkPayment(List<CreateUniversalBulkPaymentInputDto> input)
        {
            Logger.Info("CreateUniversalBulkPayment() - Started.");
            
            var transNo = new JObject();
            List<int> listPayTypeID = new List<int>();
            List<string> listOthersTypeCode = new List<string>();

            foreach (var dataInput in input)
            {

                int payNo = 0;

                var accCode = (from A in _msAccountRepo.GetAll() where A.Id == dataInput.accID select A.accCode).FirstOrDefault();
                var bookCode = (from A in _trBookingHeaderRepo.GetAll() where A.Id == dataInput.bookingHeaderID select A.bookCode).FirstOrDefault();
                var coCode = (from A in _msAccountRepo.GetAll() where A.Id == dataInput.accID select A.devCode).FirstOrDefault();
                var payForCode = (from A in _lkPayForRepo.GetAll() where A.Id == dataInput.payForID select A.payForCode).FirstOrDefault();


                var dataInputTransNo = new GenerateTransNoInputDto
                {
                    accID = dataInput.accID,
                    entityID = 1
                };

                transNo = _iPSASScheduleAppService.GenerateTransNo(dataInputTransNo);

                #region createTrPaymentHeader

                var combineCode = (from A in _trBookingDetailRepo.GetAll()
                                   join B in _msCompanyRepo.GetAll() on A.coCode equals B.coCode
                                   join C in _msAccountRepo.GetAll() on B.Id equals C.coID
                                   where A.bookingHeaderID == dataInput.bookingHeaderID && C.Id == dataInput.accID
                                   select A.combineCode).FirstOrDefault();

                var dataInputPaymentHeader = new CreatePaymentHeaderInputDto
                {
                    entityID = 1,
                    accountID = dataInput.accID,
                    bookingHeaderID = dataInput.bookingHeaderID == null ? null : dataInput.bookingHeaderID,
                    clearDate = dataInput.clearDate,
                    combineCode = combineCode == null ? "1" : combineCode, //wait
                    ket = "-",
                    payForID = dataInput.payForID,
                    paymentDate = DateTime.Now,
                    transNo = transNo.GetValue("transNo").ToString(),
                    controlNo = transNo.GetValue("transNo").ToString(),
                    isSms = false,
                    hadMail = false
                };

                int paymentHeaderId = _iInputPaymentAppService.CreateTrPaymentHeader(dataInputPaymentHeader);

                #endregion

                foreach(var dataInputPayment in dataInput.dataForPayment)
                {
                    var checkMsMapping = (from a in _contextAccounting.MS_Mapping.ToList()
                                          join b in _contextPropertySystem.LK_PayFor on a.payForCode equals b.payForCode
                                          join c in _contextPropertySystem.LK_PayType on a.payTypeCode equals c.payTypeCode
                                          where b.Id == dataInput.payForID && c.Id == dataInputPayment.payTypeID && a.othersTypeCode == dataInputPayment.othersTypeCode
                                          select a).FirstOrDefault();

                    #region createTrPaymentBulk

                    var dataCreateTrPaymentBulk = new CreateTrPaymentBulkInputDto
                    {
                        bulkPaymentKey = dataInput.bookCode + "#" + dataInput.psCode + "#" + dataInput.clearDate.Date + "#" + dataInputPayment.othersTypeCode,
                        bookingHeaderID = dataInput.bookingHeaderID,
                        clearDate = dataInput.clearDate,
                        psCode = dataInput.psCode,
                        name = dataInput.name,
                        payForID = dataInput.payForID,
                        payTypeID = dataInputPayment.payTypeID,
                        othersTypeID = dataInputPayment.othersTypeID,
                        unitID = dataInput.unitID,
                        amount = dataInputPayment.amount
                    };
                    CreateTrPaymentBulk(dataCreateTrPaymentBulk);

                    #endregion

                    #region createTrPaymentDetail
                    
                    payNo++;

                    var dataInsertPaymentDetail = new CreatePaymentDetailInputDto
                    {
                        bankName = "-",
                        chequeNo = "-",
                        dueDate = dataInput.clearDate,
                        entityID = 1,
                        ket = dataInput.name,
                        othersTypeCode = dataInputPayment.othersTypeCode,
                        paymentHeaderID = paymentHeaderId,
                        payNo = payNo,
                        payTypeID = dataInputPayment.payTypeID,
                        status = "C"
                    };

                    int paymentDetailId = _iInputPaymentAppService.CreateTrPaymentDetail(dataInsertPaymentDetail);


                    #region createTrPaymentDetailAlloc

                    foreach (var dataAlloc in dataInputPayment.dataAllocList)
                    {
                        var dataInputDetailAlloc = new CreatePaymentDetailAllocInputDto
                        {
                            entityID = 1,
                            netAmt = dataAlloc.amount == 0 ? dataAlloc.amountPerSchedNo / (decimal)(1 + dataInput.pctTax) : (dataAlloc.amountPerSchedNo - dataAlloc.amount) / (decimal)(1 + dataInput.pctTax),
                            vatAmt = dataAlloc.amount == 0 ? (dataAlloc.amountPerSchedNo / (decimal)(1 + dataInput.pctTax)) * (decimal)dataInput.pctTax : ((dataAlloc.amountPerSchedNo - dataAlloc.amount) / (decimal)(1 + dataInput.pctTax)) * (decimal)dataInput.pctTax,
                            paymentDetailID = paymentDetailId,
                            schedNo = dataAlloc.schedNo
                        };
                        _iInputPaymentAppService.CreateTrPaymentDetailAlloc(dataInputDetailAlloc);
                    }

                    #endregion

                    #region Accounting
                    //if (checkMsMapping != null)
                    //{
                    //    var dataInputJournalCode = new GenerateJurnalInputDto
                    //    {
                    //        accCode = accCode,
                    //        bookCode = bookCode,
                    //        coCode = coCode,
                    //        transNo = transNo.GetValue("transNo").ToString(),
                    //        payNo = payNo
                    //    };

                    //    var journalCode = _iInputPaymentAppService.GenerateJurnalCode(dataInputJournalCode);

                    //    #region createTrPaymentDetailJournal

                    //    var dataToInsertTRPaymentDetailJournal = new CreateAccountingTrPaymentDetailJournalInputDto
                    //    {
                    //        accCode = accCode,
                    //        bookCode = bookCode,
                    //        entityCode = "1",
                    //        payNo = payNo,
                    //        transNo = transNo.GetValue("transNo").ToString(),
                    //        journalCode = journalCode.GetValue("journalCode").ToString()
                    //    };

                    //    _iInputPaymentAppService.CreateAccountingTrPaymentDetailJournal(dataToInsertTRPaymentDetailJournal);

                    //    #endregion

                    //    #region createTrJournal

                    //    var getMsJournal = (from a in _contextAccounting.MS_JournalType.ToList()
                    //                        where a.journalTypeCode == checkMsMapping.journalTypeCode
                    //                        select new
                    //                        {
                    //                            a.COACodeFIN,
                    //                            a.amtTypeCode,
                    //                            a.ACCAlloc
                    //                        }).ToList();

                    //    foreach (var dataJournal in getMsJournal)
                    //    {
                    //        decimal debit = 0;
                    //        decimal kredit = 0;
                    //        if (dataJournal.ACCAlloc < 0)
                    //        {
                    //            if (dataJournal.amtTypeCode == "1")
                    //            {
                    //                debit = 0;
                    //                kredit = dataInputPayment.amount;
                    //            }

                    //            else if (dataJournal.amtTypeCode == "2")
                    //            {
                    //                debit = 0;
                    //                kredit = dataInputPayment.amount / (1 + (decimal)dataInput.pctTax);
                    //            }

                    //            else if (dataJournal.amtTypeCode == "3")
                    //            {
                    //                debit = 0;
                    //                kredit = (dataInputPayment.amount / (1 + (decimal)dataInput.pctTax)) * (decimal)dataInput.pctTax;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (dataJournal.amtTypeCode == "1")
                    //            {
                    //                debit = dataInputPayment.amount;
                    //                kredit = 0;
                    //            }

                    //            if (dataJournal.amtTypeCode == "2")
                    //            {
                    //                debit = dataInputPayment.amount / (1 + (decimal)dataInput.pctTax);
                    //                kredit = 0;
                    //            }

                    //            if (dataJournal.amtTypeCode == "3")
                    //            {
                    //                debit = (dataInputPayment.amount / (1 + (decimal)dataInput.pctTax)) * (decimal)dataInput.pctTax;
                    //                kredit = 0;
                    //            }
                    //        }

                    //        var dataToInsertTrJournal = new CreateTrJournalInputDto
                    //        {
                    //            COACodeAcc = "-",
                    //            COACodeFIN = dataJournal.COACodeFIN,
                    //            entityCode = "1",
                    //            journalCode = journalCode.GetValue("journalCode").ToString(),
                    //            journalDate = DateTime.Now,
                    //            debit = debit,
                    //            kredit = kredit,
                    //            remarks = "-",
                    //        };

                    //        _iInputPaymentAppService.CreateTrJournal(dataToInsertTrJournal);
                    //    }

                    //    #endregion
                    //}

                    #endregion

                    #endregion

                    listPayTypeID.Add(dataInputPayment.payTypeID);
                    listOthersTypeCode.Add(dataInputPayment.othersTypeCode);
                }

                #region TAX

            //    var payTypeCode = (from A in _lkPayTypeRepo.GetAll() where listPayTypeID.Contains(A.Id) select A.payTypeCode).ToList();

            //    var getPaymentHeaderForUpdate = (from a in _trPaymentHeaderRepo.GetAll()
            //                                     where a.Id == paymentHeaderId
            //                                     select a).FirstOrDefault();


            //    List<string> listCheckPayForCode = new List<string>
            //{
            //    "PMT", "OTP"
            //};

            //    List<string> listCheckPayTypeCode = new List<string>
            //{
            //    "CSH", "CRE", "GRO", "STN", "TRN", "CHQ", "DBT", "ADJ", "ADB", "ADC", "VRT"
            //};

            //    List<string> listCheckOthersTypeCode = new List<string>
            //{
            //    "ALH", "PMT", "ALI", "KPR", "BFE", "LHL", "DEP", "AD1", "AD2", "ADR", "BNK", "BPS", "LHA", "PAF"
            //};

            //    var updatePaymentHeader = getPaymentHeaderForUpdate.MapTo<TR_PaymentHeader>();

            //    if (listCheckPayForCode.Contains(payForCode) && listCheckPayTypeCode.Intersect(payTypeCode).Any() && listCheckOthersTypeCode.Intersect(listOthersTypeCode).Any())
            //    {
            //        var checkBatchPajakStock = (from a in _contextTAX.msBatchPajakStock.ToList()
            //                                    where a.CoCode == coCode && a.IsAvailable == true && a.YearPeriod == DateTime.Now.Year.ToString()
            //                                    orderby a.BatchID
            //                                    select a).FirstOrDefault();

            //        decimal totalAmountAll = dataInput.dataForPayment.Sum(x => x.amount);

            //        //done
            //        if (checkBatchPajakStock != null)
            //        {
            //            var FPCode = "010." + checkBatchPajakStock.FPBranchCode + "-" + checkBatchPajakStock.FPYear + "." + checkBatchPajakStock.FPNo;

            //            var dataToInsertTrFpHeader = new CreateTAXTrFPHeaderInputDto
            //            {
            //                accCode = accCode,
            //                coCode = coCode,
            //                entityCode = "1",
            //                discAmount = 0,
            //                DPAmount = 0,
            //                FPBranchCode = checkBatchPajakStock.FPBranchCode,
            //                unitPriceAmt = 0,
            //                userAddress = input.address,
            //                FPTransCode = "01",
            //                FPStatCode = "0",
            //                FPYear = checkBatchPajakStock.FPYear,
            //                FPNo = checkBatchPajakStock.FPNo,
            //                FPType = "1",
            //                transDate = DateTime.Now,
            //                unitCode = dataInput.unitCode,
            //                unitNo = input.unitNo,
            //                sourceCode = "PSY",
            //                priceType = "1",
            //                transNo = input.transNo,
            //                rentalCode = "-",
            //                paymentCode = "-",
            //                payNo = 0,
            //                pmtBatchNo = "-",
            //                FPCode = FPCode,
            //                unitPriceVat = 0,
            //                vatAmt = (totalAmountAll / (1 + (decimal)dataInput.pctTax)) * (decimal)dataInput.pctTax, //netAmt * pctTax
            //                name = dataInput.name,
            //                NPWP = input.NPWP,
            //                psCode = dataInput.psCode
            //            };

            //            CreateTAXTrFPHeader(dataToInsertTrFpHeader);

            //            var unitName = (from a in _msUnitCodeRepo.GetAll()
            //                            where a.unitCode == input.unitCode
            //                            select new { a.unitName, a.Id }).FirstOrDefault();

            //            var unitNo = (from a in _msUnitRepo.GetAll()
            //                          where a.unitCodeID == unitName.Id
            //                          select a.unitNo).ToString();

            //            var dataToInsertTrFpDetail = new CreateTAXTrFPDetailInputDto
            //            {
            //                coCode = coCode,
            //                entityCode = "1",
            //                FPCode = FPCode,
            //                transNo = 1,
            //                currencyCode = "Rp",
            //                currencyRate = 1,
            //                transDesc = "Lantai " + unitName.unitName + " No. " + unitNo + " (" + input.transNo + ")",
            //                transAmount = totalAmountAll / 1 + (decimal)dataInput.pctTax
            //            };

            //            CreateTAXTrFPDetail(dataToInsertTrFpDetail);

            //            var getBatchPajakStock = (from a in _contextTAX.msBatchPajakStock.ToList()
            //                                      where a.BatchID == checkBatchPajakStock.BatchID
            //                                      select a).FirstOrDefault();

            //            var updateBatchPajakStock = getBatchPajakStock.MapTo<msBatchPajakStock>();
            //            updateBatchPajakStock.IsAvailable = false;
            //            _contextTAX.msBatchPajakStock.Update(updateBatchPajakStock);

            //            updatePaymentHeader.isFP = "1";
            //            _trPaymentHeaderRepo.Update(updatePaymentHeader);
            //        }
            //        //kehabisan FP
            //        else
            //        {
            //            updatePaymentHeader.isFP = "2";
            //            _trPaymentHeaderRepo.Update(updatePaymentHeader);
            //        }
            //    }
            //    //no TAX
            //    else
            //    {
            //        updatePaymentHeader.isFP = "3";
            //        _trPaymentHeaderRepo.Update(updatePaymentHeader);
            //    }

                #endregion

                #region updateBookingSchedule

                var getDataShedule = (from a in _trBookingDetailScheduleRepo.GetAll()
                                      join b in _trBookingDetailRepo.GetAll() on a.bookingDetailID equals b.Id
                                      join c in _msAccountRepo.GetAll() on b.coCode equals c.devCode
                                      join d in _lkAllocRepo.GetAll() on a.allocID equals d.Id
                                      where b.bookingHeaderID == dataInput.bookingHeaderID && c.Id == dataInput.accID && d.payForID == dataInput.payForID
                                      select a).ToList();

                var getDataSheduleGroupSchedNo = (from a in getDataShedule
                                                  group a by new
                                                  {
                                                      a.schedNo
                                                  } into G
                                                  select new
                                                  {
                                                      netAmt = G.Sum(x => x.netAmt),
                                                      vatAmt = G.Sum(x => x.vatAmt),
                                                      G.Key.schedNo
                                                  }).ToList();

                var getTotalAmount = (from x in _trBookingDetailRepo.GetAll()
                                      where x.bookingHeaderID == dataInput.bookingHeaderID
                                      group x by new { x.bookingHeaderID } into G
                                      select new
                                      {
                                          bookHeaderID = G.Key.bookingHeaderID,
                                          TotalNetNetPrice = G.Sum(d => d.netNetPrice)
                                      }).FirstOrDefault();

                foreach (var dataScheduleGroupSchedNo in getDataSheduleGroupSchedNo)
                {
                    var getDataSchedulePerSchedNo = (from a in getDataShedule
                                                     where a.schedNo == dataScheduleGroupSchedNo.schedNo
                                                     select a).ToList();

                    foreach (var dataSchedulePerSchedNo in getDataSchedulePerSchedNo)
                    {
                        var getPercentage = (from x in _trBookingDetailRepo.GetAll()
                                             where x.bookingHeaderID == dataInput.bookingHeaderID && x.Id == dataSchedulePerSchedNo.bookingDetailID
                                             select new
                                             {
                                                 x.bookingHeaderID,
                                                 netNetPrice = x.netNetPrice / getTotalAmount.TotalNetNetPrice
                                             }).FirstOrDefault();

                        var getDataAmtForUpdate = (from a in dataInput.dataScheduleList
                                                   where a.schedNo == dataSchedulePerSchedNo.schedNo
                                                   select new
                                                   {
                                                       netOut = a.amount == 0 ? a.amount * getPercentage.netNetPrice : (a.amount / (decimal)(1 + dataInput.pctTax)) * getPercentage.netNetPrice,
                                                       vatOut = a.amount == 0 ? a.amount * getPercentage.netNetPrice : ((a.amount / (decimal)(1 + dataInput.pctTax)) * (decimal)dataInput.pctTax) * getPercentage.netNetPrice,
                                                   }).FirstOrDefault();

                        if (getDataAmtForUpdate != null)
                        {
                            var updateBookingSchedule = dataSchedulePerSchedNo.MapTo<TR_BookingDetailSchedule>();

                            updateBookingSchedule.netOut = getDataAmtForUpdate.netOut;
                            updateBookingSchedule.vatOut = getDataAmtForUpdate.vatOut;

                            _trBookingDetailScheduleRepo.Update(updateBookingSchedule);
                        }
                    }
                }
                #endregion

            }


            _contextAccounting.SaveChanges();

            Logger.Info("CreateUniversalBulkPayment() - Finished.");
        }
    }
}
