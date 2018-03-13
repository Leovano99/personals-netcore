using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Payment.InputPayment.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB.LippoMaster;
using System.Linq;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using Abp.Linq.Extensions;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.Domain.Uow;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using System.Data;
using Abp.UI;
using VDI.Demo.PSAS.Schedule;
using VDI.Demo.PSAS.Schedule.Dto;
using VDI.Demo.AccountingDB;
using VDI.Demo.TAXDB;
using Abp.AutoMapper;
using Newtonsoft.Json.Linq;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.MasterPlan.Unit.MS_Units.Dto;
using Microsoft.AspNetCore.Mvc;
using VDI.Demo.Dto;
using System.Net.Http;
using System.Net.Http.Headers;
using Abp.AspNetZeroCore.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using VDI.Demo.Configuration;
using VDI.Demo.Payment.PaymentLK_PayFor;
using VDI.Demo.Payment.PaymentLK_PayType;
using VDI.Demo.Payment.PaymentLK_OthersType;

namespace VDI.Demo.Payment.InputPayment
{
    public class InputPaymentAppService : DemoAppServiceBase, IInputPaymentAppService
    {
        private readonly IRepository<LK_Alloc> _lkAllocRepo;
        private readonly IRepository<LK_PayFor> _lkPayForRepo;
        private readonly IRepository<PERSONALS, string> _personalsRepo;
        private readonly IRepository<TR_PaymentHeader> _trPaymentHeaderRepo;
        private readonly IRepository<TR_BookingHeader> _trBookingHeaderRepo;
        private readonly IRepository<PERSONALS_MEMBER, string> _personalMemberRepo;
        private readonly IRepository<TR_PaymentDetail> _trPaymentDetailRepo;
        private readonly IRepository<TR_PaymentDetailAlloc> _trPaymentDetailAllocRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;
        private readonly IRepository<TR_Phone, string> _trPhoneRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Area> _msAreaRepo;
        private readonly IRepository<MS_Category> _msCategoryRepo;
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<TR_BookingDetail> _trBookingDetailRepo;
        private readonly IRepository<TR_BookingDetailSchedule> _trBookingDetailScheduleRepo;
        private readonly IRepository<PropertySystemDB.MasterPlan.Unit.MS_City> _msCityRepo;
        private readonly IRepository<TR_Address, string> _trAddressRepo;
        private readonly IRepository<LK_AddrType, string> _lkAddrTypeRepo;
        private readonly IRepository<LK_Item> _lkItemRepo;
        private readonly IRepository<MS_Company> _msCompanyRepo;
        private readonly IRepository<MS_Account> _msAccountRepo;
        private readonly IPSASScheduleAppService _iPSASScheduleAppService;
        private readonly IRepository<TR_PaymentDetailJournal, string> _trPaymentDetailJournalRepo;
        private readonly IRepository<TR_Journal, string> _trJournalRepo;
        private readonly IRepository<FP_TR_FPHeader, string> _trFPHeaderRepo;
        private readonly IRepository<FP_TR_FPDetail, string> _trFPDetailRepo;
        private readonly IRepository<MS_Mapping, string> _msMappingRepo;
        private readonly IRepository<MS_JournalType, string> _msJournalTypeRepo;
        private readonly IRepository<LK_PayType> _lkPayTypeRepo;
        private readonly IRepository<msBatchPajakStock, string> _msBatchPajakStockRepo;
        private readonly PropertySystemDbContext _contextPropertySystem;
        private readonly AccountingDbContext _contextAccounting;
        private readonly TAXDbContext _contextTAX;
        private readonly PersonalsNewDbContext _contextPersonals;
        private readonly DemoDbContext _contextEngine3;
        private readonly IRepository<SYS_FinanceCounter> _sysFinanceCounterRepo;
        //private readonly IConfigurationRoot _appConfiguration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public InputPaymentAppService(
            IRepository<LK_Alloc> lkAllocRepo,
            IRepository<LK_PayFor> lkPayForRepo,
            IRepository<PERSONALS, string> personalsRepo,
            IRepository<TR_PaymentHeader> trPaymentHeaderRepo,
            IRepository<TR_BookingHeader> trBookingHeaderRepo,
            IRepository<PERSONALS_MEMBER, string> personalMemberRepo,
            IRepository<TR_PaymentDetail> trPaymentDetailRepo,
            IRepository<TR_PaymentDetailAlloc> trPaymentDetailAllocRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<MS_UnitCode> msUnitCodeRepo,
            IRepository<TR_Phone, string> trPhoneRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<MS_Area> msAreaRepo,
            IRepository<MS_Category> msCategoryRepo,
            IRepository<MS_Cluster> msClusterRepo,
            IRepository<TR_BookingDetail> trBookingDetailRepo,
            IRepository<TR_BookingDetailSchedule> trBookingDetailScheduleRepo,
            IRepository<PropertySystemDB.MasterPlan.Unit.MS_City> msCityRepo,
            IRepository<TR_Address, string> trAddressRepo,
            IRepository<LK_AddrType, string> lkAddrTypeRepo,
            IRepository<LK_Item> lkItemRepo,
            IRepository<MS_Company> msCompanyRepo,
            IRepository<MS_Account> msAccountRepo,
            IPSASScheduleAppService iPSASScheduleAppService,
            IRepository<TR_PaymentDetailJournal, string> trPaymentDetailJournalRepo,
            IRepository<TR_Journal, string> trJournalRepo,
            IRepository<FP_TR_FPHeader, string> trFPHeaderRepo,
            IRepository<FP_TR_FPDetail, string> trFPDetailRepo,
            IRepository<MS_Mapping, string> msMappingRepo,
            IRepository<MS_JournalType, string> msJournalTypeRepo,
            IRepository<LK_PayType> lkPayTypeRepo,
            IRepository<msBatchPajakStock, string> msBatchPajakStockRepo,
            PropertySystemDbContext contextPropertySystem,
            AccountingDbContext contextAccounting,
            TAXDbContext contextTAX,
            PersonalsNewDbContext contextPersonals,
            DemoDbContext contextEngine3,
            IRepository<SYS_FinanceCounter> sysFinanceCounterRepo,
            IHostingEnvironment hostingEnvironment
            )
        {
            _lkAllocRepo = lkAllocRepo;
            _lkPayForRepo = lkPayForRepo;
            _lkPayTypeRepo = lkPayTypeRepo;
            _personalsRepo = personalsRepo;
            _trPaymentHeaderRepo = trPaymentHeaderRepo;
            _trBookingHeaderRepo = trBookingHeaderRepo;
            _personalMemberRepo = personalMemberRepo;
            _trPaymentDetailRepo = trPaymentDetailRepo;
            _trPaymentDetailAllocRepo = trPaymentDetailAllocRepo;
            _msUnitRepo = msUnitRepo;
            _msUnitCodeRepo = msUnitCodeRepo;
            _trPhoneRepo = trPhoneRepo;
            _msProjectRepo = msProjectRepo;
            _msAreaRepo = msAreaRepo;
            _msCategoryRepo = msCategoryRepo;
            _msClusterRepo = msClusterRepo;
            _trBookingDetailRepo = trBookingDetailRepo;
            _trBookingDetailScheduleRepo = trBookingDetailScheduleRepo;
            _msCityRepo = msCityRepo;
            _trAddressRepo = trAddressRepo;
            _lkAddrTypeRepo = lkAddrTypeRepo;
            _lkItemRepo = lkItemRepo;
            _msCompanyRepo = msCompanyRepo;
            _msAccountRepo = msAccountRepo;
            _iPSASScheduleAppService = iPSASScheduleAppService;
            _trPaymentDetailJournalRepo = trPaymentDetailJournalRepo;
            _trJournalRepo = trJournalRepo;
            _trFPHeaderRepo = trFPHeaderRepo;
            _trFPDetailRepo = trFPDetailRepo;
            _msMappingRepo = msMappingRepo;
            _msJournalTypeRepo = msJournalTypeRepo;
            _msBatchPajakStockRepo = msBatchPajakStockRepo;
            _contextPropertySystem = contextPropertySystem;
            _contextAccounting = contextAccounting;
            _contextTAX = contextTAX;
            _contextPersonals = contextPersonals;
            _contextEngine3 = contextEngine3;
            _sysFinanceCounterRepo = sysFinanceCounterRepo;
            _hostingEnvironment = hostingEnvironment;
            //_appConfiguration = hostingEnvironment.GetAppConfiguration();
        }

