using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.NewCommDB;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.Pricing;
using VDI.Demo.PSAS.ChangeRenov.Dto;
using VDI.Demo.PSAS.Dto;
using VDI.Demo.PSAS.Main.Dto;
using VDI.Demo.PSAS.Price;

namespace VDI.Demo.PSAS.ChangeRenov
{
    public class PSASChangeRenovAppService : DemoAppServiceBase, IChangeRenovPSASAppService
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
        private readonly IRepository<MS_Company> _msCompanyRepo;
        private readonly IRepository<TR_Address, string> _trAddressRepo;
        private readonly IRepository<PERSONALS_MEMBER, string> _personalMemberRepo;
        private readonly IRepository<TR_Phone, string> _trPhoneRepo;
        private readonly IRepository<LK_Reason> _lkReasonRepo;
        private readonly IPSASPriceAppService _iPriceAppService;
        private readonly PersonalsNewDbContext _contextPers;
        private readonly NewCommDbContext _contextNew;
        private readonly PropertySystemDbContext _contextProp;
        private readonly IRepository<TR_BookingItemPrice> _trBookingItemPriceRepo;
        private readonly IRepository<TR_BookingDetailAddDisc> _trBookingDetailAddDiscRepo;

        public PSASChangeRenovAppService(
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
            IRepository<MS_Company> msCompanyRepo,
            IRepository<TR_Address, string> trAddressRepo,
            IRepository<PERSONALS_MEMBER, string> personalMemberRepo,
            IRepository<TR_Phone, string> trPhoneRepo,
            IRepository<LK_Reason> lkReasonRepo,
            IPSASPriceAppService iPriceAppService,
            PersonalsNewDbContext contextPers,
            NewCommDbContext contextNew,
            PropertySystemDbContext contextProp,
            IRepository<TR_BookingItemPrice> trBookingItemPriceRepo,
            IRepository<TR_BookingDetailAddDisc> trBookingDetailAddDiscRepo
        )
        {
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
            _msCompanyRepo = msCompanyRepo;
            _trAddressRepo = trAddressRepo;
            _personalMemberRepo = personalMemberRepo;
            _trPhoneRepo = trPhoneRepo;
            _lkReasonRepo = lkReasonRepo;
            _iPriceAppService = iPriceAppService;
            _contextPers = contextPers;
            _contextNew = contextNew;
            _trBookingItemPriceRepo = trBookingItemPriceRepo;
            _trBookingDetailAddDiscRepo = trBookingDetailAddDiscRepo;
        }

