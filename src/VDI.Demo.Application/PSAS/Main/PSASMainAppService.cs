using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.Dto;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.NewCommDB;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.Pricing;
using VDI.Demo.PSAS.Dto;
using VDI.Demo.PSAS.Main.Dto;
using VDI.Demo.PSAS.Main.Exporter;
using VDI.Demo.PSAS.Price;
using VDI.Demo.PSAS.Schedule;
using VDI.Demo.PSAS.Schedule.Dto;
using VDI.Demo.PSAS.Term;
using VDI.Demo.PSAS.Term.Dto;

namespace VDI.Demo.PSAS.Main
{
    public class PSASMainAppService : DemoAppServiceBase, IMainPSASAppService
    {
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;
        private readonly IRepository<MS_UnitItem> _msUnitItemRepo;
        private readonly IRepository<LK_Item> _lkItemRepo;
        private readonly IRepository<TR_BookingHeader> _trBookingHeaderRepo;
        private readonly IRepository<MS_Area> _msAreaRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Product> _msProductRepo;
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<MS_Category> _msCategoryRepo;
        private readonly IRepository<MS_Detail> _msDetailRepo;
        private readonly IRepository<MS_Term> _msTermRepo;
        private readonly IRepository<LK_UnitStatus> _lkUnitStatusRepo;
        private readonly IRepository<PERSONALS, string> _personalsRepo;
        private readonly IRepository<LK_PayType> _lkPayTypeRepo;
        private readonly IRepository<MS_TransFrom> _msTransformRepo;
        private readonly IRepository<MS_Schema, string> _msSchemaRepo;
        private readonly IRepository<MS_ShopBusiness> _msShopBusinessRepo;
        private readonly IRepository<MS_SalesEvent> _msSalesEventRepo;
        private readonly IRepository<LK_SADStatus> _lkSADStatusRepo;
        private readonly IRepository<LK_Promotion> _lkPromotionRepo;
        private readonly IRepository<TR_BookingDetail> _trBookingDetailRepo;
        private readonly IRepository<LK_FinType> _lkFinTypeRepo;
        private readonly IRepository<TR_BookingCancel> _trBookingCancelRepo;
        private readonly IRepository<MS_Company> _msCompanyRepo;
        private readonly IRepository<TR_Address, string> _trAddressRepo;
        private readonly IRepository<PERSONALS_MEMBER, string> _personalMemberRepo;
        private readonly IRepository<TR_Phone, string> _trPhoneRepo;
        private readonly IRepository<LK_Reason> _lkReasonRepo;
        private readonly IPSASPriceAppService _ipriceAppService;
        private readonly PersonalsNewDbContext _contextPers;
        private readonly NewCommDbContext _contextNew;
        private readonly PropertySystemDbContext _contextProp;
        private readonly IPSASPaymentAppService _iPaymentAppService;
        private readonly IPSASScheduleAppService _iScheduleAppService;
        private readonly IPrintBookingProfileExcelExporter _printBookingProfileExcelExporter;
        private readonly TAXDbContext _contextTax;