        public List<GetDataPersonalsListDto> GetDataLookupPersonals(string filter)
        {
            List<GetDataPersonalsListDto> dataResult = new List<GetDataPersonalsListDto>();

            var getDataPersonals = (from p in _personalsRepo.GetAll()
                                    join ph in _trPhoneRepo.GetAll() on p.psCode equals ph.psCode into l1
                                    from ph in l1.DefaultIfEmpty()
                                    where p.name.Contains(filter) || ph.number.Contains(filter)
                                    select new GetDataPersonalsListDto
                                    {
                                        psCode = p.psCode,
                                        name = p.name,
                                        birthDate = p.birthDate,
                                        age = p.birthDate == null ? 0 : DateTime.Now.Year - p.birthDate.Value.Year
                                    })
                                    .OrderBy(x => x.name)
                                    .ToList();

            foreach (var dataPersonal in getDataPersonals)
            {
                var getPhone = (from ph in _trPhoneRepo.GetAll()
                                where ph.psCode == dataPersonal.psCode
                                select
                                    ph.number
                                )
                                .ToList();

                var dataToPush = new GetDataPersonalsListDto
                {
                    psCode = dataPersonal.psCode,
                    name = dataPersonal.name,
                    birthDate = dataPersonal.birthDate,
                    age = dataPersonal.age,
                    phoneNo = getPhone
                };

                dataResult.Add(dataToPush);

            }

            if (dataResult.Any())
            {
                return dataResult;
            }
            else
            {
                throw new UserFriendlyException("Please Fill Search First !");
            }

        }

        public List<GetDataBookCodeListDto> GetDataBookCode(GetDataBookCodeInputDto input)
        {
            var getDataBookCode = (from a in _trBookingHeaderRepo.GetAll()
                                   join b in _msUnitRepo.GetAll() on a.unitID equals b.Id
                                   join c in _msUnitCodeRepo.GetAll() on b.unitCodeID equals c.Id
                                   join d in _trBookingDetailRepo.GetAll() on a.Id equals d.bookingHeaderID
                                   join e in _msAccountRepo.GetAll() on d.coCode equals e.devCode
                                   where input.listProject.Contains(b.projectID) && e.Id == input.accountID
                                   select new
                                   {
                                       bookingHeaderID = a.Id,
                                       a.bookCode,
                                       a.bookDate,
                                       a.cancelDate,
                                       c.unitCode,
                                       b.unitNo,
                                       b.projectID,
                                       a.psCode,
                                       unitID = b.Id,
                                       d.pctTax
                                   })
                                   .WhereIf(input.projectID != 0, item => item.projectID == input.projectID)
                                   .WhereIf(input.startDate != null, item => item.bookDate.Date >= input.startDate.Value.Date)
                                   .WhereIf(input.endDate != null, item => item.bookDate.Date <= input.endDate.Value.Date)
                                   .WhereIf(!input.bookCode.IsNullOrWhiteSpace(), item => item.bookCode.Equals(input.bookCode))
                                   .WhereIf(!input.psCode.IsNullOrWhiteSpace(), item => item.psCode == input.psCode)
                                   .WhereIf(input.unitID != 0, item => item.unitID == input.unitID)
                                   .Select(x => new GetDataBookCodeListDto
                                   {
                                       bookingHeaderID = x.bookingHeaderID,
                                       bookCode = x.bookCode,
                                       bookDate = x.bookDate,
                                       cancelDate = x.cancelDate,
                                       unitCode = x.unitCode,
                                       unitNo = x.unitNo,
                                       projectID = x.projectID,
                                       pctTax = x.pctTax
                                   })
                                   .OrderByDescending(x => x.bookDate)
                                   .Distinct()
                                   .ToList();

            if (getDataBookCode.Any())
            {
                return getDataBookCode;
            }
            else
            {
                throw new UserFriendlyException("BookCode not found!");
            }
        }

        [UnitOfWork(isTransactional: false)]
        public List<GetDataTransNoListDto> GetDataLookupTransNo(GetDataTransNoInputDto input)
        {
            var getDataPersonal = _personalsRepo.GetAllList();
            var getDataTransNo = (from ph in _trPaymentHeaderRepo.GetAll()
                                  join bh in _trBookingHeaderRepo.GetAll() on ph.bookingHeaderID equals bh.Id into l1
                                  from bh in l1.DefaultIfEmpty()
                                  join p in getDataPersonal on bh.psCode equals p.psCode
                                  join pd in _trPaymentDetailRepo.GetAll() on ph.Id equals pd.paymentHeaderID
                                  join pda in _trPaymentDetailAllocRepo.GetAll() on pd.Id equals pda.paymentDetailID
                                  join pf in _lkPayForRepo.GetAll() on ph.payForID equals pf.Id
                                  join u in _msUnitRepo.GetAll() on bh.unitID equals u.Id
                                  join uc in _msUnitCodeRepo.GetAll() on u.unitCodeID equals uc.Id
                                  where input.accID == ph.accountID
                                  group pda by new
                                  {
                                      paymentHeaderID = ph.Id,
                                      ph.paymentDate,
                                      bookingHeaderID = bh.Id,
                                      ph.transNo,
                                      bh.bookCode,
                                      ph.payForID,
                                      pf.payForName,
                                      unitID = u.Id,
                                      u.unitCodeID,
                                      u.unitNo,
                                      uc.unitCode,
                                      p.name,
                                      p.remarks

                                  } into G
                                  select new GetDataTransNoListDto
                                  {
                                      paymentHeaderID = G.Key.paymentHeaderID,
                                      transNo = G.Key.transNo,
                                      payForID = G.Key.payForID,
                                      payFor = G.Key.payForName,
                                      payDate = G.Key.paymentDate,
                                      bookingHeaderID = G.Key.bookingHeaderID,
                                      bookCode = G.Key.bookCode,
                                      unitID = G.Key.unitID,
                                      unitCodeID = G.Key.unitCodeID,
                                      unitNo = G.Key.unitNo,
                                      unitCode = G.Key.unitCode,
                                      clientName = G.Key.name,
                                      remarks = G.Key.remarks,
                                      amount = G.Sum(x => x.netAmt) + G.Sum(x => x.vatAmt)
                                  })
                                  .WhereIf(input.payForID != 0, item => item.payForID == input.payForID)
                                  .WhereIf(input.payForID == 0, item => !item.payFor.Contains("UnKnown") )
                                  .OrderByDescending(x => x.payDate)
                                  .ToList();

            return getDataTransNo;
        }
        public List<GetDropdownUnitNoListDto> GetDropdownUnitNoByUnitCodeID(int unitCodeID)
        {
            var getDropdownUnitNo = (from a in _msUnitRepo.GetAll()
                                     where a.unitCodeID == unitCodeID
                                     orderby a.unitNo ascending
                                     select new GetDropdownUnitNoListDto
                                     {
                                         unitID = a.Id,
                                         unitNo = a.unitNo
                                     }).ToList();

            return getDropdownUnitNo;
        }

        public List<GetDropdownUnitCodeListDto> GetDropdownUnitCode(GetDataUnitCodeInputDto input)
        {
            var getDropdownunitCode = (from a in _msUnitCodeRepo.GetAll()
                                       join b in _msUnitRepo.GetAll() on a.Id equals b.unitCodeID
                                       orderby a.unitName ascending
                                       where input.listProject.Contains(b.projectID)
                                       select new 
                                       {
                                           unitCodeID = a.Id,
                                           a.unitCode,
                                           a.unitName,
                                           b.projectID
                                       })
                                       .WhereIf(input.projectID != 0, item => item.projectID == input.projectID)
                                       .Select(x => new GetDropdownUnitCodeListDto
                                       {
                                           unitCodeID = x.unitCodeID,
                                           unitCode = x.unitCode,
                                           unitName = x.unitName
                                       })
                                       .Distinct()
                                       .ToList();
            return getDropdownunitCode;
        }