        public void CalculationChangeRenov(ChangeRenovInputDto input)
        {
            //get grossprice
            var getGrossPrice = (from bip in _trBookingItemPriceRepo.GetAll()
                                 where bip.bookingHeaderID == input.bookingHeaderID && bip.itemID == input.itemID && bip.termID == input.termID
                                 group bip by 1 into G
                                 select new
                                 {
                                     grossPrice = G.Sum(x => x.grossPrice)
                                 }).FirstOrDefault();

            var getAreacoCode = (from ui in _msUnitItemRepo.GetAll()
                                 join bh in _trBookingHeaderRepo.GetAll() on ui.unitID equals bh.unitID
                                 join bd in _trBookingDetailRepo.GetAll() on new { A = bh.Id, B = ui.itemID } equals new { A = bd.bookingHeaderID, B = bd.itemID }
                                 select new
                                 {
                                     ui.area,
                                     ui.coCode
                                 }).FirstOrDefault();

            var disc = (from a in _trBookingDetailRepo.GetAll()
                        join b in _trBookingHeaderRepo.GetAll() on a.bookingHeaderID equals b.Id
                        where a.bookingHeaderID == input.bookingHeaderID
                        group a by new
                        {
                            b.unitID,
                            a.pctDisc
                        } into G
                        select new
                        {
                            discount = G.Key.pctDisc,
                            totalDisc = G.Sum(d => (double)d.amount * d.pctDisc)
                        }).FirstOrDefault();

            decimal netPrice = getGrossPrice == null ? 0 : getGrossPrice.grossPrice - (decimal)disc.totalDisc;

            var getAddDisc = (from a in _trBookingDetailAddDiscRepo.GetAll()
                              join b in _trBookingDetailRepo.GetAll()
                              on a.bookingDetailID equals b.Id
                              join c in _trBookingHeaderRepo.GetAll()
                              on b.bookingHeaderID equals c.Id
                              orderby a.addDiscNo
                              where c.Id == input.bookingHeaderID
                              select new
                              {
                                  discountName = a.addDiscDesc,
                                  discount = a.isAmount == true ? (double)a.amtAddDisc : a.pctAddDisc,
                                  a.isAmount
                              }).Distinct().ToList();

            decimal netNetPrice = netPrice;

            foreach (var addDisc in getAddDisc)
            {
                decimal discountAdd = addDisc.isAmount == true ? (decimal)addDisc.discount : (decimal)addDisc.discount * netNetPrice;

                netNetPrice = netNetPrice - discountAdd;
            }

            var countCoCode = (from ui in _msUnitItemRepo.GetAll()
                               join bh in _trBookingHeaderRepo.GetAll() on ui.unitID equals bh.unitID
                               join bd in _trBookingDetailRepo.GetAll() on new { A = bh.Id, B = ui.itemID } equals new { A = bd.bookingHeaderID, B = bd.itemID }
                               group ui by new
                               {
                                   ui.coCode
                               } into G
                               select new
                               {
                                   G.Key.coCode
                               }).Distinct().Count();

            var getDataToUpdate = (from bd in _trBookingDetailRepo.GetAll()
                                   join i in _lkItemRepo.GetAll() on bd.itemID equals i.Id
                                   where bd.bookingHeaderID == input.bookingHeaderID && !i.itemName.Contains("Tanah") && !i.itemName.Contains("Bangunan")
                                   select bd).FirstOrDefault();

            var dataToUpdate = getDataToUpdate.MapTo<TR_BookingDetail>();

            dataToUpdate.area = getAreacoCode == null ? 0 : getAreacoCode.area;
            dataToUpdate.coCode = getAreacoCode == null ? "-" : getAreacoCode.coCode;
            dataToUpdate.combineCode = countCoCode == 0 ? "1" : countCoCode.ToString();
            dataToUpdate.amount = getGrossPrice == null ? 0 : getGrossPrice.grossPrice;
            dataToUpdate.netPrice = netPrice;
            dataToUpdate.netNetPrice = netNetPrice;
            dataToUpdate.amountComm = getGrossPrice == null ? 0 : getGrossPrice.grossPrice;
            dataToUpdate.netPriceComm = netPrice;
            dataToUpdate.itemID = input.itemID;

            _trBookingDetailRepo.Update(dataToUpdate);

        }

        public GetRenovListDto GetDataChangeRenov(GetPSASParamsDto input)
        {
            var bookingHeaderId = _iPriceAppService.GetParameter(input).bookingHeaderID;

            var getDataUnit = (from bh in _trBookingHeaderRepo.GetAll()
                               join bd in _trBookingDetailRepo.GetAll() on bh.Id equals bd.bookingHeaderID
                               join i in _lkItemRepo.GetAll() on bd.itemID equals i.Id
                               join u in _msUnitRepo.GetAll() on bh.unitID equals u.Id
                               join uc in _msUnitCodeRepo.GetAll() on u.unitCodeID equals uc.Id
                               where bh.Id == bookingHeaderId && !i.itemName.Contains("Tanah") && !i.itemName.Contains("Bangunan")
                               select new GetRenovListDto
                               {
                                   bookingHeaderId = bh.Id,
                                   bookCode = bh.bookCode,
                                   unitNo = u.unitNo,
                                   unitCode = uc.unitCode,
                                   itemID = bd.itemID,
                                   renovCode = i.itemCode,
                                   termID = bh.termID
                               }).FirstOrDefault();

            return getDataUnit;
        }

        public List<GetRenovDropdownListDto> GetRenovDropdown(ItemDropdownInputDto input)
        {
            var getDataRenov = (from bip in _trBookingItemPriceRepo.GetAll()
                                join i in _lkItemRepo.GetAll() on bip.itemID equals i.Id
                                orderby i.itemName
                                where !i.itemName.Contains("Tanah") && !i.itemName.Contains("Bangunan")
                                && bip.termID == input.termID && bip.bookingHeaderID == input.bookingHeaderID
                                select new GetRenovDropdownListDto
                                {
                                    itemID = i.Id,
                                    renovCode = i.itemCode,
                                    renovName = i.itemName
                                }).Distinct().ToList();

           return getDataRenov;
        }
    }
}