        public PSASMainAppService(
            IRepository<MS_UnitItem> msUnitItem,
            IRepository<LK_Item> lkItem,
            IRepository<MS_Unit> msUnit,
            IRepository<MS_UnitCode> msUnitCode,
            IRepository<TR_BookingHeader> trBookingHeader,
            IRepository<MS_Area> msArea,
            IRepository<MS_Project> msProject,
            IRepository<MS_Product> msProduct,
            IRepository<MS_Cluster> msCluster,
            IRepository<MS_Category> msCategory,
            IRepository<MS_Detail> msDetail,
            IRepository<MS_Term> msTerm,
            IRepository<LK_UnitStatus> lkUnitStatus,
            IRepository<PERSONALS, string> personals,
            IRepository<LK_PayType> lkPayType,
            IRepository<MS_TransFrom> msTransform,
            IRepository<MS_Schema, string> msSchema,
            IRepository<MS_ShopBusiness> msShopBusiness,
            IRepository<MS_SalesEvent> msSalesEvent,
            IRepository<LK_SADStatus> lkSADStatus,
            IRepository<LK_Promotion> lkPromotion,
            IRepository<TR_BookingDetail> trBookingDetail,
            IRepository<LK_FinType> lkFinType,
            IRepository<TR_BookingCancel> trBookingCancel,
            IRepository<MS_Company> msCompanyRepo,
            IRepository<TR_Address, string> trAddressRepo,
            IRepository<PERSONALS_MEMBER, string> personalMemberRepo,
            IRepository<TR_Phone, string> trPhoneRepo,
            IRepository<LK_Reason> lkReasonRepo,
            IPSASPriceAppService ipriceAppService,
            PersonalsNewDbContext contextPers,
            NewCommDbContext contextNew,
            IPSASPaymentAppService iPaymentAppService,
            IPSASScheduleAppService iScheduleAppService,
            IPrintBookingProfileExcelExporter printBookingProfileExcelExporter,
            TAXDbContext contextTax,
            PropertySystemDbContext contextProp
        )
        {
            _contextTax = contextTax;
            _printBookingProfileExcelExporter = printBookingProfileExcelExporter;
            _iScheduleAppService = iScheduleAppService;
            _iPaymentAppService = iPaymentAppService;
            _contextProp = contextProp;
            _msUnitItemRepo = msUnitItem;
            _lkItemRepo = lkItem;
            _msUnitRepo = msUnit;
            _msUnitCodeRepo = msUnitCode;
            _trBookingHeaderRepo = trBookingHeader;
            _msAreaRepo = msArea;
            _msProjectRepo = msProject;
            _msProductRepo = msProduct;
            _msClusterRepo = msCluster;
            _msCategoryRepo = msCategory;
            _msDetailRepo = msDetail;
            _msTermRepo = msTerm;
            _lkUnitStatusRepo = lkUnitStatus;
            _personalsRepo = personals;
            _lkPayTypeRepo = lkPayType;
            _msTransformRepo = msTransform;
            _msSchemaRepo = msSchema;
            _msShopBusinessRepo = msShopBusiness;
            _msSalesEventRepo = msSalesEvent;
            _lkSADStatusRepo = lkSADStatus;
            _lkPromotionRepo = lkPromotion;
            _trBookingDetailRepo = trBookingDetail;
            _lkFinTypeRepo = lkFinType;
            _trBookingCancelRepo = trBookingCancel;
            _msCompanyRepo = msCompanyRepo;
            _trAddressRepo = trAddressRepo;
            _personalMemberRepo = personalMemberRepo;
            _trPhoneRepo = trPhoneRepo;
            _lkReasonRepo = lkReasonRepo;
            _ipriceAppService = ipriceAppService;
            _contextPers = contextPers;
            _contextNew = contextNew;
        }

