using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using VDI.Demo.Authorization;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.OnlineBooking.Diagramatic.Dto;
using VDI.Demo.OnlineBooking.Transaction;
using VDI.Demo.OnlineBooking.Transaction.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.OnlineBooking.PPOnline;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.OnlineBooking.Diagramatic
{
    //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic)]
    public class DiagramaticAppService : DemoAppServiceBase, IDiagramaticAppService
    {
        private readonly IRepository<MS_Unit> _msUnit;
        private readonly IRepository<MS_UnitItem> _msUnitItem;
        private readonly IRepository<MS_UnitItemPrice> _msUnitItemPrice;
        private readonly IRepository<MS_Detail> _msDetail;
        private readonly IRepository<MS_UnitCode> _msUnitCode;
        private readonly IRepository<MS_UnitRoom> _msUnitRoomRepo;
        private readonly IRepository<MS_Zoning> _msZoningRepo;
        private readonly IRepository<MS_Renovation> _msRenovation;
        private readonly IRepository<MS_Project> _msProject;
        private readonly IRepository<TR_UnitOrderHeader> _trUnitOrderHeader;
        private readonly IRepository<TR_UnitOrderDetail> _trUnitOrderDetail;
        private readonly IRepository<MS_TermMain> _msTermMainRepo;
        private readonly IRepository<MS_Term> _msTermRepo;
        private readonly IRepository<MS_TermAddDisc> _msTermAddDiscRepo;
        private readonly IRepository<MS_Cluster> _msCluster;
        private readonly IRepository<LK_Item> _lkItem;
        private readonly IRepository<PERSONALS, string> _personalsRepo;
        private readonly IRepository<PERSONALS_MEMBER, string> _personalMemberRepo;
        private readonly IRepository<TR_UnitReserved> _trUnitReserved;
        private readonly IRepository<LK_UnitStatus> _lkUnitStatus;
        private readonly IRepository<MS_SumberDana> _msSumberDanaRepo;
        private readonly IRepository<MS_TujuanTransaksi> _msTujuanTransaksiRepo;
        private readonly PropertySystemDbContext _contextProp;
        private readonly IRepository<LK_PaymentType> _lkPaymentType;
        private readonly ITransactionAppService _transactionAppService;

        public DiagramaticAppService(
            IRepository<MS_Unit> msUnit,
            IRepository<MS_UnitItem> msUnitItem,
            IRepository<MS_UnitItemPrice> msUnitItemPrice,
            IRepository<MS_Detail> msDetail,
            IRepository<MS_UnitCode> msUnitCode,
            IRepository<MS_Renovation> msRenovation,
            IRepository<TR_UnitOrderHeader> trUnitOrderHeader,
            IRepository<MS_TermMain> msTermMainRepo,
            IRepository<MS_Term> msTermRepo,
            IRepository<TR_UnitOrderDetail> trUnitOrderDetail,
            IRepository<MS_Cluster> msCluster,
            IRepository<LK_Item> lkItem,
            IRepository<MS_TermAddDisc> msTermAddDisc,
            IRepository<PERSONALS, string> personalsRepo,
            IRepository<PERSONALS_MEMBER, string> personalsMemberRepo,
            IRepository<TR_UnitReserved> trUnitReserved,
            IRepository<LK_UnitStatus> lkUnitStatus,
            IRepository<MS_Project> msProject,
            IRepository<MS_UnitRoom> msUnitRoomRepo,
            IRepository<MS_Zoning> msZoningRepo,
            IRepository<MS_SumberDana> msSumberDanaRepo,
            IRepository<MS_TujuanTransaksi> msTujuanTransaksiRepo,
            PropertySystemDbContext contextProp,
            IRepository<LK_PaymentType> lkPaymentType,
            ITransactionAppService transactionAppService
        )
        {
            _msUnit = msUnit;
            _msUnitItem = msUnitItem;
            _msUnitItemPrice = msUnitItemPrice;
            _msDetail = msDetail;
            _msUnitCode = msUnitCode;
            _trUnitOrderHeader = trUnitOrderHeader;
            _msTermMainRepo = msTermMainRepo;
            _msTermRepo = msTermRepo;
            _trUnitOrderDetail = trUnitOrderDetail;
            _msCluster = msCluster;
            _lkItem = lkItem;
            _msTermAddDiscRepo = msTermAddDisc;
            _personalsRepo = personalsRepo;
            _personalMemberRepo = personalsMemberRepo;
            _trUnitReserved = trUnitReserved;
            _msRenovation = msRenovation;
            _msProject = msProject;
            _msUnitRoomRepo = msUnitRoomRepo;
            _msZoningRepo = msZoningRepo;
            _lkUnitStatus = lkUnitStatus;
            _msSumberDanaRepo = msSumberDanaRepo;
            _msTujuanTransaksiRepo = msTujuanTransaksiRepo;
            _contextProp = contextProp;
            _lkPaymentType = lkPaymentType;
            _transactionAppService = transactionAppService;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetDetailUnitByProject)]
        public DetailUnitByProjectResultDto GetDetailUnitByProject(int unitID, int projectID)
        {
            var getUnitById = (from unit in _msUnit.GetAll()
                               join unitItem in _msUnitItem.GetAll()
                               on unit.Id equals unitItem.unitID
                               join detail in _msDetail.GetAll()
                               on unit.detailID equals detail.Id
                               join unitCode in _msUnitCode.GetAll()
                               on unit.unitCodeID equals unitCode.Id
                               join unitPrice in _msUnitItemPrice.GetAll()
                               on unit.Id equals unitPrice.unitID
                               join term in _msTermRepo.GetAll()
                               on unitPrice.termID equals term.Id
                               where unit.Id == unitID && unit.projectID == projectID && unitItem.itemID == 2 && term.termNo == 02
                               group unitPrice by new
                               {
                                   unitCode.unitName,
                                   unitItem.area,
                                   detail.detailName
                               } into G
                               select new DetailUnitByProjectResultDto
                               {
                                   floor = G.Key.unitName,
                                   area = G.Key.area,
                                   detailName = G.Key.detailName,
                                   sellingPrice = G.Sum(s => s.grossPrice)
                               }).FirstOrDefault();

            return getUnitById;
        }

        //REPAIR THIS!!!
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetUnitDetailWithTower)]
        public UnitDetailResultDto GetUnitDetailWithTower(int unitID)
        {
            var getRenov = (from x in _msUnitItemPrice.GetAll()
                            join y in _lkItem.GetAll()
                            on x.itemID equals y.Id
                            where x.unitID == unitID
                               && y.itemName.Contains("Renov")
                            select new ListRenovResultDto
                            {
                                renovID = y.Id,
                                renovCode = y.itemCode,
                                renovName = y.shortName
                            }).Distinct().ToList();

            var getDetail = (from a in _msTermRepo.GetAll()
                             join b in _msTermMainRepo.GetAll()
                             on a.termMainID equals b.Id
                             join c in _msUnit.GetAll()
                             on a.projectID equals c.projectID
                             join e in _msUnitCode.GetAll()
                             on c.unitCodeID equals e.Id
                             where c.Id == unitID
                             select new UnitDetailResultDto
                             {
                                 unitID = c.Id,
                                 unitCode = e.unitCode,
                                 unitNo = c.unitNo,
                                 bookingFee = b.BFAmount
                             }).FirstOrDefault();

            getDetail = new UnitDetailResultDto
            {
                unitCode = getDetail.unitCode,
                unitNo = getDetail.unitNo,
                bookingFee = getDetail.bookingFee,
                ListRenov = getRenov
            };

            return getDetail;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_ListPrice)]
        public ListResultDto<ListPriceResultDto> ListPrice(int unitID, int renovId)
        {
            var getGrossPriceItem = (from unitItemPrice in _msUnitItemPrice.GetAll()
                                     join unit in _msUnit.GetAll()
                                     on unitItemPrice.unitID equals unit.Id
                                     join unitCode in _msUnitCode.GetAll()
                                     on unit.unitCodeID equals unitCode.Id
                                     join term in _msTermRepo.GetAll()
                                     on unitItemPrice.termID equals term.Id
                                     join termMain in _msTermMainRepo.GetAll()
                                     on unit.termMainID equals termMain.Id
                                     where unitItemPrice.unitID == unitID && unitItemPrice.renovID == renovId
                                     orderby term.termNo
                                     select new ListPriceResultDto
                                     {
                                         termID = term.Id,
                                         grossPrice = unitItemPrice.grossPrice,
                                         termCode = term.termCode,
                                         termNo = term.termNo,
                                         unitCode = unitCode.unitCode,
                                         unitID = unitItemPrice.unitID,
                                         unitNo = unit.unitNo,
                                         termName = term.remarks,
                                         bookingFee = termMain.BFAmount
                                     }).ToList();

            foreach (var item in getGrossPriceItem)
            {
                var getDiscPerItem = (from a in _msTermAddDiscRepo.GetAll()
                                      join b in _msTermRepo.GetAll()
                                      on a.termID equals b.Id
                                      where a.termNo == item.termNo
                                      && b.termCode == item.termCode
                                      select a.addDiscPct).FirstOrDefault();

                var getTerm = (from a in _msTermRepo.GetAll()
                               where a.termCode == item.termCode && a.termNo == item.termNo
                               select a.remarks).FirstOrDefault();

                item.grossPrice = item.grossPrice - (item.grossPrice * Convert.ToDecimal(getDiscPerItem));
                item.termName = getTerm;
                item.disc1 = getDiscPerItem;
            }

            var getGrossPriceAll = (from x in getGrossPriceItem
                                    where x.unitID == unitID
                                    group x by new
                                    {
                                        x.unitID,
                                        x.termNo,
                                        x.unitCode,
                                        x.unitNo,
                                        x.termName,
                                        x.disc1,
                                        x.disc2,
                                        x.termCode,
                                        x.termID,
                                        x.bookingFee
                                    } into G
                                    select new ListPriceResultDto
                                    {
                                        unitID = G.Key.unitID,
                                        unitNo = G.Key.unitNo,
                                        unitCode = G.Key.unitCode,
                                        termNo = G.Key.termNo,
                                        grossPrice = G.Sum(s => s.grossPrice),
                                        termName = G.Key.termName,
                                        disc1 = G.Key.disc1,
                                        disc2 = G.Key.disc2,
                                        termCode = G.Key.termCode,
                                        termID = G.Key.termID,
                                        bookingFee = G.Key.bookingFee
                                    }).ToList();

            foreach (var item in getGrossPriceAll)
            {
                var getPctDisc = (from x in _msUnitItem.GetAll()
                                  where x.unitID == unitID
                                  select x.pctDisc).FirstOrDefault();

                var discount = item.grossPrice - (item.grossPrice * Convert.ToDecimal(getPctDisc));

                item.grossPrice = Math.Round(discount + (discount * Convert.ToDecimal(0.1)));
                item.disc2 = getPctDisc;

            }

            return new ListResultDto<ListPriceResultDto>(getGrossPriceAll);
        }

        //public void CobaHitung(string unitCode, string unitNo, string renovCode)
        //{
        //    var getGrossPrice = (from y in _msUnit.GetAll()
        //                         join x in _msUnitItemPrice.GetAll()
        //                         on new { y.unitNo, y.unitCode } equals new { x.unitNo, x.unitCode }
        //                         join z in _msTermRepo.GetAll()
        //                         on new { y.termCode, x.termNo } equals new { z.termCode, z.termNo }
        //                         join w in _msTermAddDiscRepo.GetAll()
        //                         on new { z.termCode, z.termNo } equals new { w.termCode, w.termNo }
        //                         where z.isActive == true && x.unitCode == unitCode && x.unitNo == unitNo
        //                         && x.renovCode == renovCode
        //                         orderby new { x.termNo, x.itemCode }
        //                         group x by new
        //                         {
        //                             x.termNo,
        //                             x.unitNo,
        //                             x.unitCode,
        //                         } into G
        //                         select new ListPriceDto
        //                         {
        //                             termNo = G.Key.termNo,
        //                             grossPrice = G.Sum(s => s.grossPrice),
        //                         }).ToList();
        //}
        //}



        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListUnitByUnitCode)]
        public ListResultDto<ListUnitByUnitCodeResultDto> GetListUnitByUnitCode(int unitCodeId, int projectId)
        {
            var getUnit = (from unit in _msUnit.GetAll()
                           where unit.unitCodeID == unitCodeId && new[] { 1, 8, 12 }.Contains(unit.unitStatusID)  //A, S, Z
                           orderby unit.unitNo ascending
                           select new ListUnitByUnitCodeResultDto
                           {
                               unit = unit.unitNo,
                               unitStatus = unit.unitStatusID
                           }).ToList();

            return new ListResultDto<ListUnitByUnitCodeResultDto>(getUnit);
        }

        //Dropdown Tower
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListTowerByProjectID)]
        public ListResultDto<ListTowerResultDto> GetListTowerByProjectID(int projectId)
        {
            var listTower = (from unit in _msUnit.GetAll()
                             join cluster in _msCluster.GetAll()
                             on unit.clusterID equals cluster.Id
                             where unit.projectID == projectId
                             orderby unit.clusterID ascending
                             select new ListTowerResultDto
                             {
                                 clusterID = cluster.Id,
                                 clusterCode = cluster.clusterCode,
                                 clusterName = cluster.clusterName
                             }).Distinct().ToList();

            if (listTower == null)
            {
                var error = new List<ListTowerResultDto>();

                var message = new ListTowerResultDto
                {
                    message = "data not found"
                };

                error.Add(message);

                return new ListResultDto<ListTowerResultDto>(error);
            }
            else
            {
                return new ListResultDto<ListTowerResultDto>(listTower);
            }
        }

        //Diagramatic
        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListDiagramatic)]
        public List<GetDiagramaticResultDto> GetListDiagramatic(DiagramaticMobileInputDto input)
        {
            var getUnit = (from x in _msUnit.GetAll()
                           join y in _msUnitItemPrice.GetAll()
                           on x.Id equals y.unitID
                           join z in _lkUnitStatus.GetAll()
                           on x.unitStatusID equals z.Id
                           where x.projectID == input.projectId && x.clusterID == input.clusterId
                           && (z.unitStatusCode == "A" || z.unitStatusCode == "Z")
                           select new GetDiagramaticResultDto
                           {
                               unitCodeId = x.unitCodeID,
                           }).Distinct().ToList();

            if (getUnit.Count == 0)
            {
                var error = new List<GetDiagramaticResultDto>();

                var message = new GetDiagramaticResultDto
                {
                    message = "projectId/clusterId Tidak Valid"
                };

                error.Add(message);

                //throw new UserFriendlyException("projectId/clusterId Tidak Valid");

                return new List<GetDiagramaticResultDto>(error);
            }

            foreach (var item in getUnit)
            {
                var getfloor = (from x in _msUnitCode.GetAll()
                                where x.Id == item.unitCodeId
                                orderby x.unitCode.Substring(x.unitCode.Length - 2) ascending
                                select x).FirstOrDefault();

                item.floor = getfloor.unitCode.Substring(getfloor.unitCode.Length - 2);
                item.unitCode = getfloor.unitCode;

                var getUnits = (from a in _msUnit.GetAll()
                                join b in _lkUnitStatus.GetAll() on a.unitStatusID equals b.Id
                                join c in _msUnitItem.GetAll() on a.Id equals c.unitID
                                join d in _msUnitItemPrice.GetAll() on new { unitID = a.Id, c.itemID } equals new { unitID = d.unitID, d.itemID }
                                join e in _msUnitRoomRepo.GetAll() on c.Id equals e.unitItemID into room
                                from e in room.DefaultIfEmpty()
                                join f in _msZoningRepo.GetAll() on a.zoningID equals f.Id
                                join g in _lkItem.GetAll() on c.itemID equals g.Id
                                join h in _msDetail.GetAll() on a.detailID equals h.Id
                                where a.projectID == input.projectId && a.clusterID == input.clusterId
                                && a.unitCodeID == item.unitCodeId
                                && (b.unitStatusCode == "A" || b.unitStatusCode == "Z")
                                && g.itemCode == "02"
                                orderby a.Id ascending
                                select new UnitMobileDto
                                {
                                    unitID = a.Id,
                                    unitNo = a.unitNo,
                                    unitStatusCode = b.unitStatusCode,
                                    unitStatusName = b.unitStatusName,
                                    bedroom = e.bedroom,
                                    zoningName = f.zoningName,
                                    unitType = a.unitNo.Substring(2, a.unitNo.Length),
                                    detailCode = h.detailCode,
                                    detailImage = h.detailImage,
                                    detailID = h.Id
                                }).Distinct().ToList();

                if (input.bedroom != null)
                {
                    getUnits = (from a in getUnits
                                where a.bedroom == input.bedroom
                                select a).Distinct().ToList();
                }

                if (input.unitType != null)
                {
                    getUnits = (from a in getUnits
                                where a.unitType.Contains(input.unitType)
                                select a).Distinct().ToList();
                }
                if (input.zoningID != 0)
                {
                    getUnits = (from a in getUnits
                                where a.zoningID == input.zoningID
                                select a).Distinct().ToList();
                }

                item.units = getUnits;

            }
            return getUnit.OrderByDescending(x => x.floor).ToList();

        }

        //
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListRenovation)]
        public List<ListRenovationResultDto> GetListRenovation(int unitID)
        {
            var getRenovation = (from x in _msUnitItemPrice.GetAll()
                                 join z in _msRenovation.GetAll() on x.renovID equals z.Id
                                 join item in _lkItem.GetAll() on new { itemID = x.itemID, renovCode = z.renovationCode } equals new { itemID = item.Id, renovCode = item.itemCode }
                                 where x.unitID == unitID
                                 select new ListRenovationResultDto
                                 {
                                     renovationID = z.Id,
                                     renovationCode = z.renovationCode,
                                     renovationName = item.shortName
                                 }).Distinct().ToList();

            return new List<ListRenovationResultDto>(getRenovation);
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListTerm)]
        public List<ListTermResultDto> GetListTerm(int unitID)
        {
            var getTerm = (from x in _msUnit.GetAll()
                           join y in _msTermMainRepo.GetAll() on x.termMainID equals y.Id into termMain
                           from y in termMain.DefaultIfEmpty()
                           join z in _msTermRepo.GetAll() on y.Id equals z.termMainID into term
                           from z in term.DefaultIfEmpty()
                           where x.Id == unitID
                           select new ListTermResultDto
                           {
                               termID = z.Id,
                               termMainID = x.termMainID,
                               termName = z.remarks
                           }).Distinct().ToList();

            return new List<ListTermResultDto>(getTerm);
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListTerm)]
        public GrossPriceDto GetGrossPrice(GetGrossPriceInputDto input)
        {
            //get gross price 
            var getGrossPrice = (from unitItemPrice in _msUnitItemPrice.GetAll()
                                 join unit in _msUnit.GetAll()
                                 on unitItemPrice.unitID equals unit.Id
                                 join unitCode in _msUnitCode.GetAll()
                                 on unit.unitCodeID equals unitCode.Id
                                 join termMain in _msTermMainRepo.GetAll()
                                 on unit.termMainID equals termMain.Id
                                 join term in _msTermRepo.GetAll()
                                 on unitItemPrice.termID equals term.Id
                                 where unit.Id == input.unitID
                                 && unitItemPrice.renovID == input.renovID
                                 && term.Id == input.termID
                                 group unitItemPrice by new
                                 {
                                     term.termCode,
                                     term.termNo,
                                     unitCode.unitCode,
                                     unit.unitCodeID,
                                     unit.unitNo,
                                     term.remarks,
                                     termID = term.Id,
                                     termMain.BFAmount
                                 } into G
                                 select new
                                 {
                                     sellingPrice = G.Sum(x => x.grossPrice),
                                     G.Key.termCode,
                                     G.Key.termNo,
                                     G.Key.unitCode,
                                     G.Key.unitCodeID,
                                     G.Key.unitNo,
                                     G.Key.remarks,
                                     G.Key.termID,
                                     G.Key.BFAmount
                                 }).FirstOrDefault();

            //get discount
            var getDisc = (from a in _msTermAddDiscRepo.GetAll()
                           where a.termID == getGrossPrice.termID
                           select a.addDiscPct).FirstOrDefault();

            var grossPrice = getGrossPrice.sellingPrice - (getGrossPrice.sellingPrice * Convert.ToDecimal(getDisc));

            var resultGross = Math.Round(grossPrice + (grossPrice * Convert.ToDecimal(0.1)));

            var result = new GrossPriceDto
            {
                grossPrice = resultGross,
                bookingFee = getGrossPrice.BFAmount
            };

            return result;
        }

        //private class CustomGetDiagramaticForWebListDto
        //{
        //    public int unitID { get; set; }
        //    public int unitCodeId { get; set; }
        //    public string unitCode { get; set; }
        //    public string floor { get; set; }
        //    public string unitNo { get; set; }
        //    public string unitStatusCode { get; set; }
        //    public string unitStatusName { get; set; }
        //    public int? bedroom { get; set; }
        //    public string zoningName { get; set; }
        //}

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListDiagramaticWeb)]
        public List<GetDiagramaticForWebListDto> GetListDiagramaticWeb(DiagramaticInputDto input)
        {
            List<GetDiagramaticForWebListDto> getUnit = new List<GetDiagramaticForWebListDto>();
            #region old

            getUnit = (from x in _contextProp.MS_Unit
                       join y in _contextProp.MS_UnitItemPrice on x.Id equals y.unitID
                       join z in _contextProp.LK_UnitStatus on x.unitStatusID equals z.Id
                       where x.projectID == input.projectId && x.clusterID == input.clusterId
                       //&& (x.categoryCode == "LAN" || x.categoryCode == "KDM")
                       && (z.unitStatusCode == "A" || z.unitStatusCode == "Z")
                       select new GetDiagramaticForWebListDto
                       {
                           unitCodeId = x.unitCodeID
                       }).Distinct().ToList();

            //SendConsole("getUnit:" + JsonConvert.SerializeObject(getUnit));

            if (getUnit.Count == 0)
            {
                var error = new List<GetDiagramaticForWebListDto>();

                var message = new GetDiagramaticForWebListDto
                {
                    message = "projectId/clusterId Tidak Valid"
                };

                error.Add(message);

                //throw new UserFriendlyException("projectId/clusterId Tidak Valid");

                return new List<GetDiagramaticForWebListDto>(error);
            }

            foreach (var item in getUnit)
            {
                var getfloor = (from x in _contextProp.MS_UnitCode
                                where x.Id == item.unitCodeId
                                orderby x.unitCode.Substring(x.unitCode.Length - 2) descending
                                select x).FirstOrDefault();

                item.floor = getfloor.unitCode.Substring(getfloor.unitCode.Length - 2);
                item.unitCode = getfloor.unitCode;

                var getUnits = (from x in _contextProp.MS_Unit
                                join y in _contextProp.MS_UnitItemPrice on x.Id equals y.unitID
                                join z in _contextProp.LK_UnitStatus on x.unitStatusID equals z.Id
                                join a in _contextProp.MS_UnitItem on x.Id equals a.unitID into unitItem
                                from a in unitItem.DefaultIfEmpty()
                                join b in _contextProp.MS_UnitRoom on a.Id equals b.unitItemID into unitRoom
                                from b in unitRoom.DefaultIfEmpty()
                                join c in _contextProp.MS_Zoning on x.zoningID equals c.Id
                                join d in _contextProp.MS_Detail on x.detailID equals d.Id
                                where x.projectID == input.projectId && x.clusterID == input.clusterId
                                && x.unitCodeID == item.unitCodeId
                                && (z.unitStatusCode == "A" || z.unitStatusCode == "Z")
                                orderby x.Id ascending
                                select new UnitsDto
                                {
                                    unitID = x.Id,
                                    unitNo = x.unitNo,
                                    unitStatusCode = z.unitStatusCode,
                                    unitStatusName = z.unitStatusName,
                                    bedroom = b.bedroom,
                                    zoningName = c.zoningName,
                                    detailID = x.detailID,
                                    detailCode = d.detailCode,
                                    detailImage = d.detailImage
                                }).Distinct().ToList();

                if(input.detailID != null && input.detailID != 0)
                {
                    getUnits = (from a in getUnits
                                join b in _contextProp.MS_Detail on a.detailID equals b.Id
                                where a.detailID == input.detailID
                                select a).Distinct().ToList();
                }

                item.units = getUnits;
            }
            #endregion
            #region new
            //var getAllData = (from mu in _contextProp.MS_Unit
            //                  join mui in _contextProp.MS_UnitItem on mu.Id equals mui.unitID
            //                  //into unitItem from mui_ in unitItem.DefaultIfEmpty()
            //                  join mur in _contextProp.MS_UnitRoom on mui.Id equals mur.unitItemID 
            //                  into unitRoom from mur_ in unitRoom.DefaultIfEmpty()
            //                  join lus in _contextProp.LK_UnitStatus on mu.unitStatusID equals lus.Id
            //                  join mz in _contextProp.MS_Zoning on mu.zoningID equals mz.Id
            //                  join muc in _contextProp.MS_UnitCode on mu.unitCodeID equals muc.Id
            //                  where mu.projectID == input.projectId && mu.clusterID == input.clusterId
            //                  && (lus.unitStatusCode == "A" || lus.unitStatusCode == "Z")
            //                  orderby mu.Id ascending
            //                  select new CustomGetDiagramaticForWebListDto
            //                  {
            //                      unitID = mu.Id,
            //                      unitCodeId = mu.unitCodeID,
            //                      unitCode = muc.unitCode,
            //                      floor = muc.unitCode.Substring(muc.unitCode.Length - 2),
            //                      unitNo = mu.unitNo,
            //                      unitStatusCode = lus.unitStatusCode,
            //                      unitStatusName = lus.unitStatusName,
            //                      bedroom = mur_.bedroom,
            //                      zoningName = mz.zoningName
            //                  }).GroupBy(u => u.unitCodeId)
            //                      .Select(group => new GetDiagramaticForWebListDto
            //                      {
            //                          unitID = group.First().unitID,
            //                          unitCodeId = group.First().unitCodeId,
            //                          unitCode = group.First().unitCode,
            //                          floor = group.First().floor,
            //                          units = group.Select(item => new UnitsDto
            //                          {
            //                              unitID = item.unitID,
            //                              unitNo = item.unitNo,
            //                              unitStatusCode = item.unitStatusCode,
            //                              unitStatusName = item.unitStatusName,
            //                              bedroom = item.bedroom,
            //                              zoningName = item.zoningName
            //                          }).ToList()
            //                      })
            //                    .ToList();

            if (getUnit == null)
            {
                var error = new List<GetDiagramaticForWebListDto>();
                var message = new GetDiagramaticForWebListDto
                {
                    message = "projectId/clusterId Tidak Valid"
                };
                error.Add(message);
                return new List<GetDiagramaticForWebListDto>(error);
            }
            //getUnit.AddRange(getAllData);

            #endregion

            return getUnit.OrderByDescending(x => x.floor).ToList();
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetUnitSelectionDetail)]
        public GetUnitSelectionDetailDto GetUnitSelectionDetail(int unitID)
        {
            var getUnit = (from a in _msUnit.GetAll()
                           join b in _msProject.GetAll() on a.projectID equals b.Id
                           join c in _msCluster.GetAll() on a.clusterID equals c.Id
                           join d in _msUnitCode.GetAll() on a.unitCodeID equals d.Id
                           where a.Id == unitID
                           select new GetUnitSelectionDetailDto
                           {
                               projectName = b.projectName,
                               tower = c.clusterName,
                               unitNo = a.unitNo,
                               unitCode = d.unitCode
                           }).FirstOrDefault();

            return getUnit;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetUnitSelectionDetailMobile)]
        public GetUnitSelectionDetailDto GetUnitSelectionDetailMobile(int unitID)
        {
            var getUnit = (from a in _msUnit.GetAll()
                           join b in _msProject.GetAll() on a.projectID equals b.Id
                           join c in _msCluster.GetAll() on a.clusterID equals c.Id
                           join d in _msUnitCode.GetAll() on a.unitCodeID equals d.Id
                           join e in _msTermMainRepo.GetAll() on a.termMainID equals e.Id
                           where a.Id == unitID
                           select new GetUnitSelectionDetailDto
                           {
                               projectName = b.projectName,
                               tower = c.clusterName,
                               unitNo = a.unitNo,
                               unitCode = d.unitCode,
                               bfAmount = e.BFAmount
                           }).FirstOrDefault();

            return getUnit;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetTypeDiagramatic)]
        public List<TypeDiagramaticResultDto> GetTypeDiagramatic(DiagramaticInputDto input)
        {
            var getType = (from a in _msUnit.GetAll()
                           where a.projectID == input.projectId
                           && a.clusterID == input.clusterId
                           select new TypeDiagramaticResultDto
                           {
                               type = a.unitNo.Substring(2, a.unitNo.Length)
                           }).Distinct().ToList();

            return getType;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListBedroom)]
        public List<ListBedroomResultDto> GetListBedroom(int projectID, int clusterID)
        {
            var getBedroom = (from a in _msUnitRoomRepo.GetAll()
                              join b in _msUnitItem.GetAll() on a.unitItemID equals b.Id
                              join c in _msUnit.GetAll() on b.unitID equals c.Id
                              join d in _lkItem.GetAll() on b.itemID equals d.Id
                              where c.projectID == projectID && c.clusterID == clusterID && d.itemCode == "02"
                              select new ListBedroomResultDto
                              {
                                  bedroom = a.bedroom
                              }).Distinct().ToList();
            return getBedroom;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListZoning)]
        public List<ListZoningResultDto> GetListZoning(int projectID, int clusterID)
        {
            var getZoning = (from a in _msZoningRepo.GetAll()
                             join b in _msUnit.GetAll() on a.Id equals b.zoningID
                             where b.projectID == projectID && b.clusterID == clusterID
                             select new ListZoningResultDto
                             {
                                 zoningId = a.Id,
                                 zoningCode = a.zoningCode,
                                 zoningName = a.zoningName
                             }).Distinct().ToList();
            return getZoning;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListSumberDana)]
        public List<ListSumberDanaResultDto> GetListSumberDana()
        {
            var getSumberDana = (from a in _msSumberDanaRepo.GetAll()
                                 select new ListSumberDanaResultDto
                                 {
                                     sumberDanaId = a.Id,
                                     sumberDanaCode = a.sumberDanaCode,
                                     sumberDanaName = a.sumberDanaName
                                 }).ToList();

            return getSumberDana;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetListTujuanTransaksi)]
        public List<ListTujuanTransaksiResultDto> GetListTujuanTransaksi()
        {
            var getTujuanTransaksi = (from a in _msTujuanTransaksiRepo.GetAll()
                                      select new ListTujuanTransaksiResultDto
                                      {
                                          tujuanTransaksiId = a.Id,
                                          tujuanTransaksiCode = a.tujuanTransaksiCode,
                                          tujuanTransaksiName = a.tujuanTransaksiName
                                      }).ToList();

            return getTujuanTransaksi;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetDetailDiagramatic)]
        public ListDetailDiagramaticWebResultDto GetDetailDiagramatic(int unitID)
        {
            var getUnit = (from a in _msUnitItemPrice.GetAll()
                           join b in _msUnit.GetAll() on a.unitID equals b.Id
                           join c in _lkUnitStatus.GetAll() on b.unitStatusID equals c.Id
                           join d in _msUnitItem.GetAll() on b.Id equals d.unitID
                           //join e in _msUnitRoomRepo.GetAll() on d.Id equals e.unitItemID
                           join f in _msUnitCode.GetAll() on b.unitCodeID equals f.Id
                           join g in _msTermMainRepo.GetAll() on b.termMainID equals g.Id
                           where a.unitID == unitID
                           select new ListDetailDiagramaticWebResultDto
                           {
                               unitItemID = d.Id,
                               unitID = b.Id,
                               unitNo = b.unitNo,
                               unitCode = f.unitCode,
                               unitStatus = c.unitStatusName,
                               size = d.area,
                               //bedroom = e.bedroom,
                               bookingFee = g.BFAmount
                           }).FirstOrDefault();

            var getUnitRoom = (from a in _msUnitRoomRepo.GetAll()
                               where a.unitItemID == getUnit.unitItemID
                               select a).FirstOrDefault();

            if (getUnitRoom != null)
            {
                getUnit.bedroom = getUnitRoom.bedroom;
            }

            var getTerm = (from a in _msUnitItemPrice.GetAll()
                           join b in _msTermRepo.GetAll() on a.termID equals b.Id
                           join c in _msRenovation.GetAll() on a.renovID equals c.Id
                           join d in _lkItem.GetAll() on c.renovationCode equals d.itemCode
                           where a.unitID == unitID
                           group a by new
                           {
                               termID = b.Id,
                               b.remarks,
                               renovID = c.Id,
                               d.shortName
                           } into G
                           select new ListTerm
                           {
                               termID = G.Key.termID,
                               termName = G.Key.remarks,
                               renovID = G.Key.renovID,
                               renovName = G.Key.shortName,
                               price = G.Sum(x => x.grossPrice)
                           }).Distinct().ToList();

            var getPriceArea = (from unititemprice in _msUnitItemPrice.GetAll()
                                join unititem in _msUnitItem.GetAll()
                                on new { unititemprice.unitID, unititemprice.itemID } equals new { unititem.unitID, unititem.itemID }
                                join term in _msTermRepo.GetAll()
                                on unititemprice.termID equals term.Id
                                join item in _lkItem.GetAll()
                                on unititemprice.itemID equals item.Id
                                where unititemprice.unitID == unitID &&
                                item.itemCode == "02" && term.termNo == 3
                                select new { unititemprice.grossPrice, unititem.area }).FirstOrDefault();

            var priceArea = Math.Round(getPriceArea.grossPrice / (decimal)getPriceArea.area);

            var result = new ListDetailDiagramaticWebResultDto
            {
                unitNo = getUnit == null ? null : getUnit.unitNo,
                unitCode = getUnit == null ? null : getUnit.unitCode,
                unitStatus = getUnit == null ? null : getUnit.unitStatus,
                size = getUnit == null ? 0 : getUnit.size,
                bedroom = getUnit == null ? null : getUnit.bedroom,
                bookingFee = getUnit == null ? 0 : getUnit.bookingFee,
                sellingPrice = 0,
                term = getTerm,
                pricePerArea = priceArea
            };

            return result;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Diagramatic_GetPaymentType)]
        public List<ListPaymentTypeResultDto> GetPaymentType()
        {
            var getPaymentType = (from a in _lkPaymentType.GetAll()
                                  where new[] { 4, 3, 10, 11 }.Contains(a.paymentType)
                                  select new ListPaymentTypeResultDto
                                  {
                                      paymentTypeID = a.Id,
                                      paymentType = a.paymentType,
                                      paymentTypeName = a.paymentTypeName
                                  }).ToList();

            return getPaymentType;
        }
    }
}