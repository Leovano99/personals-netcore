using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using VDI.Demo.OnlineBooking.Transaction.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;
using VDI.Demo.PropertySystemDB.Pricing;
using System.Linq;
using Abp.AutoMapper;
using VDI.Demo.Authorization.Users;
using VDI.Demo.PropertySystemDB.OnlineBooking.DemoDB;
using Abp.Domain.Uow;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.NewCommDB;
using Abp.UI;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.Helper;
using VDI.Demo.OnlineBooking.PaymentMidtrans.Dto;
using VDI.Demo.PropertySystemDB.OnlineBooking.PPOnline;
using VDI.Demo.OnlineBooking.PaymentMidtrans;
using System.Threading.Tasks;
using VDI.Demo.SqlExecuter;
using VDI.Demo.OnlineBooking.Email;
using VDI.Demo.PropertySystemDB.OnlineBooking.ProjectInfo;
using System.Transactions;
using Abp.Authorization;
using VDI.Demo.Authorization;
using VDI.Demo.OnlineBooking.Email.Dto;
using Abp.Extensions;
using Newtonsoft.Json;
using System.Net;

namespace VDI.Demo.OnlineBooking.Transaction
{
    //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction)]
    public class TransactionAppService : DemoAppServiceBase, ITransactionAppService
    {
        private readonly IRepository<TR_BookingHeader> _trBookingHeaderRepo;
        private readonly IRepository<TR_UnitReserved> _trUnitReservedRepo;
        private readonly IRepository<TR_UnitOrderHeader> _trUnitOrderHeader;
        private readonly IRepository<TR_UnitOrderDetail> _trUnitOrderDetail;
        private readonly IRepository<TR_BookingDetail> _trBookingDetailRepo;
        private readonly IRepository<PERSONALS, string> _personalsRepo;
        private readonly IRepository<PERSONALS_MEMBER, string> _personalsMemberRepo;
        private readonly IRepository<MS_UnitItemPrice> _msUnitItemPriceRepo;
        private readonly IRepository<MS_Term> _msTermRepo;
        private readonly IRepository<MS_TermAddDisc> _msTermAddDiscRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<TR_ID, string> _trIDRepo;
        private readonly IRepository<LK_Item> _lkItem;
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<TR_Email, string> _trEmailRepo;
        private readonly IRepository<TR_Phone, string> _trPhoneRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCode;
        private readonly IRepository<MS_TermMain> _msTermMain;
        private readonly IRepository<MS_DiscOnlineBooking> _msDiscOnlineBooking;
        private readonly IRepository<MP_UserPersonals> _mpUserPersonals;
        private readonly IRepository<MS_UnitItem> _msUnitItem;
        private readonly IRepository<MS_Project> _msProject;
        private readonly IRepository<MS_Schema, string> _msSchema;
        private readonly IRepository<MS_TransFrom> _msTransFrom;
        private readonly IRepository<MS_SalesEvent> _msSalesEvent;
        private readonly IRepository<MS_TermPmt> _msTermPmt;
        private readonly IRepository<LK_FinType> _lkFinType;
        private readonly IRepository<TR_BookingDetailAddDisc> _trBookingAddDisc;
        private readonly IRepository<TR_MKTAddDisc> _trMKTAddDisc;
        private readonly IRepository<TR_CommAddDisc> _trCommAddDisc;
        private readonly IRepository<MS_TermDP> _msTermDP;
        private readonly IRepository<TR_BookingDetailDP> _trBookingDetailDP;
        private readonly IRepository<MS_Renovation> _msRenovation;
        private readonly IRepository<LK_BookingTrType> _lkBookingTrType;
        private readonly IRepository<TR_CashAddDisc> _trCashAddDisc;
        private readonly IRepository<TR_BookingSalesDisc> _trBookingSalesDisc;
        private readonly IRepository<MS_TermDiscOnlineBooking> _msTermDiscOnlineBooking;
        private readonly IRepository<LK_BookingOnlineStatus> _lkBookingOnlineStatus;
        private readonly IRepository<TR_BookingHeaderTerm> _trBookingHeaderTerm;
        private readonly IRepository<TR_BookingItemPrice> _trBookingItemPrice;
        private readonly IRepository<LK_UnitStatus> _lkUnitStatus;
        private readonly IRepository<MS_Category> _msCategory;
        private readonly IRepository<MS_TaxType> _msTaxType;
        private readonly IRepository<TR_BookingTax> _trBookingTax;
        private readonly IRepository<LK_Alloc> _lkAlloc;
        private readonly IRepository<LK_DPCalc> _lkDpCalc;
        private readonly IRepository<TR_BookingDetailSchedule> _trBookingDetailSchedule;
        private readonly IRepository<TR_PaymentDetailAlloc> _trPaymentDetailAlloc;
        private readonly IRepository<TR_PaymentDetail> _trPaymentDetail;
        private readonly IRepository<TR_PaymentHeader> _trPaymentHeader;
        private readonly IRepository<LK_PayFor> _lkPayFor;
        private readonly IRepository<LK_PaymentType> _lkPaymentType;
        private readonly PropertySystemDbContext _context;
        private readonly IRepository<TR_SoldUnit, string> _trSoldUnit;
        private readonly IRepository<TR_SoldUnitRequirement, string> _trSoldUnitRequirement;
        private readonly IRepository<SYS_BookingCounter> _sysBookingCounter;
        private readonly IRepository<MS_BobotComm, string> _msBobotComm;
        private readonly IRepository<MS_SchemaRequirement, string> _msSchemaRequirement;
        private readonly IRepository<User, long> _userRepo;
        private readonly IPaymentOBAppService _paymentMidtrans;
        private readonly IEmailAppService _emailAppService;
        private readonly IRepository<MS_ProjectInfo> _msProjectInfo;
        private readonly IRepository<TR_PaymentOnlineBook> _trPaymentOnlineBook;
        private readonly IRepository<MS_SumberDana> _msSumberDana;
        private readonly IRepository<MS_TujuanTransaksi> _msTujuanTransaksi;
        private readonly IRepository<MS_BankOLBooking> _msBankOLBooking;
        private readonly IRepository<MS_Detail> _msDetail;
        private readonly IRepository<MS_Cluster> _msCluster;
        private readonly PersonalsNewDbContext _contextPers;
        private readonly NewCommDbContext _contextNew;
        private readonly PaymentOBConfiguration _configuration;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public TransactionAppService(
            IRepository<MS_Unit> msUnit,
            IRepository<TR_BookingHeader> trBookingHeaderRepo,
            IRepository<TR_UnitReserved> trUnitReservedRepo,
            IRepository<TR_UnitOrderHeader> trUnitOrderHeader,
            IRepository<TR_UnitOrderDetail> trUnitOrderDetail,
            IRepository<TR_BookingDetail> trBookingDetailRepo,
            IRepository<PERSONALS, string> personalsRepo,
            IRepository<PERSONALS_MEMBER, string> personalsMemberRepo,
            IRepository<MS_UnitItemPrice> msUnitItemPrice,
            IRepository<MS_Term> msTermRepo,
            IRepository<MS_TermAddDisc> msTermAddDiscRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<TR_ID, string> trIDRepo,
            IRepository<LK_Item> lkItem,
            IRepository<MS_UnitCode> msUnitCode,
            IRepository<MS_TermMain> msTermMain,
            IRepository<MS_DiscOnlineBooking> msDiscOnlineBooking,
            IRepository<MP_UserPersonals> mpUserPersonals,
            IRepository<MS_UnitItem> msUnitItem,
            IRepository<MS_Project> msProject,
            IRepository<MS_Schema, string> msSchema,
            IRepository<MS_Cluster> msClusterRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<TR_Email, string> trEmailRepo,
            IRepository<TR_Phone, string> trPhoneRepo,
            IRepository<MS_TransFrom> msTransFrom,
            IRepository<MS_SalesEvent> msSalesEvent,
            IRepository<MS_TermPmt> msTermPmt,
            IRepository<LK_FinType> lkFinType,
            IRepository<MS_TermDP> msTermDP,
            IRepository<TR_BookingDetailDP> trBookingDetailDP,
            IRepository<TR_BookingDetailAddDisc> trBookingAddDisc,
            IRepository<TR_MKTAddDisc> trMKTAddDisc,
            IRepository<TR_CommAddDisc> trCommAddDisc,
            IRepository<MS_Renovation> msRenovation,
            IRepository<LK_BookingTrType> lkBookingTrType,
            IRepository<TR_CashAddDisc> trCashAddDisc,
            IRepository<TR_BookingSalesDisc> trBookingSalesDisc,
            IRepository<MS_TermDiscOnlineBooking> msTermDiscOnlineBooking,
            IRepository<LK_BookingOnlineStatus> lkOnlineBookingStatus,
            IRepository<TR_BookingHeaderTerm> trBookingHeaderTerm,
            IRepository<TR_BookingItemPrice> trBookingItemPrice,
            IRepository<LK_UnitStatus> lkUnitStatus,
            IRepository<MS_Category> msCategory,
            IRepository<MS_TaxType> msTaxType,
            IRepository<TR_BookingTax> trBookingTax,
            IRepository<LK_Alloc> lkAlloc,
            PropertySystemDbContext context,
            IRepository<LK_DPCalc> lkDpCalc,
            IRepository<TR_BookingDetailSchedule> trBookingDetailSchedule,
            IRepository<TR_PaymentDetailAlloc> trPaymentDetailAlloc,
            IRepository<TR_PaymentDetail> trPaymentDetail,
            IRepository<TR_PaymentHeader> trPaymentHeader,
            IRepository<LK_PayFor> lkPayFor,
            IRepository<TR_SoldUnit, string> trSoldUnit,
            IRepository<TR_SoldUnitRequirement, string> trSoldUnitRequirement,
            IRepository<SYS_BookingCounter> sysBookingCounter,
            IRepository<MS_BobotComm, string> msBobotComm,
            IRepository<MS_SchemaRequirement, string> msSchemaRequirement,
            IRepository<User, long> userRepo,
            IRepository<LK_PaymentType> lkPaymentType,
            IPaymentOBAppService paymentMidtrans,
            IEmailAppService emailAppService,
            IRepository<MS_ProjectInfo> msProjectInfo,
            IRepository<TR_PaymentOnlineBook> trPaymentOnlineBook,
            IRepository<MS_Cluster> msCluster,
            PersonalsNewDbContext contextPers,
            NewCommDbContext contextNew,
            PaymentOBConfiguration configuration,
            IRepository<MS_TujuanTransaksi> msTujuanTransaksi,
            IRepository<MS_SumberDana> msSumberDana,
            IRepository<MS_Detail> msDetail,
            IRepository<MS_BankOLBooking> msBankOlBooking,
            IUnitOfWorkManager unitOfWorkManager
            )
        {
            _trBookingHeaderRepo = trBookingHeaderRepo;
            _trUnitReservedRepo = trUnitReservedRepo;
            _trUnitOrderHeader = trUnitOrderHeader;
            _trUnitOrderDetail = trUnitOrderDetail;
            _trBookingDetailRepo = trBookingDetailRepo;
            _personalsRepo = personalsRepo;
            _personalsMemberRepo = personalsMemberRepo;
            _msUnitItemPriceRepo = msUnitItemPrice;
            _msTermRepo = msTermRepo;
            _msTermAddDiscRepo = msTermAddDiscRepo;
            _msUnitRepo = msUnitRepo;
            _trIDRepo = trIDRepo;
            _lkItem = lkItem;
            _msUnitRepo = msUnit;
            _msUnitCode = msUnitCode;
            _msTermMain = msTermMain;
            _msClusterRepo = msClusterRepo;
            _msProjectRepo = msProjectRepo;
            _trEmailRepo = trEmailRepo;
            _trPhoneRepo = trPhoneRepo;
            _msDiscOnlineBooking = msDiscOnlineBooking;
            _mpUserPersonals = mpUserPersonals;
            _msUnitItem = msUnitItem;
            _msProject = msProject;
            _msSchema = msSchema;
            _msTransFrom = msTransFrom;
            _msSalesEvent = msSalesEvent;
            _msTermPmt = msTermPmt;
            _msTermDP = msTermDP;
            _trBookingDetailDP = trBookingDetailDP;
            _trBookingAddDisc = trBookingAddDisc;
            _trMKTAddDisc = trMKTAddDisc;
            _trCommAddDisc = trCommAddDisc;
            _msRenovation = msRenovation;
            _lkBookingTrType = lkBookingTrType;
            _trCashAddDisc = trCashAddDisc;
            _trBookingSalesDisc = trBookingSalesDisc;
            _msTermDiscOnlineBooking = msTermDiscOnlineBooking;
            _lkBookingOnlineStatus = lkOnlineBookingStatus;
            _trBookingHeaderTerm = trBookingHeaderTerm;
            _lkFinType = lkFinType;
            _trBookingItemPrice = trBookingItemPrice;
            _lkUnitStatus = lkUnitStatus;
            _msCategory = msCategory;
            _msTaxType = msTaxType;
            _trBookingTax = trBookingTax;
            _context = context;
            _lkAlloc = lkAlloc;
            _trBookingDetailSchedule = trBookingDetailSchedule;
            _lkDpCalc = lkDpCalc;
            _trPaymentDetail = trPaymentDetail;
            _trPaymentDetailAlloc = trPaymentDetailAlloc;
            _trPaymentHeader = trPaymentHeader;
            _lkPayFor = lkPayFor;
            _trSoldUnit = trSoldUnit;
            _trSoldUnitRequirement = trSoldUnitRequirement;
            _sysBookingCounter = sysBookingCounter;
            _msBobotComm = msBobotComm;
            _msSchemaRequirement = msSchemaRequirement;
            _userRepo = userRepo;
            _lkPaymentType = lkPaymentType;
            _paymentMidtrans = paymentMidtrans;
            _emailAppService = emailAppService;
            _msProjectInfo = msProjectInfo;
            _trPaymentOnlineBook = trPaymentOnlineBook;
            _msCluster = msCluster;
            _contextPers = contextPers;
            _contextNew = contextNew;
            _configuration = configuration;
            _msBankOLBooking = msBankOlBooking;
            _unitOfWorkManager = unitOfWorkManager;
            _msTujuanTransaksi = msTujuanTransaksi;
            _msSumberDana = msSumberDana;
            _msDetail = msDetail;
        }


        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrUnitReserved)]
        public InsertTrUnitReservedResultDto InsertTrUnitReserved(InsertTRUnitReservedInputDto input)
        {
            var checkStatusUnit = (from unit in _msUnitRepo.GetAll()
                                   join status in _lkUnitStatus.GetAll()
                                   on unit.unitStatusID equals status.Id
                                   where status.unitStatusCode == "A" && unit.Id == input.unitID
                                   select unit).Count();

            var checkData = (from reserved in _trUnitReservedRepo.GetAll()
                             where reserved.unitID == input.unitID
                             select reserved).Count();
            if (checkData == 0 && checkStatusUnit != 0)
            {
                var getData = (from unit in _msUnitRepo.GetAll()
                               join term in _msTermRepo.GetAll()
                               on unit.termMainID equals term.termMainID
                               join termMain in _msTermMain.GetAll()
                               on term.termMainID equals termMain.Id
                               where unit.Id == input.unitID && term.Id == input.termID
                               select new
                               {
                                   unitID = unit.Id,
                                   termID = term.Id,
                                   termMain.BFAmount
                               }).FirstOrDefault();

                var getSales = (from user in UserManager.Users
                                join sales in _mpUserPersonals.GetAll()
                                on user.Id equals sales.userID
                                where user.Id == input.userID
                                select new
                                {
                                    user.Id,
                                    sales.userType,
                                    user.UserName,
                                    sales.psCode
                                }).FirstOrDefault();

                var getDisc1 = (from a in _msDiscOnlineBooking.GetAll()
                                where a.projectID == input.projectID
                                select new
                                {
                                    a.discPct,
                                    a.discDesc,
                                }).FirstOrDefault();

                var getDisc2 = (from a in _msTermDiscOnlineBooking.GetAll()
                                where a.termID == input.termID
                                select new
                                {
                                    a.pctDisc,
                                    a.discName
                                }).FirstOrDefault();


                //var remarks = "Selling Price: " + input.sellingPrice + ", Disc1: " + (getDisc1 != null ? getDisc1.discDesc : "0")
                //    + ", Disc2: " + (getDisc2 != null ? getDisc2.discName : "0") + ".";

                var data = new TR_UnitReserved
                {
                    unitID = getData.unitID,
                    reservedBy = getSales.UserName,
                    renovID = input.renovID,
                    termID = input.termID,
                    SellingPrice = input.sellingPrice,
                    BFAmount = getData.BFAmount,
                    reserveDate = DateTime.Now,
                    releaseDate = null,
                    pscode = null,
                    remarks = null,
                    disc1 = getDisc1 != null ? getDisc1.discPct : 0,
                    disc2 = getDisc2 != null ? getDisc2.pctDisc : 0,
                    specialDiscType = getDisc1 != null ? getDisc1.discDesc : null,
                    groupBU = "-"
                };
                var unitResevedID = _trUnitReservedRepo.InsertAndGetId(data);

                var getUnit = (from unit in _msUnitRepo.GetAll()
                               where unit.Id == input.unitID
                               select unit).FirstOrDefault();

                var updateUnit = getUnit.MapTo<MS_Unit>();

                updateUnit.unitStatusID = 12;

                _msUnitRepo.Update(updateUnit);

                return new InsertTrUnitReservedResultDto { result = true, message = "Success" };
            }
            else
            {
                return new InsertTrUnitReservedResultDto { result = false, message = "Unit is already reserved" };
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdatePsCodeTrUnitReserved)]
        public List<ResultMessageDto> UpdatePsCodeTrUnitReserved(UpdateTRUnitReserved input)
        {
            var arrResult = new List<ResultMessageDto>();
            foreach (var item in input.unit)
            {
                var getUnit = (from x in _trUnitReservedRepo.GetAll()
                               where x.unitID == item.unitID
                               select x).FirstOrDefault();
                if (getUnit != null)
                {
                    var updatePsCode = getUnit.MapTo<TR_UnitReserved>();

                    updatePsCode.pscode = input.psCode;

                    _trUnitReservedRepo.Update(updatePsCode);

                    var result = new ResultMessageDto { result = true, message = "Success" };

                    arrResult.Add(result);
                }
                else {

                    var result = new ResultMessageDto { result = false, message = "Failed, unit is not exist" };

                    arrResult.Add(result);
                }
            }

            return arrResult;
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrUnitOrderHeader)]
        public TrUnitOrderHeaderResultDto InsertTrUnitOrderHeader(CreateTransactionUniversalDto input)
        {
            var getOrderCode = (from a in _trUnitOrderHeader.GetAll()
                                orderby a.orderCode descending
                                select a).FirstOrDefault();

            int order = 0;
            if (getOrderCode != null)
            {
                var getOrder = getOrderCode.orderCode.Substring(5, 7);
                order = Convert.ToInt32(getOrder) + 1;
            }

            var OrderCode = String.Format("{0:LK-OB0000000}", order);

            var userInfo = (from user in _mpUserPersonals.GetAll()
                            where user.userID == input.userID
                            select user.psCode).FirstOrDefault();

            var memberName = (from personal in _personalsRepo.GetAll()
                              join member in _personalsMemberRepo.GetAll()
                              on personal.psCode equals member.psCode
                              join phone in _trPhoneRepo.GetAll()
                              on member.psCode equals phone.psCode into trphone
                              from phone in trphone.DefaultIfEmpty()
                              where member.psCode == userInfo
                              select new { personal, member, phone }).FirstOrDefault();

            var getCustomer = (from a in _personalsRepo.GetAll()
                               join b in _trEmailRepo.GetAll()
                               on a.psCode equals b.psCode into trEmail
                               from b in trEmail.DefaultIfEmpty()
                               join c in _trPhoneRepo.GetAll()
                               on a.psCode equals c.psCode into trPhone
                               from c in trPhone.DefaultIfEmpty()
                               where a.psCode == input.pscode
                               select new
                               {
                                   psName = a.name,
                                   psEmail = b.email,
                                   psPhone = c.number
                               }).FirstOrDefault();

            var getStatus = (from a in _lkBookingOnlineStatus.GetAll()
                             where a.statusType == "1"
                             select a.Id).FirstOrDefault();

            var getPaymentName = (from payment in _lkPaymentType.GetAll()
                                  where payment.Id == input.payTypeID
                                  select payment.paymentTypeName).FirstOrDefault();

            var dataUnitOrderHeader = new TR_UnitOrderHeader
            {
                orderCode = OrderCode,
                orderDate = DateTime.Now,
                psCode = input.pscode,
                psName = getCustomer.psName,
                psEmail = getCustomer.psEmail,
                psPhone = getCustomer.psPhone,
                memberCode = memberName.member.memberCode,
                memberName = memberName.personal.name,
                totalAmt = input.totalAmt,
                paymentTypeID = input.payTypeID,
                //REPAIR OUTSTANDING
                statusID = getStatus,
                userID = input.userID,
                scmCode = memberName.member.scmCode,
                oldOrderCode = string.Empty,
                sumberDanaID = input.sumberDanaID,
                tujuanTransaksiID = input.tujuanTransaksiID,
                bankRekeningPemilik = input.bankRekeningPemilik,
                nomorRekeningPemilik = input.nomorRekeningPemilik
            };
            var id = _trUnitOrderHeader.InsertAndGetId(dataUnitOrderHeader);

            var insertUnitOrderDetail = new TrUnitOrderDetailInputDto
            {
                orderHeaderID = id,
                arrUnit = input.arrUnit,
                userID = input.userID
            };

            InsertTrUnitOrderDetail(insertUnitOrderDetail);

            var insertPaymentOB = new InsertTrPaymentOnlineBookInputDto
            {
                bankAccName = "Not Defined",
                bankAccNo = "Not Defined",
                paymentAmt = input.totalAmt,
                bankName = "Not Defined",
                paymentType = getPaymentName,
                UnitOrderHeaderID = id
            };
            InsertTrPaymentOnlineBook(insertPaymentOB);

            return new TrUnitOrderHeaderResultDto
            {
                scmCode = memberName.member.scmCode,
                memberCode = memberName.member.memberCode,
                memberName = memberName.personal.name,
                unitOrderHeaderID = id,
                result = true
            };
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrUnitOrderDetail)]
        private ResultMessageDto InsertTrUnitOrderDetail(TrUnitOrderDetailInputDto input)
        {
            foreach (var items in input.arrUnit)
            {
                var checkTrUnitReserved = (from x in _trUnitReservedRepo.GetAll()
                                           where x.unitID == items.unitID
                                           && x.releaseDate == null
                                           select x).FirstOrDefault();

                var getRenovName = (from item in _lkItem.GetAll()
                                    join unititem in _msUnitItem.GetAll()
                                    on item.Id equals unititem.itemID
                                    join unit in _msUnitRepo.GetAll()
                                    on unititem.unitID equals unit.Id
                                    join unitItemPrice in _msUnitItemPriceRepo.GetAll()
                                    on unit.Id equals unitItemPrice.unitID
                                    select item.shortName).FirstOrDefault();

                var getDisc1 = (from a in _msDiscOnlineBooking.GetAll()
                                where a.projectID == items.projectID
                                select new
                                {
                                    a.discPct,
                                    a.discDesc,
                                }).FirstOrDefault();

                var getDisc2 = (from a in _msTermDiscOnlineBooking.GetAll()
                                where a.termID == checkTrUnitReserved.termID
                                select new
                                {
                                    a.pctDisc,
                                    a.discName
                                }).FirstOrDefault();

                var remarks = "Selling Price: " + checkTrUnitReserved.SellingPrice + ", Disc1: "
                        + (getDisc1 != null ? getDisc1.discDesc : "0") + ", Disc2: "
                        + (getDisc2 != null ? getDisc2.discName : "0") + ".";

                var dataUnitOrderDetail = new TR_UnitOrderDetail
                {
                    UnitOrderHeaderID = input.orderHeaderID,
                    unitID = checkTrUnitReserved.unitID,
                    renovID = checkTrUnitReserved.renovID,
                    termID = checkTrUnitReserved.termID,
                    BFAmount = items.bfAmount,
                    sellingPrice = items.sellingprice,
                    PPNo = string.Empty,
                    remarks = remarks,
                    disc1 = getDisc1 != null ? getDisc1.discPct : 0,
                    disc2 = getDisc2 != null ? getDisc2.pctDisc : 0,
                    specialDiscType = getDisc1 != null ? getDisc1.discDesc : null,
                    groupBU = string.Empty
                };

                _trUnitOrderDetail.Insert(dataUnitOrderDetail);

                var updateRemarksReserved = new UpdateRemarksReservedInputDto
                {
                    projectID = items.projectID,
                    sellingPrice = items.sellingprice,
                    termID = items.termID,
                    unitID = items.unitID,
                    userID = input.userID
                };
                UpdateRemarksReserved(updateRemarksReserved);
            }

            return new ResultMessageDto
            {
                result = true,
                message = "Success"
            };
        }

        //[UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingHeader)]
        private TrBookingHeaderResultDto InsertTrBookingHeader(TrBookingHeaderInputDto input)
        {
            var unit = (from term in _msTermRepo.GetAll()
                        join units in _msUnitRepo.GetAll()
                        on term.termMainID equals units.termMainID
                        join unitCode in _msUnitCode.GetAll()
                        on units.unitCodeID equals unitCode.Id
                        where term.Id == input.termID
                        && units.Id == input.unitID
                        select new { term, units, unitCode.unitCode }).FirstOrDefault();


            //var getSchemeID = (from schema in _msSchema.GetAll()
            //                   where schema.scmCode == memberName.member.scmCode
            //                   select schema.Id).FirstOrDefault();

            var getProject = (from units in _msUnitRepo.GetAll()
                              join project in _msProject.GetAll()
                              on units.projectID equals project.Id
                              where units.Id == input.unitID
                              select project.projectCode).FirstOrDefault();

            var getRenovCode = (from x in _msUnitItemPriceRepo.GetAll()
                                join z in _msRenovation.GetAll() on x.renovID equals z.Id
                                where x.unitID == input.unitID
                                && x.termID == input.termID
                                select z.renovationCode).FirstOrDefault();

            var getTransID = (from x in _msTransFrom.GetAll()
                              where x.transCode == "00026"
                              select x.Id).FirstOrDefault();

            var getEvent = (from x in _msSalesEvent.GetAll()
                            where x.eventCode == "00012"
                            select x.Id).FirstOrDefault();

            var data = new TR_BookingHeader
            {
                entityID = 1,
                bookCode = BookCode(getProject),
                unitID = input.unitID,
                bookDate = DateTime.Now,
                psCode = input.psCode,
                scmCode = "011",
                memberCode = input.memberCode,
                memberName = input.memberName,
                termID = input.termID,
                PPJBDue = unit.term.PPJBDue,
                termRemarks = unit.term.remarks,
                NUP = "-",
                isSK = false,
                BFPayTypeCode = "-",
                bankNo = "-",
                bankName = "-",
                transID = getTransID,
                discBFCalcType = unit.term.discBFCalcType,
                DPCalcType = unit.term.DPCalcType,
                netPriceComm = 0,
                KPRBankCode = "-",
                isPenaltyStop = false,
                isSMS = false,
                bankRekeningPemilik = input.bankRekeningPemilik,
                nomorRekeningPemilik = input.nomorRekeningPemilik,
                sumberDanaID = input.sumberDanaID,
                tujuanTransaksiID = input.tujuanTransaksiID,
                eventID = getEvent,
                //YANG DATANYA '-'
                promotionID = 8,
                shopBusinessID = 1,
                SADStatusID = 1,
                facadeID = 1,
                //REPAIR
                remarks = "-",
            };

            var id = _trBookingHeaderRepo.InsertAndGetId(data);

            return new TrBookingHeaderResultDto
            {
                bookingHeaderID = id,
                result = true,
                message = data.bookCode,
                bookDate = data.bookDate,
                unitID = data.unitID,
                termRemarks = data.termRemarks
            };
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingDetail)]
        private List<bookingDetailIDDto> InsertTrBookingDetail(TrBookingDetailInputDto input)
        {
            var getUnit = (from termMain in _msTermMain.GetAll()
                           join term in _msTermRepo.GetAll()
                           on termMain.Id equals term.termMainID
                           join termPmt in _msTermPmt.GetAll()
                           on term.Id equals termPmt.termID
                           join unit in _msUnitRepo.GetAll()
                           on termMain.Id equals unit.termMainID
                           join unitItem in _msUnitItem.GetAll()
                           on unit.Id equals unitItem.unitID
                           join unitItemPrice in _msUnitItemPriceRepo.GetAll()
                           on new { unitID = unit.Id, itemID = unitItem.itemID, termID = term.Id }
                           equals new { unitID = unitItemPrice.unitID, itemID = unitItemPrice.itemID, termID = unitItemPrice.termID }
                           where unit.Id == input.unitID && unitItemPrice.termID == input.termID/* && term.Id == input.termID*/
                           select new
                           {
                               pctTax = unitItem.pctTax,
                               amount = unitItem.amount,
                               area = unitItem.area,
                               coCode = unitItem.coCode,
                               pctDisc = unitItem.pctDisc,
                               itemID = unitItem.itemID,
                               finTypeID = termPmt.finTypeID,
                               finStartDue = termPmt.finStartDue,
                               entityID = unit.entityID,

                           }).Distinct().ToList();


            var penampung = new List<bookingDetailIDDto>();

            if (getUnit != null)
            {
                short refno = 0;
                var combineCodes = 0;

                List<string> coCodes = new List<string>();

                foreach (var item in getUnit)
                {
                    //var getArea = (from unitItem in _msUnitItem.GetAll()
                    //               where unitItem.itemID == item.itemID
                    //               && unitItem.unitID == input.unitID
                    //               && unitItem.)

                    var lastBookNo = (from t in _sysBookingCounter.GetAll()
                                      where t.coCode == item.coCode
                                      orderby t.bookNo descending
                                      select t).FirstOrDefault();

                    int bookNo;

                    if (lastBookNo == null)
                    {
                        bookNo = 1;

                        var data = new SYS_BookingCounter
                        {
                            coCode = item.coCode,
                            bookNo = bookNo,
                            BASTNo = 0,
                            entityID = 1
                        };
                        _sysBookingCounter.Insert(data);
                    }
                    else
                    {
                        bookNo = lastBookNo.bookNo + 1;

                        var updateBookNo = lastBookNo.MapTo<SYS_BookingCounter>();

                        updateBookNo.bookNo = bookNo;
                    }

                    string combine = "";
                    refno++;

                    var checkCoCode = coCodes.Where(x => x == item.coCode).Select(x => x).Count();

                    if (checkCoCode == 0)
                    {
                        combineCodes++;
                        combine = refno + "-" + combineCodes;
                    }
                    else
                    {
                        combineCodes = 1;
                        combine = refno + "-" + combineCodes;

                    }
                    coCodes.Add(item.coCode);

                    var getBookingTrTypeID = (from type in _lkBookingTrType.GetAll()
                                              where type.bookingTrType == "BK"
                                              select type.Id).FirstOrDefault();

                    var detail = new TR_BookingDetail
                    {
                        entityID = item.entityID,
                        bookingHeaderID = input.bookingHeaderID,
                        refNo = refno,
                        itemID = item.itemID,
                        coCode = combine,
                        bookNo = bookNo,
                        BFAmount = 0,
                        amount = 0,
                        pctDisc = item.pctDisc,
                        pctTax = item.pctTax,
                        area = item.area,
                        finTypeID = item.finTypeID,
                        combineCode = combineCodes.ToString(),
                        amountComm = 0,
                        netPriceComm = 0,
                        amountMKT = 0,
                        netPriceMKT = 0,
                        netPriceCash = 0,
                        netPrice = 0,
                        netNetPrice = 0,
                        adjPrice = 0,
                        adjArea = 0,
                        bookingTrTypeID = getBookingTrTypeID
                    };
                    var bookingDetailID = _trBookingDetailRepo.InsertAndGetId(detail);

                    var result = new bookingDetailIDDto
                    {
                        bookingHeaderID = input.bookingHeaderID,
                        bookingDetailID = bookingDetailID,
                        itemID = item.itemID,
                        result = true,
                        area = item.area,
                        bookNo = bookNo,
                        coCode = combine,
                        pctTax = item.pctTax
                    };
                    penampung.Add(result);
                }
                return penampung;
            }
            else
            {
                return null;
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrPaymentOnlineBook)]
        private void InsertTrPaymentOnlineBook(InsertTrPaymentOnlineBookInputDto input)
        {
            var data = new TR_PaymentOnlineBook
            {
                bankAccName = input.bankAccName,
                bankAccNo = input.bankAccNo,
                paymentAmt = input.paymentAmt,
                bankName = input.bankName,
                offlineType = input.offlineType,
                paymentDate = DateTime.Now,
                paymentType = input.paymentType,
                resiImage = input.resiImage,
                resiNo = input.resiNo,
                UnitOrderHeaderID = input.UnitOrderHeaderID
            };
            _trPaymentOnlineBook.Insert(data);
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_DeleteTrUnitReserved)]
        public ResultMessageDto DeleteTrUnitReserved(int unitReservedID)
        {
            var checkId = (from a in _trUnitReservedRepo.GetAll()
                           where a.Id == unitReservedID
                           select a).FirstOrDefault();

            if (checkId != null)
            {
                _trUnitReservedRepo.Delete(unitReservedID);

                var getUnit = (from unit in _msUnitRepo.GetAll()
                               where unit.Id == checkId.unitID
                               select unit).FirstOrDefault();

                var getAvailableCode = (from status in _lkUnitStatus.GetAll()
                                        where status.unitStatusCode == "A"
                                        select status.Id).FirstOrDefault();

                var update = getUnit.MapTo<MS_Unit>();

                update.unitStatusID = getAvailableCode;

                _msUnitRepo.Update(update);

                return new ResultMessageDto
                {
                    message = "Unit was deleted",
                    result = true
                };
            }
            else
            {
                return new ResultMessageDto
                {
                    message = "Failed",
                    result = false
                };
            }

        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertAddDisc)]
        private ResultMessageDto InsertAddDisc(InsertAddDiscInputDto input)  //Kurang Test
        {
            var GetListUnitAddDisc = (from unit in _msUnitRepo.GetAll()
                                      join unititem in _msUnitItem.GetAll()
                                      on unit.Id equals unititem.unitID
                                      join item in _lkItem.GetAll()
                                      on unititem.itemID equals item.Id
                                      join termMain in _msTermMain.GetAll()
                                      on unit.termMainID equals termMain.Id
                                      join term in _msTermRepo.GetAll()
                                      on termMain.Id equals term.termMainID
                                      //belum di test
                                      join unititemprice in _msUnitItemPriceRepo.GetAll()
                                      on new { unitID = unit.Id, itemID = unititem.itemID, termID = term.Id }
                                      equals new { unitID = unititemprice.unitID, itemID = unititemprice.itemID, termID = unititemprice.termID }
                                      join termAddDisc in _msTermAddDiscRepo.GetAll()
                                      on term.Id equals termAddDisc.termID
                                      join bookingHeader in _trBookingHeaderRepo.GetAll()
                                      on unit.Id equals bookingHeader.unitID
                                      join bookingDetail in _trBookingDetailRepo.GetAll()
                                      on bookingHeader.Id equals bookingDetail.bookingHeaderID
                                      where unit.Id == input.unitID && unititemprice.termID == input.termID
                                      && bookingDetail.Id == input.bookingDetailID
                                      select new
                                      {
                                          unitNo = unit.unitNo,
                                          itemCode = item.itemCode,
                                          addDiscNo = termAddDisc.addDiscNo,
                                          addDisc = termAddDisc.addDiscPct,
                                          coCode = unititem.coCode,
                                          bookingDetailID = bookingDetail.Id,
                                          bookNo = bookingDetail.bookNo,
                                          entityID = bookingDetail.entityID,
                                          addDisctAmt = termAddDisc.addDiscAmt
                                      }).Distinct().ToList();

            var getDisc1 = (from a in _msDiscOnlineBooking.GetAll()
                            join b in _msUnitRepo.GetAll()
                            on a.projectID equals b.projectID into msUnit
                            from b in msUnit.DefaultIfEmpty()
                            join c in _msUnitItem.GetAll()
                            on b.Id equals c.unitID
                            join d in _msTermRepo.GetAll()
                            on b.termMainID equals d.termMainID
                            join e in _msTermAddDiscRepo.GetAll()
                            on d.Id equals e.termID
                            join f in _msUnitItemPriceRepo.GetAll()
                            on new { unitID = b.Id, c.itemID, termID = d.Id } equals new { unitID = f.unitID, f.itemID, termID = f.termID }
                            join g in _trBookingHeaderRepo.GetAll()
                            on b.Id equals g.unitID
                            join h in _trBookingDetailRepo.GetAll()
                            on g.Id equals h.bookingHeaderID
                            where b.Id == input.unitID && d.Id == input.termID && h.Id == input.bookingDetailID
                            select new
                            {
                                addDiscNo = e.addDiscNo,
                                e.addDiscAmt,
                                addDisc = a.discPct,
                                addDiscDesc = a.discDesc,
                                coCode = c.coCode,
                                bookingDetailID = h.Id,
                                bookNo = h.bookNo,
                                entityID = h.entityID
                            }).Distinct().ToList();

            var getDisc2 = (from a in _msTermDiscOnlineBooking.GetAll()
                            join b in _msTermRepo.GetAll()
                            on a.termID equals b.Id
                            join c in _msTermAddDiscRepo.GetAll()
                            on b.Id equals c.termID
                            join d in _msUnitRepo.GetAll()
                            on b.projectID equals d.projectID into msUnit
                            from d in msUnit.DefaultIfEmpty()
                            join e in _msUnitItem.GetAll()
                            on d.Id equals e.unitID
                            join f in _msUnitItemPriceRepo.GetAll()
                            on new { unitID = d.Id, e.itemID, termID = b.Id } equals new { unitID = f.unitID, f.itemID, termID = f.termID }
                            join g in _trBookingHeaderRepo.GetAll()
                            on b.Id equals g.unitID
                            join h in _trBookingDetailRepo.GetAll()
                            on g.Id equals h.bookingHeaderID
                            where d.Id == input.unitID && b.Id == input.termID && h.Id == input.bookingDetailID
                            select new
                            {
                                addDiscNo = c.addDiscNo,
                                c.addDiscAmt,
                                addDisc = a.pctDisc,
                                addDiscDesc = a.discName,
                                coCode = e.coCode,
                                bookingDetailID = h.Id,
                                bookNo = h.bookNo,
                                entityID = h.entityID
                            }).Distinct().ToList();

            if (GetListUnitAddDisc != null && getDisc1 != null && getDisc2 != null)
            {
                foreach (var item in GetListUnitAddDisc)
                {
                    var AddDisc = new TR_BookingDetailAddDisc
                    {
                        entityID = item.entityID,
                        bookingDetailID = item.bookingDetailID,
                        addDiscNo = item.addDiscNo,
                        addDiscDesc = string.Empty,
                        pctAddDisc = item.addDisc,
                        isAmount = false,
                        amtAddDisc = item.addDisctAmt
                    };
                    _trBookingAddDisc.InsertAsync(AddDisc);

                    var MKTDisc = new TR_MKTAddDisc
                    {
                        entityID = item.entityID,
                        bookingDetailID = item.bookingDetailID,
                        pctAddDisc = item.addDisc,
                        addDiscNo = item.addDiscNo,
                        addDiscDesc = string.Empty,
                        isAmount = item.addDisctAmt == 0 ? false : true,
                        amtAddDisc = item.addDisctAmt
                        //REPAIR
                        //addDiscDesc = 
                    };
                    _trMKTAddDisc.InsertAsync(MKTDisc);

                    var CommDisc = new TR_CommAddDisc
                    {
                        entityID = item.entityID,
                        bookingDetailID = item.bookingDetailID,
                        pctAddDisc = item.addDisc,
                        addDiscNo = item.addDiscNo,
                        addDiscDesc = string.Empty,
                        isAmount = item.addDisctAmt == 0 ? false : true,
                        amtAddDisc = item.addDisctAmt
                        //REPAIR
                        //addDiscDesc = 
                    };
                    _trCommAddDisc.InsertAsync(CommDisc);
                }

                foreach (var item in getDisc1)
                {
                    var AddDisc = new TR_BookingDetailAddDisc
                    {
                        entityID = item.entityID,
                        bookingDetailID = item.bookingDetailID,
                        addDiscNo = item.addDiscNo,
                        addDiscDesc = item.addDiscDesc,
                        pctAddDisc = item.addDisc,
                        isAmount = false,
                        amtAddDisc = 0
                    };
                    _trBookingAddDisc.InsertAsync(AddDisc);

                    var MKTDisc = new TR_MKTAddDisc
                    {
                        entityID = item.entityID,
                        bookingDetailID = item.bookingDetailID,
                        addDiscNo = item.addDiscNo,
                        addDiscDesc = item.addDiscDesc,
                        pctAddDisc = item.addDisc,
                        isAmount = item.addDiscAmt == 0 ? false : true,
                        amtAddDisc = item.addDiscAmt
                    };
                    _trMKTAddDisc.InsertAsync(MKTDisc);

                    var CommDisc = new TR_CommAddDisc
                    {
                        entityID = item.entityID,
                        bookingDetailID = item.bookingDetailID,
                        addDiscNo = item.addDiscNo,
                        addDiscDesc = item.addDiscDesc,
                        pctAddDisc = item.addDisc,
                        isAmount = item.addDiscAmt == 0 ? false : true,
                        amtAddDisc = item.addDiscAmt
                    };
                    _trCommAddDisc.InsertAsync(CommDisc);
                }

                foreach (var item in getDisc2)
                {
                    var AddDisc = new TR_BookingDetailAddDisc
                    {
                        entityID = item.entityID,
                        bookingDetailID = item.bookingDetailID,
                        addDiscNo = item.addDiscNo,
                        addDiscDesc = item.addDiscDesc,
                        pctAddDisc = item.addDisc,
                        isAmount = false,
                        amtAddDisc = item.addDiscAmt
                    };
                    _trBookingAddDisc.InsertAsync(AddDisc);

                    var MKTDisc = new TR_MKTAddDisc
                    {
                        entityID = item.entityID,
                        bookingDetailID = item.bookingDetailID,
                        addDiscNo = item.addDiscNo,
                        addDiscDesc = item.addDiscDesc,
                        pctAddDisc = item.addDisc,
                        isAmount = item.addDiscAmt == 0 ? false : true,
                        amtAddDisc = item.addDiscAmt

                    };
                    _trMKTAddDisc.InsertAsync(MKTDisc);

                    var CommDisc = new TR_CommAddDisc
                    {
                        entityID = item.entityID,
                        bookingDetailID = item.bookingDetailID,
                        addDiscNo = item.addDiscNo,
                        addDiscDesc = item.addDiscDesc,
                        pctAddDisc = item.addDisc,
                        isAmount = item.addDiscAmt == 0 ? false : true,
                        amtAddDisc = item.addDiscAmt
                    };
                    _trCommAddDisc.InsertAsync(CommDisc);
                }

                return new ResultMessageDto { result = true };
            }
            else
            {
                return new ResultMessageDto { message = "Data not found", result = false };
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingDetailDP)]
        private List<InsertTRDetailDPResultDto> InsertTrBookingDetailDP(TrBookingDetailDPInputDto input)
        {
            var getTermDP = (from term in _msTermRepo.GetAll()
                             join termDP in _msTermDP.GetAll()
                             on term.Id equals termDP.termID
                             join termPmt in _msTermPmt.GetAll()
                             on term.Id equals termPmt.termID
                             join unit in _msUnitRepo.GetAll()
                             on term.termMainID equals unit.termMainID
                             join unitItemPrice in _msUnitItemPriceRepo.GetAll()
                             on unit.Id equals unitItemPrice.unitID
                             join bookingHeader in _trBookingHeaderRepo.GetAll()
                             on termPmt.termID equals bookingHeader.termID
                             join bookingDetail in _trBookingDetailRepo.GetAll()
                             on bookingHeader.Id equals bookingDetail.bookingHeaderID
                             where term.Id == input.termID && unit.Id == input.unitID && bookingDetail.Id == input.bookingDetailID
                             select new GetListTermDPResultDto
                             {
                                 daysDue = termDP.daysDue,
                                 DPNo = termDP.DPNo,
                                 DPPct = termDP.DPPct,
                                 DPAmount = termDP.DPAmount,
                                 bookingDetailID = bookingDetail.Id,
                                 entityID = bookingDetail.entityID
                             }).Distinct().ToList();


            var arrDetailDP = new List<InsertTRDetailDPResultDto>();
            foreach (var item in getTermDP)
            {

                var detailDP = new TR_BookingDetailDP
                {
                    entityID = item.entityID,
                    bookingDetailID = item.bookingDetailID,
                    dpNo = item.DPNo,
                    daysDue = item.daysDue,
                    DPPct = item.DPPct,
                    DPAmount = item.DPAmount
                };
                _trBookingDetailDP.Insert(detailDP);

                var detailDPResult = new InsertTRDetailDPResultDto
                {
                    DPNo = item.DPNo,
                    daysDue = item.daysDue,
                    DPPct = item.DPPct,
                    DPAmount = item.DPAmount
                };
                arrDetailDP.Add(detailDPResult);
            }
            return arrDetailDP;

        }

        //[UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTransactionUniversal)]
        public ResultMessageDto InsertTransactionUniversal(CreateTransactionUniversalDto input)
        {
            Logger.InfoFormat("Start InsertTransactionUniversal()");
            var param = JsonConvert.SerializeObject(input);
            Logger.InfoFormat(param);

            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                if (input.pscode != null || input.payTypeID != 0 || input.arrUnit != null)
                {
                    TrBookingHeaderResultDto bookingHeaderID = new TrBookingHeaderResultDto();
                    foreach (var item in input.arrUnit)
                    {
                        var resultUpdateNetPrice = new List<UpdateNetPriceResultDto>();

                        //using (var unitOfWork = _unitOfWorkManager.Begin())
                        //{
                        //using (var scope = new TransactionScope(TransactionScopeOption.Required))
                        //{

                        //Step 1
                        var dataInsertTrBookingHeader = new TrBookingHeaderInputDto
                        {
                            userID = input.userID,
                            unitID = item.unitID,
                            termID = item.termID,
                            psCode = input.pscode,
                            sellingPrice = item.sellingprice,
                            tujuanTransaksiID = input.tujuanTransaksiID,
                            sumberDanaID = input.sumberDanaID,
                            bankRekeningPemilik = input.bankRekeningPemilik,
                            nomorRekeningPemilik = input.nomorRekeningPemilik,
                            memberCode = input.memberCode,
                            memberName = input.memberName
                        };
                        bookingHeaderID = InsertTrBookingHeader(dataInsertTrBookingHeader);

                        //Step 2
                        var dataInsertTrBookingDetail = new TrBookingDetailInputDto
                        {
                            bookingHeaderID = bookingHeaderID.bookingHeaderID,
                            termID = item.termID,
                            unitID = item.unitID
                        };
                        var trBookingDetailID = InsertTrBookingDetail(dataInsertTrBookingDetail);

                        //Step 3
                        var dataInsertTrBookingSalesAddDisc = new TrBookingSalesAddDiscInputDto
                        {
                            bookingHeaderID = bookingHeaderID.bookingHeaderID,
                            termID = item.termID,
                            unitID = item.unitID
                        };
                        var trBookingSalesAddDisc = InsertTrBookingSalesAddDisc(dataInsertTrBookingSalesAddDisc);


                        foreach (var bookingDetail in trBookingDetailID)
                        {
                            //Step 4
                            var dataInsertAddDisc = new InsertAddDiscInputDto
                            {
                                bookingDetailID = bookingDetail.bookingDetailID,
                                unitID = item.unitID,
                                termID = item.termID
                            };
                            InsertAddDisc(dataInsertAddDisc);

                            //var hasilInsertAddDisc = new ResultMessageDto { result = true };

                            //Step 5
                            var dataTrBookingDetailDP = new TrBookingDetailDPInputDto
                            {
                                bookingDetailID = bookingDetail.bookingDetailID,
                                termID = item.termID,
                                unitID = item.unitID
                            };
                            var trBookingDetailDP = InsertTrBookingDetailDP(dataTrBookingDetailDP);

                            //var hasilBookingDP = new ResultMessageDto { result = true };

                            //Step 6
                            var dataTrCashAddDisc = new TrCashAddDiscInputDto
                            {
                                bookingDetailID = bookingDetail.bookingDetailID,
                                termID = item.termID,
                                unitID = item.unitID
                            };
                            var trCashAddDisc = InsertTrCashAddDisc(dataTrCashAddDisc);

                            //var hasilCashAddDisc = new ResultMessageDto { result = true };

                            //Step 7
                            var dataInsertTrBookingTax = new InsertTrBookingTaxInputDto
                            {
                                bookingDetailID = bookingDetail.bookingDetailID,
                                sellingPrice = item.sellingprice,
                                termID = item.termID,
                                unitID = item.unitID
                                //TANYAKAN MBAK JESS
                                //netNetPrice = ?
                            };
                            var resultInsertTrBookingTax = InsertTrBookingTax(dataInsertTrBookingTax);


                            ////Step 8
                            //var dataInsertTrBookingDetailSchedule = new InsertTrBookingDetailScheduleInputDto
                            //{
                            //    bookingHeaderID = bookingHeaderID.bookingHeaderID
                            //};
                            //InsertTrBookingDetailSchedule(dataInsertTrBookingDetailSchedule);

                        }

                        //Step 9
                        var dataUpdateBFAmount = new UpdateBFAmountInputDto
                        {
                            unitID = item.unitID,
                        };

                        var updateBFAmount = UpdateBFAmount(dataUpdateBFAmount);

                        //Step 10
                        var dataTrBookingHeaderTerm = new InsertTrBookingHeaderTermInputDto
                        {
                            unitID = item.unitID,
                        };
                        var insertTrBookingHeaderTerm = InsertTrBookingHeaderTerm(dataTrBookingHeaderTerm);

                        //Step 11
                        var dataTrBookingItemPrice = new InsertTrBookingHeaderTermInputDto
                        {
                            unitID = item.unitID
                        };
                        var insertTrBookingItemPrice = InsertTrBookingItemPrice(dataTrBookingItemPrice);

                        //Step 12
                        var dataUpdateUnit = new InsertTrBookingHeaderTermInputDto
                        {
                            unitID = item.unitID
                        };
                        var updateUnitSold = UpdateUnitSold(dataUpdateUnit);

                        //Step 13
                        var dataUpdateNetPriceBookingHeaderDetail = new UpdateBookingDetailInputDto
                        {
                            bookingHeaderID = bookingHeaderID.bookingHeaderID,
                            termID = item.termID,
                            listSalesDisc = trBookingSalesAddDisc,
                            listBookingItemPrice = insertTrBookingItemPrice,
                            listBookingHeaderTerm = insertTrBookingHeaderTerm,
                            listBookingDetail = trBookingDetailID
                        };
                        resultUpdateNetPrice = UpdateNetPriceBookingHeaderDetail(dataUpdateNetPriceBookingHeaderDetail);

                        var dataUpdateRemarks = new UpdateRemarksDto
                        {
                            bookingHeaderID = bookingHeaderID.bookingHeaderID,
                            sellingPrice = item.sellingprice
                        };
                        var updateRemarks = UpdateRemarksTrBookingHeader(dataUpdateRemarks);

                        //Step 14
                        var dataUpdateOrderStatusFullyPaid = new UpdateOrderStatusFullyPaid
                        {
                            bookingHeaderID = bookingHeaderID.bookingHeaderID,
                            orderHeaderID = input.orderHeaderID
                        };
                        var resultUpdateOrderStatusFullyPaid = UpdateOrderStatusFullyPaid(dataUpdateOrderStatusFullyPaid);

                        //Step 15
                        var dataUpdateReleaseDate = new UpdateReleaseDateInputDto
                        {
                            psCode = input.pscode,
                            renovID = item.renovID,
                            termID = item.termID,
                            unitID = item.unitID
                        };
                        var resultdataUpdateReleaseDate = UpdateReleaseDate(dataUpdateReleaseDate);

                        //    scope.Complete();
                        //}
                        //    unitOfWork.Complete();
                        //}

                        //Step 16
                        var dataInsertTrSoldUnit = new InsertTrSoldUnitInputDto
                        {
                            sellingPrice = item.sellingprice,
                            unitID = item.unitID,
                            userID = input.userID,
                            memberCode = input.memberCode,
                            scmCode = input.scmCode,
                            netNetPrice = resultUpdateNetPrice,
                            listBookingDetail = trBookingDetailID,
                            listBookingHeader = bookingHeaderID
                        };
                        var resultInsertTrSoldUnit = InsertTrSoldUnit(dataInsertTrSoldUnit);

                        //Step 17
                        var dataInsertTrSoldUnitRequirement = new InsertTrSoldUnitRequirementInputDto
                        {
                            unitID = item.unitID,
                            userID = input.userID,
                            scmCode = input.scmCode,
                            memberCode = input.memberCode,
                            listBookingDetail = trBookingDetailID
                        };
                        var resultInsertTrSoldUnitRequirement = InsertTrSoldUnitRequirement(dataInsertTrSoldUnitRequirement);

                        //_contextNew.SaveChanges();
                        //CurrentUnitOfWork.SaveChanges();

                        //var getBooking = (from booking in _trBookingHeaderRepo.GetAll()
                        //                  orderby booking.CreationTime descending
                        //                  select booking).FirstOrDefault();

                        //var getBookings = (from booking in _contextNew.TR_SoldUnit
                        //                   orderby booking.inputTime descending
                        //                   select booking).FirstOrDefault();

                        //var getBookingss = (from booking in _trUnitOrderDetail.GetAll()
                        //                    join a in _trUnitOrderHeader.GetAll() on booking.UnitOrderHeaderID equals a.Id
                        //                    where a.Id == input.orderHeaderID
                        //                    select booking).FirstOrDefault();

                        var dataOrder = (from a in _trUnitOrderHeader.GetAll()
                                         join b in _trUnitOrderDetail.GetAll() on a.Id equals b.UnitOrderHeaderID
                                         join c in _msSumberDana.GetAll() on a.sumberDanaID equals c.Id
                                         join d in _msTujuanTransaksi.GetAll() on a.tujuanTransaksiID equals d.Id
                                         join e in _msUnitRepo.GetAll() on b.unitID equals e.Id
                                         join f in _msProjectRepo.GetAll() on e.projectID equals f.Id
                                         where b.unitID == item.unitID
                                         select new KonfirmasiPesananDto
                                         {
                                            orderCode = a.orderCode,
                                            unitID = b.unitID,
                                            sumberDanaPembelian = c.sumberDanaName,
                                            tujuanTransaksi = d.tujuanTransaksiName,
                                            imageProject = f.image != null ? f.image : "-",
                                            kodePelanggan = a.psCode,
                                            psName = a.psName,
                                            noHpPembeli = a.psPhone,
                                            email = a.psEmail,
                                            noDealCloser = a.memberCode,
                                            namaDealCloser = a.memberName,
                                            namaBank = a.bankRekeningPemilik,
                                            noRekening = a.nomorRekeningPemilik,
                                            bfAmount = b.BFAmount.ToString(),
                                            hargaJual = b.sellingPrice.ToString(),
                                            tanggalBooking = bookingHeaderID.bookDate.ToString(),
                                            renovID = b.renovID,
                                            caraPembayaran = bookingHeaderID.termRemarks
                                         }).FirstOrDefault();

                        var getCustomerInfo = (from a in _contextPers.PERSONAL
                                               join b in _contextPers.TR_ID on a.psCode equals b.psCode into idno
                                               from b in idno.DefaultIfEmpty()
                                               where a.psCode == dataOrder.kodePelanggan
                                               select new
                                               {
                                                   a.birthDate,
                                                   a.NPWP,
                                                   idNo = b.idNo != null ? b.idNo : "-"
                                               }).FirstOrDefault();

                        dataOrder.birthDate = getCustomerInfo.birthDate.ToString();
                        dataOrder.noNPWP = getCustomerInfo.NPWP;
                        dataOrder.noIdentitas = getCustomerInfo.idNo;

                        var getMember = (from a in _contextPers.PERSONALS_MEMBER
                                         join b in _contextPers.TR_Phone on a.psCode equals b.psCode into phone
                                         from b in phone.DefaultIfEmpty()
                                         where a.memberCode == dataOrder.noDealCloser
                                         select b.number).FirstOrDefault();

                        dataOrder.noHp = getMember;

                        var getDataUnit = (from a in _msUnitRepo.GetAll()
                                           join b in _msUnitCode.GetAll() on a.unitCodeID equals b.Id
                                           join c in _msCluster.GetAll() on a.clusterID equals c.Id
                                           join d in _msCategory.GetAll() on a.categoryID equals d.Id
                                           join e in _msUnitItem.GetAll() on a.Id equals e.unitID
                                           join h in _msDetail.GetAll() on a.detailID equals h.Id
                                           group e by new
                                           {
                                               a.unitNo,
                                               b.unitCode,
                                               c.clusterName,
                                               d.categoryName,
                                               h.detailName,
                                               a.Id
                                           } into G
                                           where G.Key.Id == item.unitID
                                           select new unitDto
                                           {
                                               UnitNo = G.Key.unitNo,
                                               UnitCode = G.Key.unitCode,
                                               cluster = G.Key.clusterName,
                                               category = G.Key.categoryName,
                                               tipe = G.Key.detailName,
                                               luas = G.Sum(a => a.area).ToString()
                                           }).ToList();

                        var getRenov = (from a in _msRenovation.GetAll()
                                        where a.Id == dataOrder.renovID
                                        select a.renovationName).FirstOrDefault();

                        getDataUnit.FirstOrDefault().renovation = getRenov;

                        dataOrder.listUnit = getDataUnit;

                        //getDataOrder.FirstOrDefault().listUnit = getDataUnit;

                        var dataBank = (from bank in _msBankOLBooking.GetAll()
                                        join unit in _msUnitRepo.GetAll() on new { bank.projectID, bank.clusterID } equals new { unit.projectID, unit.clusterID }
                                        where unit.Id == dataOrder.unitID
                                        select new listBankDto
                                        {
                                            bankName = bank.bankName,
                                            noVA = bank.bankRekNo
                                        }).ToList();

                        dataOrder.listBank = dataBank;

                        Logger.InfoFormat(JsonConvert.SerializeObject(dataBank));

                        var getProjectInfo = (from project in _context.MS_Project
                                              join info in _context.MS_ProjectInfo on project.Id equals info.projectID into a
                                              from projectInfo in a.DefaultIfEmpty()
                                              join unit in _context.MS_Unit on project.Id equals unit.projectID
                                              where unit.Id == item.unitID
                                              orderby projectInfo.CreationTime descending
                                              select new
                                              {
                                                  project.projectName,
                                                  projectInfo.projectMarketingOffice,
                                                  projectInfo.projectMarketingPhone
                                              }).FirstOrDefault();

                        Logger.InfoFormat(JsonConvert.SerializeObject(getProjectInfo));
                        Logger.InfoFormat(JsonConvert.SerializeObject(dataOrder));


                        var sendEmail = new BookingSuccessInputDto
                        {
                            bookDate = DateTime.Now,
                            customerName = dataOrder.psName,
                            devPhone = getProjectInfo.projectMarketingPhone != null ? getProjectInfo.projectMarketingPhone : "-",
                            memberName = dataOrder.namaDealCloser,
                            memberPhone = dataOrder.noHp != null ? dataOrder.noHp : "-",
                            projectImage = dataOrder.imageProject != null ? dataOrder.imageProject : "-",
                            projectName = getProjectInfo.projectName
                        };
                        var body = _emailAppService.bookingSuccess(sendEmail);

                        using (var client = new WebClient())
                        {
                            var url = _configuration.ApiPdfUrl.EnsureEndsWith('/') + "api/Pdf/KonfirmasiPesananPdf";
                            client.Headers.Add("Content-Type:application/json");
                            client.Headers.Add("Accept:application/json");
                            var result = client.UploadString(url, JsonConvert.SerializeObject(dataOrder));
                            Logger.InfoFormat(result);
                            var trimResult = result.Replace(@"\\", @"\").Trim(new char[1] { '"' });
                            Logger.InfoFormat(trimResult);

                            var email = new SendEmailInputDto
                            {
                                body = body,
                                toAddress = dataOrder.email,
                                subject = "Konfirmasi Pemesanan Unit" + getDataUnit.FirstOrDefault().UnitCode + " " + getDataUnit.FirstOrDefault().UnitNo,
                                pathKP = trimResult
                            };

                            _emailAppService.ConfigurationEmail(email);
                        }
                    }
                    unitOfWork.Complete();



                    return new ResultMessageDto { result = true, message = bookingHeaderID.message };
                }
                else
                {
                    return new ResultMessageDto { result = false, message = "Parameter cannot be null" };
                }
                
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrCashAddDisc)]
        private ResultMessageDto InsertTrCashAddDisc(TrCashAddDiscInputDto input)
        {
            var checkTerm = (from a in _msTermRepo.GetAll()
                             where a.Id == input.termID
                             select a.termNo).FirstOrDefault();

            if (checkTerm == 1)
            {
                var getDisc = (from a in _msUnitItemPriceRepo.GetAll()
                               join b in _msUnitItem.GetAll()
                               on a.unitID equals b.unitID
                               join c in _msTermRepo.GetAll()
                               on a.termID equals c.Id
                               join d in _msTermAddDiscRepo.GetAll()
                               on c.Id equals d.termID
                               join e in _trBookingHeaderRepo.GetAll()
                               on a.unitID equals e.unitID
                               join f in _trBookingDetailRepo.GetAll()
                               on e.Id equals f.bookingHeaderID
                               where a.unitID == input.unitID && a.termID == input.termID && f.Id == input.bookingDetailID
                               select new
                               {
                                   addDisc = d.addDiscPct,
                                   addDiscNo = d.addDiscNo,
                                   coCOde = b.coCode,
                                   bookingDetailID = f.Id,
                                   bookNo = f.bookNo,
                                   entityID = f.entityID
                               }).FirstOrDefault();
                if (getDisc != null)
                {
                    var data = new TR_CashAddDisc
                    {
                        pctAddDisc = getDisc.addDisc,
                        addDiscNo = getDisc.addDiscNo,
                        bookingDetailID = getDisc.bookingDetailID,
                        entityID = getDisc.entityID
                    };
                    _trCashAddDisc.Insert(data);

                    return new ResultMessageDto { result = true };
                }
                else
                {
                    return new ResultMessageDto { message = "Data not found", result = false };
                }
            }

            return new ResultMessageDto { result = true };
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingSalesAddDisc)]
        private List<InsertTrBookingSalesAddDiscResultDto> InsertTrBookingSalesAddDisc(TrBookingSalesAddDiscInputDto input)
        {
            var getSalesDisc = (from a in _msUnitItem.GetAll()
                                join b in _msUnitItemPriceRepo.GetAll()
                                on new { a.unitID, a.itemID } equals new { b.unitID, b.itemID }
                                join c in _trBookingHeaderRepo.GetAll()
                                on new { a.unitID, b.termID } equals new { c.unitID, c.termID }
                                where a.unitID == input.unitID && b.termID == input.termID && c.Id == input.bookingHeaderID
                                select new
                                {
                                    a.pctDisc,
                                    a.pctTax,
                                    a.itemID,
                                    c.Id
                                }).ToList();

            var penampung = new List<InsertTrBookingSalesAddDiscResultDto>();

            foreach (var item in getSalesDisc)
            {
                var data = new TR_BookingSalesDisc
                {
                    bookingHeaderID = item.Id,
                    itemID = item.itemID,
                    pctDisc = item.pctDisc,
                    pctTax = item.pctDisc
                };

                _trBookingSalesDisc.Insert(data);

                var result = new InsertTrBookingSalesAddDiscResultDto
                {
                    bookingHeaderID = item.Id,
                    pctTax = item.pctTax,
                    itemID = item.itemID
                };
                penampung.Add(result);
            }

            return penampung;

        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_GetDetailBookingUnit)]
        public ListDetailBookingUnitResultDto GetDetailBookingUnit(int userID, string psCode)
        {
            //get user personal
            var getUserPersonal = (from a in _mpUserPersonals.GetAll()
                                   where a.userID == userID
                                   select a).FirstOrDefault();

            //get member
            var getMember = (from a in _personalsMemberRepo.GetAll()
                             where a.psCode == getUserPersonal.psCode
                             select new
                             {
                                 a.memberCode
                             }).FirstOrDefault();

            //customer detail
            var getCustomer = (from a in _personalsRepo.GetAll()
                               join b in _trIDRepo.GetAll() on a.psCode equals b.psCode
                               join c in _trEmailRepo.GetAll() on a.psCode equals c.psCode
                               where a.psCode == psCode
                               select new GetCustomer
                               {
                                   customerName = a.name,
                                   birthDate = a.birthDate,
                                   idNo = b.idNo,
                                   email = c.email
                               }).FirstOrDefault();

            //unit detail
            var getDetailBooking = (from unitReserved in _trUnitReservedRepo.GetAll()
                                    join term in _msTermRepo.GetAll()
                                    on unitReserved.termID equals term.Id
                                    //REPAIR BENER NGGK?
                                    join renov in _msRenovation.GetAll()
                                    on unitReserved.renovID equals renov.Id
                                    join unit in _msUnitRepo.GetAll()
                                    on unitReserved.unitID equals unit.Id
                                    join cluster in _msClusterRepo.GetAll()
                                    on unit.clusterID equals cluster.Id
                                    join project in _msProjectRepo.GetAll()
                                    on unit.projectID equals project.Id
                                    join unitCode in _msUnitCode.GetAll()
                                    on unit.unitCodeID equals unitCode.Id
                                    where unitReserved.releaseDate == null
                                    && unitReserved.reservedBy == getMember.memberCode
                                    && unitReserved.pscode == psCode
                                    && unitReserved.remarks == null
                                    select new ListUnit
                                    {
                                        clusterID = cluster.Id,
                                        projectID = project.Id,
                                        renovID = renov.Id,
                                        termID = term.Id,
                                        unitID = unit.Id,
                                        clusterName = cluster.clusterName,
                                        unitCode = unitCode.unitCode,
                                        unitNo = unit.unitNo,
                                        projectName = project.projectName,
                                        termName = term.remarks,
                                        renovName = renov.renovationName,
                                        sellingPrice = unitReserved.SellingPrice,
                                        bookingFee = unitReserved.BFAmount
                                    }).ToList();

            //total amount to be paid
            var amount = (from a in _trUnitReservedRepo.GetAll()
                          where a.releaseDate == null
                              && a.reservedBy == getMember.memberCode
                              && a.pscode == psCode
                              && a.remarks == null
                          group a by new
                          {
                              a.pscode
                          } into G
                          select new
                          {
                              sellingPrice = G.Sum(x => x.BFAmount)
                          }
                          ).FirstOrDefault();

            if (getDetailBooking != null)
            {
                var result = new ListDetailBookingUnitResultDto
                {
                    unit = getDetailBooking,
                    amountToBePaid = amount.sellingPrice,
                    customer = getCustomer
                };
                return result;
            }
            else
            {
                var result = new ListDetailBookingUnitResultDto
                {
                    message = "Data not found"
                };
                return result;
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateBFAmount)]
        private List<UpdateBfAmoutResultDto> UpdateBFAmount(UpdateBFAmountInputDto input)
        {
            var arrBfAmount = new List<UpdateBfAmoutResultDto>();

            var getNetPrice = (from a in _msUnitRepo.GetAll()
                               join b in _msTermMain.GetAll() on a.termMainID equals b.Id
                               join c in _msTermRepo.GetAll() on b.Id equals c.termMainID
                               join d in _msUnitItem.GetAll() on a.Id equals d.unitID
                               join e in _msUnitItemPriceRepo.GetAll() on new { unitID = a.Id, termID = c.Id, d.itemID } equals new { unitID = e.unitID, termID = e.termID, e.itemID }
                               where a.Id == input.unitID && c.termNo == 3
                               //&& (c.remarks.ToLowerInvariant() == "cicilan 12 kali" || c.remarks.ToLowerInvariant() == "cicilan 12x")
                               orderby e.itemID ascending
                               select new
                               {
                                   BFAmount = b.BFAmount,
                                   grossPrice = e.grossPrice,
                                   itemID = e.itemID,
                                   remarks = c.remarks
                               }).Distinct().ToList();

            var totalGrossPrice = getNetPrice.Sum(s => s.grossPrice);

            foreach (var item in getNetPrice)
            {
                var BFAlloc = Math.Round((item.grossPrice / totalGrossPrice) * item.BFAmount, 2);

                var getBookingDetail = (from a in _trBookingHeaderRepo.GetAll()
                                        join b in _trBookingDetailRepo.GetAll() on a.Id equals b.bookingHeaderID
                                        where a.unitID == input.unitID && b.itemID == item.itemID
                                        select b).FirstOrDefault();

                var updateBookingDetail = getBookingDetail.MapTo<TR_BookingDetail>();

                updateBookingDetail.BFAmount = BFAlloc;

                var bfAmount = new UpdateBfAmoutResultDto
                {
                    bfAmount = BFAlloc
                };
                arrBfAmount.Add(bfAmount);
            }

            return arrBfAmount;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingHeaderTerm)]
        private List<InsertTrBookingHeaderTermResultDto> InsertTrBookingHeaderTerm(InsertTrBookingHeaderTermInputDto input)
        {
            var getTerm = (from a in _msUnitRepo.GetAll()
                           join b in _msTermMain.GetAll() on a.termMainID equals b.Id
                           join c in _msTermRepo.GetAll() on b.Id equals c.termMainID
                           join d in _msTermAddDiscRepo.GetAll() on c.Id equals d.termID
                           join e in _msTermPmt.GetAll() on c.Id equals e.termID
                           join f in _trBookingHeaderRepo.GetAll() on a.Id equals f.unitID
                           join g in _lkFinType.GetAll() on e.finTypeID equals g.Id
                           where a.Id == input.unitID
                           select new
                           {
                               c.DPCalcType,
                               c.PPJBDue,
                               c.discBFCalcType,
                               d.addDiscPct,
                               d.addDiscNo,
                               bookingHeaderID = f.Id,
                               e.finStartDue,
                               termID = c.Id,
                               g.finTypeCode,
                               c.remarks
                           }).ToList();

            var penampung = new List<InsertTrBookingHeaderTermResultDto>();

            foreach (var item in getTerm)
            {
                var data = new TR_BookingHeaderTerm
                {
                    DPCalcType = item.DPCalcType,
                    PPJBDue = item.PPJBDue,
                    addDiscNo = item.addDiscNo,
                    addDisc = item.addDiscPct,
                    bookingHeaderID = item.bookingHeaderID,
                    discBFCalcType = item.discBFCalcType,
                    finStartDue = item.finStartDue,
                    finTypeCode = item.finTypeCode,
                    remarks = item.remarks,
                    termID = item.termID
                };
                _trBookingHeaderTerm.Insert(data);

                var result = new InsertTrBookingHeaderTermResultDto
                {
                    bookingHeaderID = item.bookingHeaderID,
                    remarks = item.remarks,
                    termID = item.termID
                };
                penampung.Add(result);
            }

            return penampung;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingItemPrice)]
        private List<InsertTrBookingItemPriceResultDto> InsertTrBookingItemPrice(InsertTrBookingHeaderTermInputDto input)
        {
            var getItemPrice = (from a in _msUnitItemPriceRepo.GetAll()
                                join b in _trBookingHeaderRepo.GetAll() on a.unitID equals b.unitID
                                join d in _msRenovation.GetAll() on a.renovID equals d.Id
                                where a.unitID == input.unitID
                                select new
                                {
                                    a.entityID,
                                    a.grossPrice,
                                    a.itemID,
                                    a.termID,
                                    a.unitID,
                                    b.Id,
                                    d.renovationCode
                                }).ToList();

            var penampung = new List<InsertTrBookingItemPriceResultDto>();

            foreach (var item in getItemPrice)
            {
                var data = new TR_BookingItemPrice
                {
                    bookingHeaderID = item.Id,
                    entityID = item.entityID,
                    itemID = item.itemID,
                    grossPrice = item.grossPrice,
                    renovCode = item.renovationCode,
                    termID = item.termID
                };
                _trBookingItemPrice.Insert(data);

                var result = new InsertTrBookingItemPriceResultDto
                {
                    bookingHeaderID = item.Id,
                    grossPrice = item.grossPrice,
                    itemID = item.itemID,
                    termID = item.termID
                };
                penampung.Add(result);
            }

            return penampung;

        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateUnitSold)]
        private ResultMessageDto UpdateUnitSold(InsertTrBookingHeaderTermInputDto input)
        {
            var getSoldStatusID = (from a in _lkUnitStatus.GetAll()
                                   where a.unitStatusCode == "S"
                                   select a.Id).FirstOrDefault();

            var getUnit = (from a in _msUnitRepo.GetAll()
                           where a.Id == input.unitID
                           select a).FirstOrDefault();

            if (getUnit != null)
            {
                var updateUnit = getUnit.MapTo<MS_Unit>();

                updateUnit.unitStatusID = getSoldStatusID;

                _msUnitRepo.Update(updateUnit);

                return new ResultMessageDto { result = true };
            }
            else
            {
                return new ResultMessageDto { result = false, message = "Data not found" };
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateRemarksTrBookingHeader)]
        private ResultMessageDto UpdateRemarksTrBookingHeader(UpdateRemarksDto input)
        {
            var getData = (from header in _trBookingHeaderRepo.GetAll()
                           where header.Id == input.bookingHeaderID
                           select header).FirstOrDefault();

            var getRemarks = (from reserved in _trUnitReservedRepo.GetAll()
                              join unit in _msUnitRepo.GetAll()
                              on reserved.unitID equals unit.Id
                              join unitcode in _msUnitCode.GetAll()
                              on unit.unitCodeID equals unitcode.Id
                              join renovation in _msRenovation.GetAll()
                              on reserved.renovID equals renovation.Id
                              join term in _msTermRepo.GetAll()
                              on reserved.termID equals term.Id
                              join project in _msProject.GetAll()
                              on unit.projectID equals project.Id
                              where unit.Id == getData.unitID
                              select new
                              {
                                  unitcode.unitCode,
                                  unit.unitNo,
                                  renovation.renovationCode,
                                  term.termCode,
                                  term.termNo,
                                  term.remarks,
                                  project.projectCode
                              }).FirstOrDefault();

            var remarks = "[DailyDealCloser] SP "
            + getRemarks.unitCode + " " + getRemarks.unitNo + " "
            + getRemarks.renovationCode + " " + getRemarks.termCode + " " + getRemarks.termNo + "-"
            + getRemarks.remarks + " Booking Success. Book Code: "
            + BookCode(getRemarks.projectCode) + " NetNetPriceSP+PPN: " + string.Format("{0:n2}", input.sellingPrice) + " "
            + "NetNetPriceSPR+PPN: " + string.Format("{0:n2}", 0.00);



            var updateRemarks = getData.MapTo<TR_BookingHeader>();

            updateRemarks.remarks = remarks;

            _trBookingHeaderRepo.Update(updateRemarks);

            return new ResultMessageDto
            {
                message = "Success",
                result = true
            };
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingTax)]
        private ResultMessageDto InsertTrBookingTax(InsertTrBookingTaxInputDto input)
        {
            var checkCategory = (from unit in _msUnitRepo.GetAll()
                                 join category in _msCategory.GetAll()
                                 on unit.categoryID equals category.Id
                                 where unit.Id == input.unitID
                                 select category.categoryCode).FirstOrDefault();

            var getArea = (from unit in _msUnitRepo.GetAll()
                           join unitItem in _msUnitItem.GetAll()
                           on unit.Id equals unitItem.unitID
                           where unit.Id == input.unitID && unitItem.itemID == 2
                           select unitItem.area).FirstOrDefault();

            var getTerm = (from term in _msTermRepo.GetAll()
                           where term.Id == input.termID
                           select term.termNo).FirstOrDefault();

            var getTaxType = (from taxType in _msTaxType.GetAll()
                              where taxType.taxTypeCode.ToLowerInvariant() == "pph22"
                              select taxType.Id).FirstOrDefault();

            if (checkCategory.ToLowerInvariant() == "lan" && input.sellingPrice >= 5000000000 && getArea >= 400 && getTerm == 1)
            {
                var tax = (double)input.netNetPrice * 0.05;

                var data = new TR_BookingTax
                {
                    amount = (decimal)tax,
                    bookingDetailID = input.bookingDetailID,
                    taxTypeID = getTaxType
                };
                _trBookingTax.Insert(data);
            }
            else if (checkCategory.ToLowerInvariant() == "kdm" && input.sellingPrice >= 5000000000 && getArea >= 150 && getTerm == 1)
            {
                var tax = (double)input.netNetPrice * 0.05;

                var data = new TR_BookingTax
                {
                    amount = (decimal)tax,
                    bookingDetailID = input.bookingDetailID,
                    taxTypeID = getTaxType
                };
                _trBookingTax.Insert(data);
            }
            else
            {
                var data = new TR_BookingTax
                {
                    amount = 0,
                    bookingDetailID = input.bookingDetailID,
                    taxTypeID = getTaxType
                };
                _trBookingTax.Insert(data);
            }
            return new ResultMessageDto
            {
                message = "Success",
                result = true
            };
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateOrderStatusFullyPaid)]
        private ResultMessageDto UpdateOrderStatusFullyPaid(UpdateOrderStatusFullyPaid input)
        {
            var getStatus = (from status in _lkBookingOnlineStatus.GetAll()
                             where status.statusType == "2"
                             select status.Id).FirstOrDefault();

            var getOrderHeader = (from orderHeader in _trUnitOrderHeader.GetAll()
                                  where orderHeader.Id == input.orderHeaderID
                                  select orderHeader).FirstOrDefault();

            var update = getOrderHeader.MapTo<TR_UnitOrderHeader>();

            update.statusID = getStatus;

            _trUnitOrderHeader.Update(update);

            var getOrderDetail = (from orderDetail in _trUnitOrderDetail.GetAll()
                                  where orderDetail.UnitOrderHeaderID == input.orderHeaderID
                                  select orderDetail).Distinct().ToList();
            foreach (var item in getOrderDetail)
            {
                var updateDetail = item.MapTo<TR_UnitOrderDetail>();

                updateDetail.bookingHeaderID = input.bookingHeaderID;

                _trUnitOrderDetail.Update(updateDetail);
            }

            return new ResultMessageDto
            {
                result = true
            };
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateReleaseDate)]
        private ResultMessageDto UpdateReleaseDate(UpdateReleaseDateInputDto input)
        {
            var getReserved = (from reserved in _trUnitReservedRepo.GetAll()
                               where reserved.unitID == input.unitID
                               && reserved.termID == input.termID
                               && reserved.renovID == input.renovID
                               && reserved.pscode == input.psCode
                               select reserved).FirstOrDefault();
            var update = getReserved.MapTo<TR_UnitReserved>();

            update.releaseDate = DateTime.Now;

            _trUnitReservedRepo.Update(update);

            return new ResultMessageDto
            {
                result = true
            };
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrSoldUnit)]
        private ResultMessageDto InsertTrSoldUnit(InsertTrSoldUnitInputDto input)
        {
            //var getProject = (from project in _context.MS_Project.ToList()
            //                  join unit in _context.MS_Unit.ToList()
            //                  on project.Id equals unit.projectID
            //                  join unitCode in _context.MS_UnitCode.ToList()
            //                  on unit.unitCodeID equals unitCode.Id
            //                  join cluster in _context.MS_Cluster.ToList()
            //                  on unit.clusterID equals cluster.Id
            //                  where unit.Id == input.unitID
            //                  select new
            //                  {
            //                      project.BusinessGroup,
            //                      unitCode.unitCode,
            //                      projectID = project.Id,
            //                      project.projectCode,
            //                      unit.clusterID,
            //                      cluster.clusterCode,
            //                      unitCode.unitName,
            //                      unit.unitNo
            //                  }).FirstOrDefault();

            var getProject = (from project in _msProject.GetAll()
                              join unit in _msUnitRepo.GetAll()
                              on project.Id equals unit.projectID
                              join unitCode in _msUnitCode.GetAll()
                              on unit.unitCodeID equals unitCode.Id
                              join cluster in _msCluster.GetAll()
                              on unit.clusterID equals cluster.Id
                              where unit.Id == input.unitID
                              select new
                              {
                                  project.BusinessGroup,
                                  unitCode.unitCode,
                                  projectID = project.Id,
                                  project.projectCode,
                                  unit.clusterID,
                                  cluster.clusterCode,
                                  unitCode.unitName,
                                  unit.unitNo
                              }).FirstOrDefault();

            //var getLandItemID = (from item in _context.LK_Item.ToList()
            //                   where item.itemCode == "01"
            //                   select item.Id).FirstOrDefault();

            var getLandItemID = (from item in _lkItem.GetAll()
                                 where item.itemCode == "01"
                                 select item.Id).FirstOrDefault();

            var getBuildItemID = (from item in _lkItem.GetAll()
                                  where item.itemCode == "02"
                                  select item.Id).FirstOrDefault();

            var listBookingDetail = input.listBookingDetail.ToList();

            var getLandArea = (from detail in listBookingDetail
                               where detail.itemID == getLandItemID
                               select new
                               {
                                   detail.area,
                                   detail.bookNo,
                                   detail.coCode
                               }).FirstOrDefault();

            //var getLandArea = (from bookingDetail in _trBookingDetailRepo.GetAll()
            //                   join bookingHeader in _trBookingHeaderRepo.GetAll()
            //                   on bookingDetail.bookingHeaderID equals bookingHeader.Id
            //                   where bookingHeader.unitID == input.unitID
            //                   && bookingDetail.itemID == getLandItemID
            //                   select new
            //                   {
            //                       bookingDetail.area,
            //                       bookingDetail.bookNo,
            //                       bookingDetail.coCode
            //                   }).FirstOrDefault();

            var getBuildArea = (from detail in listBookingDetail
                                where detail.itemID == getBuildItemID
                                select detail.area).FirstOrDefault();

            //var getBuildArea = (from bookingDetail in _trBookingDetailRepo.GetAll()
            //                    join bookingHeader in _trBookingHeaderRepo.GetAll()
            //                    on bookingDetail.bookingHeaderID equals bookingHeader.Id
            //                    where bookingHeader.unitID == input.unitID
            //                    && bookingDetail.itemID == getBuildItemID
            //                    select bookingDetail.area).FirstOrDefault();

            var getPctBobot = (from bobotComm in _contextNew.MS_BobotComm.ToList()
                               where bobotComm.projectCode == getProject.projectCode
                               && bobotComm.clusterCode == getProject.clusterCode
                               && bobotComm.scmCode == input.scmCode
                               select bobotComm.pctBobot).FirstOrDefault();

            var getPctComm = (from termPmt in _msTermPmt.GetAll()
                              join finType in _lkFinType.GetAll()
                              on termPmt.finTypeID equals finType.Id
                              where termPmt.termID == input.listBookingHeader.termID
                              select finType.pctComm).FirstOrDefault();

            var totalNetNetPrice = input.netNetPrice.Sum(x => x.netNetPrice);

            var unitPrice = totalNetNetPrice * (decimal)getPctBobot;

            var insert = new TR_SoldUnit
            {
                bookNo = getLandArea.bookNo.ToString(),
                bookDate = DateTime.Now,
                calculateUseMaster = true,
                memberCode = input.memberCode,
                pctBobot = getPctBobot,
                roadCode = getProject.unitCode,
                roadName = getProject.unitName,
                //termRemarks = getTerm.termRemarks,
                unitBuildArea = (float)getBuildArea,
                unitLandArea = (float)getLandArea.area,
                scmCode = input.scmCode,
                //unitID = input.unitID,
                unitNo = getProject.unitNo,
                //REPAIR HANAN
                ACDCode = "-",
                batchNo = "-",
                cancelDate = null,
                CDCode = "-",
                //changeDealClosureReason = string.Empty,
                entityCode = "1",
                holdDate = null,
                //holdReason = string.Empty,
                PPJBDate = null,
                Remarks = input.listBookingHeader.termRemarks,
                xprocessDate = null,
                xreqInstPayDate = null,
                devCode = getLandArea.coCode,
                netNetPrice = totalNetNetPrice,
                unitPrice = unitPrice,
                pctComm = getPctComm,
                //FIXING
                propCode = "LK",
                inputTime = DateTime.Now,
                inputUN = input.memberCode,
                modifTime = DateTime.Now,
                modifUN = input.memberCode
            };
            _contextNew.Add(insert);

            //_contextNew.SaveChanges();

            return new ResultMessageDto
            {
                result = true
            };
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrSoldUnitRequirement)]
        private ResultMessageDto InsertTrSoldUnitRequirement(InsertTrSoldUnitRequirementInputDto input)
        {
            var listBookingDetail = input.listBookingDetail.ToList();

            var getScmID = (from schema in _contextNew.MS_Schema.ToList()
                            where schema.scmCode == input.scmCode
                            select schema).FirstOrDefault();

            if (getScmID != null)
            {
                var getSoldUnitRequirement = (from schemaRequirement in _contextNew.MS_SchemaRequirement.ToList()
                                              where schemaRequirement.scmCode == getScmID.scmCode
                                              select schemaRequirement).ToList();

                if (getSoldUnitRequirement != null)
                {


                    var getLandItemID = (from item in _lkItem.GetAll()
                                         where item.itemCode == "01"
                                         select item.Id).FirstOrDefault();

                    var getBookNo = (from detail in listBookingDetail
                                     where detail.itemID == getLandItemID
                                     select new
                                     {
                                         detail.bookNo,
                                         detail.coCode
                                     }).FirstOrDefault();

                    //var getBookNo = (from bookingDetail in _trBookingDetailRepo.GetAll()
                    //                 join bookingHeader in _trBookingHeaderRepo.GetAll()
                    //                 on bookingDetail.bookingHeaderID equals bookingHeader.Id
                    //                 where bookingHeader.unitID == input.unitID
                    //                 && bookingDetail.itemID == getLandItemID
                    //                 select new
                    //                 {
                    //                     bookingDetail.bookNo,
                    //                     bookingDetail.coCode
                    //                 }).FirstOrDefault();

                    foreach (var item in getSoldUnitRequirement)
                    {
                        var data = new TR_SoldUnitRequirement
                        {
                            bookNo = getBookNo.bookNo.ToString(),
                            entityCode = item.entityCode,
                            orPctPaid = item.orPctPaid,
                            pctPaid = item.pctPaid,
                            reqDesc = item.reqDesc,
                            reqNo = (byte)item.reqNo,
                            scmCode = item.scmCode,
                            processDate = null,
                            reqDate = null,
                            devCode = getBookNo.coCode,
                            inputTime = DateTime.Now,
                            inputUN = input.memberCode,
                            modifTime = DateTime.Now,
                            modifUN = input.memberCode
                        };

                        _contextNew.Add(data);

                        //_contextNew.SaveChanges();
                    }
                }
            }
            return new ResultMessageDto
            {
                result = true
            };
        }

        public void SchedulerStatusOrderExpired()
        {
            var getStatusOutstanding = (from status in _lkBookingOnlineStatus.GetAll()
                                        where status.statusType == "1"
                                        select status.Id).FirstOrDefault();

            var getData = (from orderHeader in _trUnitOrderHeader.GetAll()
                           where orderHeader.statusID == getStatusOutstanding
                           orderby orderHeader.CreationTime descending
                           select orderHeader).Distinct().ToList();

            TimeSpan banding = TimeSpan.Parse("12:00:00");

            foreach (var item in getData)
            {
                if ((item.CreationTime - DateTime.Now) >= banding)
                {
                    var expiredDate = item.CreationTime.AddHours(12);
                    //REPAIR JAM 7 dan 10 dan 9
                    if (expiredDate.TimeOfDay <= TimeSpan.Parse("07:00:00") && expiredDate.TimeOfDay >= TimeSpan.Parse("22:00:00"))
                    {
                        if (DateTime.Now.TimeOfDay == TimeSpan.Parse("09:00:00"))
                        {
                            var getStatusCancel = (from status in _lkBookingOnlineStatus.GetAll()
                                                   where status.statusType == "3"
                                                   select status.Id).FirstOrDefault();

                            var update = item.MapTo<TR_UnitOrderHeader>();

                            update.statusID = getStatusCancel;

                            _trUnitOrderHeader.Update(update);

                            var getOrderDetail = (from orderheader in _trUnitOrderHeader.GetAll()
                                                  join orderdetail in _trUnitOrderDetail.GetAll()
                                                  on orderheader.Id equals orderdetail.UnitOrderHeaderID
                                                  where orderheader.Id == item.Id
                                                  select new
                                                  {
                                                      orderHeaderID = orderheader.Id,
                                                      unitID = orderdetail.unitID
                                                  }).Distinct().ToList();

                            foreach (var unit in getOrderDetail)
                            {
                                var getUnit = (from msUnit in _msUnitRepo.GetAll()
                                               where msUnit.Id == unit.unitID
                                               select msUnit).FirstOrDefault();

                                var getUnitStatus = (from unitStatus in _lkUnitStatus.GetAll()
                                                     where unitStatus.unitStatusCode == "A"
                                                     select unitStatus.Id).FirstOrDefault();

                                var getCustInfo = (from personals in _contextPers.PERSONAL.ToList()
                                                   where personals.psCode == item.psCode
                                                   select personals).FirstOrDefault();

                                var getSalesInfo = (from member in _contextPers.PERSONALS_MEMBER.ToList()
                                                    join phone in _contextPers.TR_Phone.ToList() on member.psCode equals phone.psCode into trPhone
                                                    from c in trPhone.DefaultIfEmpty()
                                                    where member.memberCode == item.memberCode
                                                    select new { member, c }).FirstOrDefault();

                                var getProjectInfo = (from project in _msProject.GetAll()
                                                      join info in _msProjectInfo.GetAll() on project.Id equals info.projectID into a
                                                      from projectInfo in a.DefaultIfEmpty()
                                                      where project.Id == getUnit.projectID
                                                      orderby projectInfo.CreationTime descending
                                                      select new { project, projectInfo }).FirstOrDefault();

                                var getUnitCode = (from unitcode in _msUnitCode.GetAll()
                                                   where unitcode.Id == getUnit.unitCodeID
                                                   select unitcode.unitCode).FirstOrDefault();

                                var updateUnit = getUnit.MapTo<MS_Unit>();

                                updateUnit.unitStatusID = getUnitStatus;

                                _msUnitRepo.Update(updateUnit);

                                var emailExpired = new UnitExpiredInputDto
                                {
                                    customerName = getCustInfo.name,
                                    devPhone = getProjectInfo.projectInfo.projectMarketingPhone == null ? "-" : getProjectInfo.projectInfo.projectMarketingPhone,
                                    marketingOffice = getProjectInfo.projectInfo.projectMarketingOffice == null ? "-" : getProjectInfo.projectInfo.projectMarketingOffice,
                                    memberName = item.memberName,
                                    memberPhone = getSalesInfo.c.number == null ? "-" : getSalesInfo.c.number,
                                    orderCode = item.orderCode,
                                    projectImage = getProjectInfo.project.image,
                                    projectName = getProjectInfo.project.projectName,
                                    unitCode = getUnitCode,
                                    unitNo = getUnit.unitNo
                                };

                                var body = _emailAppService.UnitExpired(emailExpired);

                                var email = new SendEmailInputDto
                                {
                                    body = body,
                                    toAddress = "keniamalia1@gmail.com", //dataUnitOrderHeader.psEmail,
                                    subject = "Pembatalan  Order" + item.orderCode + "atas Unit" + getUnit + "/" + getUnitCode
                                };

                                _emailAppService.ConfigurationEmail(email);
                            }
                        }
                    }
                    else
                    {
                        var getStatus = (from status in _lkBookingOnlineStatus.GetAll()
                                         where status.statusType == "4"
                                         select status.Id).FirstOrDefault();

                        var getOrderHeader = (from orderheader in _trUnitOrderHeader.GetAll()
                                              join orderdetail in _trUnitOrderDetail.GetAll()
                                              on orderheader.Id equals orderdetail.UnitOrderHeaderID
                                              where orderheader.Id == item.Id
                                              select new
                                              {
                                                  orderHeaderID = orderheader.Id,
                                                  unitID = orderdetail.unitID
                                              }).Distinct().ToList();



                        var update = getOrderHeader.MapTo<TR_UnitOrderHeader>();

                        update.statusID = getStatus;

                        _trUnitOrderHeader.Update(update);

                        foreach (var unit in getOrderHeader)
                        {
                            var getUnit = (from msUnit in _msUnitRepo.GetAll()
                                           where msUnit.Id == unit.unitID
                                           select msUnit).FirstOrDefault();

                            var getUnitStatus = (from unitStatus in _lkUnitStatus.GetAll()
                                                 where unitStatus.unitStatusCode == "A"
                                                 select unitStatus.Id).FirstOrDefault();

                            var updateUnit = getUnit.MapTo<MS_Unit>();

                            updateUnit.unitStatusID = getUnitStatus;

                            _msUnitRepo.Update(updateUnit);
                        }
                    }

                }
            }
        }

        //harusnya scheduler
        public void SchedulerTRUnitReserved()
        {
            var getData = (from a in _trUnitReservedRepo.GetAll()
                           where a.releaseDate != null
                           orderby a.CreationTime descending
                           select new
                           {
                               a.Id,
                               a.reserveDate,
                               a.unitID
                           }).ToList();

            TimeSpan banding = TimeSpan.Parse("00:30:00");

            foreach (var item in getData)
            {
                if ((item.reserveDate - DateTime.Now) >= banding)
                {
                    var getUnitReserved = (from x in _trUnitReservedRepo.GetAll()
                                           where x.Id == item.Id
                                           select x).FirstOrDefault();

                    var updateUnitReserved = getUnitReserved.MapTo<TR_UnitReserved>();

                    updateUnitReserved.releaseDate = DateTime.Now;

                    _trUnitReservedRepo.Update(updateUnitReserved);

                    var getUnit = (from y in _msUnitRepo.GetAll()
                                   where y.Id == item.unitID
                                   select y).FirstOrDefault();

                    var updateUnit = getUnit.MapTo<MS_Unit>();

                    updateUnit.unitStatusID = 1;

                    _msUnitRepo.Update(updateUnit);
                }
            }
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_GetTrUnitReserved)]
        public List<GetTrUnitReservedDto> GetTrUnitReserved(int userID)
        {
            //get user personal
            var getUserPersonal = (from a in _mpUserPersonals.GetAll()
                                   where a.userID == userID
                                   select a).FirstOrDefault();

            //get member
            var getMember = (from a in _personalsMemberRepo.GetAll()
                             where a.psCode == getUserPersonal.psCode
                             select new
                             {
                                 a.memberCode
                             }).FirstOrDefault();

            var getTrUnitReserved = (from unitReserved in _trUnitReservedRepo.GetAll()
                                     join term in _msTermRepo.GetAll()
                                     on unitReserved.termID equals term.Id
                                     //REPAIR BENER NGGK?
                                     join renov in _msRenovation.GetAll()
                                     on unitReserved.renovID equals renov.Id
                                     join unit in _msUnitRepo.GetAll()
                                     on unitReserved.unitID equals unit.Id
                                     join cluster in _msClusterRepo.GetAll()
                                     on unit.clusterID equals cluster.Id
                                     join project in _msProjectRepo.GetAll()
                                     on unit.projectID equals project.Id
                                     join unitCode in _msUnitCode.GetAll()
                                     on unit.unitCodeID equals unitCode.Id
                                     where unitReserved.releaseDate == null
                                        && unitReserved.reservedBy == getMember.memberCode
                                        && unitReserved.remarks == null
                                     select new GetTrUnitReservedDto
                                     {
                                         unitReservedID = unitReserved.Id,
                                         clusterName = cluster.clusterName,
                                         unitID = unit.Id,
                                         unitCode = unitCode.unitCode,
                                         unitNo = unit.unitNo,
                                         projectID = project.Id,
                                         projectName = project.projectName,
                                         termID = term.Id,
                                         termName = term.remarks,
                                         renovID = renov.Id,
                                         renovName = renov.renovationName,
                                         sellingPrice = unitReserved.SellingPrice,
                                         bookingFee = unitReserved.BFAmount
                                     }).Distinct().ToList();

            return getTrUnitReserved;
        }

        private void UpdateRemarksReserved(UpdateRemarksReservedInputDto input)
        {
            //get user personal
            var getUserPersonal = (from a in _mpUserPersonals.GetAll()
                                   where a.userID == input.userID
                                   select a).FirstOrDefault();

            //get member
            var getMember = (from a in _personalsMemberRepo.GetAll()
                             where a.psCode == getUserPersonal.psCode
                             select new
                             {
                                 a.memberCode
                             }).FirstOrDefault();

            var getDisc1 = (from a in _msDiscOnlineBooking.GetAll()
                            where a.projectID == input.projectID
                            select new
                            {
                                a.discPct,
                                a.discDesc,
                            }).FirstOrDefault();

            var getDisc2 = (from a in _msTermDiscOnlineBooking.GetAll()
                            where a.termID == input.termID
                            select new
                            {
                                a.pctDisc,
                                a.discName
                            }).FirstOrDefault();


            var remarks = "Selling Price: " + input.sellingPrice + ", Disc1: " + (getDisc1 != null ? getDisc1.discDesc : "0")
                + ", Disc2: " + (getDisc2 != null ? getDisc2.discName : "0") + ".";

            var getReserved = (from reserved in _trUnitReservedRepo.GetAll()
                               where reserved.unitID == input.unitID
                               && reserved.reservedBy == getMember.memberCode
                               select reserved).FirstOrDefault();

            var update = getReserved.MapTo<TR_UnitReserved>();

            update.remarks = remarks;

            _trUnitReservedRepo.Update(update);
        }

        private string BookCode(string projectCode)
        {
            var dateTime = DateTime.Now;
            var dateString = dateTime.ToString();
            var year = (dateTime.Year).ToString().Substring(2, 2);
            var date = dateString.Substring(3, 2);
            var month = dateString.Substring(0, 2);
            var time = dateString.IndexOf(" ");
            var tm = dateString.Substring(time + 1).Replace(@":", string.Empty).Replace(@" ", string.Empty);
            var dt = tm.Remove(tm.Length - 2);
            var bookCode = "";

            if (tm.Length > 5)
            {
                var bookCodeSlash = projectCode + year + month + date + dt + dateTime.Millisecond.ToString();
                bookCode = bookCodeSlash.Replace(@"/", string.Empty);
            }
            else
            {
                var bookCodeSlash = projectCode + year + month + date + "0" + dt + dateTime.Millisecond.ToString();
                bookCode = bookCodeSlash.Replace(@"/", string.Empty);
            }

            return bookCode;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_UpdateNetPriceBookingHeaderDetail)]
        private List<UpdateNetPriceResultDto> UpdateNetPriceBookingHeaderDetail(UpdateBookingDetailInputDto input)
        {
            var getBookingItemPrice = input.listBookingItemPrice.ToList();
            var getSalesDisc = input.listSalesDisc.ToList();
            var getHeaderTerm = input.listBookingHeaderTerm.ToList();
            var getBookingDetail = input.listBookingDetail.ToList();
            var penampung = new List<UpdateNetPriceResultDto>();

            var getUpdateBookingDetail = (from itemPrice in getBookingItemPrice
                                          join salesDisc in getSalesDisc
                                          on new { itemPrice.bookingHeaderID, itemPrice.itemID }
                                          equals new { salesDisc.bookingHeaderID, salesDisc.itemID }
                                          join headerTerm in getHeaderTerm
                                          on itemPrice.termID equals headerTerm.termID
                                          join bookingDetail in getBookingDetail
                                          on new { itemPrice.bookingHeaderID, itemPrice.itemID }
                                          equals new { bookingDetail.bookingHeaderID, bookingDetail.itemID }
                             //join baru
                                          join term in _msTermRepo.GetAll()
                                          on itemPrice.termID equals term.Id
                                          where itemPrice.bookingHeaderID == input.bookingHeaderID
                                          && term.termNo == 3
                                          orderby itemPrice.itemID ascending
                                          select new
                                          {
                                              itemPrice.grossPrice,
                                              salesDisc.pctTax,
                                              headerTerm.remarks,
                                              bookingDetail.bookingDetailID
                                          }).ToList();

            var arrNetPriceComm = new List<ListNetPriceCommResultDto>();

            var netPrice = new ListNetPriceCommResultDto();

            foreach (var item in getUpdateBookingDetail)
            {
                var getBookDetail = (from a in _trBookingDetailRepo.GetAll()
                                     where a.bookingHeaderID == input.bookingHeaderID && a.Id == item.bookingDetailID
                                     orderby a.itemID ascending
                                     select a).FirstOrDefault();

                var updateBookingDetail = getBookDetail.MapTo<TR_BookingDetail>();

                var netPriceComm = Math.Round(item.grossPrice - (item.grossPrice * (decimal)item.pctTax), 2);

                updateBookingDetail.netPriceComm = netPriceComm;
                updateBookingDetail.netPriceMKT = netPriceComm;
                updateBookingDetail.amountComm = item.grossPrice;
                updateBookingDetail.amountMKT = item.grossPrice;

                _trBookingDetailRepo.Update(updateBookingDetail);

                netPrice = new ListNetPriceCommResultDto
                {
                    netPriceComm = Math.Round(item.grossPrice - (item.grossPrice * (decimal)item.pctTax), 2)
                };
                arrNetPriceComm.Add(netPrice);
            }

            var getUpdateNetPrice = (from itemPrice in getBookingItemPrice
                                     join salesDisc in getSalesDisc
                                     on new { itemPrice.bookingHeaderID, itemPrice.itemID }
                                     equals new { salesDisc.bookingHeaderID, salesDisc.itemID }
                                     join headerTerm in getHeaderTerm
                                     on itemPrice.termID equals headerTerm.termID
                                     join bookingDetail in getBookingDetail
                                     on new { itemPrice.bookingHeaderID, itemPrice.itemID }
                                     equals new { bookingDetail.bookingHeaderID, bookingDetail.itemID }
                                     where itemPrice.bookingHeaderID == input.bookingHeaderID
                                     && headerTerm.termID == input.termID
                                     orderby itemPrice.itemID ascending
                                     select new
                                     {
                                         itemPrice.grossPrice,
                                         salesDisc.pctTax,
                                         headerTerm.remarks,
                                         bookingDetail.bookingDetailID
                                     }).ToList();

            foreach (var items in getUpdateNetPrice)
            {
                var getDisc = (from a in _trUnitOrderDetail.GetAll()
                               join b in _trBookingHeaderRepo.GetAll() on new { a.unitID, a.termID } equals new { b.unitID, b.termID }
                               where b.Id == input.bookingHeaderID
                               select new
                               {
                                   a.disc1,
                                   a.disc2
                               }).FirstOrDefault();

                var getBookDetail = (from a in _trBookingDetailRepo.GetAll()
                                     where a.bookingHeaderID == input.bookingHeaderID && a.Id == items.bookingDetailID
                                     orderby a.itemID ascending
                                     select a).FirstOrDefault();

                var updateBookingDetail = getBookDetail.MapTo<TR_BookingDetail>();

                var netNetPrice = Math.Round(items.grossPrice - ((items.grossPrice * (getDisc == null ? 0 : (decimal)getDisc.disc1)) + (items.grossPrice * (getDisc == null ? 0 : (decimal)getDisc.disc2))), 2);

                updateBookingDetail.netPrice = Math.Round(items.grossPrice - (items.grossPrice * (decimal)items.pctTax), 2);
                updateBookingDetail.amount = items.grossPrice;
                updateBookingDetail.netNetPrice = netNetPrice;

                var getArrNetNetPrice = new UpdateNetPriceResultDto
                {
                    result = true,
                    netNetPrice = netNetPrice
                };
                penampung.Add(getArrNetNetPrice);
            }

            var getUpdateNetPriceCash = (from itemPrice in getBookingItemPrice
                                         join salesDisc in getSalesDisc
                                         on new { itemPrice.bookingHeaderID, itemPrice.itemID }
                                         equals new { salesDisc.bookingHeaderID, salesDisc.itemID }
                                         join headerTerm in getHeaderTerm
                                         on itemPrice.termID equals headerTerm.termID
                                         join bookingDetail in getBookingDetail
                                         on new { itemPrice.bookingHeaderID, itemPrice.itemID }
                                         equals new { bookingDetail.bookingHeaderID, bookingDetail.itemID }
                                         join term in _msTermRepo.GetAll()
                                         on itemPrice.termID equals term.Id
                                         where itemPrice.bookingHeaderID == input.bookingHeaderID
                                         && term.termNo == 3
                                         //&& headerTerm.remarks.ToLowerInvariant() == "cash"
                                         orderby itemPrice.itemID ascending
                                         select new
                                         {
                                             itemPrice.grossPrice,
                                             salesDisc.pctTax,
                                             headerTerm.remarks,
                                             bookingDetail.bookingDetailID
                                         }).ToList();

            foreach (var itm in getUpdateNetPriceCash)
            {
                var getBookDetail = (from a in _trBookingDetailRepo.GetAll()
                                     where a.bookingHeaderID == input.bookingHeaderID && a.Id == itm.bookingDetailID
                                     orderby a.itemID ascending
                                     select a).FirstOrDefault();

                var updateBookingDetail = getBookDetail.MapTo<TR_BookingDetail>();

                updateBookingDetail.netPriceCash = Math.Round(itm.grossPrice - (itm.grossPrice * (decimal)itm.pctTax), 2);
            }


            //Update Net Price Comm Booking Header
            var totalNetPriceComm = arrNetPriceComm.Sum(s => s.netPriceComm);

            var getBookingHeader = (from a in _trBookingHeaderRepo.GetAll()
                                    where a.Id == input.bookingHeaderID
                                    select a).FirstOrDefault();

            var updateBookingHeader = getBookingHeader.MapTo<TR_BookingHeader>();

            updateBookingHeader.netPriceComm = totalNetPriceComm;

            return penampung;
        }

        [UnitOfWork(isTransactional: false)]
        public async Task<ResultMessageDto> ReorderUnit(CreateTransactionUniversalDto input)
        {
            var getOrderHeader = (from header in _trUnitOrderHeader.GetAll()
                                  where header.Id == input.orderHeaderID
                                  select header).FirstOrDefault();

            var getStatusCancel = (from status in _lkBookingOnlineStatus.GetAll()
                                   where status.statusType == "4"
                                   select status.Id).FirstOrDefault();

            var update = getOrderHeader.MapTo<TR_UnitOrderHeader>();

            update.statusID = getStatusCancel;

            _trUnitOrderHeader.Update(update);


            var dataDoBooking = new CreateTransactionUniversalDto
            {
                arrPP = input.arrPP,
                arrUnit = input.arrUnit,
                totalAmt = input.totalAmt,
                bankRekeningPemilik = input.bankRekeningPemilik,
                memberCode = input.memberCode,
                memberName = input.memberName,
                nomorRekeningPemilik = input.nomorRekeningPemilik,
                orderCode = input.orderCode,
                orderHeaderID = input.orderHeaderID,
                payTypeID = input.payTypeID,
                pscode = input.pscode,
                scmCode = input.scmCode,
                sumberDanaID = input.sumberDanaID,
                tujuanTransaksiID = input.tujuanTransaksiID,
                userID = input.userID
            };

            await DoBookingMidransReq(dataDoBooking);

            return new ResultMessageDto
            {
                result = true
            };
        }

        private int GetAllocIDbyCode(string allocCode)
        {
            var allocID = (from a in _lkAlloc.GetAll()
                           where a.allocCode == allocCode
                           select a.Id).FirstOrDefault();

            return allocID;
        }

        //public GetScheduleUniversalsDto GetOriginalSchedule(GetOriginalScheduleInputDto input)
        //{
        //    List<GetSchedulerListDto> dataSchedule = new List<GetSchedulerListDto>();

        //    var listBfAmount = input.listBfAmount.ToList();

        //    var pctTax = input.listPctTax.FirstOrDefault().pctTax;

        //    var totalAmount = input.listBfAmount.Sum(x => x.bfAmount);

        //    var sellingPrices = input.listNetNetPrice.Sum(x => x.netNetPrice);

        //    var bookDate = input.bookDate.bookDate;

        //    short schecNo = 1;

        //    //get sebagian data BF
        //    //var dataBF = (from bh in _trBookingHeaderRepo.GetAll()
        //    //              join bd in _trBookingDetailRepo.GetAll() on bh.Id equals bd.bookingHeaderID
        //    //              where bh.Id == bookingHeaderID
        //    //              group bd by new
        //    //              {
        //    //                  bh.bookDate,
        //    //                  bd.bookingHeaderID,
        //    //                  bd.pctTax
        //    //              } into G
        //    //              select new
        //    //              {
        //    //                  dueDate = G.Key.bookDate,
        //    //                  totalAmount = G.Sum(X => X.BFAmount),
        //    //                  sellingPrice = G.Sum(x => x.netNetPrice),
        //    //                  G.Key.pctTax
        //    //              }).FirstOrDefault();

        //    //harga jual
        //    var sellingPrice = sellingPrices * (decimal)(1 + pctTax);

        //    //data BF untuk di push
        //    var dataBFFinal = new GetSchedulerListDto
        //    {
        //        dueDate = bookDate,
        //        allocCode = "BF",
        //        allocID = GetAllocIDbyCode("BF"),
        //        totalAmount = totalAmount,
        //        netAmount = totalAmount / (decimal)(1 + pctTax),
        //        VATAmount = (totalAmount / (decimal)(1 + pctTax)) * (decimal)(pctTax),
        //        netOutstanding = totalAmount / (decimal)(1 + pctTax),
        //        VATOutstanding = (totalAmount / (decimal)(1 + pctTax)) * (decimal)(pctTax),
        //        paymentAmount = 0,
        //        totalOutstanding = totalAmount,
        //        schedNo = schecNo
        //    };

        //    //push data BF
        //    dataSchedule.Add(dataBFFinal);


        //    //DP
        //    var dataDP = (from bh in _trBookingHeaderRepo.GetAll()
        //                  join bd in _trBookingDetailRepo.GetAll() on bh.Id equals bd.bookingHeaderID
        //                  join bdd in _trBookingDetailDP.GetAll() on bd.Id equals bdd.bookingDetailID
        //                  join dc in _lkDpCalc.GetAll() on bdd.dpCalcID equals dc.Id into l1
        //                  from dc in l1.DefaultIfEmpty()
        //                  where bh.Id == bookingHeaderID
        //                  select new
        //                  {
        //                      bdd.DPAmount,
        //                      bdd.DPPct,
        //                      bdd.daysDue,
        //                      bdd.monthsDue,
        //                      bdd.dpNo,
        //                      dpCalcID = bdd.dpCalcID == null ? null : bdd.dpCalcID
        //                  } into bdd
        //                  group bdd by new
        //                  {
        //                      bdd.DPAmount,
        //                      bdd.DPPct,
        //                      bdd.daysDue,
        //                      bdd.monthsDue,
        //                      bdd.dpNo,
        //                      bdd.dpCalcID
        //                  } into G
        //                  select new
        //                  {
        //                      G.Key.dpNo,
        //                      G.Key.DPAmount,
        //                      G.Key.DPPct,
        //                      G.Key.daysDue,
        //                      G.Key.monthsDue,
        //                      G.Key.dpCalcID
        //                  }).OrderBy(x => x.dpNo).ToList();



        //    int countDP = dataDP.Count;

        //    decimal totalDP = 0;

        //    //selling
        //    var calcTypeID4 = (from A in _lkDpCalc.GetAll()
        //                       where A.DPCalcType == "4"
        //                       select A.Id).FirstOrDefault();

        //    var DPFinal = new GetSchedulerListDto();

        //    for (var i = 0; i < countDP; i++)
        //    {
        //        schecNo++;

        //        var DPValue = dataDP[i].DPPct != 0 ? sellingPrice * (decimal)dataDP[i].DPPct : dataDP[i].DPAmount;


        //        //first DP
        //        DPFinal = new GetSchedulerListDto
        //        {
        //            dueDate = dataDP[0].daysDue != 0 ? dataBFFinal.dueDate.AddDays(dataDP[0].daysDue) : dataBFFinal.dueDate.AddMonths((int)dataDP[0].monthsDue),
        //            allocCode = "DP",
        //            allocID = GetAllocIDbyCode("DP"),
        //            totalAmount = dataDP[i].dpCalcID == calcTypeID4 ? DPValue - dataBFFinal.totalAmount : DPValue,
        //            netAmount = dataDP[i].dpCalcID == calcTypeID4 ? (DPValue - dataBFFinal.totalAmount) / (decimal)(1 + dataBF.pctTax) : DPValue / (decimal)(1 + dataBF.pctTax),
        //            VATAmount = dataDP[i].dpCalcID == calcTypeID4 ? ((DPValue - dataBFFinal.totalAmount) / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax) : (DPValue / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax),
        //            netOutstanding = dataDP[i].dpCalcID == calcTypeID4 ? (DPValue - dataBFFinal.totalAmount) / (decimal)(1 + dataBF.pctTax) : DPValue / (decimal)(1 + dataBF.pctTax),
        //            VATOutstanding = dataDP[i].dpCalcID == calcTypeID4 ? ((DPValue - dataBFFinal.totalAmount) / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax) : (DPValue / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax),
        //            paymentAmount = 0,
        //            totalOutstanding = dataDP[i].dpCalcID == calcTypeID4 ? DPValue - dataBFFinal.totalAmount : DPValue,
        //            schedNo = schecNo
        //        };

        //        //DP selain pertama
        //        if (i != 0)
        //        {
        //            DPFinal.dueDate = dataDP[i].daysDue != 0 ? dataSchedule[dataSchedule.Count - 1].dueDate.AddDays(dataDP[i].daysDue) : dataSchedule[dataSchedule.Count - 1].dueDate.AddMonths((int)dataDP[i].monthsDue);
        //        }



        //        totalDP += DPFinal.totalAmount;

        //        dataSchedule.Add(DPFinal);
        //    }

        //    //INS
        //    var j = 0;

        //    //get data INS
        //    var dataINS = (from bh in _trBookingHeaderRepo.GetAll()
        //                   join t in _msTermPmt.GetAll() on bh.termID equals t.termID
        //                   join ft in _lkFinType.GetAll() on t.finTypeID equals ft.Id
        //                   where bh.Id == bookingHeaderID
        //                   select new
        //                   {
        //                       ft.finTimes,
        //                       t.finStartDue,
        //                       t.finStartM
        //                   }).FirstOrDefault();

        //    //looping untuk mengasih schedNo dll
        //    while (j < dataINS.finTimes)
        //    {
        //        schecNo++;

        //        var dataINSFinal = new GetSchedulerListDto();

        //        //INS Selain pertama
        //        dataINSFinal = new GetSchedulerListDto
        //        {
        //            dueDate = dataSchedule[dataSchedule.Count - 1].dueDate.AddMonths(j),
        //            allocCode = "INS",
        //            allocID = GetAllocIDbyCode("INS"),
        //            totalAmount = (sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes,
        //            netAmount = ((sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes) / (decimal)(1 + dataBF.pctTax),
        //            VATAmount = (((sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes) / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax),
        //            netOutstanding = ((sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes) / (decimal)(1 + dataBF.pctTax),
        //            VATOutstanding = (((sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes) / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax),
        //            paymentAmount = 0,
        //            totalOutstanding = (sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes,
        //            schedNo = schecNo
        //        };

        //        //ins pertama
        //        if (j == 0)
        //        {
        //            dataINSFinal.dueDate = dataINS.finStartDue != 0 ? dataBFFinal.dueDate.AddDays(dataINS.finStartDue) : dataBFFinal.dueDate.AddMonths((int)dataINS.finStartM);
        //        }
        //        dataSchedule.Add(dataINSFinal);
        //        j++;
        //    }
        //    //dataResult.OrderBy(x => x.schedNo);

        //    var dataResult = new GetScheduleUniversalsDto
        //    {
        //        pctTax = dataBF.pctTax,
        //        dataSchedule = dataSchedule
        //    };

        //    return dataResult;
        //}

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_InsertTrBookingDetailSchedule)]
        //private void InsertTrBookingDetailSchedule(InsertTrBookingDetailScheduleInputDto input)
        //{
        //    var listNetNetPrice = input.listNetNetPrice.ToList();

        //    var getTotalAmount = listNetNetPrice.Sum(x => x.netNetPrice);

        //    var percentage = new List<decimal>();

        //    if (getTotalAmount == 0)
        //        throw new UserFriendlyException("Devide By Zero");

        //    foreach (var item in listNetNetPrice)
        //    {
        //        var getBookingDetail = item.netNetPrice / getTotalAmount;

        //        percentage.Add(getBookingDetail);
        //    }

        //    var dataOri = GetOriginalSchedule(input.bookingHeaderID);

        //    if (percentage == null)
        //        throw new UserFriendlyException("Booking Detail NULL");

        //    ////get data booking schedule by booking detail
        //    //var getAllDetailScheduleDB = (from x in _trBookingHeaderRepo.GetAll()
        //    //                              join y in _trBookingDetailRepo.GetAll() on x.Id equals y.bookingHeaderID
        //    //                              join z in _trBookingDetailSchedule.GetAll() on y.Id equals z.bookingDetailID
        //    //                              where x.Id == input.bookingHeaderID
        //    //                              select new
        //    //                              {
        //    //                                  ID = z.Id
        //    //                              }).ToList();

        //    ////delete existing data
        //    //foreach (var delSch in getAllDetailScheduleDB)
        //    //{
        //    //    _trBookingDetailSchedule.Delete(delSch.ID);
        //    //}

        //    //Calculate data schedule

        //    int schedNumber = 0;

        //    foreach (var schedule in dataOri.dataSchedule)
        //    {
        //        List<TR_BookingDetailSchedule> dataFinal = new List<TR_BookingDetailSchedule>();
        //        var data = new TR_BookingDetailSchedule();

        //        schedNumber++;

        //        foreach (var bookDetail in getBookingDetail)
        //        {
        //            data = new TR_BookingDetailSchedule
        //            {
        //                allocID = schedule.allocID,
        //                bookingDetailID = bookDetail.bookingDetailID,
        //                dueDate = schedule.dueDate,
        //                entityID = 1,
        //                netAmt = schedule.netAmount * bookDetail.percentage,
        //                netOut = schedule.netAmount * bookDetail.percentage,
        //                remarks = String.IsNullOrEmpty(schedule.remarks) ? string.Empty : schedule.remarks,
        //                schedNo = (short)schedNumber,
        //                vatAmt = schedule.VATAmount * bookDetail.percentage,
        //                vatOut = schedule.VATAmount * bookDetail.percentage,
        //            };

        //            dataFinal.Add(data);
        //        }
        //        var dataReturn = (from A in dataFinal select A).OrderBy(x => x.schedNo).ToList();
        //        _context.BulkInsert(_trBookingDetailSchedule, dataReturn);
        //    }
        //}

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_DoBookingMidransReq)]
        public async Task<PaymentOnlineBookingResponse> DoBookingMidransReq(CreateTransactionUniversalDto input)
        {
            var dataUnitOrderHeader = new CreateTransactionUniversalDto
            {
                payTypeID = input.payTypeID,
                pscode = input.pscode,
                totalAmt = input.totalAmt,
                userID = input.userID,
                arrUnit = input.arrUnit,
                bankRekeningPemilik = input.bankRekeningPemilik,
                nomorRekeningPemilik = input.nomorRekeningPemilik,
                sumberDanaID = input.sumberDanaID,
                tujuanTransaksiID = input.tujuanTransaksiID

            };

            var orderID = InsertTrUnitOrderHeader(dataUnitOrderHeader);

            var getPaymentType = (from a in _lkPaymentType.GetAll()
                                  where a.Id == input.payTypeID
                                  select a.paymentType).FirstOrDefault();

            var getCustomer = (from a in _personalsRepo.GetAll()
                               join b in _trEmailRepo.GetAll()
                               on a.psCode equals b.psCode into trEmail
                               from b in trEmail.DefaultIfEmpty()
                               join c in _trPhoneRepo.GetAll()
                               on a.psCode equals c.psCode into trPhone
                               from c in trPhone.DefaultIfEmpty()
                               where a.psCode == input.pscode
                               select new
                               {
                                   a.name,
                                   b.email,
                                   c.number,
                               }).FirstOrDefault();


            var dataMidtrans = new PaymentOnlineBookingRequest();

            var dataTransaction = new transactionDetailsDto
            {
                gross_amount = input.totalAmt,
                order_id = orderID.unitOrderHeaderID.ToString()
            };

            var dataCustomer = new customerDetailsDto
            {
                first_name = getCustomer.name,
                email = getCustomer.email,
                last_name = " ",
                phone = getCustomer.number
            };

            var arrUnits = new List<itemDetailsDto>();

            foreach (var item in input.arrUnit)
            {
                var unitNo = (from a in _msUnitRepo.GetAll()
                              where a.Id == item.unitID
                              select a.unitNo).FirstOrDefault();

                var dataUnit = new itemDetailsDto
                {
                    id = item.unitID.ToString(),
                    name = unitNo,
                    price = item.bfAmount,
                    quantity = 1
                };

                arrUnits.Add(dataUnit);
            }

            var dataBankTransfer = new bankTransferDto
            {
                bank = "permata",
                //va_number = ""
            };

            var dataCreditCard = new creditCard
            {
                token_id = "00001"
            };

            var dataBcaClickPay = new bcaKlikPayDto
            {
                type = "1",
                description = "Pembelian Unit"
            };

            var dataCimbClick = new cimbClicksDto
            {
                description = "Pembelian Unit"
            };

            if (getPaymentType == 4)
            {
                dataMidtrans = new PaymentOnlineBookingRequest
                {
                    payment_type = "bank_transfer",
                    transaction_details = dataTransaction,
                    customer_details = dataCustomer,
                    item_details = arrUnits,
                    bank_transfer = dataBankTransfer,
                };
            }
            else if (getPaymentType == 3)
            {

                dataMidtrans = new PaymentOnlineBookingRequest
                {
                    payment_type = "credit_card",
                    transaction_details = dataTransaction,
                    customer_details = dataCustomer,
                    item_details = arrUnits,
                    credit_card = dataCreditCard,
                };
            }
            else if (getPaymentType == 10)
            {
                dataMidtrans = new PaymentOnlineBookingRequest
                {
                    payment_type = "bca_klikpay",
                    transaction_details = dataTransaction,
                    customer_details = dataCustomer,
                    item_details = arrUnits,
                    bca_klikpay = dataBcaClickPay
                };
            }
            else
            {
                dataMidtrans = new PaymentOnlineBookingRequest
                {
                    payment_type = "cimb_clicks",
                    transaction_details = dataTransaction,
                    customer_details = dataCustomer,
                    item_details = arrUnits,
                    cimb_clicks = dataCimbClick
                };
            }
            var returnMidtrans = await _paymentMidtrans.CreatePayment(dataMidtrans);

            ////Kirim Email
            //if (input.payTypeID == 4)
            //{
            //    foreach (var item in input.arrUnit)
            //    {
            //        var orderDetails = (from orderDetail in _trUnitOrderDetail.GetAll()
            //                    join orderHeader in _trUnitOrderHeader.GetAll()
            //                    on orderDetail.UnitOrderHeaderID equals orderHeader.Id
            //                    where orderDetail.bookingHeaderID == null
            //                    && orderDetail.unitID == item.unitID
            //                    orderby orderDetail.CreationTime descending
            //                    select new { orderDetail, orderHeader }).FirstOrDefault();

            //        var getUnitInfo = (from a in _msUnitRepo.GetAll()
            //                           join b in _msUnitCode.GetAll() on a.unitCodeID equals b.Id
            //                           join c in _msClusterRepo.GetAll() on a.clusterID equals c.Id
            //                           join d in _msProject.GetAll() on a.projectID equals d.Id
            //                           join e in _msProjectInfo.GetAll() on d.Id equals e.projectID into projectInfo
            //                           from e in projectInfo.DefaultIfEmpty()
            //                           where a.Id == item.unitID
            //                           select new
            //                           {
            //                               a.unitNo,
            //                               b.unitCode,
            //                               c.clusterName,
            //                               d.projectName,
            //                               d.image,
            //                               e.projectMarketingOffice,
            //                               e.projectMarketingPhone
            //                           }).FirstOrDefault();

            //        var getSalesInfo = (from member in _contextPers.PERSONALS_MEMBER.ToList()
            //                            join phone in _contextPers.TR_Phone.ToList() on member.psCode equals phone.psCode into trPhone
            //                            from c in trPhone.DefaultIfEmpty()
            //                            where member.memberCode == orderDetails.orderHeader.memberCode
            //                            select new { member, c }).FirstOrDefault();

            //        var dataEmail = new AfterReservedInputDto
            //        {
            //            customerName = getCustomer.name,
            //            BFAmount = dataUnitOrderHeader.totalAmt,
            //            bankName = "Permata",
            //            memberName = dataUnitOrderHeader.memberName,
            //            orderCode = dataUnitOrderHeader.orderCode,
            //            unitCode = getUnitInfo.unitCode,
            //            unitNo = getUnitInfo.unitNo,
            //            orderDate = orderDetails.orderHeader.orderDate,
            //            expiredDate = orderDetails.orderHeader.orderDate + (TimeSpan.Parse("12:00:00")),
            //            memberPhone = getSalesInfo.c.number,
            //            clusterName = getUnitInfo.clusterName,
            //            projectName = getUnitInfo.projectName,
            //            devPhone = getUnitInfo.projectMarketingPhone,
            //            marketingOffice = getUnitInfo.projectMarketingOffice,
            //            vaNumber = returnMidtrans.permata_va_number,
            //            projectImage = getUnitInfo.image
            //        };
            //        var body = _emailAppService.bodyAfterReserved(dataEmail);

            //        var email = new SendEmailInputDto
            //        {
            //            body = body,
            //            toAddress = "keniamalia1@gmail.com", //dataUnitOrderHeader.psEmail,
            //            subject = " Konfirmasi Order " + orderDetails.orderHeader.orderCode + " atas Unit " + getUnitInfo.unitCode
            //        };

            //        _emailAppService.ConfigurationEmail(email);
            //    }
            //}

            return new PaymentOnlineBookingResponse { status_code = returnMidtrans.status_code, status_message = returnMidtrans.status_message, redirect_url = returnMidtrans.redirect_url };

        }

        //[UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Transaction_DoBooking)]
        public void DoBooking(PaymentOnlineBookingResponse input)
        {
            Logger.InfoFormat("DoBooking() - Start.");
            //var responseinput = _paymentMidtranshelper.ValidateResponseStatus(JsonConvert.DeserializeObject<PaymentOnlineBookingResponse>(input));

            Logger.DebugFormat("response payment midtrans. {0}" +
                "status code            = {1}{0}" +
                "status message         = {2}{0}" +
                "transaction id         = {3}{0}" +
                "transaction time       = {4}{0}" +
                "transaction status     = {5}{0}" +
                "payment type           = {6}{0}" +
                "gross amount           = {7}{0}" +
                "order id               = {8}{0}" +
                "error message          = {9}{0}" +
                "fraud status           = {10}{0}" +
                "signature key          = {11}{0}" +
                "approval code          = {12}{0}" +
                "billerKey|billerCode   = {13}{0}",
            Environment.NewLine, input.status_code, input.status_message,
                input.transaction_id, input.transaction_time, input.transaction_status,
                input.payment_type, input.gross_amount, input.order_id,
                input.error_messages, input.fraud_status, input.signature_key,
                input.approval_code, input.bill_key + "|" + input.biller_code);

            Logger.InfoFormat("DoBooking() - End.");

            var getOrder = (from a in _trUnitOrderHeader.GetAll()
                            where a.Id == Convert.ToInt32(input.order_id)
                            select a).FirstOrDefault();

            var getDetail = (from a in _trUnitOrderHeader.GetAll()
                             join b in _trUnitOrderDetail.GetAll()
                             on a.Id equals b.UnitOrderHeaderID
                             where a.Id == Convert.ToInt32(input.order_id)
                             select new UnitUniversalResultDto
                             {
                                 renovID = b.renovID,
                                 sellingprice = b.sellingPrice,
                                 termID = b.termID,
                                 unitID = b.unitID
                             }).ToList();

            var arrUnits = new List<UnitUniversalResultDto>();

            foreach (var item in getDetail)
            {
                var dataUnit = new UnitUniversalResultDto
                {
                    renovID = item.renovID,
                    sellingprice = item.sellingprice,
                    termID = item.termID,
                    unitID = item.unitID,

                };

                arrUnits.Add(dataUnit);
            }

            var data = new CreateTransactionUniversalDto
            {
                payTypeID = getOrder.paymentTypeID,
                pscode = getOrder.psCode,
                orderHeaderID = Convert.ToInt32(input.order_id),
                orderCode = getOrder.orderCode,
                arrUnit = arrUnits,
                userID = getOrder.userID,
                memberCode = getOrder.memberCode,
                memberName = getOrder.memberName,
                sumberDanaID = getOrder.sumberDanaID,
                bankRekeningPemilik = getOrder.bankRekeningPemilik,
                nomorRekeningPemilik = getOrder.nomorRekeningPemilik,
                tujuanTransaksiID = getOrder.tujuanTransaksiID,
                scmCode = getOrder.scmCode
            };

            InsertTransactionUniversal(data);

            var getBooking = (from booking in _trBookingHeaderRepo.GetAll()
                              orderby booking.CreationTime descending
                              select booking).FirstOrDefault();

            var getBookings = (from booking in _contextNew.TR_SoldUnit
                               orderby booking.inputTime descending
                               select booking).FirstOrDefault();

            var getBookingss = (from booking in _trUnitOrderDetail.GetAll()
                                join a in _trUnitOrderHeader.GetAll() on booking.UnitOrderHeaderID equals a.Id
                                where a.Id == Convert.ToInt32(input.order_id)
                                select booking).FirstOrDefault();
        }

        public KonfirmasiPesananDto PaymentFinish(string order_id)
        {
            Logger.InfoFormat("PaymentFinish() - Start.");

            var getDataOrderDetail = (from booking in _trUnitOrderDetail.GetAll()
                                      join a in _trUnitOrderHeader.GetAll() on booking.UnitOrderHeaderID equals a.Id
                                      where a.Id == Convert.ToInt32(order_id)
                                      select booking).ToList();

            var getBooking = (from booking in _trBookingHeaderRepo.GetAll()
                              where booking.Id == getDataOrderDetail.FirstOrDefault().bookingHeaderID
                              select booking).ToList();

            var getBookingItem = (from booking in _trBookingItemPrice.GetAll()
                                  where booking.bookingHeaderID == getBooking.FirstOrDefault().Id
                                  select booking).ToList();

            var dataKP = (from x in _context.TR_UnitOrderHeader
                          join a in getDataOrderDetail on x.Id equals a.UnitOrderHeaderID
                          join b in _context.MS_Unit on a.unitID equals b.Id
                          join d in _contextPers.PERSONAL.ToList() on x.psCode equals d.psCode
                          join f in _context.MS_TujuanTransaksi on x.tujuanTransaksiID equals f.Id
                          join g in _context.MS_SumberDana on x.sumberDanaID equals g.Id
                          join h in _contextPers.TR_ID.ToList() on x.psCode equals h.psCode into iden
                          from h in iden.DefaultIfEmpty()
                          join i in _contextNew.MS_Schema.ToList() on x.scmCode equals i.scmCode
                          join j in _contextPers.PERSONALS_MEMBER.ToList() on new { x.memberCode, i.scmCode } equals new { j.memberCode, j.scmCode }
                          join k in _contextPers.PERSONAL.ToList() on j.psCode equals k.psCode
                          join l in _contextPers.TR_Phone.ToList() on k.psCode equals l.psCode into phone
                          from l in phone.DefaultIfEmpty()
                          join m in _context.MS_Project on b.projectID equals m.Id
                          join p in _context.MS_Detail on b.detailID equals p.Id
                          join s in _context.MS_Term on a.termID equals s.Id
                          where x.Id == Convert.ToInt32(order_id) && new string[] { "1", "3", "5", "7" }.Contains(h.idType)
                          select new KonfirmasiPesananDto
                          {
                              orderCode = x.orderCode,
                              kodePelanggan = x.psCode,
                              tanggalBooking = DateTime.Now.ToString(),
                              psName = x.psName,
                              birthDate = d.birthDate.ToString(),
                              noHpPembeli = x.psPhone,
                              noIdentitas = (h == null ? "-" : h.idNo),
                              noNPWP = d.NPWP,
                              email = x.psEmail,
                              //BookCode = e.bookCode,
                              hargaJual = a.sellingPrice.ToString(),
                              bfAmount = a.BFAmount.ToString(),
                              imageProject = m.image,
                              noHp = l.number,
                              noDealCloser = k.psCode,
                              namaDealCloser = k.name,
                              caraPembayaran = s.remarks,
                              tujuanTransaksi = f.tujuanTransaksiName,
                              sumberDanaPembelian = g.sumberDanaName,
                              namaBank = x.bankRekeningPemilik,
                              noRekening = x.nomorRekeningPemilik,
                              unitID = a.unitID
                          }).FirstOrDefault();


            var dataUnit = (from a in _context.MS_Unit
                            join b in _context.MS_Project on a.projectID equals b.Id
                            join c in _context.MS_Cluster on a.clusterID equals c.Id
                            join d in _context.MS_UnitItem on a.Id equals d.unitID
                            join e in _context.MS_Detail on a.detailID equals e.Id
                            join f in _context.MS_Category on a.categoryID equals f.Id
                            join g in _context.MS_UnitCode on a.unitCodeID equals g.Id
                            join i in getBookingItem on d.itemID  equals i.itemID
                            join j in _context.MS_Renovation on i.renovCode equals j.renovationCode
                            group d by new
                            {
                                d.unitID,
                                a.unitNo,
                                g.unitCode,
                                c.clusterName,
                                f.categoryName,
                                e.detailName,
                                j.renovationName
                            } into G
                            where G.Key.unitID == dataKP.unitID
                            select new unitDto
                            {
                                UnitNo = G.Key.unitNo,
                                UnitCode = G.Key.unitCode.Contains("-") ? G.Key.unitCode : null,
                                category = G.Key.categoryName,
                                cluster = G.Key.clusterName,
                                luas = G.Sum(d => d.area).ToString(),
                                tipe = G.Key.detailName,
                                renovation = G.Key.renovationName
                            }).ToList();

            dataKP.listUnit = dataUnit;

            var dataBank = (from bank in _context.MS_BankOLBooking
                            join unit in _context.MS_Unit on new { bank.projectID, bank.clusterID } equals new { unit.projectID, unit.clusterID }
                            join header in getBooking on unit.Id equals header.unitID
                            where unit.Id == dataKP.unitID
                            select new listBankDto
                            {
                                bankName = bank.bankName,
                                noVA = bank.bankRekNo
                            }).ToList();

            dataKP.listBank = dataBank;

            string json = JsonConvert.SerializeObject(dataKP);

            Logger.InfoFormat(json);

            var getProjectInfo = (from project in _context.MS_Project
                                  join info in _context.MS_ProjectInfo on project.Id equals info.projectID into a
                                  from projectInfo in a.DefaultIfEmpty()
                                  join unit in _context.MS_Unit on project.Id equals unit.projectID
                                  where unit.Id == dataKP.unitID
                                  orderby projectInfo.CreationTime descending
                                  select new
                                  {
                                      project.projectName,
                                      projectInfo.projectMarketingOffice,
                                      projectInfo.projectMarketingPhone
                                  }).FirstOrDefault();

            Logger.InfoFormat(JsonConvert.SerializeObject(getProjectInfo));


            var sendEmail = new BookingSuccessInputDto
            {
                bookDate = DateTime.Now,
                customerName = dataKP.psName,
                devPhone = getProjectInfo.projectMarketingPhone != null ? getProjectInfo.projectMarketingPhone : "-",
                memberName = dataKP.namaDealCloser,
                memberPhone = dataKP.noHp,
                projectImage = dataKP.imageProject != null ? dataKP.imageProject : "-",
                projectName = getProjectInfo.projectName
            };
            var body = _emailAppService.bookingSuccess(sendEmail);


            using (var client = new WebClient())
            {
                var url = _configuration.ApiPdfUrl.EnsureEndsWith('/') + "api/Pdf/KonfirmasiPesananPdf";
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(url, JsonConvert.SerializeObject(dataKP));
                Logger.InfoFormat(result);
                var trimResult = result.Replace(@"\\", @"\").Trim(new char[1] { '"' });
                Logger.InfoFormat(trimResult);

                var email = new SendEmailInputDto
                {
                    body = body,
                    toAddress = dataKP.email,
                    subject = "Konfirmasi Pemesanan Unit" + dataUnit.FirstOrDefault().UnitCode + " " + dataUnit.FirstOrDefault().UnitNo,
                    pathKP = trimResult
                };

                _emailAppService.ConfigurationEmail(email);
            }

            return dataKP;

        }

        public void DoBookingMobey(CreateTransactionUniversalDto input)
        {
            var dataInsertTransactionUniversal = new CreateTransactionUniversalDto
            {
                arrPP = input.arrPP,
                arrUnit = input.arrUnit,
                totalAmt = input.totalAmt,
                bankRekeningPemilik = input.bankRekeningPemilik,
                memberCode = input.memberCode,
                memberName = input.memberName,
                scmCode = input.scmCode,
                sumberDanaID = input.sumberDanaID,
                nomorRekeningPemilik = input.nomorRekeningPemilik,
                orderCode = input.orderCode,
                orderHeaderID = input.orderHeaderID,
                payTypeID = input.payTypeID,
                pscode = input.pscode,
                tujuanTransaksiID = input.tujuanTransaksiID,
                userID = input.userID
            };
            InsertTransactionUniversal(dataInsertTransactionUniversal);

            //Todo : Send Email KP
        }

        public void SchedulerBookingReminder()
        {
            var data = (from orderDetail in _trUnitOrderDetail.GetAll()
                        join orderHeader in _trUnitOrderHeader.GetAll()
                        on orderDetail.UnitOrderHeaderID equals orderHeader.Id
                        where orderDetail.bookingHeaderID == null
                        orderby orderDetail.CreationTime descending
                        select new { orderDetail, orderHeader }).Distinct().ToList();

            TimeSpan banding = TimeSpan.Parse("10:00:00");

            foreach (var item in data)
            {
                if ((item.orderDetail.CreationTime - DateTime.Now) >= banding)
                {
                    var getCustInfo = (from personals in _contextPers.PERSONAL.ToList()
                                       where personals.psCode == item.orderHeader.psCode
                                       select personals).FirstOrDefault();

                    var getUnitInfo = (from a in _msUnitRepo.GetAll()
                                       join b in _msUnitCode.GetAll() on a.unitCodeID equals b.Id
                                       join c in _msClusterRepo.GetAll() on a.clusterID equals c.Id
                                       where a.Id == item.orderDetail.unitID
                                       select new
                                       {
                                           a.unitNo,
                                           b.unitCode,
                                           c.clusterName,
                                           a.projectID
                                       }).FirstOrDefault();

                    var getProjectInfo = (from project in _msProject.GetAll()
                                          join info in _msProjectInfo.GetAll() on project.Id equals info.projectID into a
                                          from projectInfo in a.DefaultIfEmpty()
                                          where project.Id == getUnitInfo.projectID
                                          orderby projectInfo.CreationTime descending
                                          select new { project, projectInfo }).FirstOrDefault();

                    var getSalesInfo = (from member in _contextPers.PERSONALS_MEMBER.ToList()
                                        join phone in _contextPers.TR_Phone.ToList() on member.psCode equals phone.psCode into trPhone
                                        from c in trPhone.DefaultIfEmpty()
                                        where member.memberCode == item.orderHeader.memberCode
                                        select new { member, c }).FirstOrDefault();

                    var statusMidtrans = _paymentMidtrans.CheckPaymentStatus(item.orderHeader.Id.ToString());

                    var sendEmail = new Reminder2JamInputDto
                    {
                        bookingFee = item.orderDetail.sellingPrice,
                        customerName = getCustInfo.name,
                        devPhone = getProjectInfo.projectInfo.projectMarketingPhone != null ? getProjectInfo.projectInfo.projectMarketingPhone : "-",
                        marketingOffice = getProjectInfo.projectInfo.projectMarketingOffice != null ? getProjectInfo.projectInfo.projectMarketingOffice : "-",
                        memberName = item.orderHeader.memberName,
                        memberPhone = getSalesInfo.c.number != null ? getSalesInfo.c.number : "-",
                        orderDate = item.orderHeader.orderDate,
                        projectImage = getProjectInfo.project.image,
                        projectName = getProjectInfo.project.projectName,
                        unitCode = getUnitInfo.unitCode,
                        unitNo = getUnitInfo.unitNo,
                        bankName = "Permata",
                        vaNumber = statusMidtrans.Result.permata_va_number
                    };
                    var body = _emailAppService.Reminder2Jam(sendEmail);

                    var email = new SendEmailInputDto
                    {
                        body = body,
                        toAddress = "keniamalia1@gmail.com", //dataUnitOrderHeader.psEmail,
                        subject = "Reminder Booking Fee Payment atas Order" + item.orderHeader.orderCode
                    };

                    _emailAppService.ConfigurationEmail(email);
                }
            }
        }

        public List<KPPage2InputDto> KPPage2(int orderHeaderID)
        {
            var arrTerm = new List<listIlustrasiPembayaranDto>();

            var ilustrasi = new List<KPPage2InputDto>();

            var DP = new List<listDP>();

            var Cicilan = new List<listCicilan>();

            var getTerm = (from a in _context.TR_UnitOrderHeader
                           join b in _context.TR_UnitOrderDetail on a.Id equals b.UnitOrderHeaderID
                           join c in _context.TR_BookingHeader on b.bookingHeaderID equals c.Id
                           join d in _context.TR_BookingHeaderTerm on new { bookingHeaderID = c.Id, c.termID } equals new { bookingHeaderID = d.bookingHeaderID, d.termID }
                           where a.Id == orderHeaderID
                           select new listIlustrasiPembayaranDto
                           {
                               termID = c.termID,
                               termName = d.remarks,
                               bookingFee = b.BFAmount,
                               sellingPrice = b.sellingPrice,
                               tglJatuhTempo = c.bookDate,
                               orderHeaderID = a.Id
                           }).ToList();

            var getDPTerm = (from a in getTerm
                             join e in _context.MS_TermDP on a.termID equals e.termID
                             where a.orderHeaderID == orderHeaderID
                             select new
                             {
                                 a.sellingPrice,
                                 e.DPNo,
                                 e.DPPct,
                                 e.daysDue,
                                 a.tglJatuhTempo
                            }).ToList();

            foreach (var item in getDPTerm)
            {
                var dataDPTerm = new listDP
                {
                    amount = item.sellingPrice * (decimal)(item.DPPct),
                    DPNo = item.DPNo,
                    tglJatuhTempo = item.tglJatuhTempo.AddDays(item.daysDue)
                };
                DP.Add(dataDPTerm);
            }

            getTerm.FirstOrDefault().listDP = DP;

            var getFinTimes = (from a in getTerm
                              join b in _context.MS_TermPmt on a.termID equals b.termID
                              join c in _context.LK_FinType on b.finTypeID equals c.Id
                              where a.orderHeaderID == orderHeaderID
                              select new
                              {
                                  totalDP = DP.Sum(a => a.amount),
                                  c.finTimes,
                                  BFAmaount = a.bookingFee,
                                  a.sellingPrice,
                                  b.finStartDue,
                                  a.tglJatuhTempo
                              }).FirstOrDefault();

            var totalAmount = getFinTimes.sellingPrice - getFinTimes.BFAmaount - getFinTimes.totalDP;

            var amount = totalAmount / getFinTimes.finTimes;

            var lastDP = DP[DP.Count - 1];

            var startDue = lastDP.tglJatuhTempo;
                //getFinTimes.tglJatuhTempo.AddDays(getFinTimes.daysDue);

            if (getFinTimes.finTimes == 1)
            {
                var dataCicilan = new listCicilan
                {
                    amount = "-",
                    cicilanNo = 1,
                    pelunasan1 = amount.ToString(),
                    tglJatuhTempo = startDue.AddDays(getFinTimes.finStartDue)
                };
                Cicilan.Add(dataCicilan);
            }
            else
            {
                var month = 0;
                var cicilanNo = 1;
                for (var i = 0; i <= getFinTimes.finTimes - 1; i++)
                {
                    var tglJatuhTempo = startDue.AddDays(getFinTimes.finStartDue);
                    
                    if (i == 1)
                    {
                        var dataCicilan = new listCicilan
                        {
                            amount = amount.ToString(),
                            cicilanNo = cicilanNo,
                            pelunasan1 = "-",
                            tglJatuhTempo = tglJatuhTempo
                        };

                        Cicilan.Add(dataCicilan);
                    }
                    else
                    {
                        var dataCicilan = new listCicilan
                        {
                            amount = amount.ToString(),
                            cicilanNo = cicilanNo,
                            pelunasan1 = "-",
                            tglJatuhTempo = tglJatuhTempo.AddMonths(month)
                        };
                        Cicilan.Add(dataCicilan);
                    }
                    month++;
                    
                }
                cicilanNo++;
            }

            getTerm.FirstOrDefault().listCicilan = Cicilan;

            arrTerm.Add(getTerm.FirstOrDefault());

            var getTop5 = (from a in _context.TR_UnitOrderHeader
                           join b in _context.TR_UnitOrderDetail on a.Id equals b.UnitOrderHeaderID
                           join c in _context.TR_BookingHeader on b.bookingHeaderID equals c.Id
                           join d in _context.TR_BookingHeaderTerm on c.Id equals d.bookingHeaderID
                           where a.Id == orderHeaderID
                           orderby d.Id ascending
                           select new listIlustrasiPembayaranDto
                           {
                               termID = d.termID,
                               termName = d.remarks,
                               bookingFee = b.BFAmount,
                               sellingPrice = b.sellingPrice,
                               tglJatuhTempo = c.bookDate,
                               orderHeaderID = a.Id
                           }).ToList().Take(5);
            

            foreach (var item in getTop5)
            {
                var DPTop5 = new List<listDP>();

                var getDPTop5 = (from a in getTop5
                                 join b in _context.MS_TermDP on a.termID equals b.termID
                                 where a.orderHeaderID == orderHeaderID && a.termID == item.termID
                                 select new
                                 {
                                     a.sellingPrice,
                                     b.DPNo,
                                     b.DPPct,
                                     b.daysDue,
                                     a.tglJatuhTempo
                                 }).ToList();

                foreach (var dto in getDPTop5)
                {
                    var dataDPTop5 = new listDP
                    {
                        amount = dto.sellingPrice * (decimal)(1 - dto.DPPct),
                        DPNo = dto.DPNo,
                        tglJatuhTempo = dto.tglJatuhTempo.AddDays(dto.daysDue)
                    };
                    DPTop5.Add(dataDPTop5);
                }

                item.listDP = DPTop5;

                var listCicilan = new List<listCicilan>();

                var getFinTimesTop5 = (from a in getTop5
                                       join b in _context.MS_TermPmt on a.termID equals b.termID
                                       join c in _context.LK_FinType on b.finTypeID equals c.Id
                                       where a.orderHeaderID == orderHeaderID && a.termID == item.termID
                                       select new
                                       {
                                           totalDP = DPTop5.Sum(a => a.amount),
                                           c.finTimes,
                                           BFAmaount = a.bookingFee,
                                           a.sellingPrice,
                                           b.finStartDue,
                                           a.tglJatuhTempo
                                       }).FirstOrDefault();

                var totalAmountTop5 = getFinTimesTop5.sellingPrice - getFinTimesTop5.BFAmaount - getFinTimesTop5.totalDP;

                var amountTop5 = totalAmountTop5 / getFinTimesTop5.finTimes;

                var lastDPTop5 = DPTop5[DPTop5.Count - 1];

                var startDueTop5 = lastDPTop5.tglJatuhTempo;
                //getFinTimesTop5.tglJatuhTempo.AddDays(getFinTimesTop5.daysDue);

                if (getFinTimesTop5.finTimes == 1)
                {
                    var dataCicilan = new listCicilan
                    {
                        amount = "-",
                        cicilanNo = 1,
                        pelunasan1 = amount.ToString(),
                        tglJatuhTempo = startDueTop5.AddDays(getFinTimesTop5.finStartDue)
                    };
                    listCicilan.Add(dataCicilan);
                }
                else
                {
                    var month = 0;
                    var cicilanNo = 1;
                    for (var i = 0; i <= getFinTimesTop5.finTimes - 1; i++)
                    {
                        var tglJatuhTempo = startDueTop5.AddDays(getFinTimesTop5.finStartDue);
                        
                        if (i == 1)
                        {
                            var dataCicilan = new listCicilan
                            {
                                amount = amount.ToString(),
                                cicilanNo = cicilanNo,
                                pelunasan1 = "-",
                                tglJatuhTempo = tglJatuhTempo
                            };

                            listCicilan.Add(dataCicilan);
                        }
                        else
                        {
                            var dataCicilan = new listCicilan
                            {
                                amount = amount.ToString(),
                                cicilanNo = cicilanNo,
                                pelunasan1 = "-",
                                tglJatuhTempo = tglJatuhTempo.AddMonths(month)
                            };
                            listCicilan.Add(dataCicilan);
                        }
                        month++;
                       
                    }
                    cicilanNo++;
                }

                item.listCicilan = listCicilan;

                arrTerm.Add(item);
            }
            
            var data = new KPPage2InputDto
            {
                ilustrasiPembayaran = arrTerm
            };

            ilustrasi.Add(data);

            return ilustrasi;
        }
    }
}