        [UnitOfWork(isTransactional: false)]
        public GetUniversalMainListDto GetPSASMain(GetPSASParamsDto input)
        {
            var UnitID = _ipriceAppService.GetParameter(input);
            var getAllHeaderMain = (from x in _trBookingDetailRepo.GetAll()
                                    join y in _lkItemRepo.GetAll() on x.itemID equals y.Id
                                    where x.bookingHeaderID == UnitID.bookingHeaderID
                                    select new GetPSASMainHeaderListDto
                                    {
                                        itemName = y.itemName,
                                        coCode = x.coCode,
                                        area = x.area,
                                        netPriceMKT = x.netPriceMKT,
                                        netPriceComm = x.netPriceComm,
                                        netNetPrice = x.netNetPrice
                                    }
                              ).ToList();

            //var getAllPersonals = _personalsRepo.GetAllList();
            //var getAllPersonalsMember = _personalMemberRepo.GetAllList();
            //var getAllAddress = _trAddressRepo.GetAllList();
            //var getAllPhone = _trPhoneRepo.GetAllList();
            //var getAllSchema = _msSchemaRepo.GetAllList();

            var getDataPersonal = (from x in _contextPers.PERSONAL
                                   join a in _trBookingHeaderRepo.GetAll().ToList() on x.psCode equals a.psCode
                                   join b in _contextPers.TR_Address on a.psCode equals b.psCode into address
                                   from b in address.DefaultIfEmpty()
                                   join c in _contextPers.TR_Phone on a.psCode equals c.psCode into phone
                                   from c in phone.DefaultIfEmpty()
                                   where a.Id == UnitID.bookingHeaderID
                                   select new
                                   {
                                       dataPsCode = a.psCode,
                                       npwp = x.NPWP,
                                       nama = x.name,
                                       address = (b == null ? "-" : b.address),
                                       phone = (c == null ? "-" : c.number)
                                   }).ToList();

            var getMainData = (from a in _contextProp.TR_BookingHeader
                               join b in _contextProp.MS_Unit on a.unitID equals b.Id into unit
                               from b in unit.DefaultIfEmpty()
                               join d in _contextProp.MS_Area on b.areaID equals d.Id into area
                               from d in area.DefaultIfEmpty()
                               join e in _contextProp.MS_Project on b.projectID equals e.Id into project
                               from e in project.DefaultIfEmpty()
                               join f in _contextProp.MS_Product on b.productID equals f.Id into product
                               from f in product.DefaultIfEmpty()
                               join j in _contextProp.MS_Term on a.termID equals j.Id into term
                               from j in term.DefaultIfEmpty()
                               join l in getDataPersonal on a.psCode equals l.dataPsCode
                               join m in _contextProp.LK_PayType on a.BFPayTypeCode equals m.payTypeCode into payType
                               from m in payType.DefaultIfEmpty()
                               join n in _contextProp.MS_Transfrom on a.transID equals n.Id into transform
                               from n in transform.DefaultIfEmpty()
                               join o in _contextNew.MS_Schema.ToList() on a.scmCode equals o.scmCode into schema
                               from o in schema.DefaultIfEmpty()
                               join q in _contextProp.MS_SalesEvent on a.eventID equals q.Id into salesEvent
                               from q in salesEvent.DefaultIfEmpty()
                               join r in _contextProp.LK_SADStatus on a.SADStatusID equals r.Id into SADStatus
                               from r in SADStatus.DefaultIfEmpty()
                               join t in _contextProp.TR_BookingDetail on a.Id equals t.bookingHeaderID
                               join v in _contextProp.TR_BookingCancel on a.Id equals v.bookingHeaderID into bookingCancel
                               from v in bookingCancel.DefaultIfEmpty()
                               join z in _contextProp.LK_Reason on v.reasonID equals z.Id into lkReason
                               from z in lkReason.DefaultIfEmpty()
                               where a.Id == UnitID.bookingHeaderID
                               group t by new
                               {
                                   e.projectName,
                                   f.productName,
                                   d.regionName,
                                   j.termCode,
                                   j.termNo,
                                   termRemarks = j.remarks,
                                   a.memberName,
                                   l.address,
                                   l.npwp,
                                   l.phone,
                                   a.psCode,
                                   q.eventName,
                                   n.transCode,
                                   n.transName,
                                   m.payTypeDesc,
                                   a.bankName,
                                   a.bankNo,
                                   o.scmName,
                                   o.scmCode,
                                   r.statusDesc,
                                   a.memberCode,
                                   l.nama,
                                   a.bookDate,
                                   a.cancelDate,
                                   a.remarks,
                                   z.reasonDesc,
                                   reasonRemarks = v.remarks
                               } into G

                               select new GetPSASMainListDto
                               {
                                   projectName = (G.Key.projectName == null ? "-" : G.Key.projectName),
                                   productName = (G.Key.productName == null ? "-" : G.Key.productName),
                                   territory = (G.Key.regionName == null ? "-" : G.Key.regionName),
                                   termCode = (G.Key.termCode == null ? "-" : G.Key.termCode),
                                   termNo = (G.Key.termCode == null ? "-" : G.Key.termNo + " - " + G.Key.termRemarks),
                                   name = (G.Key.nama == null ? "-" : G.Key.nama),
                                   address = (G.Key.address == null ? "-" : G.Key.address),
                                   NPWP = (G.Key.npwp == null ? "-" : G.Key.npwp),
                                   phone = (G.Key.phone == null ? "-" : G.Key.phone),
                                   psCode = (G.Key.psCode == null ? "" : G.Key.psCode),
                                   salesEvent = (G.Key.eventName == null ? "-" : G.Key.eventName),
                                   transactionCome = (G.Key.transCode == null ? "-" : G.Key.transCode + " | " + G.Key.transName),
                                   payType = (G.Key.payTypeDesc == null ? "-" : G.Key.payTypeDesc),
                                   bankName = (G.Key.bankName == null ? "" : G.Key.bankName),
                                   amount = G.Sum(d => d.BFAmount),
                                   noRekening = (G.Key.bankNo == null ? "" : G.Key.bankNo),
                                   schema = (G.Key.scmCode == null ? null : G.Key.scmCode) + " - " + (G.Key.scmName == null ? null : G.Key.scmName),
                                   memberID = (G.Key.memberCode == null ? "" : G.Key.memberCode),
                                   memberName = (G.Key.memberName == null ? "" : G.Key.memberName),
                                   bookDate = G.Key.bookDate,
                                   bookedStatus = (G.Key.cancelDate == null ? "-" : "Cancel"),
                                   cancelDate = G.Key.cancelDate,
                                   remarks = (G.Key.remarks == null ? "-" : G.Key.remarks),
                                   status = (G.Key.statusDesc == null ? "-" : G.Key.statusDesc),
                                   reason = (G.Key.reasonDesc == null ? "-" : G.Key.reasonDesc),
                                   reasonRemarks = (G.Key.reasonRemarks == null ? "-" : G.Key.reasonRemarks)
                               }
                          ).FirstOrDefault();

            var getDataUnit = (from bh in _trBookingHeaderRepo.GetAll()
                               join u in _msUnitRepo.GetAll() on bh.unitID equals u.Id
                               join uc in _msUnitCodeRepo.GetAll() on u.unitCodeID equals uc.Id
                               where u.Id == UnitID.unitID
                               select new {
                                   bh.bookCode,
                                   u.unitNo,
                                   uc.unitCode
                               }).FirstOrDefault();

            var getAllData = new GetUniversalMainListDto
            {
                GetAllPSASMainHeaderDto = getAllHeaderMain,
                GetPSASMainDto = getMainData,
                unitCode = getDataUnit.unitCode,
                unitNo = getDataUnit.unitNo,
                bookCode = getDataUnit.bookCode
            };
            return getAllData;
        }

