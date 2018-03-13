using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;
using VDI.Demo.PropertySystemDB.Pricing;
using Abp.Application.Services.Dto;
using VDI.Demo.OnlineBooking.BookingHistory.Dto;
using Abp.UI;
using Abp.Domain.Uow;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.OnlineBooking.PPOnline;
using Abp.Authorization;
using VDI.Demo.Authorization;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.OnlineBooking.BookingHistory
{
    //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_BookingHistory)]
    public class BookingHistoryAppService : DemoAppServiceBase, IBookingHistoryAppService
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
        private readonly IRepository<MS_TermMain> _msTermMainRepo;
        private readonly IRepository<TR_ID, string> _trIDRepo;
        private readonly IRepository<LK_Item> _lkItem;
        private readonly IRepository<TR_Address, string> _trAddress;
        private readonly IRepository<MS_UnitCode> _msUnitCode;
        private readonly IRepository<MS_Renovation> _msRenovation;
        private readonly IRepository<LK_BookingOnlineStatus> _lkBookingOnlineStatusRepo;
        private readonly IRepository<LK_PaymentType> _lkPaymentTypeRepo;
        private readonly IRepository<MS_Project> _msProject;


        public BookingHistoryAppService(
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
            IRepository<TR_Address, string> trAddress,
            IRepository<MS_TermMain> msTermMainRepo,
            IRepository<MS_UnitCode> msUnitCode,
            IRepository<MS_Renovation> msRenovation,
            IRepository<LK_BookingOnlineStatus> lkBookingOnlineStatusRepo,
            IRepository<LK_PaymentType> lkPaymentTypeRepo,
            IRepository<MS_Project> msProject

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
            _trAddress = trAddress;
            _msTermMainRepo = msTermMainRepo;
            _msUnitCode = msUnitCode;
            _msRenovation = msRenovation;
            _lkBookingOnlineStatusRepo = lkBookingOnlineStatusRepo;
            _lkPaymentTypeRepo = lkPaymentTypeRepo;
            _msProject = msProject;
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_BookingHistory_GetDetailBookingHistory)]
        public DetailBookingHistoryResultDto GetDetailBookingHistory(int orderID)
        {
            var history = (from orderHeader in _trUnitOrderHeader.GetAll()
                           join Status in _lkBookingOnlineStatusRepo.GetAll()
                           on orderHeader.statusID equals Status.Id
                           join PaymentType in _lkPaymentTypeRepo.GetAll()
                           on orderHeader.paymentTypeID equals PaymentType.Id
                           where orderHeader.Id == orderID
                           select new { orderHeader, Status, PaymentType }).FirstOrDefault();

            if (history == null)
            {
                var details = new DetailBookingHistoryResultDto
                {
                    message = "Data Not Found"
                };

                return details;
            }
            else
            {
                var getDetail = (from orderDetail in _trUnitOrderDetail.GetAll()
                                 join orderHeader in _trUnitOrderHeader.GetAll()
                                 on orderDetail.UnitOrderHeaderID equals orderHeader.Id
                                 join unit in _msUnitRepo.GetAll()
                                 on orderDetail.unitID equals unit.Id
                                 join unitcode in _msUnitCode.GetAll()
                                 on unit.unitCodeID equals unitcode.Id
                                 join unitItemPrice in _msUnitItemPriceRepo.GetAll()
                                 on unit.Id equals unitItemPrice.unitID
                                 join renovation in _msRenovation.GetAll()
                                 on orderDetail.renovID equals renovation.Id
                                 join renov in _lkItem.GetAll()
                                 on renovation.renovationCode equals renov.itemCode
                                 join project in _msProject.GetAll()
                                 on unit.projectID equals project.Id
                                 where orderDetail.UnitOrderHeaderID == history.orderHeader.Id
                                 join term in _msTermRepo.GetAll()
                                 on orderDetail.termID equals term.Id
                                 where orderDetail.UnitOrderHeaderID == history.orderHeader.Id && renov.itemName.Contains("Renov")
                                 select new { orderDetail, unit, unitcode, renov, term, project }).Distinct().ToList();

                var getAddress = (from x in _trAddress.GetAll()
                                  where x.psCode == history.orderHeader.psCode
                                  select x.address).FirstOrDefault();

                var penampung = new List<UnitResultDto>();
                foreach (var item in getDetail)
                {
                    var data = new UnitResultDto
                    {
                        unitcode = item.unitcode.unitCode,
                        unitno = item.unit.unitNo,
                        renovcode = item.renov.shortName,
                        termno = item.term.termNo,
                        termName = item.term.remarks,
                        sellingprice = item.orderDetail.sellingPrice,
                        bfamount = item.orderDetail.BFAmount,
                        remarks = item.orderDetail.remarks,
                        disc1 = item.orderDetail.disc1,
                        disc2 = item.orderDetail.disc2,
                        renovID = item.orderDetail.renovID,
                        termID = item.orderDetail.termID,
                        unitID = item.orderDetail.unitID,
                        projectID = item.project.Id
                    };
                    penampung.Add(data);
                }

                var arrPP = new List<PPNResultDto>();

                var pp = new PPNResultDto
                {
                    PPNo = ""
                };

                arrPP.Add(pp);

                var getKTP = (from id in _trIDRepo.GetAll()
                              where id.psCode == history.orderHeader.psCode
                              select id).FirstOrDefault();

                var detail = new DetailBookingHistoryResultDto();

                if (getAddress != null && getDetail != null && history != null)
                {
                    detail = new DetailBookingHistoryResultDto
                    {
                        orderHeaderID = history.orderHeader.Id,
                        statusID = history.Status.Id,
                        payTypeID = history.PaymentType.Id,
                        orderCode = history.orderHeader.orderCode,
                        address = getAddress,
                        payType = history.PaymentType.paymentTypeName,
                        membercode = history.orderHeader.memberCode,
                        membername = history.orderHeader.memberName,
                        pscode = history.orderHeader.psCode,
                        psname = history.orderHeader.psName,
                        custemail = history.orderHeader.psEmail,
                        custphone = history.orderHeader.psPhone,
                        scmcode = history.orderHeader.scmCode,
                        status = history.Status.statusTypeName,
                        IDNo = getKTP.idNo,
                        orderDate = history.orderHeader.orderDate,
                        arrUnit = penampung,
                        arrPP = arrPP,
                        bankRekeningPemilik = history.orderHeader.bankRekeningPemilik,
                        nomorRekeningPemilik = history.orderHeader.nomorRekeningPemilik,
                        sumberDanaID = history.orderHeader.sumberDanaID,
                        tujuanTransaksiID = history.orderHeader.tujuanTransaksiID,
                        totalAmount = history.orderHeader.totalAmt
                    };
                }
                else if (getAddress == null && getDetail != null && history != null)
                {
                    detail = new DetailBookingHistoryResultDto
                    {
                        orderHeaderID = history.orderHeader.Id,
                        statusID = history.Status.Id,
                        payTypeID = history.PaymentType.Id,
                        orderCode = history.orderHeader.orderCode,
                        address = null,
                        payType = history.PaymentType.paymentTypeName,
                        membercode = history.orderHeader.memberCode,
                        membername = history.orderHeader.memberName,
                        pscode = history.orderHeader.psCode,
                        psname = history.orderHeader.psName,
                        custemail = history.orderHeader.psEmail,
                        custphone = history.orderHeader.psPhone,
                        scmcode = history.orderHeader.scmCode,
                        status = history.Status.statusTypeName,
                        IDNo = getKTP == null ? "0" : getKTP.idNo,
                        orderDate = history.orderHeader.orderDate,
                        arrUnit = penampung,
                        arrPP = arrPP,
                        bankRekeningPemilik = history.orderHeader.bankRekeningPemilik,
                        nomorRekeningPemilik = history.orderHeader.nomorRekeningPemilik,
                        sumberDanaID = history.orderHeader.sumberDanaID,
                        tujuanTransaksiID = history.orderHeader.tujuanTransaksiID,
                        totalAmount = history.orderHeader.totalAmt
                    };
                }
                else
                {
                    detail = new DetailBookingHistoryResultDto
                    {
                        message = "Data Not Found"
                    };
                }
                return detail;
            }

        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_BookingHistory_GetListBookingHistory)]
        public ListResultDto<ListBookingHistoryResultDto> GetListBookingHistory(string memberCode)
        {
            var getBooking = (from trUnitHeader in _trUnitOrderHeader.GetAll()
                              join lkStatus in _lkBookingOnlineStatusRepo.GetAll()
                              on trUnitHeader.statusID equals lkStatus.Id
                              where trUnitHeader.memberCode == memberCode && lkStatus.statusType != "4"
                              orderby trUnitHeader.Id descending
                              select new ListBookingHistoryResultDto
                              {
                                  statusID = lkStatus.Id,
                                  orderID = trUnitHeader.Id,
                                  orderCode = trUnitHeader.orderCode,
                                  orderDate = trUnitHeader.orderDate,
                                  psName = trUnitHeader.psName,
                                  status = lkStatus.statusTypeName
                              }).ToList();

            return new ListResultDto<ListBookingHistoryResultDto>(getBooking);
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_BookingHistory_SearchingBookingHistoryMobile)]
        public List<ListBookingHistoryResultDto> SearchingBookingHistoryMobile(SearchingBookingHistoryInputDto input)
        {
            var getBooking = (from trUnitHeader in _trUnitOrderHeader.GetAll()
                              join lkStatus in _lkBookingOnlineStatusRepo.GetAll()
                              on trUnitHeader.statusID equals lkStatus.Id
                              where trUnitHeader.memberCode == input.memberCode && lkStatus.statusType != "4"
                                    && (trUnitHeader.orderCode.Contains(input.filter)
                                    || trUnitHeader.psName.Contains(input.filter))
                              orderby trUnitHeader.Id descending
                              select new ListBookingHistoryResultDto
                              {
                                  statusID = lkStatus.Id,
                                  orderID = trUnitHeader.Id,
                                  orderCode = trUnitHeader.orderCode,
                                  orderDate = trUnitHeader.orderDate,
                                  psName = trUnitHeader.psName,
                                  status = lkStatus.statusTypeName
                              }).ToList();

            return getBooking;
        }
    }
}