        [UnitOfWork(isTransactional: false)]
        public GetDataClientInfoListDto GetClientInfo(int bookingHeaderID)
        {
            var result = new GetDataClientInfoListDto();

            var getDataBooking = (from a in _trBookingHeaderRepo.GetAll()
                                  where a.Id == bookingHeaderID
                                  select a).FirstOrDefault();

            var getDataPersonal = (from a in _contextPersonals.PERSONAL.ToList()
                                   join c in _contextPersonals.TR_Address.ToList() on a.psCode equals c.psCode into address
                                   from e in address.DefaultIfEmpty()
                                   where a.psCode == getDataBooking.psCode
                                   select new 
                                   {
                                       psCode = a.psCode,
                                       name = a.name,
                                       address = e == null ? null : e.address,
                                       addrType = e == null ? null : e.addrType,
                                       NPWP = a.NPWP
                                   }).FirstOrDefault();

            if (getDataPersonal != null)
            {
                var dataPhone = (from a in _contextPersonals.TR_Phone.ToList()
                                 where a.psCode == getDataPersonal.psCode
                                 select new GetPhone
                                 {
                                     phone = a == null ? null : a.number
                                 }).ToList();

                if(getDataPersonal.address != null)
                {
                    var dataAddrType = (from a in _contextPersonals.TR_Address.ToList()
                                        join b in _contextPersonals.LK_AddrType.ToList() on a.addrType equals b.addrType into addrType
                                        from f in addrType.DefaultIfEmpty()
                                        where f.addrTypeName.Contains("Corres")
                                        select new
                                        {
                                            address = a == null ? null : a.address
                                        }).FirstOrDefault();

                    result = new GetDataClientInfoListDto
                    {
                        psCode = getDataPersonal.psCode,
                        name = getDataPersonal.name,
                        NPWP = getDataPersonal.NPWP,
                        address = dataAddrType.address,
                        listPhone = dataPhone
                    };

                }
                else
                {
                    result = new GetDataClientInfoListDto
                    {
                        psCode = getDataPersonal.psCode,
                        name = getDataPersonal.name,
                        NPWP = getDataPersonal.NPWP,
                        address = null,
                        listPhone = dataPhone
                    };
                }
            }

            return result;
        }

        public GetDataUnitInfoListDto GetDataUnitInfo(int bookingHeaderID)
        {
            var getDataUnitInfo = (from bh in _trBookingHeaderRepo.GetAll()
                                   join u in _msUnitRepo.GetAll() on bh.unitID equals u.Id
                                   join uc in _msUnitCodeRepo.GetAll() on u.unitCodeID equals uc.Id
                                   join p in _msProjectRepo.GetAll() on u.projectID equals p.Id
                                   join a in _msAreaRepo.GetAll() on u.areaID equals a.Id
                                   join c in _msCategoryRepo.GetAll() on u.categoryID equals c.Id
                                   join cl in _msClusterRepo.GetAll() on u.clusterID equals cl.Id
                                   join ci in _msCityRepo.GetAll() on a.cityID equals ci.Id
                                   where bh.Id == bookingHeaderID
                                   select new GetDataUnitInfoListDto
                                   {
                                       project = p.projectName + " (" + ci.cityName + ")",
                                       category = c.categoryName + " (" + cl.clusterCode + ")",
                                       unitCode = uc.unitCode,
                                       unitNo = u.unitNo
                                   }).FirstOrDefault();

            return getDataUnitInfo;
        }

        public List<GetDataSchedulePaymentListDto> GetDataSchedulePayment(GetDataSchedulePaymentInputDto input)
        {
            var dataSchedules = (from x in _trBookingDetailScheduleRepo.GetAll()
                                 join bd in _trBookingDetailRepo.GetAll() on x.bookingDetailID equals bd.Id
                                 join bh in _trBookingHeaderRepo.GetAll() on bd.bookingHeaderID equals bh.Id
                                 join ac in _msAccountRepo.GetAll() on bd.coCode equals ac.devCode
                                 join a in _lkAllocRepo.GetAll() on x.allocID equals a.Id
                                 where bh.Id == input.bookingHeaderID && a.payForID == input.payForID && ac.Id == input.accountID
                                 orderby x.schedNo
                                 group x by new
                                 {
                                     bd.coCode,
                                     x.schedNo,
                                     x.dueDate,
                                     x.allocID,
                                     x.remarks,
                                     a.allocCode,
                                     bd.pctTax
                                 } into G
                                 select new GetDataSchedulePaymentListDto
                                 {
                                     allocID = G.Key.allocID,
                                     allocCode = G.Key.allocCode,
                                     netAmount = 0,
                                     VATAmount = 0,
                                     netOutstanding = G.Sum(d => d.netOut),
                                     VATOutstanding = G.Sum(d => d.vatOut),
                                     dueDate = G.Key.dueDate,
                                     schedNo = G.Key.schedNo,
                                     pctTax = G.Key.pctTax
                                 }).ToList();

            return dataSchedules;
        }