        public FileDto GetUniversalPSASToExport(GetPSASParamsDto input)
        {
            //var psasMain = GetPSASMain(input);
            var psasMain = GetPsasMainToExport(input);
            var psasPrice = _ipriceAppService.GetUniversalPrice(input);
            var psasPayment = GetPaymentByBookCode(input);
            var paramSchedule = new GetPSASScheduleParamDto() {
                                    bookCode = input.bookCode,
                                    coCode = "All",
                                    unitCode = input.unitCode,
                                    unitNo = input.unitNo
                                };
            var psasSchedule = GetSchedule(paramSchedule);

            var dataToExport = new GetUniversalPsasDto()
            {
                psasMain = psasMain,
                psasPayment = psasPayment,
                psasPrice = psasPrice,
                psasSchedule = psasSchedule
            };

            return _printBookingProfileExcelExporter.GenerateExcelBookingProfile(dataToExport);
        }

        private GetPSASMainUnitDetailDto GetPsasMainToExport(GetPSASParamsDto input)
        {
            var UnitID = _ipriceAppService.GetParameter(input);

            var getDataPersonal = (from x in _contextPers.PERSONAL
                                   join a in _trBookingHeaderRepo.GetAll().ToList() on x.psCode equals a.psCode
                                   join b in _contextPers.TR_Address on a.psCode equals b.psCode into address
                                   from b in address.DefaultIfEmpty()
                                   join c in _contextPers.TR_Phone on a.psCode equals c.psCode into phone
                                   from c in phone.DefaultIfEmpty()
                                   where a.Id == UnitID.bookingHeaderID
                                   select new
                                   {
                                       dataPsCode = a.psCode,
                                       npwp = x.NPWP,
                                       nama = x.name,
                                       address = (b == null ? "-" : b.address),
                                       phone = (c == null ? "-" : c.number)
                                   }).ToList();

            var getDataDocumentPPJB = (from x in _contextProp.TR_BookingDocument
                                       join a in _contextProp.MS_DocumentPS on x.docID equals a.Id
                                       where x.bookingHeaderID == UnitID.bookingHeaderID && a.docCode == "PPJB"
                                       select x).FirstOrDefault();
            var getDataDocumentCN = (from x in _contextProp.TR_BookingDocument
                                       join a in _contextProp.MS_DocumentPS on x.docID equals a.Id
                                       where x.bookingHeaderID == UnitID.bookingHeaderID && a.docCode == "CN"
                                       select x).FirstOrDefault();
            var getDataDocumentKPU = (from x in _contextProp.TR_BookingDocument
                                     join a in _contextProp.MS_DocumentPS on x.docID equals a.Id
                                     where x.bookingHeaderID == UnitID.bookingHeaderID && a.docCode == "KPU"
                                     select x).FirstOrDefault();
            var getDataDocumentPPPU = (from x in _contextProp.TR_BookingDocument
                                      join a in _contextProp.MS_DocumentPS on x.docID equals a.Id
                                      where x.bookingHeaderID == UnitID.bookingHeaderID && a.docCode == "PPPU"
                                      select x).FirstOrDefault();

            var getMainData = (from a in _contextProp.TR_BookingHeader
                               join b in _contextProp.MS_Unit on a.unitID equals b.Id into unit
                               from b in unit.DefaultIfEmpty()
                               join d in _contextProp.MS_Area on b.areaID equals d.Id into area
                               from d in area.DefaultIfEmpty()
                               join e in _contextProp.MS_Project on b.projectID equals e.Id into project
                               from e in project.DefaultIfEmpty()
                               join f in _contextProp.MS_Product on b.productID equals f.Id into product
                               from f in product.DefaultIfEmpty()
                               join j in _contextProp.MS_Term on a.termID equals j.Id into term
                               from j in term.DefaultIfEmpty()
                               join l in getDataPersonal on a.psCode equals l.dataPsCode into pers
                               from l in pers.DefaultIfEmpty()
                               join m in _contextProp.LK_PayType on a.BFPayTypeCode equals m.payTypeCode into payType
                               from m in payType.DefaultIfEmpty()
                               join n in _contextProp.MS_Transfrom on a.transID equals n.Id into transform
                               from n in transform.DefaultIfEmpty()
                               join o in _contextNew.MS_Schema.ToList() on a.scmCode equals o.scmCode into schema
                               from o in schema.DefaultIfEmpty()
                               join q in _contextProp.MS_SalesEvent on a.eventID equals q.Id into salesEvent
                               from q in salesEvent.DefaultIfEmpty()
                               join r in _contextProp.LK_SADStatus on a.SADStatusID equals r.Id into SADStatus
                               from r in SADStatus.DefaultIfEmpty()
                               join t in _contextProp.TR_BookingDetail on a.Id equals t.bookingHeaderID
                               join v in _contextProp.TR_BookingCancel on a.Id equals v.bookingHeaderID into bookingCancel
                               from v in bookingCancel.DefaultIfEmpty()
                               join z in _contextProp.LK_Reason on v.reasonID equals z.Id into lkReason
                               from z in lkReason.DefaultIfEmpty()
                               join aa in _contextProp.MS_Category on b.categoryID equals aa.Id into ca
                               from aa in ca.DefaultIfEmpty()
                               join ab in _contextProp.MS_Cluster on b.clusterID equals ab.Id into cl
                               from ab in cl.DefaultIfEmpty()
                               join ac in _contextProp.MS_Detail on b.detailID equals ac.Id into dt
                               from ac in dt.DefaultIfEmpty()
                               join ae in _contextProp.MS_UnitCode on b.unitCodeID equals ae.Id into unc
                               from ae in unc.DefaultIfEmpty()
                               where a.Id == UnitID.bookingHeaderID
                               group t by new
                               {
                                   e.projectName,
                                   f.productName,
                                   d.regionName,
                                   j.termCode,
                                   j.termNo,
                                   termRemarks = j.remarks,
                                   a.memberName,
                                   l.address,
                                   l.npwp,
                                   l.phone,
                                   a.psCode,
                                   q.eventName,
                                   n.transCode,
                                   n.transName,
                                   m.payTypeDesc,
                                   a.bankName,
                                   a.bankNo,
                                   o.scmName,
                                   o.scmCode,
                                   r.statusDesc,
                                   a.memberCode,
                                   l.nama,
                                   a.bookDate,
                                   a.cancelDate,
                                   a.remarks,
                                   z.reasonDesc,
                                   reasonRemarks = v.remarks,
                                   aa.categoryName,
                                   ab.clusterName,
                                   ac.detailName,
                                   b.unitNo,
                                   ae.unitCode,
                                   ae.unitName,
                                   a.PPJBDue,
                                   a.bookCode
                               } into G
                               select new GetPSASMainUnitDetailDto
                               {
                                   projectName = (G.Key.projectName == null ? "" : G.Key.projectName),
                                   productName = (G.Key.productName == null ? "" : G.Key.productName),
                                   territory = (G.Key.regionName == null ? "" : G.Key.regionName),
                                   termCode = (G.Key.termCode == null ? "" : G.Key.termCode),
                                   termNo = (G.Key.termCode == null ? "" : G.Key.termNo + " - " + G.Key.termRemarks),
                                   name = (G.Key.nama == null ? "" : G.Key.nama),
                                   address = (G.Key.address == null ? "" : G.Key.address),
                                   NPWP = (G.Key.npwp == null ? "" : G.Key.npwp),
                                   phone = (G.Key.phone == null ? "" : G.Key.phone),
                                   psCode = (G.Key.psCode == null ? "" : G.Key.psCode),
                                   salesEvent = (G.Key.eventName == null ? "" : G.Key.eventName),
                                   transactionCome = (G.Key.transCode == null ? "" : G.Key.transCode + " | " + G.Key.transName),
                                   payType = (G.Key.payTypeDesc == null ? "" : G.Key.payTypeDesc),
                                   bankName = (G.Key.bankName == null ? "" : G.Key.bankName),
                                   amount = G.Sum(d => d.BFAmount),
                                   noRekening = (G.Key.bankNo == null ? "" : G.Key.bankNo),
                                   schema = (G.Key.scmCode == null ? null : G.Key.scmCode) + " - " + (G.Key.scmName == null ? null : G.Key.scmName),
                                   memberID = (G.Key.memberCode == null ? "" : G.Key.memberCode),
                                   memberName = (G.Key.memberName == null ? "" : G.Key.memberName),
                                   bookDate = G.Key.bookDate,
                                   bookedStatus = (G.Key.cancelDate == null ? "" : "Cancel"),
                                   cancelDate = G.Key.cancelDate,
                                   remarks = (G.Key.remarks == null ? "" : G.Key.remarks),
                                   status = (G.Key.statusDesc == null ? "" : G.Key.statusDesc),
                                   reason = (G.Key.reasonDesc == null ? "" : G.Key.reasonDesc),
                                   reasonRemarks = (G.Key.reasonRemarks == null ? "" : G.Key.reasonRemarks),
                                   finDesc = (G.Key.termRemarks == null ? "" : G.Key.termRemarks),
                                   unitCode = (G.Key.unitCode == null ? "" : G.Key.unitCode),
                                   unitNo = (G.Key.unitNo == null ? "" : G.Key.unitNo),
                                   unitName = (G.Key.unitName == null ? "" : G.Key.unitName),
                                   sadStatus = (G.Key.statusDesc == null ? "" : G.Key.statusDesc),
                                   membershipType = (G.Key.scmName == null ? "" : G.Key.scmName),
                                   bookCode = (G.Key.bookCode == null ? "" : G.Key.bookCode),
                                   categoryName = (G.Key.categoryName == null ? "" : G.Key.categoryName),
                                   clusterName = (G.Key.clusterName == null ? "" : G.Key.clusterName),
                                   detailName = (G.Key.detailName == null ? "" : G.Key.detailName)
                               }
                          ).FirstOrDefault();

            getMainData.ppjb = getDataDocumentPPJB == null ? "" : getDataDocumentPPJB.docNo;
            getMainData.kpu = getDataDocumentKPU == null ? "" : getDataDocumentKPU.docNo;
            getMainData.pppu = getDataDocumentPPPU == null ? "" : getDataDocumentPPPU.docNo;
            getMainData.cn = getDataDocumentCN == null ? "" : getDataDocumentCN.docNo;

            return getMainData;
        }

        private List<GetPSASMainPaymentDto> GetPaymentByBookCode(GetPSASParamsDto input)
        {
            var bhID = _ipriceAppService.GetParameter(input);

            var getData = (from pda in _contextProp.TR_PaymentDetailAlloc
                           join pd in _contextProp.TR_PaymentDetail on new { S1 = pda.paymentDetailID, S2 = "C" } equals new { S1 = pd.Id, S2 = pd.status } //into l1
                           //from pd in l1.DefaultIfEmpty()
                           join ph in _contextProp.TR_PaymentHeader on new { S1 = pd.paymentHeaderID, S2 = bhID.bookingHeaderID } equals new { S1 = ph.Id, S2 = ph.bookingHeaderID } //into l2
                           //from ph in l2.DefaultIfEmpty()
                           join pf in _contextProp.LK_PayFor on ph.payForID equals pf.Id
                           join b in _contextProp.MS_Bank on pd.bankName equals b.bankCode into l3
                           from b in l3.DefaultIfEmpty()
                           join bb in _contextProp.MS_BankBranch on b.Id equals bb.bankID into l4
                           from bb in l4.DefaultIfEmpty()
                           join a in _contextProp.MS_Account on ph.accountID equals a.Id into l5
                           from a in l5.DefaultIfEmpty()
                           join pt in _contextProp.LK_PayType on pd.payTypeID equals pt.Id into l6
                           from pt in l6.DefaultIfEmpty()
                           join tax in _contextTax.FP_TR_FPHeader.ToList() on ph.transNo equals tax.transNo into l7
                           from tax in l7.DefaultIfEmpty()
                           where pf.payForCode != "OTP"
                           select new
                           {
                               paymentDate = ph.paymentDate == null ? default(DateTime) : ph.paymentDate,
                               clearDate = ph.clearDate == null ? null : ph.clearDate,
                               accCode = a.accCode == null ? null : a.accCode,
                               transNo = ph.transNo == null ? null : ph.transNo,
                               payForCode = pf.payForCode == null ? null : pf.payForCode,
                               othersTypeCode = pd.othersTypeCode == null ? null : pd.othersTypeCode,
                               payTypeCode = pt.payTypeCode == null ? null : pt.payTypeCode,
                               bankName = pd.bankName == null ? null : pd.bankName,
                               bankBranchName = bb.bankBranchName == null ? null : bb.bankBranchName,
                               pda.netAmt,
                               pda.vatAmt,
                               ket = pd.ket == null ? null : pd.ket,
                               FPCode = tax.FPCode == null ? null : tax.FPCode
                           } into pda
                           group pda by new
                           {
                               pda.paymentDate,
                               pda.clearDate,
                               pda.accCode,
                               pda.transNo,
                               pda.payForCode,
                               pda.othersTypeCode,
                               pda.payTypeCode,
                               pda.bankName,
                               pda.bankBranchName,
                               pda.ket,
                               pda.FPCode
                           } into G
                           select new
                           {
                               bankName = G.Key.bankName == null ? String.Empty : G.Key.bankName,
                               bankBranchName = G.Key.bankBranchName == null ? String.Empty : G.Key.bankBranchName,
                               accCode = G.Key.accCode == null ? null : G.Key.accCode,
                               transNo = G.Key.transNo == null ? null : G.Key.transNo,
                               paymentDate = G.Key.paymentDate == null ? default(DateTime) : G.Key.paymentDate,
                               clearDate = G.Key.clearDate == null ? null : G.Key.clearDate,
                               payForCode = G.Key.payForCode == null ? null : G.Key.payForCode,
                               payTypeCode = G.Key.payTypeCode == null ? null : G.Key.payTypeCode,
                               othersTypeCode = G.Key.othersTypeCode == null ? null : G.Key.othersTypeCode,
                               netAmount = G.Sum(X => X.netAmt),
                               vatAmount = G.Sum(x => x.vatAmt),
                               remarks = G.Key.ket == null ? null : G.Key.ket,
                               fpCode = G.Key.FPCode == null ? null : G.Key.FPCode
                           }).ToList();

            List<GetPSASMainPaymentDto> listResult = new List<GetPSASMainPaymentDto>();

            foreach (var item in getData)
            {
                TypeDto type = new TypeDto
                {
                    payFor = item.payForCode,
                    payType = item.payTypeCode,
                    otherType = item.othersTypeCode
                };

                GetPSASMainPaymentDto result = new GetPSASMainPaymentDto
                {
                    bankName = item.bankName,
                    bankBranch = item.bankBranchName,
                    account = item.accCode,
                    transNo = item.transNo,
                    PMTDate = item.paymentDate,
                    clearDate = item.clearDate,
                    type = type,
                    netAmount = item.netAmount,
                    vatAmt = item.vatAmount,
                    remarks = item.remarks,
                    taxFP = item.fpCode
                };

                listResult.Add(result);
            }
            return listResult;
        }