        public int CreateTrPaymentDetail(CreatePaymentDetailInputDto input)
        {
            Logger.Info("CreateTrPaymentDetail() - Started.");

            var data = new TR_PaymentDetail
            {
                entityID = input.entityID,
                bankName = input.bankName,
                chequeNo = input.chequeNo,
                dueDate = input.dueDate,
                ket = input.ket,
                othersTypeCode = input.othersTypeCode,
                paymentHeaderID = input.paymentHeaderID,
                payNo = input.payNo,
                payTypeID = input.payTypeID,
                status = input.status,
            };

            try
            {
                Logger.DebugFormat("CreateTrPaymentDetail() - Start insert TR Payment Detail. Parameters sent:{0}" +
                    "entityID = {1}{0}" +
                    "bankName = {2}{0}" +
                    "chequeNo = {3}{0}" +
                    "dueDate = {4}{0}" +
                    "othersTypeCode = {5}{0}" +
                    "paymentHeaderID = {6}{0}" +
                    "payNo = {7}{0}" +
                    "payTypeID = {8}{0}" +
                    "status = {9}{0}" +
                    "ket = {10}{0}"
                    , Environment.NewLine, input.entityID, input.bankName, input.chequeNo, input.dueDate, input.othersTypeCode, input.paymentHeaderID, input.payNo, input.payTypeID, input.status, input.ket);

                var paymentDetailID = _trPaymentDetailRepo.InsertAndGetId(data);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("CreateTrPaymentDetail() - Ended insert TR Payment Detail.");
                Logger.Info("CreateTrPaymentDetail() - Finished.");
                return paymentDetailID;

            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreateTrPaymentDetail() - ERROR DataException. Result = {0}", ex.Message);
                Logger.Info("CreateTrPaymentDetail() - Finished.");
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateTrPaymentDetail() - ERROR Exception. Result = {0}", ex.Message);
                Logger.Info("CreateTrPaymentDetail() - Finished.");
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        public void CreateTrPaymentDetailAlloc(CreatePaymentDetailAllocInputDto input)
        {
            Logger.Info("CreateTrPaymentDetailAlloc() - Started.");

            var data = new TR_PaymentDetailAlloc
            {
                entityID = input.entityID,
                netAmt = input.netAmt,
                paymentDetailID = input.paymentDetailID,
                schedNo = input.schedNo,
                vatAmt = input.vatAmt,
            };

            try
            {
                Logger.DebugFormat("CreateTrPaymentDetailAlloc() - Start insert TR Payment Detail Alloc. Parameters sent:{0}" +
                    "entityID        = {1}{0}" +
                    "netAmt          = {2}{0}" +
                    "paymentDetailID = {3}{0}" +
                    "schedNo         = {4}{0}" +
                    "vatAmt          = {5}{0}"
                    , Environment.NewLine, input.entityID, input.netAmt, input.paymentDetailID, input.schedNo, input.vatAmt);

                _trPaymentDetailAllocRepo.Insert(data);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("CreateTrPaymentDetailAlloc() - Ended insert TR Payment Detail Alloc.");
                Logger.Info("CreateTrPaymentDetailAlloc() - Finished.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreateTrPaymentDetailAlloc() - ERROR DataException. Result = {0}", ex.Message);
                Logger.Info("CreateTrPaymentDetailAlloc() - Finished.");
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateTrPaymentDetailAlloc() - ERROR Exception. Result = {0}", ex.Message);
                Logger.Info("CreateTrPaymentDetailAlloc() - Finished.");
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }
        public List<GetDropdownAllocationLkItemListDto> GetDropdownAllocationLkItem(GetDropdownAllocationLkItemInputDto input)
        {
            var getDataLkItem = (from bd in _trBookingDetailRepo.GetAll()
                                 join bh in _trBookingHeaderRepo.GetAll() on bd.bookingHeaderID equals bh.Id
                                 join a in _msAccountRepo.GetAll() on bd.coCode equals a.devCode
                                 join i in _lkItemRepo.GetAll() on bd.itemID equals i.Id
                                 where a.Id == input.accountID && bd.bookingHeaderID == input.bookingHeaderID
                                 select new GetDropdownAllocationLkItemListDto
                                 {
                                     itemID = bd.itemID,
                                     itemCode = i.itemName
                                 }).ToList();

            return getDataLkItem;
        }

        public int CreateTrPaymentHeader(CreatePaymentHeaderInputDto input)
        {
            Logger.Info("CreateTrPaymentHeader() - Started.");

            var data = new TR_PaymentHeader
            {
                accountID = input.accountID == 0 ? 958 : input.accountID,
                bookingHeaderID = input.bookingHeaderID,
                clearDate = input.clearDate,
                combineCode = input.combineCode,
                entityID = input.entityID,
                paymentDate = input.paymentDate,
                transNo = input.transNo,
                payForID = input.payForID,
                controlNo = input.controlNo,
                ket = input.ket,
                isSMS = input.isSms,
                SMSSentTime = input.SMSSentTime,
                hadmail = input.hadMail,
                apvTime = input.apvTime,
                apvUN = input.apvUn,
                mailTime = input.mailTime
            };

            try
            {
                Logger.DebugFormat("CreateTrPaymentHeader() - Start insert TR Payment Header. Parameters sent:{0}" +
                    "entityID = {1}{0}" +
                    "accountID = {2}{0}" +
                    "bookingHeaderID = {3}{0}" +
                    "clearDate = {4}{0}" +
                    "combineCode = {5}{0}" +
                    "paymentDate = {6}{0}" +
                    "transNo = {7}{0}" +
                    "payForID = {8}{0}" +
                    "controlNo = {9}{0}" +
                    "ket = {10}{0}" +
                    "isSMS = {11}{0}" +
                    "SMSSentTime = {12}{0}" +
                    "hadmail = {13}{0}" +
                    "apvTime = {14}{0}" +
                    "apvUN = {15}{0}" +
                    "mailTime = {16}{0}"
                    , Environment.NewLine, input.entityID, input.accountID, input.bookingHeaderID, input.clearDate, input.combineCode, input.paymentDate
                    , input.transNo, input.payForID, input.controlNo, input.ket, input.isSms, input.SMSSentTime, input.hadMail, input.apvTime, input.apvUn, input.mailTime);

                var paymentHeaderID = _trPaymentHeaderRepo.InsertAndGetId(data);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("CreateTrPaymentHeader() - Ended insert TR Payment Header.");

                Logger.Info("CreateTrPaymentHeader() - Finished.");

                return paymentHeaderID;
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreateTrPaymentHeader() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateTrPaymentHeader() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        public void CreateInputPaymentUniversal(CreateInputPaymentUniversalInputDto input)
        {
            Logger.Info("CreateInputPaymentUniversal() - Started.");

            var accCode = (from A in _msAccountRepo.GetAll() where A.Id == input.accountID select A.accCode).FirstOrDefault();
            var bookCode = (from A in _trBookingHeaderRepo.GetAll() where A.Id == input.bookingHeaderID select A.bookCode).FirstOrDefault();
            var coCode = (from A in _msAccountRepo.GetAll() where A.Id == input.accountID select A.devCode).FirstOrDefault();
            var payForCode = (from A in _lkPayForRepo.GetAll() where A.Id == input.payForID select A.payForCode).FirstOrDefault();

            List<int> listPayTypeID = new List<int>();
            List<string> listOthersTypeCode = new List<string>();
            List<string> listPayForCode = new List<string>
            {
                "OTP", "OTH", "UNK", "UOA", "UOP"
            };
            
            #region createPaymentHeader

            var combineCode = (from A in _trBookingDetailRepo.GetAll()
                               join B in _msCompanyRepo.GetAll() on A.coCode equals B.coCode
                               join C in _msAccountRepo.GetAll() on B.Id equals C.coID
                               where A.bookingHeaderID == input.bookingHeaderID && C.Id == input.accountID
                               select A.combineCode).FirstOrDefault();

            var dataInputPaymentHeader = new CreatePaymentHeaderInputDto
            {
                entityID = 1,
                accountID = input.accountID,
                bookingHeaderID = input.bookingHeaderID == null ? null : input.bookingHeaderID,
                clearDate = input.clearDate,
                combineCode = combineCode == null ? "1" : combineCode, //wait
                ket = input.description,
                payForID = input.payForID,
                paymentDate = input.paymentDate,
                transNo = input.transNo,
                controlNo = input.transNo,
                isSms = false,
                hadMail = false
            };

            int paymentHeaderId = CreateTrPaymentHeader(dataInputPaymentHeader);
            #endregion

            #region createPaymentDetail

            foreach (var dataPaymentDetail in input.dataPaymentDetail)
            {
                var dataInsertPaymentDetail = new CreatePaymentDetailInputDto
                {
                    bankName = dataPaymentDetail.bankName,
                    chequeNo = dataPaymentDetail.chequeNo,
                    dueDate = dataPaymentDetail.dueDate,
                    entityID = 1,
                    ket = dataPaymentDetail.ket,
                    othersTypeCode = dataPaymentDetail.othersTypeCode.IsNullOrEmpty() ? "PMT" : dataPaymentDetail.othersTypeCode,
                    paymentHeaderID = paymentHeaderId,
                    payNo = dataPaymentDetail.payNo,
                    payTypeID = dataPaymentDetail.payTypeID,
                    status = "C"
                };

                int paymentDetailId = CreateTrPaymentDetail(dataInsertPaymentDetail);

                var checkMsMapping = (from a in _contextAccounting.MS_Mapping.ToList()
                                      join b in _contextPropertySystem.LK_PayFor on a.payForCode equals b.payForCode
                                      join c in _contextPropertySystem.LK_PayType on a.payTypeCode equals c.payTypeCode
                                      where b.Id == input.payForID && c.Id == dataPaymentDetail.payTypeID && a.othersTypeCode == dataPaymentDetail.othersTypeCode
                                      select a).FirstOrDefault();

                if (listPayForCode.Contains(payForCode))
                {
                    #region createPaymentDetailAlloc
                    
                        var dataInputDetailAlloc = new CreatePaymentDetailAllocInputDto
                        {
                            entityID = 1,
                            netAmt = dataPaymentDetail.totalAmountDetail,
                            vatAmt = 0,
                            paymentDetailID = paymentDetailId,
                            schedNo = 0
                        };
                        CreateTrPaymentDetailAlloc(dataInputDetailAlloc);

                    #endregion

                    #region Accounting
                    if (checkMsMapping != null)
                    {
                        var dataInputJournalCode = new GenerateJurnalInputDto
                        {
                            accCode = accCode,
                            bookCode = bookCode == null ? "-" : bookCode,
                            coCode = coCode,
                            transNo = input.transNo,
                            payNo = dataPaymentDetail.payNo
                        };

                        var journalCode = GenerateJurnalCode(dataInputJournalCode);

                        #region createTrPaymentDetailJournal

                        var dataToInsertTRPaymentDetailJournal = new CreateAccountingTrPaymentDetailJournalInputDto
                        {
                            accCode = accCode,
                            bookCode = bookCode == null ? "-" : bookCode,
                            entityCode = "1",
                            payNo = dataPaymentDetail.payNo,
                            transNo = input.transNo,
                            journalCode = journalCode.GetValue("journalCode").ToString()
                        };

                        CreateAccountingTrPaymentDetailJournal(dataToInsertTRPaymentDetailJournal);

                        #endregion

                        #region createTrJournal

                        var getMsJournal = (from a in _contextAccounting.MS_JournalType.ToList()
                                            where a.journalTypeCode == checkMsMapping.journalTypeCode
                                            select new
                                            {
                                                a.COACodeFIN,
                                                a.amtTypeCode,
                                                a.ACCAlloc
                                            }).ToList();
                        if (getMsJournal.Any())
                        {
                            foreach (var dataJournal in getMsJournal)
                            {
                                decimal debit = 0;
                                decimal kredit = 0;

                                if (dataJournal.ACCAlloc < 0)
                                {
                                    if (dataJournal.amtTypeCode == "1")
                                    {
                                        debit = 0;
                                        kredit = dataPaymentDetail.totalAmountDetail;
                                    }

                                    else if (dataJournal.amtTypeCode == "2")
                                    {
                                        debit = 0;
                                        kredit = dataPaymentDetail.totalAmountDetail / (1 + (decimal)input.pctTax);
                                    }

                                    else if (dataJournal.amtTypeCode == "3")
                                    {
                                        debit = 0;
                                        kredit = (dataPaymentDetail.totalAmountDetail / (1 + (decimal)input.pctTax)) * (decimal)input.pctTax;
                                    }
                                }
                                else
                                {
                                    if (dataJournal.amtTypeCode == "1")
                                    {
                                        debit = dataPaymentDetail.totalAmountDetail;
                                        kredit = 0;
                                    }

                                    if (dataJournal.amtTypeCode == "2")
                                    {
                                        debit = dataPaymentDetail.totalAmountDetail / (1 + (decimal)input.pctTax);
                                        kredit = 0;
                                    }

                                    if (dataJournal.amtTypeCode == "3")
                                    {
                                        debit = (dataPaymentDetail.totalAmountDetail / (1 + (decimal)input.pctTax)) * (decimal)input.pctTax;
                                        kredit = 0;
                                    }
                                }

                                var dataToInsertTrJournal = new CreateTrJournalInputDto
                                {
                                    COACodeAcc = "-",
                                    COACodeFIN = dataJournal.COACodeFIN,
                                    entityCode = "1",
                                    journalCode = journalCode.GetValue("journalCode").ToString(),
                                    journalDate = DateTime.Now,
                                    debit = debit,
                                    kredit = kredit,
                                    remarks = "-",

                                };

                                CreateTrJournal(dataToInsertTrJournal);
                            }
                        }
                        #endregion
                    }

                    #endregion
                }
                else
                {
                    #region createPaymentDetailAlloc

                    foreach (var dataAlloc in dataPaymentDetail.dataAlloc)
                    {
                        var dataInputDetailAlloc = new CreatePaymentDetailAllocInputDto
                        {
                            entityID = 1,
                            netAmt = dataAlloc.netAmt,
                            vatAmt = dataAlloc.vatAmt,
                            paymentDetailID = paymentDetailId,
                            schedNo = dataAlloc.schedNo
                        };
                        CreateTrPaymentDetailAlloc(dataInputDetailAlloc);
                    }

                    #endregion

                    #region Accounting
                    if (checkMsMapping != null)
                    {
                        var dataInputJournalCode = new GenerateJurnalInputDto
                        {
                            accCode = accCode,
                            bookCode = bookCode,
                            coCode = coCode,
                            transNo = input.transNo,
                            payNo = dataPaymentDetail.payNo
                        };

                        var journalCode = GenerateJurnalCode(dataInputJournalCode);

                        #region createTrPaymentDetailJournal

                        var dataToInsertTRPaymentDetailJournal = new CreateAccountingTrPaymentDetailJournalInputDto
                        {
                            accCode = accCode,
                            bookCode = bookCode,
                            entityCode = "1",
                            payNo = dataPaymentDetail.payNo,
                            transNo = input.transNo,
                            journalCode = journalCode.GetValue("journalCode").ToString()
                        };

                        CreateAccountingTrPaymentDetailJournal(dataToInsertTRPaymentDetailJournal);

                        #endregion

                        #region createTrJournal

                        var getMsJournal = (from a in _contextAccounting.MS_JournalType.ToList()
                                            where a.journalTypeCode == checkMsMapping.journalTypeCode
                                            select new
                                            {
                                                a.COACodeFIN,
                                                a.amtTypeCode,
                                                a.ACCAlloc
                                            }).ToList();

                        foreach (var dataJournal in getMsJournal)
                        {
                            decimal debit = 0;
                            decimal kredit = 0;
                            if (dataJournal.ACCAlloc < 0)
                            {
                                if (dataJournal.amtTypeCode == "1")
                                {
                                    debit = 0;
                                    kredit = dataPaymentDetail.totalAmountDetail;
                                }

                                else if (dataJournal.amtTypeCode == "2")
                                {
                                    debit = 0;
                                    kredit = dataPaymentDetail.totalAmountDetail / (1 + (decimal)input.pctTax);
                                }

                                else if (dataJournal.amtTypeCode == "3")
                                {
                                    debit = 0;
                                    kredit = (dataPaymentDetail.totalAmountDetail / (1 + (decimal)input.pctTax)) * (decimal)input.pctTax;
                                }
                            }
                            else
                            {
                                if (dataJournal.amtTypeCode == "1")
                                {
                                    debit = dataPaymentDetail.totalAmountDetail;
                                    kredit = 0;
                                }

                                if (dataJournal.amtTypeCode == "2")
                                {
                                    debit = dataPaymentDetail.totalAmountDetail / (1 + (decimal)input.pctTax);
                                    kredit = 0;
                                }

                                if (dataJournal.amtTypeCode == "3")
                                {
                                    debit = (dataPaymentDetail.totalAmountDetail / (1 + (decimal)input.pctTax)) * (decimal)input.pctTax;
                                    kredit = 0;
                                }
                            }

                            var dataToInsertTrJournal = new CreateTrJournalInputDto
                            {
                                COACodeAcc = "-",
                                COACodeFIN = dataJournal.COACodeFIN,
                                entityCode = "1",
                                journalCode = journalCode.GetValue("journalCode").ToString(),
                                journalDate = DateTime.Now,
                                debit = debit,
                                kredit = kredit,
                                remarks = "-",

                            };

                            CreateTrJournal(dataToInsertTrJournal);
                        }

                        #endregion
                    }

                    #endregion
                }



                listPayTypeID.Add(dataPaymentDetail.payTypeID);
                listOthersTypeCode.Add(dataPaymentDetail.othersTypeCode);
            }

            #endregion

            var getTotalAmount = (from x in _trBookingDetailRepo.GetAll()
                                  where x.bookingHeaderID == input.bookingHeaderID
                                  group x by new { x.bookingHeaderID } into G
                                  select new
                                  {
                                      bookHeaderID = G.Key.bookingHeaderID,
                                      TotalNetNetPrice = G.Sum(d => d.netNetPrice)
                                  }).FirstOrDefault();

            #region TAX

            var payTypeCode = (from A in _lkPayTypeRepo.GetAll() where listPayTypeID.Contains(A.Id) select A.payTypeCode).ToList();

            var getPaymentHeaderForUpdate = (from a in _trPaymentHeaderRepo.GetAll()
                                             where a.Id == paymentHeaderId
                                             select a).FirstOrDefault();


            List<string> listCheckPayForCode = new List<string>
            {
                "PMT", "OTP"
            };

            List<string> listCheckPayTypeCode = new List<string>
            {
                "CSH", "CRE", "GRO", "STN", "TRN", "CHQ", "DBT", "ADJ", "ADB", "ADC", "VRT"
            };

            List<string> listCheckOthersTypeCode = new List<string>
            {
                "ALH", "PMT", "ALI", "KPR", "BFE", "LHL", "DEP", "AD1", "AD2", "ADR", "BNK", "BPS", "LHA", "PAF"
            };

            var updatePaymentHeader = getPaymentHeaderForUpdate.MapTo<TR_PaymentHeader>();

            if (listCheckPayForCode.Contains(payForCode) && listCheckPayTypeCode.Intersect(payTypeCode).Any() && listCheckOthersTypeCode.Intersect(listOthersTypeCode).Any())
            {
                var checkBatchPajakStock = (from a in _contextTAX.msBatchPajakStock.ToList()
                                            where a.CoCode == coCode && a.IsAvailable == true && a.YearPeriod == DateTime.Now.Year.ToString()
                                            orderby a.BatchID
                                            select a).FirstOrDefault();

                decimal totalAmountAll = input.dataPaymentDetail.Sum(x => x.totalAmountDetail);

                //done
                if (checkBatchPajakStock != null)
                {
                    var FPCode = "010." + checkBatchPajakStock.FPBranchCode + "-" + checkBatchPajakStock.FPYear + "." + checkBatchPajakStock.FPNo;

                    var dataToInsertTrFpHeader = new CreateTAXTrFPHeaderInputDto
                    {
                        accCode = accCode,
                        coCode = coCode,
                        entityCode = "1",
                        discAmount = 0,
                        DPAmount = 0,
                        FPBranchCode = checkBatchPajakStock.FPBranchCode,
                        unitPriceAmt = getTotalAmount.TotalNetNetPrice,
                        userAddress = input.address == null ? "-" : input.address,
                        FPTransCode = "01",
                        FPStatCode = "0",
                        FPYear = checkBatchPajakStock.FPYear,
                        FPNo = checkBatchPajakStock.FPNo,
                        FPType = "1",
                        transDate = input.paymentDate,
                        unitCode = input.unitCode,
                        unitNo = input.unitNo,
                        sourceCode = "PSY",
                        priceType = "1",
                        transNo = input.transNo,
                        rentalCode = "-",
                        paymentCode = "-",
                        payNo = 0,
                        pmtBatchNo = "-",
                        FPCode = FPCode,
                        unitPriceVat = (decimal)0.1 * getTotalAmount.TotalNetNetPrice,
                        vatAmt = (totalAmountAll / (1 + (decimal)input.pctTax)) * (decimal)input.pctTax, //netAmt * pctTax
                        name = input.name,
                        NPWP = input.NPWP == null ? "-" : input.NPWP,
                        psCode = input.psCode
                    };

                    CreateTAXTrFPHeader(dataToInsertTrFpHeader);

                    var unitName = (from a in _msUnitCodeRepo.GetAll()
                                    where a.unitCode == input.unitCode
                                    select new { a.unitName, a.Id }).FirstOrDefault();

                    var unitNo = (from a in _msUnitRepo.GetAll()
                                  where a.unitCodeID == unitName.Id
                                  select a.unitNo).ToString();

                    var dataToInsertTrFpDetail = new CreateTAXTrFPDetailInputDto
                    {
                        coCode = coCode,
                        entityCode = "1",
                        FPCode = FPCode,
                        transNo = 1,
                        currencyCode = "Rp",
                        currencyRate = 1,
                        transDesc = "Lantai " + unitName.unitName + " No. " + unitNo + " (" + input.transNo + ")",
                        transAmount = totalAmountAll / (1 + (decimal)input.pctTax)
                    };

                    CreateTAXTrFPDetail(dataToInsertTrFpDetail);

                    var getBatchPajakStock = (from a in _contextTAX.msBatchPajakStock.ToList()
                                              where a.BatchID == checkBatchPajakStock.BatchID
                                              select a).FirstOrDefault();

                    var updateBatchPajakStock = getBatchPajakStock.MapTo<msBatchPajakStock>();
                    updateBatchPajakStock.IsAvailable = false;
                    _contextTAX.msBatchPajakStock.Update(updateBatchPajakStock);

                    updatePaymentHeader.isFP = "1";
                    _trPaymentHeaderRepo.Update(updatePaymentHeader);
                }
                //kehabisan FP
                else
                {
                    updatePaymentHeader.isFP = "2";
                    _trPaymentHeaderRepo.Update(updatePaymentHeader);
                }
            }
            //no TAX
            else
            {
                updatePaymentHeader.isFP = "3";
                _trPaymentHeaderRepo.Update(updatePaymentHeader);
            }

            #endregion

            #region updateBookingSchedule

            if (input.bookingHeaderID != null && payForCode != "OTP")
            {
                var getDataShedule = (from a in _trBookingDetailScheduleRepo.GetAll()
                                      join b in _trBookingDetailRepo.GetAll() on a.bookingDetailID equals b.Id
                                      join c in _msAccountRepo.GetAll() on b.coCode equals c.devCode
                                      join d in _lkAllocRepo.GetAll() on a.allocID equals d.Id
                                      where b.bookingHeaderID == input.bookingHeaderID && c.Id == input.accountID && d.payForID == input.payForID
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
                
                foreach (var dataScheduleGroupSchedNo in getDataSheduleGroupSchedNo)
                {
                    var getDataSchedulePerSchedNo = (from a in getDataShedule
                                                     where a.schedNo == dataScheduleGroupSchedNo.schedNo
                                                     select a).ToList();

                    foreach (var dataSchedulePerSchedNo in getDataSchedulePerSchedNo)
                    {
                        var getPercentage = (from x in _trBookingDetailRepo.GetAll()
                                             where x.bookingHeaderID == input.bookingHeaderID && x.Id == dataSchedulePerSchedNo.bookingDetailID
                                             select new
                                             {
                                                 x.bookingHeaderID,
                                                 netNetPrice = x.netNetPrice / getTotalAmount.TotalNetNetPrice
                                             }).FirstOrDefault();

                        var getDataAmtForUpdate = (from a in input.dataSchedule
                                                   where a.schedNoSchedule == dataSchedulePerSchedNo.schedNo && a.netAmtSchedule != 0
                                                   select new
                                                   {
                                                       netOut = a.netOutSchedule * getPercentage.netNetPrice,
                                                       vatOut = a.vatOutSchedule * getPercentage.netNetPrice,
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
            }
            #endregion
            
            _contextAccounting.SaveChanges();

            _contextTAX.SaveChanges();

            Logger.Info("CreateInputPaymentUniversal() - Finished.");
        }


        public void CreateAccountingTrPaymentDetailJournal(CreateAccountingTrPaymentDetailJournalInputDto input)
        {
            Logger.Info("CreateAccountingTrPaymentDetailJournal() - Started.");

            var data = new TR_PaymentDetailJournal
            {
                entityCode = input.entityCode,
                accCode = input.accCode,
                bookCode = input.bookCode,
                payNo = input.payNo,
                transNo = input.transNo,
                journalCode = input.journalCode
            };

            try
            {
                Logger.DebugFormat("CreateAccountingTrPaymentDetailJournal() - Start insert TR Payment Detail Journal. Parameters sent:{0}" +
                    "entityCode   = {1}{0}" +
                    "accCode      = {2}{0}" +
                    "bookCode     = {3}{0}" +
                    "payNo        = {4}{0}" +
                    "transNo      = {5}{0}" +
                    "journalCode  = {6}{0}"
                    , Environment.NewLine, input.entityCode, input.accCode, input.bookCode, input.payNo, input.transNo, input.journalCode);

                _contextAccounting.TR_PaymentDetailJournal.Add(data);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("CreateAccountingTrPaymentDetailJournal() - Ended insert TR Payment Detail Journal.");
                Logger.Info("CreateAccountingTrPaymentDetailJournal() - Finished.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreateAccountingTrPaymentDetailJournal() - ERROR DataException. Result = {0}", ex.Message);
                Logger.Info("CreateAccountingTrPaymentDetailJournal() - Finished.");
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateAccountingTrPaymentDetailJournal() - ERROR Exception. Result = {0}", ex.Message);
                Logger.Info("CreateAccountingTrPaymentDetailJournal() - Finished.");
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        public void CreateTAXTrFPHeader(CreateTAXTrFPHeaderInputDto input)
        {
            Logger.Info("CreateTAXTrFPHeader() - Started.");

            var data = new FP_TR_FPHeader
            {
                entityCode = input.entityCode,
                coCode = input.coCode,
                FPCode = input.FPCode,
                DPAmount = input.DPAmount,
                FPBranchCode = input.FPBranchCode,
                FPNo = input.FPNo,
                FPStatCode = input.FPStatCode,
                FPTransCode = input.FPTransCode,
                FPType = input.FPType,
                FPYear = input.FPYear,
                NPWP = input.NPWP,
                accCode = input.accCode,
                discAmount = input.discAmount,
                name = input.name,
                payNo = input.payNo,
                paymentCode = input.paymentCode,
                pmtBatchNo = input.pmtBatchNo,
                priceType = input.priceType,
                psCode = input.psCode,
                rentalCode = input.rentalCode,
                sourceCode = input.sourceCode,
                transDate = input.transDate,
                transNo = input.transNo,
                unitCode = input.unitCode,
                unitNo = input.unitNo,
                unitPriceAmt = input.unitPriceAmt,
                unitPriceVat = input.unitPriceVat,
                userAddress = input.userAddress,
                vatAmt = input.vatAmt,
            };

            try
            {
                Logger.DebugFormat("CreateTAXTrFPHeader() - Start insert TR FP Header. Parameters sent:{0}" +
                    "entityCode        = {1}{0}" +
                    "coCode            = {2}{0}" +
                    "FPCode            = {3}{0}" +
                    "DPAmount          = {4}{0}" +
                    "FPBranchCode      = {5}{0}" +
                    "FPNo              = {6}{0}" +
                    "FPStatCode        = {7}{0}" +
                    "FPTransCode       = {8}{0}" +
                    "FPType            = {9}{0}" +
                    "FPYear            = {10}{0}" +
                    "NPWP              = {11}{0}" +
                    "accCode           = {12}{0}" +
                    "discAmount        = {13}{0}" +
                    "name              = {14}{0}" +
                    "payNo             = {15}{0}" +
                    "paymentCode       = {16}{0}" +
                    "pmtBatchNo        = {17}{0}" +
                    "priceType         = {18}{0}" +
                    "psCode            = {19}{0}" +
                    "rentalCode        = {20}{0}" +
                    "sourceCode        = {21}{0}" +
                    "transDate         = {22}{0}" +
                    "transNo           = {23}{0}" +
                    "unitCode          = {24}{0}" +
                    "unitNo            = {25}{0}" +
                    "unitPriceAmt      = {26}{0}" +
                    "unitPriceVat      = {27}{0}" +
                    "userAddress       = {28}{0}" +
                    "vatAmt            = {29}{0}"
                    , Environment.NewLine
                    , input.entityCode, input.coCode, input.FPCode, input.DPAmount, input.FPBranchCode, input.FPNo, input.FPStatCode, input.FPTransCode, input.FPType, input.FPYear, input.NPWP, input.accCode, input.discAmount, input.name, input.payNo, input.paymentCode, input.pmtBatchNo, input.priceType, input.psCode, input.rentalCode, input.sourceCode, input.transDate, input.transNo, input.unitCode, input.unitNo, input.unitPriceAmt, input.unitPriceVat, input.userAddress, input.vatAmt
                    );

                _contextTAX.FP_TR_FPHeader.Add(data);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("CreateTAXTrFPHeader() - Ended insert TR FP Header.");
                Logger.Info("CreateTAXTrFPHeader() - Finished.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreateTAXTrFPHeader() - ERROR DataException. Result = {0}", ex.Message);
                Logger.Info("CreateTAXTrFPHeader() - Finished.");
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateTAXTrFPHeader() - ERROR Exception. Result = {0}", ex.Message);
                Logger.Info("CreateTAXTrFPHeader() - Finished.");
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        public JObject GenerateJurnalCode(GenerateJurnalInputDto input)
        {
            JObject obj = new JObject();
            string journalCode = "";

            var checkJurnalCode = (from A in _contextAccounting.TR_PaymentDetailJournal.ToList()
                                   where A.accCode == input.accCode
                                   orderby A.CreationTime descending
                                   select A.journalCode).FirstOrDefault();

            string runningNumber = "";

            if (checkJurnalCode != null)
            {
                var dataExisting = checkJurnalCode.Split('.');

                string coCodeExisting = dataExisting[1];
                string accCodeExisting = dataExisting[2];
                string yearExisting = dataExisting[3];
                string monthExisting = dataExisting[4];
                int runningNumberExisting = Convert.ToInt32(dataExisting[5]) + input.payNo;
                runningNumber = runningNumberExisting.ToString("D5");

                //data existing
                if (input.coCode == coCodeExisting && input.accCode == accCodeExisting && DateTime.Now.Year.ToString() == yearExisting && DateTime.Now.Month.ToString() == monthExisting)
                {

                    journalCode = "PRS." + input.coCode + "." + input.accCode + "." + yearExisting + "." + monthExisting + "." + runningNumber;
                }
                else
                {
                    int i = input.payNo;
                    runningNumber = i.ToString("D5");
                    journalCode = "PRS." + input.coCode + "." + input.accCode + "." + DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + runningNumber;
                }

            }
            else
            {
                int i = input.payNo;
                runningNumber = i.ToString("D5");
                journalCode = "PRS." + input.coCode + "." + input.accCode + "." + DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + runningNumber;
            }
            obj.Add("journalCode", journalCode);
            return obj;
        }

        public void CreateTrJournal(CreateTrJournalInputDto input)
        {
            Logger.Info("CreateTrJournal() - Started.");

            var data = new TR_Journal
            {
                COACodeAcc = input.COACodeAcc,
                COACodeFIN = input.COACodeFIN,
                debit = input.debit,
                entityCode = input.entityCode,
                journalCode = input.journalCode,
                journalDate = input.journalDate,
                kredit = input.kredit,
                remarks = input.remarks
            };

            try
            {
                Logger.DebugFormat("CreateTrJournal() - Start insert TR Journal. Parameters sent:{0}" +
                    "COACodeAcc = {1}{0}" +
                    "COACodeFIN = {2}{0}" +
                    "debit = {3}{0}" +
                    "entityCode = {4}{0}" +
                    "journalCode = {5}{0}" +
                    "journalDate = {6}{0}" +
                    "kredit = {7}{0}" +
                    "remarks = {8}{0}"
                    , Environment.NewLine, input.COACodeAcc, input.COACodeFIN, input.debit, input.entityCode, input.journalCode, input.journalDate, input.kredit, input.remarks);

                _contextAccounting.TR_Journal.Add(data);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("CreateTrJournal() - Ended insert TR Journal.");

                Logger.Info("CreateTrJournal() - Finished.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreateTrJournal() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateTrJournal() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        public void CreateTAXTrFPDetail(CreateTAXTrFPDetailInputDto input)
        {
            Logger.Info("CreateTAXTrFPDetail() - Started.");

            var data = new FP_TR_FPDetail
            {
                entityCode = input.entityCode,
                coCode = input.coCode,
                FPCode = input.FPCode,
                currencyCode = input.currencyCode,
                currencyRate = input.currencyRate,
                transAmount = input.transAmount,
                transNo = input.transNo,
                transDesc = input.transDesc
            };

            try
            {
                Logger.DebugFormat("CreateTAXTrFPDetail() - Start insert FP TR FP Detail. Parameters sent:{0}" +
                    "entityCode = {1}{0}" +
                    "coCode = {2}{0}" +
                    "FPCode = {3}{0}" +
                    "currencyCode = {4}{0}" +
                    "currencyRate = {5}{0}" +
                    "transAmount = {6}{0}" +
                    "transNo = {7}{0}" +
                    "transDesc = {8}{0}"
                    , Environment.NewLine, input.entityCode, input.coCode, input.FPCode, input.currencyCode, input.currencyRate, input.transAmount, input.transNo, input.transDesc);

                _contextTAX.FP_TR_FPDetail.Add(data);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("CreateTAXTrFPDetail() - Ended insert FP TR FP Detail.");

                Logger.Info("CreateTAXTrFPDetail() - Finished.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreateTAXTrFPDetail() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateTAXTrFPDetail() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        public JObject GenerateTransNoWithoutCheckSys(GenerateTransNoInputDto input)
        {
            JObject obj = new JObject();
            string transNo = "";
            var year = DateTime.Now.Year.ToString();
            string accCode = (from A in _msAccountRepo.GetAll()
                              where A.Id == input.accID
                              select A.accCode).FirstOrDefault();

            var checkSysFinance = (from sfc in _sysFinanceCounterRepo.GetAll()
                                   where sfc.accID == input.accID && sfc.entityID == input.entityID && sfc.year == year
                                   orderby sfc.CreationTime descending
                                   select sfc).FirstOrDefault();
            //kalau belum ada
            if (checkSysFinance == null)
            {
                //generate transNo
                int i = 1;
                string incNo1 = i.ToString("D6");

                transNo = accCode + "/" + year + "/" + incNo1;

            }
            else
            {
                //generate transNo
                long incNoAdd = checkSysFinance.transNo + 1;
                string incNo = incNoAdd.ToString("D6");

                transNo = accCode + "/" + year + "/" + incNo;
            }
            obj.Add("transNo", transNo);
            return obj;
        }

        public List<GetProjectByAccountListDto> GetProjectListByAccount(int accountID)
        {
            var getProjectListByAccountID = (from a in _msAccountRepo.GetAll()
                                             join b in _msProjectRepo.GetAll() on a.projectID equals b.Id
                                             where a.Id == accountID
                                             select new GetProjectByAccountListDto
                                             {
                                                 Id = b.Id,
                                                 project = b.projectCode + " - " + b.projectName,
                                             }).ToList();

            return getProjectListByAccountID;
        }

        public GetDataPrintORByTransNoListDto GetDataPrintORByTransNo(string transNo)
        {
            List<GetDataPayment> listPayment = new List<GetDataPayment>();

            var getDataPrintOR = (from a in _trPaymentHeaderRepo.GetAll()
                                  join b in _msAccountRepo.GetAll() on a.accountID equals b.Id
                                  join c in _trBookingHeaderRepo.GetAll() on a.bookingHeaderID equals c.Id
                                  join d in _lkPayForRepo.GetAll() on a.payForID equals d.Id
                                  join e in _msProjectRepo.GetAll() on b.projectID equals e.Id
                                  where a.transNo == transNo
                                  select new 
                                  {
                                      paymentHeaderID   = a.Id,
                                      accountID         = a.accountID,
                                      accountCode       = b.accCode,
                                      accountName       = b.accName,
                                      bookingHeaderID   = c.Id,
                                      bookCode          = c.bookCode,
                                      clearDate         = a.clearDate,
                                      ketPaymentHeader  = a.ket,
                                      payForID          = d.Id,
                                      payFor            = d.payForCode + " - " + d.payForName,
                                      paymentDate       = a.paymentDate,
                                      projectID         = e.Id,
                                      project           = e.projectCode + " - " + e.projectName
                                  }).FirstOrDefault();

            var getDataPaymentDetail = (from a in _trPaymentDetailRepo.GetAll()
                                        join b in _lkPayTypeRepo.GetAll() on a.payTypeID equals b.Id
                                        where a.paymentHeaderID == getDataPrintOR.paymentHeaderID
                                        select new 
                                        {
                                            paymentDetailID = a.Id,
                                            a.payNo,
                                            b.payTypeCode,
                                            a.bankName,
                                            a.chequeNo,
                                            a.status,
                                            a.ket,
                                            a.dueDate
                                        }).ToList();

            foreach(var dataPaymentDetail in getDataPaymentDetail)
            {
                var getDataAmountPerPaymentDetail = (from a in _trPaymentDetailAllocRepo.GetAll()
                                                     where a.paymentDetailID == dataPaymentDetail.paymentDetailID
                                                     group a by new
                                                     {
                                                         a.paymentDetailID
                                                     } into G
                                                     select new 
                                                     {
                                                         amount = G.Sum(x => x.netAmt) + G.Sum(x => x.vatAmt),
                                                         netAlloc = G.Sum(x => x.netAmt)
                                                     }).FirstOrDefault();

                var dataPayment = new GetDataPayment
                {
                    payNo = dataPaymentDetail.payNo,
                    payTypeCode = dataPaymentDetail.payTypeCode,
                    bankName = dataPaymentDetail.bankName,
                    chequeNo = dataPaymentDetail.chequeNo,
                    status = dataPaymentDetail.status,
                    ket = dataPaymentDetail.ket,
                    dueDate = dataPaymentDetail.dueDate,
                    amount = getDataAmountPerPaymentDetail.amount,
                    netAlloc = getDataAmountPerPaymentDetail.netAlloc
                };

                listPayment.Add(dataPayment);
            }

            var result = new GetDataPrintORByTransNoListDto
            {
                accountID = getDataPrintOR.accountID,
                accountCode = getDataPrintOR.accountCode,
                accountName = getDataPrintOR.accountName,
                bookingHeaderID = getDataPrintOR.bookingHeaderID,
                bookCode = getDataPrintOR.bookCode,
                clearDate = getDataPrintOR.clearDate,
                ketPaymentHeader = getDataPrintOR.ketPaymentHeader,
                payForID = getDataPrintOR.payForID,
                payFor = getDataPrintOR.payFor,
                paymentDate = getDataPrintOR.paymentDate,
                projectID = getDataPrintOR.projectID,
                project = getDataPrintOR.project,
                listDataPayment = listPayment
            };

            return result;
        }

        [HttpPost]
        public JObject GetDataToPrintOR(PrintORDto input)
        {
            JObject obj = new JObject();
            var webRootPath = _hostingEnvironment.WebRootPath;

            var getDataAccount = (from a in _msAccountRepo.GetAll()
                                  join b in _msCompanyRepo.GetAll() on a.coID equals b.Id
                                  join c in _msProjectRepo.GetAll() on a.projectID equals c.Id
                                  where a.Id == input.accountID  && a.projectID == input.projectID
                                  select new
                                  {
                                      a.accName,
                                      b.address,
                                      b.NPWP,
                                      c.image
                                  }).FirstOrDefault();

            var getDataPaymentFor = (from a in _lkPayForRepo.GetAll()
                                     where a.Id == input.payForID
                                     select a.payForName).FirstOrDefault();

            var totalAmountFormat = NumberHelper.IndoFormat(input.totalAmount);
            var ppn = NumberHelper.IndoFormat((decimal)0.1 * input.totalAmount);
            var totalAll = NumberHelper.IndoFormat(input.totalAmount + (decimal)0.1 * input.totalAmount);
            var terbilang = NumberHelper.Terbilang(input.totalAmount + (decimal)0.1 * input.totalAmount);
            
            var getListDataAlloc = (from a in input.listDataAlloc
                                    select new GetDataAlloc
                                    {
                                        payType = a.payType,
                                        netAlloc = a.netAlloc,
                                        netAllocFormat = NumberHelper.IndoFormat(a.netAlloc)
                                    }).ToList();

            //var imageDefault = Path.Combine(webRootPath, @"Assets\Upload\CompanyImage\imageDefault.jpeg");

            var projectImage = ""; 

            if (getDataAccount.image == null)
            {
                projectImage = Convert.ToBase64String(File.ReadAllBytes(getDataAccount.image));
            }
            else
            {
                projectImage = Convert.ToBase64String(File.ReadAllBytes(@"wwwroot\Assets\Upload\CompanyImage\imageDefault.jpeg"));
            }

            var paymentDate = input.paymentDate.ToString("d MMM yyyy");

            var dataPrintOR = new PrintORDto
            {
                accountName = getDataAccount.accName,
                accountNPWP = getDataAccount.NPWP == null ? "-" : getDataAccount.NPWP,
                accountAddress = getDataAccount.address == null ? "-" : getDataAccount.address,
                projectImage = projectImage,
                clusterName = input.clusterName,
                paymentFor = getDataPaymentFor,
                custName = input.custName,
                custAddress = input.custAddress == null ? "-" : input.custAddress,
                custNPWP = input.custNPWP == null ? "-" : input.custNPWP,
                paymentDateFormat = paymentDate,
                psCode = input.psCode,
                totalAmount = input.totalAmount,
                totalAmountFormat = totalAmountFormat,
                ppn = ppn,
                totalAll = totalAll,
                terbilang = terbilang,
                transNo = input.transNo,
                unitCode = input.unitCode,
                unitNo = input.unitNo,
                lantai = input.unitCode.Substring(input.unitCode.Length - 2),
                userLogin = input.userLogin,
                listDataAlloc = getListDataAlloc
            };

            var appsettingsjson = JObject.Parse(File.ReadAllText("appsettings.json"));
            var webConfigApp = (JObject)appsettingsjson["App"];
            var configuration = webConfigApp.Property("apiPdfUrl").Value.ToString();


            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                var url = configuration + "api/Pdf/OfficialReceiptPdf";

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeTypeNames.ApplicationJson));
                request.Content = new StringContent(JsonConvert.SerializeObject(dataPrintOR), Encoding.UTF8, MimeTypeNames.ApplicationJson);

                var response = ReadResponse(url, client, request);

                
                var oldPath = Path.Combine(webRootPath, @"Assets\Upload\PrintOR");
                if (!Directory.Exists(oldPath))
                {
                    Directory.CreateDirectory(oldPath);
                }

                var fileName = input.transNo;
                var fileNameNew = fileName.Replace('/', '-');

                var filePath = Path.Combine(oldPath, "OR_" + fileNameNew + ".pdf");
                var getBytes = Convert.FromBase64String(response.Result);
                File.WriteAllBytes(filePath, getBytes);
                var filePathResult = @"Assets\Upload\PrintOR\" + "OR_" + fileNameNew + ".pdf";

                obj.Add("filePath", filePathResult);

                return obj;
            }
        }

        private async Task<string> ReadResponse(string url, HttpClient client, HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);

            var success = await response.Content.ReadAsStringAsync();
            var indexFirstSlash = success.IndexOf(@"\");
            var trimString = success.Substring(indexFirstSlash+1,success.Length).Trim(new char[1] { '"' });

            return trimString;
        }
    }
}