        private List<GetPSASMainScheduleDto> GetSchedule(GetPSASScheduleParamDto input)
        {
            var getInput = new GetPSASParamsDto()
            {
                bookCode = input.bookCode,
                unitCode = input.unitCode,
                unitNo = input.unitNo
            };
            var getParamsPSAS = _ipriceAppService.GetParameter(getInput);

            var checkScheduleData = (from x in _contextProp.TR_BookingDetailSchedule
                                     join a in _contextProp.TR_BookingDetail on x.bookingDetailID equals a.Id
                                     join b in _contextProp.TR_BookingHeader on a.bookingHeaderID equals b.Id
                                     join c in _contextProp.LK_Alloc on x.allocID equals c.Id
                                     join d in _contextProp.TR_PenaltySchedule on new { A = b.Id, B = (int)x.schedNo, C = c.allocCode } equals new { A = d.bookingHeaderID, B = d.ScheduleTerm, C = d.ScheduleAllocCode } into pen
                                     from d in pen.DefaultIfEmpty()
                                     where b.Id == getParamsPSAS.bookingHeaderID
                                     select new
                                     {
                                         x.schedNo,
                                         x.dueDate,
                                         x.allocID,
                                         x.netAmt,
                                         x.vatAmt,
                                         x.remarks,
                                         a.pctTax,
                                         a.coCode,
                                         x.netOut,
                                         x.vatOut,
                                         penaltyAge = d == null ? 0 : d.penaltyAging,
                                         penaltyAmount = d == null ? 0 : d.penaltyAmount
                                     });
            
            List<GetPSASMainScheduleDto> dataSchedule = new List<GetPSASMainScheduleDto>();

            if (checkScheduleData.Any())
            {
                var dataSchedules = (from x in checkScheduleData
                                     where (input.coCode == "All") || (input.coCode != "All" && x.coCode == input.coCode)
                                     group x by new
                                     {
                                         x.schedNo,
                                         x.dueDate,
                                         x.allocID,
                                         x.remarks,
                                         x.penaltyAge,
                                         x.penaltyAmount
                                     } into G
                                     select new GetPSASMainScheduleDto
                                     {
                                         allocID = G.Key.allocID,
                                         allocCode = (from da in _contextProp.LK_Alloc where da.Id == G.Key.allocID select da.allocCode).FirstOrDefault(),
                                         netAmount = G.Sum(d => d.netAmt),
                                         VATAmount = G.Sum(d => d.vatAmt),
                                         totalAmount = G.Sum(d => d.netAmt) + G.Sum(d => d.vatAmt),
                                         netOutstanding = G.Sum(d => d.netOut),
                                         VATOutstanding = G.Sum(d => d.vatOut),
                                         totalOutstanding = G.Sum(d => d.netOut) + G.Sum(d => d.vatOut),
                                         dueDate = G.Key.dueDate,
                                         schedNo = G.Key.schedNo,
                                         remarks = G.Key.remarks,
                                         paymentAmount = (G.Sum(d => d.netAmt) + G.Sum(d => d.vatAmt)) - (G.Sum(d => d.netOut) + G.Sum(d => d.vatOut)),
                                         penaltyAge = G.Key.penaltyAge,
                                         penaltyAmount = G.Key.penaltyAmount
                                     }).ToList();

                foreach (var dataSched in dataSchedules)
                {
                    var dataPayment = (from pda in _contextProp.TR_PaymentDetailAlloc
                                       join pd in _contextProp.TR_PaymentDetail on pda.paymentDetailID equals pd.Id
                                       join ph in _contextProp.TR_PaymentHeader on pd.paymentHeaderID equals ph.Id
                                       join pt in _contextProp.LK_PayType on pd.payTypeID equals pt.Id
                                       where ph.bookingHeaderID == getParamsPSAS.bookingHeaderID && pda.schedNo == dataSched.schedNo
                                       select new DataPaymentListDto
                                       {
                                           clearDate = ph.clearDate,
                                           transNo = ph.transNo,
                                           payNo = pd.payNo,
                                           otherType = pd.othersTypeCode,
                                           payType = pt.payTypeCode,
                                           netAmountPayment = pda.netAmt,
                                           vatAmountPayment = pda.vatAmt,
                                           totalAmountPayment = pda.netAmt + pda.vatAmt
                                       }).ToList();

                    var dataSchedPush = new GetPSASMainScheduleDto
                    {
                        allocID = dataSched.allocID,
                        allocCode = dataSched.allocCode,
                        netAmount = dataSched.netAmount,
                        VATAmount = dataSched.VATAmount,
                        totalAmount = dataSched.totalAmount,
                        dueDate = dataSched.dueDate,
                        schedNo = dataSched.schedNo,
                        paymentAmount = dataSched.paymentAmount,
                        netOutstanding = dataSched.netOutstanding,
                        VATOutstanding = dataSched.VATOutstanding,
                        totalOutstanding = dataSched.totalOutstanding,
                        remarks = dataSched.remarks,
                        penaltyAge = dataSched.penaltyAge,
                        penaltyAmount = dataSched.penaltyAmount,
                        dataPayment = dataPayment
                    };

                    dataSchedule.Add(dataSchedPush);
                }

                dataSchedule.OrderBy(x => x.allocID).ThenBy(x => x.dueDate);
                
            }

            return dataSchedule;
        }
    }
}
