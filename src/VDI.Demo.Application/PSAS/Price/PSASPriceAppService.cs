using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PSAS.Dto;
using System.Linq;
using VDI.Demo.PSAS.Price.Dto;
using Abp.UI;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using System.Threading.Tasks;
using Abp.AutoMapper;
using System.Data;
using VDI.Demo.PSAS.Term.Dto;
using System.Diagnostics;
using Newtonsoft.Json;
using Visionet_Backend_NetCore.Komunikasi;
using VDI.Demo.EntityFrameworkCore;

namespace VDI.Demo.PSAS.Price
{
    public class PSASPriceAppService : DemoAppServiceBase, IPSASPriceAppService
    {
        private readonly IRepository<TR_BookingHeader> _trBookingHeaderRepo;
        private readonly IRepository<TR_BookingDetail> _trBookingDetailRepo;
        private readonly IRepository<TR_BookingDetailAddDisc> _trBookingDetailAddDiscRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<TR_BookingItemPrice> _trBookingItemPriceRepo;
        private readonly IRepository<TR_BookingSalesDisc> _trBookingSalesDiscRepo;
        private readonly PropertySystemDbContext _contextProp;
        private readonly IRepository<TR_BookingDetailAddDiscHistory> _trBookingDetailAddDiscHistory;
        private readonly IRepository<TR_MKTAddDisc> _trMktAddDiscRepo;
        private readonly IRepository<TR_CommAddDisc> _trCommAddDiscRepo;
        private readonly IRepository<TR_MKTAddDiscHistory> _trMktAddDiscHistoryRepo;
        private readonly IRepository<TR_CommAddDiscHistory> _trCommAddDiscHistoryRepo;

        public PSASPriceAppService(
            IRepository<TR_BookingHeader> trBookingHeaderRepo,
            IRepository<TR_BookingDetail> trBookingDetailRepo,
            IRepository<TR_BookingDetailAddDisc> trBookingDetailAddDiscRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<MS_UnitCode> msUnitCodeRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<TR_BookingItemPrice> trBookingItemPriceRepo,
            IRepository<TR_BookingSalesDisc> trBookingSalesDiscRepo,
            PropertySystemDbContext contextProp,
            IRepository<TR_BookingDetailAddDiscHistory> trBookingDetailAddDiscHistoryRepo,
            IRepository<TR_MKTAddDisc> trMktAddDiscRepo,
            IRepository<TR_CommAddDisc> trCommAddDiscRepo,
            IRepository<TR_MKTAddDiscHistory> trMktAddDiscHistoryRepo,
            IRepository<TR_CommAddDiscHistory> trCommAddDiscHistoryyRepo
            )
        {
            _trBookingHeaderRepo = trBookingHeaderRepo;
            _trBookingDetailRepo = trBookingDetailRepo;
            _msUnitRepo = msUnitRepo;
            _msUnitCodeRepo = msUnitCodeRepo;
            _trBookingDetailAddDiscRepo = trBookingDetailAddDiscRepo;
            _msProjectRepo = msProjectRepo;
            _trBookingItemPriceRepo = trBookingItemPriceRepo;
            _trBookingSalesDiscRepo = trBookingSalesDiscRepo;
            _contextProp = contextProp;
            _trBookingDetailAddDiscHistory = trBookingDetailAddDiscHistoryRepo;
            _trMktAddDiscRepo = trMktAddDiscRepo;
            _trCommAddDiscRepo = trCommAddDiscRepo;
            _trMktAddDiscHistoryRepo = trMktAddDiscHistoryRepo;
            _trCommAddDiscHistoryRepo = trCommAddDiscHistoryyRepo;
        }

        public void CreateUpdateDiscountPrice(CreateUpdatePriceParamsDto input)
        {
            Logger.Info("CreateUpdateDiscountPrice() - Started.");
            var unitID = GetParameter(input.paramsCheck);

            Logger.DebugFormat("CreateUpdateDiscountPrice() - Start get data Booking Detail. Parameters sent:{0}" +
                        "bookingHeaderID = {1}{0}" +
                        "unitID = {2}{0}"
                        , Environment.NewLine, unitID.bookingHeaderID, unitID.unitID);
            //REPAIR THIS
            var getbookingDetail = (from x in _trBookingDetailRepo.GetAll()
                                    join c in _trBookingHeaderRepo.GetAll()
                                    on x.bookingHeaderID equals c.Id
                                    where c.Id == unitID.bookingHeaderID
                                    && c.unitID == unitID.unitID
                                    select x).ToList();

            Logger.DebugFormat("CreateUpdateDiscountPrice() - Ended get data Booking Detail.");

            foreach (var item in input.DiscountList)
            {
                Logger.DebugFormat("CreateUpdateDiscountPrice() - Start checking before update TR Booking Detail Add Disc. Parameters sent:{0}" +
                            "discNo = {1}{0}"
                            , Environment.NewLine, item.discNo);

                var checkExists = (from a in _trBookingDetailAddDiscRepo.GetAll()
                                   join x in getbookingDetail
                                   on a.bookingDetailID equals x.Id
                                   where item.discNo == a.addDiscNo
                                   select a);

                Logger.DebugFormat("CreateUpdateDiscountPrice() - Ended checking before insert Account. Result = {0}", checkExists.Count());

                if (checkExists.Any())
                {
                    foreach (var disc in checkExists.ToList())
                    {
                        Logger.DebugFormat("CreateUpdateDiscountPrice() - Start checking table history before insert TR Booking Detail Add Disc History. Parameters sent:{0}" +
                            "bookingDetailID = {1}{0}"
                            , Environment.NewLine, disc.bookingDetailID);

                        var checkHistory = (from A in _trBookingDetailAddDiscHistory.GetAll()
                                            orderby A.Id descending
                                            where A.bookingDetailID == disc.bookingDetailID
                                            select A).FirstOrDefault();

                        //insert into tb history
                        var dataToInsertHistory = new TR_BookingDetailAddDiscHistory
                        {
                            addDiscDesc = disc.addDiscDesc,
                            addDiscNo = disc.addDiscNo,
                            amtAddDisc = disc.amtAddDisc,
                            isAmount = disc.isAmount,
                            pctAddDisc = disc.pctAddDisc,
                            bookingDetailID = disc.bookingDetailID,
                            entityID = disc.entityID,
                            historyNo = checkHistory == null ? Convert.ToByte(0) : Convert.ToByte(checkHistory.historyNo + 1)
                        };

                        var update = disc.MapTo<TR_BookingDetailAddDisc>();

                        update.pctAddDisc = input.isAmount == true ? 0 : item.pctDisc;
                        update.amtAddDisc = input.isAmount == true ? item.amountDisc : 0;
                        update.isAmount = input.isAmount;
                        try
                        {
                            _trBookingDetailAddDiscHistory.Insert(dataToInsertHistory);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("CreateUpdateDiscountPrice() - Start update TR Booking Detail Add Disc. Parameters sent:{0}" +
                                "pctAddDisc = {1}{0}" +
                                "amtAddDisc = {2}{0}" +
                                "isAmount = {3}{0}"
                                , Environment.NewLine, item.pctDisc, item.amountDisc, input.isAmount);

                            _trBookingDetailAddDiscRepo.Update(update);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("CreateUpdateDiscountPrice() - Ended update TR Booking Detail Add Disc.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPrice() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPrice() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }

                    }
                }
                else
                {
                    foreach (var detail in getbookingDetail)
                    {
                        var insert = new TR_BookingDetailAddDisc
                        {
                            addDiscDesc = item.addDiscDesc,
                            addDiscNo = item.discNo,
                            amtAddDisc = input.isAmount == true ? item.amountDisc : 0,
                            isAmount = input.isAmount,
                            pctAddDisc = input.isAmount == true ? 0 : item.pctDisc,
                            bookingDetailID = detail.Id,
                            entityID = 1
                        };

                        try
                        {
                            Logger.DebugFormat("CreateUpdateDiscountPrice() - Start insert TR Booking Detail Add Disc. Parameters sent:{0}" +
                                "entityID = {1}{0}" +
                                "addDiscDesc = {2}{0}" +
                                "addDiscNo = {3}{0}" +
                                "amtAddDisc = {4}{0}" +
                                "isAmount = {5}{0}" +
                                "pctAddDisc = {6}{0}" +
                                "bookingDetailID = {7}{0}"
                                , Environment.NewLine, 1, item.addDiscDesc, item.discNo, item.amountDisc, input.isAmount, item.pctDisc, detail.Id);

                            _trBookingDetailAddDiscRepo.Insert(insert);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("CreateUpdateDiscountPrice() - Ended insert TR Booking Detail Add Disc.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPrice() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPrice() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                }
            }
            Logger.Info("CreateUpdateDiscountPrice() - Finished.");
        }

        public GetPSASPriceListDto GetCommisionPrice(GetPSASParamsDto input)
        {
            var unitID = GetParameter(input);

            var projectCode = (from a in _msUnitRepo.GetAll()
                               join b in _msProjectRepo.GetAll() on a.projectID equals b.Id
                               select b.projectCode).FirstOrDefault();

            var getArea = (from a in _trBookingDetailRepo.GetAll()
                           where a.bookingHeaderID == unitID.bookingHeaderID
                           group a by a.bookingHeaderID into G
                           select new GetAreaLlistDto
                           {
                               bangunan = G.Sum(d => d.area),
                               total = G.Sum(d => d.area)
                           }).FirstOrDefault();

            var getGrossPrice = (from a in _trBookingDetailRepo.GetAll()
                                 where a.bookingHeaderID == unitID.bookingHeaderID
                                 group a by a.bookingHeaderID into G
                                 select new GetGrosspriceLlistDto
                                 {
                                     bangunan = Math.Round(G.Sum(d => d.amountComm)),
                                     total = Math.Round(G.Sum(d => d.amountComm))
                                 }).FirstOrDefault();

            var getDiscount = (from a in _trBookingDetailRepo.GetAll()
                               join b in _trBookingHeaderRepo.GetAll()
                               on a.bookingHeaderID equals b.Id
                               where b.unitID == unitID.unitID
                               && a.bookingHeaderID == unitID.bookingHeaderID
                               group a by new
                               {
                                   b.unitID,
                                   //a.amountComm,
                                   a.pctDisc
                               } into G
                               select new GetDiscountLlistDto
                               {
                                   discount = G.Key.pctDisc,
                                   bangunan = Math.Round(G.Sum(d => (double)d.amountComm * d.pctDisc)),
                                   total = Math.Round(G.Sum(d => (double)d.amountComm * d.pctDisc))
                               }).FirstOrDefault();

            var getNetPrice = new GetNetpriceLlistDto
            {
                bangunan = Convert.ToDouble(getGrossPrice.bangunan) - Convert.ToDouble(getDiscount.bangunan),
                total = Convert.ToDouble(getGrossPrice.total) - Convert.ToDouble(getDiscount.total),
            };

            //ToDo: fixing addDisc
            var getDiscountA = (from a in _trCommAddDiscRepo.GetAll()
                                join b in _trBookingDetailRepo.GetAll()
                                on a.bookingDetailID equals b.Id
                                join c in _trBookingHeaderRepo.GetAll()
                                on b.bookingHeaderID equals c.Id
                                orderby a.addDiscNo
                                where c.Id == unitID.bookingHeaderID
                                && c.unitID == unitID.unitID
                                && b.itemID == 2
                                select new 
                                {
                                    bookingDetailID = b.Id,
                                    bookingDetailAddDiscID = a.Id,
                                    discountName = a.addDiscDesc,
                                    discount = a.isAmount == true ? (double)a.amtAddDisc : a.pctAddDisc,
                                    isAmount = a.isAmount,
                                    bangunan = a.isAmount == true ? (double)a.amtAddDisc : Math.Round(a.pctAddDisc * (double)getNetPrice.bangunan),
                                    total = a.isAmount == true ? (double)a.amtAddDisc : Math.Round(a.pctAddDisc * (double)getNetPrice.bangunan)
                                }).ToList();

            double dataNetPrice = (double)getNetPrice.bangunan;
            double netPriceDisc = 0;

            List<GetDiscountAlistDto> discDto = new List<GetDiscountAlistDto>();
            foreach (var dataDisc in getDiscountA)
            {
                var discountAdd = dataDisc.isAmount == true ? (double)dataDisc.discount : Math.Round(dataDisc.discount * dataNetPrice);
                dataNetPrice = dataNetPrice - discountAdd;
                netPriceDisc = netPriceDisc + discountAdd;
                discDto.Add(new GetDiscountAlistDto
                {
                    bookingDetailID = dataDisc.bookingDetailID,
                    bookingDetailAddDiscID = dataDisc.bookingDetailAddDiscID,
                    discountName = dataDisc.discountName,
                    discount = dataDisc.discount,
                    isAmount = dataDisc.isAmount,
                    bangunan = discountAdd,
                    total = discountAdd
                });
            }

            var getAddDisc = new GetAddDiscLlistDto
            {
                bangunan = Math.Round(netPriceDisc),
                total = Math.Round(netPriceDisc)
            };

            var getNetNetPrice = new GetNetNetPriceLlistDto
            {
                bangunan = Math.Round(Convert.ToDouble(getNetPrice.bangunan) - Convert.ToDouble(getAddDisc.bangunan)),
                total = Math.Round(Convert.ToDouble(getNetPrice.total) - Convert.ToDouble(getAddDisc.total))
            };

            var getVAT = new GetVATLlistDto
            {
                discount = 0.1,
                bangunan = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) * 0.1),
                total = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) * 0.1)
            };

            var checkInterest = (from a in _msUnitRepo.GetAll()
                                 join b in _msProjectRepo.GetAll()
                                 on a.projectID equals b.Id
                                 where a.Id == unitID.unitID
                                 && b.projectCode == "SDH"
                                 select a).Any();

            var getInterest = new GetInterestLlistDto();

            if (checkInterest)
            {
                getInterest = new GetInterestLlistDto
                {
                    bangunan = 0,
                    total = 0
                };
            }
            else
            {
                //Nothing
            }

            var getTotal = new GetTotalLlistDto
            {
                bangunan = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) + Convert.ToDouble(getVAT.bangunan) + Convert.ToDouble(getInterest.bangunan)),
                total = Math.Round(Convert.ToDouble(getNetNetPrice.total) + Convert.ToDouble(getVAT.total) + Convert.ToDouble(getInterest.total))
            };

            var result = new GetPSASPriceListDto
            {
                bookingHeaderID = unitID.bookingHeaderID,
                projectCode = projectCode,
                area = getArea,
                grossPrice = getGrossPrice,
                discount = getDiscount,
                netPrice = getNetPrice,
                addDisc = getAddDisc,
                discountA = discDto,
                netNetPrice = getNetNetPrice,
                VATPrice = getVAT,
                interest = getInterest,
                total = getTotal
            };

            return result;
        }


        public GetPSASPriceListDto GetMarketingPrice(GetPSASParamsDto input)
        {
            var unitID = GetParameter(input);

            var projectCode = (from a in _msUnitRepo.GetAll()
                               join b in _msProjectRepo.GetAll() on a.projectID equals b.Id
                               select b.projectCode).FirstOrDefault();

            var getArea = (from a in _trBookingDetailRepo.GetAll()
                           where a.bookingHeaderID == unitID.bookingHeaderID
                           group a by a.bookingHeaderID into G
                           select new GetAreaLlistDto
                           {
                               bangunan = G.Sum(d => d.area),
                               total = G.Sum(d => d.area)
                           }).FirstOrDefault();

            var getGrossPrice = (from a in _trBookingDetailRepo.GetAll()
                                 where a.bookingHeaderID == unitID.bookingHeaderID
                                 group a by a.bookingHeaderID into G
                                 select new GetGrosspriceLlistDto
                                 {
                                     bangunan = Math.Round(G.Sum(d => d.amountMKT)),
                                     total = Math.Round(G.Sum(d => d.amountMKT))
                                 }).FirstOrDefault();

            var getDiscount = (from a in _trBookingDetailRepo.GetAll()
                               join b in _trBookingHeaderRepo.GetAll()
                               on a.bookingHeaderID equals b.Id
                               where b.unitID == unitID.unitID
                               && a.bookingHeaderID == unitID.bookingHeaderID
                               group a by new
                               {
                                   b.unitID,
                                   //a.amountMKT,
                                   a.pctDisc
                               } into G
                               select new GetDiscountLlistDto
                               {
                                   discount = G.Key.pctDisc,
                                   bangunan = Math.Round(G.Sum(d => (double)d.amountMKT * d.pctDisc)),
                                   total = Math.Round(G.Sum(d => (double)d.amountMKT * d.pctDisc))
                               }).FirstOrDefault();

            var getNetPrice = new GetNetpriceLlistDto
            {
                bangunan = Convert.ToDouble(getGrossPrice.bangunan) - Convert.ToDouble(getDiscount.bangunan),
                total = Convert.ToDouble(getGrossPrice.total) - Convert.ToDouble(getDiscount.total),
            };

            //ToDo: fixing addDisc
            var getDiscountA = (from a in _trMktAddDiscRepo.GetAll()
                                join b in _trBookingDetailRepo.GetAll()
                                on a.bookingDetailID equals b.Id
                                join c in _trBookingHeaderRepo.GetAll()
                                on b.bookingHeaderID equals c.Id
                                orderby a.addDiscNo
                                where c.Id == unitID.bookingHeaderID
                                && c.unitID == unitID.unitID
                                && b.itemID == 2
                                select new 
                                {
                                    bookingDetailID = b.Id,
                                    bookingDetailAddDiscID = a.Id,
                                    discountName = a.addDiscDesc,
                                    discount = a.isAmount == true ? (double)a.amtAddDisc : a.pctAddDisc,
                                    isAmount = a.isAmount,
                                    bangunan = a.isAmount == true ? (double)a.amtAddDisc : Math.Round(a.pctAddDisc * (double)getNetPrice.bangunan),
                                    total = a.isAmount == true ? (double)a.amtAddDisc : Math.Round(a.pctAddDisc * (double)getNetPrice.bangunan)
                                }).ToList();

            double dataNetPrice = (double)getNetPrice.bangunan;
            double netPriceDisc = 0;

            List<GetDiscountAlistDto> discDto = new List<GetDiscountAlistDto>();
            foreach (var dataDisc in getDiscountA)
            {
                var discountAdd = dataDisc.isAmount == true ? (double)dataDisc.discount : Math.Round(dataDisc.discount * dataNetPrice);
                dataNetPrice = dataNetPrice - discountAdd;
                netPriceDisc = netPriceDisc + discountAdd;
                discDto.Add(new GetDiscountAlistDto
                {
                    bookingDetailID = dataDisc.bookingDetailID,
                    bookingDetailAddDiscID = dataDisc.bookingDetailAddDiscID,
                    discountName = dataDisc.discountName,
                    discount = dataDisc.discount,
                    isAmount = dataDisc.isAmount,
                    bangunan = discountAdd,
                    total = discountAdd
                });
            }

            var getAddDisc = new GetAddDiscLlistDto
            {
                bangunan = Math.Round(netPriceDisc),
                total = Math.Round(netPriceDisc)
            };

            var getNetNetPrice = new GetNetNetPriceLlistDto
            {
                bangunan = Math.Round(Convert.ToDouble(getNetPrice.bangunan) - Convert.ToDouble(getAddDisc.bangunan)),
                total = Math.Round(Convert.ToDouble(getNetPrice.total) - Convert.ToDouble(getAddDisc.total))
            };

            var getVAT = new GetVATLlistDto
            {
                discount = 0.1,
                bangunan = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) * 0.1),
                total = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) * 0.1)
            };

            var checkInterest = (from a in _msUnitRepo.GetAll()
                                 join b in _msProjectRepo.GetAll()
                                 on a.projectID equals b.Id
                                 where a.Id == unitID.unitID
                                 && b.projectCode == "SDH"
                                 select a).Any();

            var getInterest = new GetInterestLlistDto();

            if (checkInterest)
            {
                getInterest = new GetInterestLlistDto
                {
                    bangunan = 0,
                    total = 0
                };
            }
            else
            {
                //Nothing
            }

            var getTotal = new GetTotalLlistDto
            {
                bangunan = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) + Convert.ToDouble(getVAT.bangunan) + Convert.ToDouble(getInterest.bangunan)),
                total = Math.Round(Convert.ToDouble(getNetNetPrice.total) + Convert.ToDouble(getVAT.total) + Convert.ToDouble(getInterest.total))
            };

            var result = new GetPSASPriceListDto
            {
                bookingHeaderID = unitID.bookingHeaderID,
                projectCode = projectCode,
                area = getArea,
                grossPrice = getGrossPrice,
                discount = getDiscount,
                netPrice = getNetPrice,
                addDisc = getAddDisc,
                discountA = discDto,
                netNetPrice = getNetNetPrice,
                VATPrice = getVAT,
                interest = getInterest,
                total = getTotal
            };

            return result;
        }


        public GetPSASResultDto GetParameter(GetPSASParamsDto input)
        {
            GetPSASResultDto getunitID;

            if (input.bookCode != null && input.unitNo == null && input.unitCode == null)
            {
                getunitID = (from a in _trBookingHeaderRepo.GetAll()
                             where a.bookCode == input.bookCode
                             orderby a.CreationTime descending
                             select new GetPSASResultDto
                             {
                                 unitID = a.unitID,
                                 bookingHeaderID = a.Id
                             }).FirstOrDefault();

                if (getunitID == null)
                {
                    throw new UserFriendlyException("bookCode '" + input.bookCode + "' not exists");
                }
            }
            else if (input.unitNo != null && input.unitCode != null && input.bookCode == null)
            {
                getunitID = (from a in _msUnitRepo.GetAll()
                             join b in _msUnitCodeRepo.GetAll()
                             on a.unitCodeID equals b.Id
                             join c in _trBookingHeaderRepo.GetAll()
                             on a.Id equals c.unitID
                             where a.unitNo == input.unitNo
                             && b.unitCode == input.unitCode
                             orderby a.CreationTime descending
                             select new GetPSASResultDto
                             {
                                 unitID = a.Id,
                                 bookingHeaderID = c.Id
                             }).FirstOrDefault();

                if (getunitID == null)
                {
                    throw new UserFriendlyException("unitCode '" + input.unitCode + "', unitNo '" + input.unitNo + "' not exists");
                }
            }
            else
            {
                throw new UserFriendlyException("Invalid Parameter");
            }
            return getunitID;
        }

        public GetPSASPriceListDto GetPSASPrice(GetPSASParamsDto input)
        {
            var unitID = GetParameter(input);

            var projectCode = (from a in _msUnitRepo.GetAll()
                               join b in _msProjectRepo.GetAll() on a.projectID equals b.Id
                               select b.projectCode).FirstOrDefault();

            var getArea = (from a in _trBookingDetailRepo.GetAll()
                           where a.bookingHeaderID == unitID.bookingHeaderID
                           group a by a.bookingHeaderID into G
                           select new GetAreaLlistDto
                           {
                               bangunan = G.Sum(d => d.area),
                               total = G.Sum(d => d.area)
                           }).FirstOrDefault();

            var getGrossPrice = (from a in _trBookingDetailRepo.GetAll()
                                 where a.bookingHeaderID == unitID.bookingHeaderID
                                 group a by a.bookingHeaderID into G
                                 select new GetGrosspriceLlistDto
                                 {
                                     bangunan = Math.Round(G.Sum(d => d.amount)),
                                     total = Math.Round(G.Sum(d => d.amount))
                                 }).FirstOrDefault();

            var getDiscount = (from a in _trBookingDetailRepo.GetAll()
                               join b in _trBookingHeaderRepo.GetAll()
                               on a.bookingHeaderID equals b.Id
                               where b.unitID == unitID.unitID
                               && a.bookingHeaderID == unitID.bookingHeaderID
                               group a by new
                               {
                                   b.unitID,
                                   //a.amount,
                                   a.pctDisc
                               } into G
                               select new GetDiscountLlistDto
                               {
                                   discount = G.Key.pctDisc,
                                   bangunan = Math.Round(G.Sum(d => (double)d.amount * d.pctDisc)),
                                   total = Math.Round(G.Sum(d => (double)d.amount * d.pctDisc))
                               }).FirstOrDefault();

            var getNetPrice = new GetNetpriceLlistDto
            {
                bangunan = Convert.ToDouble(getGrossPrice.bangunan) - Convert.ToDouble(getDiscount.bangunan),
                total = Convert.ToDouble(getGrossPrice.total) - Convert.ToDouble(getDiscount.total),
            };

            //ToDo: fixing addDisc
            var getDiscountA = (from a in _trBookingDetailAddDiscRepo.GetAll()
                                join b in _trBookingDetailRepo.GetAll()
                                on a.bookingDetailID equals b.Id
                                join c in _trBookingHeaderRepo.GetAll()
                                on b.bookingHeaderID equals c.Id
                                orderby a.addDiscNo
                                where c.Id == unitID.bookingHeaderID
                                && c.unitID == unitID.unitID
                                && b.itemID == 2
                                select new 
                                {
                                    bookingDetailID = b.Id,
                                    bookingDetailAddDiscID = a.Id,
                                    discountName = a.addDiscDesc,
                                    discount = a.isAmount == true ? (double)a.amtAddDisc : a.pctAddDisc,
                                    isAmount = a.isAmount,
                                    bangunan = a.isAmount == true ? (double)a.amtAddDisc : Math.Round(a.pctAddDisc * (double)getNetPrice.bangunan),
                                    total = a.isAmount == true ? (double)a.amtAddDisc : Math.Round(a.pctAddDisc * (double)getNetPrice.bangunan)
                                }).ToList();

            double dataNetPrice = (double)getNetPrice.bangunan;
            double netPriceDisc = 0;

            List<GetDiscountAlistDto> discDto = new List<GetDiscountAlistDto>();
            foreach (var dataDisc in getDiscountA)
            {
                var discountAdd = dataDisc.isAmount == true ? (double)dataDisc.discount : Math.Round(dataDisc.discount * dataNetPrice);
                dataNetPrice = dataNetPrice - discountAdd;
                netPriceDisc = netPriceDisc + discountAdd;
                discDto.Add(new GetDiscountAlistDto
                {
                    bookingDetailID = dataDisc.bookingDetailID,
                    bookingDetailAddDiscID = dataDisc.bookingDetailAddDiscID,
                    discountName = dataDisc.discountName,
                    discount = dataDisc.discount,
                    isAmount = dataDisc.isAmount,
                    bangunan = discountAdd,
                    total = discountAdd
                });
            }

            var getAddDisc = new GetAddDiscLlistDto
            {
                bangunan = Math.Round(netPriceDisc),
                total = Math.Round(netPriceDisc)
            };

            var getNetNetPrice = new GetNetNetPriceLlistDto
            {
                bangunan = Math.Round(Convert.ToDouble(getNetPrice.bangunan) - Convert.ToDouble(getAddDisc.bangunan)),
                total = Math.Round(Convert.ToDouble(getNetPrice.total) - Convert.ToDouble(getAddDisc.total))
            };

            var getVAT = new GetVATLlistDto
            {
                discount = 0.1,
                bangunan = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) * 0.1),
                total = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) * 0.1)
            };

            var checkInterest = (from a in _msUnitRepo.GetAll()
                                 join b in _msProjectRepo.GetAll()
                                 on a.projectID equals b.Id
                                 where a.Id == unitID.unitID
                                 && b.projectCode == "SDH"
                                 select a).Any();

            var getInterest = new GetInterestLlistDto();

            if (checkInterest)
            {
                getInterest = new GetInterestLlistDto
                {
                    bangunan = 0,
                    total = 0
                };
            }
            else
            {
                //Nothing
            }

            var getTotal = new GetTotalLlistDto
            {
                bangunan = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) + Convert.ToDouble(getVAT.bangunan) + Convert.ToDouble(getInterest.bangunan)),
                total = Math.Round(Convert.ToDouble(getNetNetPrice.total) + Convert.ToDouble(getVAT.total) + Convert.ToDouble(getInterest.total))
            };

            var result = new GetPSASPriceListDto
            {
                bookingHeaderID = unitID.bookingHeaderID,
                projectCode = projectCode,
                area = getArea,
                grossPrice = getGrossPrice,
                discount = getDiscount,
                netPrice = getNetPrice,
                addDisc = getAddDisc,
                discountA = discDto,
                netNetPrice = getNetNetPrice,
                VATPrice = getVAT,
                interest = getInterest,
                total = getTotal
            };

            return result;
        }

        public GetUniversalListDto GetUniversalPrice(GetPSASParamsDto input)
        {
            var getPSASPrice = GetPSASPrice(input);
            var getMarketingPrice = GetMarketingPrice(input);
            var getCommisionPrice = GetCommisionPrice(input);

            var result = new GetUniversalListDto
            {
                PSASPrice = getPSASPrice,
                MarketingPrice = getMarketingPrice,
                CommisionPrice = getCommisionPrice
            };
            return result;
        }

        public List<GetAddDiscListDto> GetDiscountPrice(GetPSASParamsDto input)
        {
            var unitID = GetParameter(input);

            var getSingleDetailID = (from a in _trBookingDetailRepo.GetAll()
                                     join c in _trBookingHeaderRepo.GetAll()
                                     on a.bookingHeaderID equals c.Id
                                     where c.Id == unitID.bookingHeaderID
                                     && c.unitID == unitID.unitID
                                     select a.Id).FirstOrDefault();

            var getDetailAddDisc = (from a in _trBookingDetailAddDiscRepo.GetAll()
                                    where a.bookingDetailID == getSingleDetailID
                                    select new GetAddDiscListDto
                                    {
                                        addDiscDesc = a.addDiscDesc,
                                        addDiscNo = a.addDiscNo,
                                        isAmount = a.isAmount,
                                        pctAddDisc = a.pctAddDisc,
                                        amtAddDisc = a.amtAddDisc
                                    }).ToList();

            return getDetailAddDisc;
        }

        public void UpdateCommisionPrice(UpdatePSASParamsDto input)
        {
            Logger.Info("UpdateCommisionPrice() - Started.");

            var unitID = GetParameter(input.paramsCheck);

            Logger.DebugFormat("UpdateCommisionPrice() - Start get data Booking Detail for update. Parameters sent:{0}" +
                        "bookingHeaderID = {1}{0}"
                        , Environment.NewLine, unitID.bookingHeaderID);

            var getDetail = (from x in _trBookingDetailRepo.GetAll()
                             where x.bookingHeaderID == unitID.bookingHeaderID
                             select x).ToList();

            Logger.DebugFormat("UpdateCommisionPrice() - Ended get data Booking Detail.");

            var getTotal = getDetail.Sum(x => x.amountComm);

            var dataPrice = UpdatePriceUniversal(input);

            foreach (var item in getDetail)
            {
                var percent = getTotal == 0 ? 0 : (item.amountComm / getTotal);

                var getGrossPrice = Math.Round(percent * input.grossPrice);
                var netPrice = Math.Round(percent * (Decimal)dataPrice.netPrice.total);
                var netNetPrice = Math.Round(percent * (Decimal)dataPrice.netNetPrice.total);

                var update = item.MapTo<TR_BookingDetail>();

                update.amountComm = getGrossPrice;
                update.netPriceComm = netPrice;

                try
                {
                    Logger.DebugFormat("UpdateCommisionPrice() - Start update TR Booking Detail. Parameters sent:{0}" +
                        "amountComm = {1}{0}"
                        , Environment.NewLine, getGrossPrice);

                    _trBookingDetailRepo.Update(update);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("UpdateCommisionPrice() - Ended update TR Booking Detail");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateCommisionPrice() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateCommisionPrice() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("UpdateCommisionPrice() - Finished.");
        }

        public void UpdateMarketingPrice(UpdatePSASParamsDto input)
        {
            Logger.Info("UpdateMarketingPrice() - Started.");

            var unitID = GetParameter(input.paramsCheck);

            Logger.DebugFormat("UpdateMarketingPrice() - Start get data Booking Detail for update. Parameters sent:{0}" +
                        "bookingHeaderID = {1}{0}"
                        , Environment.NewLine, unitID.bookingHeaderID);

            var getDetail = (from x in _trBookingDetailRepo.GetAll()
                             where x.bookingHeaderID == unitID.bookingHeaderID
                             select x).ToList();

            Logger.DebugFormat("UpdateMarketingPrice() - Ended get data Booking Detail.");

            var getTotal = getDetail.Sum(x => x.amountMKT);

            var dataPrice = UpdatePriceUniversal(input);

            foreach (var item in getDetail)
            {
                var percent = getTotal == 0 ? 0 : (item.amountMKT / getTotal);

                var getGrossPrice = Math.Round(percent * input.grossPrice);
                var netPrice = Math.Round(percent * (Decimal)dataPrice.netPrice.total);
                var netNetPrice = Math.Round(percent * (Decimal)dataPrice.netNetPrice.total);

                var update = item.MapTo<TR_BookingDetail>();

                update.amountMKT = getGrossPrice;
                update.netPriceMKT = netPrice;

                try
                {
                    Logger.DebugFormat("UpdateMarketingPrice() - Start update TR Booking Detail. Parameters sent:{0}" +
                        "amountMKT = {1}{0}"
                        , Environment.NewLine, getGrossPrice);

                    _trBookingDetailRepo.Update(update);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("UpdateMarketingPrice() - Ended update TR Booking Detail");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateMarketingPrice() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMarketingPrice() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("UpdateMarketingPrice() - Finished.");
        }

        public void UpdatePSASPrice(UpdatePSASParamsDto input)
        {
            Logger.Info("UpdatePSASPrice() - Started.");

            var unitID = GetParameter(input.paramsCheck);

            Logger.DebugFormat("UpdatePSASPrice() - Start get data Booking Detail for update. Parameters sent:{0}" +
                        "bookingHeaderID = {1}{0}"
                        , Environment.NewLine, unitID.bookingHeaderID);

            var getDetail = (from x in _trBookingDetailRepo.GetAll()
                             where x.bookingHeaderID == unitID.bookingHeaderID
                             select x).ToList();

            Logger.DebugFormat("UpdatePSASPrice() - Ended get data Booking Detail.");

            var getTotal = getDetail.Sum(x => x.amount);

            var dataPrice = UpdatePriceUniversal(input);

            foreach (var item in getDetail)
            {
                var percent = getTotal == 0 ? 0 : (item.amount / getTotal);

                var getGrossPrice = Math.Round(percent * input.grossPrice);
                var netPrice = Math.Round(percent * (Decimal)dataPrice.netPrice.total);
                var netNetPrice = Math.Round(percent * (Decimal)dataPrice.netNetPrice.total);

                var update = item.MapTo<TR_BookingDetail>();

                update.amount = getGrossPrice;
                update.netPrice = netPrice;
                update.netNetPrice = netNetPrice;

                try
                {
                    Logger.DebugFormat("UpdatePSASPrice() - Start update TR Booking Detail. Parameters sent:{0}" +
                        "amount = {1}{0}"
                        , Environment.NewLine, getGrossPrice);

                    _trBookingDetailRepo.Update(update);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("UpdatePSASPrice() - Ended update TR Booking Detail");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdatePSASPrice() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdatePSASPrice() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("UpdatePSASPrice() - Finished.");
        }

        public void GeneratePrice(UpdateTermInputDto input)
        {
            #region performance test
            SendConsole("start stopwatch");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            #endregion

            try
            {
                #region old
                /*
                //get gross price
                var grossPrice = (from A in _trBookingHeaderRepo.GetAll()
                                  join B in _trBookingItemPriceRepo.GetAll() on new { A = A.Id, B = A.termID } equals new { A = B.bookingHeaderID, B = (int)B.termID }
                                  where B.termID == input.termID && A.unitID == input.unitID && A.cancelDate != null
                                  group B by new
                                  {
                                      A.Id
                                  } into G
                                  select new
                                  {
                                      bookingHeaderId = G.Key.Id,
                                      grossPrice = G.Sum(x => x.grossPrice)
                                  }).FirstOrDefault();

                //to get pct and booking detailID
                var getData = (from A in _trBookingHeaderRepo.GetAll()
                               join B in _trBookingItemPriceRepo.GetAll() on new { A = A.Id, B = A.termID } equals new { A = B.bookingHeaderID, B = (int)B.termID }
                               join C in _trBookingDetailRepo.GetAll() on new { A = B.itemID, B = A.Id } equals new { A = C.itemID, B = C.bookingHeaderID }
                               where A.Id == grossPrice.bookingHeaderId && B.termID == input.termID
                               select new
                               {
                                   bookingDetailID = C.Id,
                                   grossPricePerItem = B.grossPrice,
                                   itemID = B.termID,
                                   percentage = B.grossPrice / grossPrice.grossPrice
                               });
                
                var percentages = getData.ToList();

                var pctDisc = (from A in _trBookingHeaderRepo.GetAll()
                               join B in _trBookingSalesDiscRepo.GetAll() on A.Id equals B.bookingHeaderID
                               where A.Id == grossPrice.bookingHeaderId
                               select B.pctDisc).FirstOrDefault();

                var disc = grossPrice.grossPrice * (decimal)pctDisc;

                var netPrice = grossPrice.grossPrice - disc;

                var addDisc = (from A in _trBookingDetailAddDiscRepo.GetAll()
                               where A.bookingDetailID == getData.FirstOrDefault().bookingDetailID
                               select new
                               {
                                   pctDisc = A.pctAddDisc,
                                   //disc = A.pctAddDisc * (double)netPrice
                               }).ToList();

                decimal totalAddDisc = 0;
                foreach (var pctAddDisc in addDisc)
                {
                    var addDiscount = (decimal)pctAddDisc.pctDisc * netPrice;

                    totalAddDisc += addDiscount;
                }

                var netNetPrice = netPrice - totalAddDisc;

                //update
                foreach (var percentage in percentages)
                {
                    var dataToUpdate = (from A in _trBookingDetailRepo.GetAll()
                                        join B in _trBookingHeaderRepo.GetAll() on A.bookingHeaderID equals B.Id
                                        where A.bookingHeaderID == grossPrice.bookingHeaderId && A.Id == percentage.bookingDetailID
                                        select A).FirstOrDefault();

                    var update = dataToUpdate.MapTo<TR_BookingDetail>();

                    update.amount = grossPrice.grossPrice * percentage.percentage;
                    update.netPrice = netPrice * percentage.percentage;
                    update.netNetPrice = netNetPrice * percentage.percentage;
                    update.pctDisc = pctDisc;
                }
                */
                #endregion

                #region old sesuai sql
                /*
                var getDataNetPrice = (
                             from bh in _contextProp.TR_BookingHeader
                             join bgross in
                                 (from bh in _contextProp.TR_BookingHeader
                                  join bip in _contextProp.TR_BookingItemPrice on bh.termID equals bip.termID
                                  where bh.unitID == input.unitID && bh.termID == input.termID && bh.Id == bip.bookingHeaderID
                                   && bh.termID == bip.termID
                                  group new { bh, bip } by new { bh.unitID, bip.termID } into grp
                                  select new
                                  {
                                      JmlGrossPrice = (double)grp.Sum(x => x.bip.grossPrice),
                                      grp.Key.unitID,
                                      grp.Key.termID
                                  })
                              on bh.unitID equals bgross.unitID
                             join bip in _contextProp.TR_BookingItemPrice on bh.Id equals bip.bookingHeaderID
                             join bd in _contextProp.TR_BookingDetail on bip.itemID equals bd.itemID
                             join bsd in _contextProp.TR_BookingSalesDisc on bh.Id equals bsd.bookingHeaderID
                             join addDisc in (
                                from bh in _contextProp.TR_BookingHeader
                                join bip in _contextProp.TR_BookingItemPrice on bh.termID equals bip.termID
                                join bd in _contextProp.TR_BookingDetail on bh.Id equals bd.bookingHeaderID
                                join bdad in _contextProp.TR_BookingDetailAddDisc on bd.Id equals bdad.bookingDetailID
                                where bip.itemID == bd.itemID && bh.unitID == input.unitID && bh.termID == input.termID && bh.Id == bip.bookingHeaderID
                                group new { bdad } by new { bdad.bookingDetailID } into grp2
                                select new
                                {
                                    JmlDisc = (double)grp2.Sum(x => x.bdad.pctAddDisc),
                                    grp2.Key.bookingDetailID
                                }
                             ) on bd.Id equals addDisc.bookingDetailID
                             where bh.termID == bgross.termID && bh.termID == bip.termID && bh.Id == bd.bookingHeaderID && bip.itemID == bsd.itemID

                             select new
                             {
                                 bd.Id,
                                 bip.grossPrice,
                                 bip.termID,
                                 PersenGrossPrice = ((double)bip.grossPrice / bgross.JmlGrossPrice),
                                 bsd.pctDisc,
                                 Disc = ((double)bip.grossPrice * bsd.pctDisc),
                                 NetPrice = ((double)bip.grossPrice - ((double)bip.grossPrice * bsd.pctDisc)),
                                 TotalAddDisc = (addDisc.JmlDisc * ((double)bip.grossPrice - ((double)bip.grossPrice * bsd.pctDisc))),
                                 NetNetPrice = (((double)bip.grossPrice - ((double)bip.grossPrice * bsd.pctDisc)) - (addDisc.JmlDisc * ((double)bip.grossPrice - ((double)bip.grossPrice * bsd.pctDisc))))
                             });
                             */
                #endregion
                Logger.Info("GeneratePrice() - Started.");

                var getDataNetPrice = (
                             from bh in _contextProp.TR_BookingHeader
                             join bgross in
                                 (from bh in _contextProp.TR_BookingHeader
                                  join bip in _contextProp.TR_BookingItemPrice on bh.termID equals bip.termID
                                  where bh.unitID == input.unitID && bh.cancelDate == null && bh.termID == input.termID && bh.Id == bip.bookingHeaderID
                                   && bh.termID == bip.termID
                                  group new { bip } by new { bip.termID } into grp
                                  select new
                                  {
                                      JmlGrossPrice = (double)grp.Sum(x => x.bip.grossPrice)
                                  })
                             on 1 equals 1
                             join bip in _contextProp.TR_BookingItemPrice on bh.Id equals bip.bookingHeaderID
                             join bd in _contextProp.TR_BookingDetail on bip.itemID equals bd.itemID
                             join bsd in _contextProp.TR_BookingSalesDisc on bh.Id equals bsd.bookingHeaderID
                             join addDisc in (
                                from bh in _contextProp.TR_BookingHeader
                                join bip in _contextProp.TR_BookingItemPrice on bh.termID equals bip.termID
                                join bd in _contextProp.TR_BookingDetail on bh.Id equals bd.bookingHeaderID
                                join bdad in _contextProp.TR_BookingDetailAddDisc on bd.Id equals bdad.bookingDetailID
                                where bip.itemID == bd.itemID && bh.unitID == input.unitID && bh.cancelDate == null && bh.termID == input.termID && bh.Id == bip.bookingHeaderID
                                group new { bdad } by new { bdad.bookingDetailID } into grp2
                                select new
                                {
                                    JmlDisc = (double)grp2.Sum(x => x.bdad.pctAddDisc),
                                    grp2.Key.bookingDetailID
                                }
                             ) on bd.Id equals addDisc.bookingDetailID
                             where bh.termID == input.termID && bh.termID == bip.termID && bh.Id == bd.bookingHeaderID && bip.itemID == bsd.itemID
                             && bh.unitID == input.unitID
                             select new
                             {
                                 bd.Id,
                                 bip.grossPrice,
                                 bip.termID,
                                 PersenGrossPrice = ((double)bip.grossPrice / bgross.JmlGrossPrice),
                                 bsd.pctDisc,
                                 Disc = ((double)bip.grossPrice * bsd.pctDisc),
                                 NetPrice = ((double)bip.grossPrice - ((double)bip.grossPrice * bsd.pctDisc)),
                                 TotalAddDisc = (addDisc.JmlDisc * ((double)bip.grossPrice - ((double)bip.grossPrice * bsd.pctDisc))),
                                 NetNetPrice = (((double)bip.grossPrice - ((double)bip.grossPrice * bsd.pctDisc)) - (addDisc.JmlDisc * ((double)bip.grossPrice - ((double)bip.grossPrice * bsd.pctDisc))))
                             }).ToList();

                var dataNetNetPrice = getDataNetPrice.Sum(x => x.NetPrice);

                //per jumlah add disc
                var getAddDisc = (from a in _trBookingDetailAddDiscRepo.GetAll()
                                  join b in _trBookingDetailRepo.GetAll()
                                  on a.bookingDetailID equals b.Id
                                  join c in _trBookingHeaderRepo.GetAll()
                                  on b.bookingHeaderID equals c.Id
                                  orderby a.addDiscNo
                                  where c.unitID == input.unitID && c.cancelDate == null
                                  select new
                                  {
                                      discountName = a.addDiscDesc,
                                      discount = a.isAmount == true ? (double)a.amtAddDisc : a.pctAddDisc,
                                      a.isAmount
                                  }).Distinct().ToList();

                foreach( var dataAddDisc in getAddDisc)
                {
                    var discountAdd = dataAddDisc.isAmount == true ? (double)dataAddDisc.discount : Math.Round(dataAddDisc.discount * dataNetNetPrice);

                    dataNetNetPrice = dataNetNetPrice - discountAdd;
                }

                //foreach (var dataAddDisc in getAddDisc)
                //{
                //    var discountAdd = dataAddDisc.isAmount == true ? (double)dataAddDisc.discount : Math.Round(dataAddDisc.discount * totalNetPrice);
                //    dataNetPrice = dataNetPrice - discountAdd;
                //    netPriceDisc = netPriceDisc + discountAdd;
                //    discDto.Add(new GetDiscountAlistDto
                //    {
                //        bookingDetailID = dataDisc.bookingDetailID,
                //        bookingDetailAddDiscID = dataDisc.bookingDetailAddDiscID,
                //        discountName = dataDisc.discountName,
                //        discount = dataDisc.discount,
                //        isAmount = dataDisc.isAmount,
                //        bangunan = discountAdd,
                //        total = discountAdd
                //    });
                //}


                if (getDataNetPrice.Any())
                {
                    //update
                    foreach (var data in getDataNetPrice)
                    {
                        var dataToUpdate = (from A in _trBookingDetailRepo.GetAll()
                                            join B in getDataNetPrice on A.Id equals B.Id
                                            where A.Id == data.Id
                                            select A).FirstOrDefault();

                        var update = dataToUpdate.MapTo<TR_BookingDetail>();

                        update.amount = data.grossPrice;
                        update.netPrice = (decimal)data.NetPrice;
                        update.netNetPrice = (decimal)dataNetNetPrice * (decimal)data.PersenGrossPrice;
                        update.pctDisc = data.pctDisc;

                        _trBookingDetailRepo.Update(update);
                        CurrentUnitOfWork.SaveChanges();
                    }

                    //SendConsole("getDataNetPrice:" + JsonConvert.SerializeObject(getDataNetPrice));
                }
                else
                {
                    throw new UserFriendlyException("Data Not Found!");
                }

                Logger.Info("GeneratePrice() - Finished.");

            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            #region performance test
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            SendConsole("stop stopwatch: " + elapsedMs + " ms");
            #endregion

        }

        public GetPSASPriceListDto UpdatePriceUniversal(UpdatePSASParamsDto input)
        {
            var unitID = GetParameter(input.paramsCheck);

            var projectCode = (from a in _msUnitRepo.GetAll()
                               join b in _msProjectRepo.GetAll() on a.projectID equals b.Id
                               select b.projectCode).FirstOrDefault();

            var getArea = (from a in _trBookingDetailRepo.GetAll()
                           where a.bookingHeaderID == unitID.bookingHeaderID
                           group a by a.bookingHeaderID into G
                           select new GetAreaLlistDto
                           {
                               bangunan = G.Sum(d => d.area),
                               total = G.Sum(d => d.area)
                           }).FirstOrDefault();

            var getGrossPrice = new GetGrosspriceLlistDto
            {
                bangunan = input.grossPrice,
                total = input.grossPrice
            };

            var getPctDisc = (from x in _trBookingDetailRepo.GetAll()
                              join a in _trBookingHeaderRepo.GetAll() on x.bookingHeaderID equals a.Id
                              where a.unitID == unitID.unitID && x.bookingHeaderID == unitID.bookingHeaderID
                              select x.pctDisc).FirstOrDefault();

            var dataDiscount = (double)getGrossPrice.total * getPctDisc;

            var getDiscount = new GetDiscountLlistDto
            {
                discount = getPctDisc,
                bangunan = dataDiscount,
                total = dataDiscount
            };

            var getNetPrice = new GetNetpriceLlistDto
            {
                bangunan = Convert.ToDouble(getGrossPrice.bangunan) - Convert.ToDouble(getDiscount.bangunan),
                total = Convert.ToDouble(getGrossPrice.total) - Convert.ToDouble(getDiscount.total),
            };

            //ToDo: fixing addDisc
            var getDiscountA = (from a in _trBookingDetailAddDiscRepo.GetAll()
                                join b in _trBookingDetailRepo.GetAll()
                                on a.bookingDetailID equals b.Id
                                join c in _trBookingHeaderRepo.GetAll()
                                on b.bookingHeaderID equals c.Id
                                orderby a.addDiscNo
                                where c.Id == unitID.bookingHeaderID
                                && c.unitID == unitID.unitID
                                && b.itemID == 2
                                select new
                                {
                                    bookingDetailID = b.Id,
                                    bookingDetailAddDiscID = a.Id,
                                    discountName = a.addDiscDesc,
                                    discount = a.isAmount == true ? (double)a.amtAddDisc : a.pctAddDisc,
                                    isAmount = a.isAmount,
                                    bangunan = a.isAmount == true ? (double)a.amtAddDisc : Math.Round(a.pctAddDisc * (double)getNetPrice.bangunan),
                                    total = a.isAmount == true ? (double)a.amtAddDisc : Math.Round(a.pctAddDisc * (double)getNetPrice.bangunan)
                                }).ToList();

            double dataNetPrice = (double)getNetPrice.bangunan;
            double netPriceDisc = 0;

            List<GetDiscountAlistDto> discDto = new List<GetDiscountAlistDto>();
            foreach (var dataDisc in getDiscountA)
            {
                var discountAdd = dataDisc.isAmount == true ? (double)dataDisc.discount : Math.Round(dataDisc.discount * dataNetPrice);
                dataNetPrice = dataNetPrice - discountAdd;
                netPriceDisc = netPriceDisc + discountAdd;
                discDto.Add(new GetDiscountAlistDto
                {
                    bookingDetailID = dataDisc.bookingDetailID,
                    bookingDetailAddDiscID = dataDisc.bookingDetailAddDiscID,
                    discountName = dataDisc.discountName,
                    discount = dataDisc.discount,
                    isAmount = dataDisc.isAmount,
                    bangunan = discountAdd,
                    total = discountAdd
                });
            }

            var getAddDisc = new GetAddDiscLlistDto
            {
                bangunan = Math.Round(netPriceDisc),
                total = Math.Round(netPriceDisc)
            };

            var getNetNetPrice = new GetNetNetPriceLlistDto
            {
                bangunan = Math.Round(Convert.ToDouble(getNetPrice.bangunan) - Convert.ToDouble(getAddDisc.bangunan)),
                total = Math.Round(Convert.ToDouble(getNetPrice.total) - Convert.ToDouble(getAddDisc.total))
            };

            var getVAT = new GetVATLlistDto
            {
                discount = 0.1,
                bangunan = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) * 0.1),
                total = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) * 0.1)
            };

            var checkInterest = (from a in _msUnitRepo.GetAll()
                                 join b in _msProjectRepo.GetAll()
                                 on a.projectID equals b.Id
                                 where a.Id == unitID.unitID
                                 && b.projectCode == "SDH"
                                 select a).Any();

            var getInterest = new GetInterestLlistDto();

            if (checkInterest)
            {
                getInterest = new GetInterestLlistDto
                {
                    bangunan = 0,
                    total = 0
                };
            }
            else
            {
                //Nothing
            }

            var getTotal = new GetTotalLlistDto
            {
                bangunan = Math.Round(Convert.ToDouble(getNetNetPrice.bangunan) + Convert.ToDouble(getVAT.bangunan) + Convert.ToDouble(getInterest.bangunan)),
                total = Math.Round(Convert.ToDouble(getNetNetPrice.total) + Convert.ToDouble(getVAT.total) + Convert.ToDouble(getInterest.total))
            };

            var result = new GetPSASPriceListDto
            {
                bookingHeaderID = unitID.bookingHeaderID,
                projectCode = projectCode,
                area = getArea,
                grossPrice = getGrossPrice,
                discount = getDiscount,
                netPrice = getNetPrice,
                addDisc = getAddDisc,
                discountA = discDto,
                netNetPrice = getNetNetPrice,
                VATPrice = getVAT,
                interest = getInterest,
                total = getTotal
            };

            return result;
        }


        #region debug console
        protected void SendConsole(string msg)
        {
            if (Setting_variabel.enable_tcp_debug == true)
            {
                if (Setting_variabel.Komunikasi_TCPListener == null)
                {
                    Setting_variabel.Komunikasi_TCPListener = new Visionet_Backend_NetCore.Komunikasi.Komunikasi_TCPListener(17000);
                    Task.Run(() => StartListenerLokal());
                }

                if (Setting_variabel.Komunikasi_TCPListener != null)
                {
                    if (Setting_variabel.Komunikasi_TCPListener.IsRunning())
                    {
                        if (Setting_variabel.ConsoleBayangan != null)
                        {
                            Setting_variabel.ConsoleBayangan.Send("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "] " + msg);
                        }
                    }
                }
            }

        }
        #endregion

        #region listener tcp debug
        protected void StartListenerLokal()
        {
            if (Setting_variabel.Komunikasi_TCPListener != null)
                Setting_variabel.Komunikasi_TCPListener.StartListener();
        }

        protected void StopListenerLokal()
        {
            if (Setting_variabel.Komunikasi_TCPListener != null)
                Setting_variabel.Komunikasi_TCPListener.StopListener();
        }

        public List<GetAddDiscListDto> GetDiscountPriceMKT(GetPSASParamsDto input)
        {
            var unitID = GetParameter(input);

            var getSingleDetailID = (from a in _trBookingDetailRepo.GetAll()
                                     join c in _trBookingHeaderRepo.GetAll()
                                     on a.bookingHeaderID equals c.Id
                                     where c.Id == unitID.bookingHeaderID
                                     && c.unitID == unitID.unitID
                                     select a.Id).FirstOrDefault();

            var getDetailAddDisc = (from a in _trMktAddDiscRepo.GetAll()
                                    where a.bookingDetailID == getSingleDetailID
                                    select new GetAddDiscListDto
                                    {
                                        addDiscDesc = a.addDiscDesc,
                                        addDiscNo = a.addDiscNo,
                                        isAmount = a.isAmount,
                                        pctAddDisc = a.pctAddDisc,
                                        amtAddDisc = a.amtAddDisc
                                    }).ToList();

            return getDetailAddDisc;
        }

        public List<GetAddDiscListDto> GetDiscountPriceComm(GetPSASParamsDto input)
        {
            var unitID = GetParameter(input);

            var getSingleDetailID = (from a in _trBookingDetailRepo.GetAll()
                                     join c in _trBookingHeaderRepo.GetAll()
                                     on a.bookingHeaderID equals c.Id
                                     where c.Id == unitID.bookingHeaderID
                                     && c.unitID == unitID.unitID
                                     select a.Id).FirstOrDefault();

            var getDetailAddDisc = (from a in _trCommAddDiscRepo.GetAll()
                                    where a.bookingDetailID == getSingleDetailID
                                    select new GetAddDiscListDto
                                    {
                                        addDiscDesc = a.addDiscDesc,
                                        addDiscNo = a.addDiscNo,
                                        isAmount = a.isAmount,
                                        pctAddDisc = a.pctAddDisc,
                                        amtAddDisc = a.amtAddDisc
                                    }).ToList();

            return getDetailAddDisc;
        }

        public void CreateUpdateDiscountPriceMKT(CreateUpdatePriceParamsDto input)
        {
            Logger.Info("CreateUpdateDiscountPriceMKT() - Started.");
            var unitID = GetParameter(input.paramsCheck);

            Logger.DebugFormat("CreateUpdateDiscountPriceMKT() - Start get data Booking Detail. Parameters sent:{0}" +
                        "bookingHeaderID = {1}{0}" +
                        "unitID = {2}{0}"
                        , Environment.NewLine, unitID.bookingHeaderID, unitID.unitID);
            //REPAIR THIS
            var getbookingDetail = (from x in _trBookingDetailRepo.GetAll()
                                    join c in _trBookingHeaderRepo.GetAll()
                                    on x.bookingHeaderID equals c.Id
                                    where c.Id == unitID.bookingHeaderID
                                    && c.unitID == unitID.unitID
                                    select x).ToList();

            Logger.DebugFormat("CreateUpdateDiscountPriceMKT() - Ended get data Booking Detail.");

            foreach (var item in input.DiscountList)
            {
                Logger.DebugFormat("CreateUpdateDiscountPriceMKT() - Start checking before update TR MKT Add Disc. Parameters sent:{0}" +
                            "discNo = {1}{0}"
                            , Environment.NewLine, item.discNo);

                var checkExists = (from a in _trMktAddDiscRepo.GetAll()
                                   join x in getbookingDetail
                                   on a.bookingDetailID equals x.Id
                                   where item.discNo == a.addDiscNo
                                   select a);

                Logger.DebugFormat("CreateUpdateDiscountPriceMKT() - Ended checking before insert TR MKT Add Disc. Result = {0}", checkExists.Count());

                if (checkExists.Any())
                {
                    foreach (var disc in checkExists.ToList())
                    {
                        Logger.DebugFormat("CreateUpdateDiscountPriceMKT() - Start checking table history before insert TR MKT Add Disc History. Parameters sent:{0}" +
                            "bookingDetailID = {1}{0}"
                            , Environment.NewLine, disc.bookingDetailID);

                        var checkHistory = (from A in _trMktAddDiscHistoryRepo.GetAll()
                                            orderby A.Id descending
                                            where A.bookingDetailID == disc.bookingDetailID
                                            select A).FirstOrDefault();

                        //insert into tb history
                        var dataToInsertHistory = new TR_MKTAddDiscHistory
                        {
                            addDiscDesc = disc.addDiscDesc,
                            addDiscNo = disc.addDiscNo,
                            amtAddDisc = disc.amtAddDisc,
                            isAmount = disc.isAmount,
                            pctAddDisc = disc.pctAddDisc,
                            bookingDetailID = disc.bookingDetailID,
                            entityID = disc.entityID,
                            historyNo = checkHistory == null ? Convert.ToByte(0) : Convert.ToByte(checkHistory.historyNo + 1)
                        };

                        var update = disc.MapTo<TR_MKTAddDisc>();

                        update.pctAddDisc = input.isAmount == true ? 0 : item.pctDisc;
                        update.amtAddDisc = input.isAmount == true ? item.amountDisc : 0;
                        update.isAmount = input.isAmount;
                        try
                        {
                            _trMktAddDiscHistoryRepo.Insert(dataToInsertHistory);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("CreateUpdateDiscountPriceMKT() - Start update TR MKT Add Disc. Parameters sent:{0}" +
                                "pctAddDisc = {1}{0}" +
                                "amtAddDisc = {2}{0}" +
                                "isAmount = {3}{0}"
                                , Environment.NewLine, item.pctDisc, item.amountDisc, input.isAmount);

                            _trMktAddDiscRepo.Update(update);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("CreateUpdateDiscountPriceMKT() - Ended update TR MKT Add Disc.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPriceMKT() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPriceMKT() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }

                    }
                }
                else
                {
                    foreach (var detail in getbookingDetail)
                    {
                        var insert = new TR_MKTAddDisc
                        {
                            addDiscDesc = item.addDiscDesc,
                            addDiscNo = item.discNo,
                            amtAddDisc = input.isAmount == true ? item.amountDisc : 0,
                            isAmount = input.isAmount,
                            pctAddDisc = input.isAmount == true ? 0 : item.pctDisc,
                            bookingDetailID = detail.Id,
                            entityID = 1
                        };

                        try
                        {
                            Logger.DebugFormat("CreateUpdateDiscountPriceMKT() - Start insert TR MKT Add Disc. Parameters sent:{0}" +
                                "entityID = {1}{0}" +
                                "addDiscDesc = {2}{0}" +
                                "addDiscNo = {3}{0}" +
                                "amtAddDisc = {4}{0}" +
                                "isAmount = {5}{0}" +
                                "pctAddDisc = {6}{0}" +
                                "bookingDetailID = {7}{0}"
                                , Environment.NewLine, 1, item.addDiscDesc, item.discNo, item.amountDisc, input.isAmount, item.pctDisc, detail.Id);

                            _trMktAddDiscRepo.Insert(insert);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("CreateUpdateDiscountPriceMKT() - Ended insert TR MKT Add Disc.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPriceMKT() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPriceMKT() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                }
            }
            Logger.Info("CreateUpdateDiscountPriceMKT() - Finished.");
        }

        public void CreateUpdateDiscountPriceComm(CreateUpdatePriceParamsDto input)
        {
            Logger.Info("CreateUpdateDiscountPriceComm() - Started.");
            var unitID = GetParameter(input.paramsCheck);

            Logger.DebugFormat("CreateUpdateDiscountPriceComm() - Start get data Booking Detail. Parameters sent:{0}" +
                        "bookingHeaderID = {1}{0}" +
                        "unitID = {2}{0}"
                        , Environment.NewLine, unitID.bookingHeaderID, unitID.unitID);
            //REPAIR THIS
            var getbookingDetail = (from x in _trBookingDetailRepo.GetAll()
                                    join c in _trBookingHeaderRepo.GetAll()
                                    on x.bookingHeaderID equals c.Id
                                    where c.Id == unitID.bookingHeaderID
                                    && c.unitID == unitID.unitID
                                    select x).ToList();

            Logger.DebugFormat("CreateUpdateDiscountPriceComm() - Ended get data Booking Detail.");

            foreach (var item in input.DiscountList)
            {
                Logger.DebugFormat("CreateUpdateDiscountPriceComm() - Start checking before update TR Booking Detail Add Disc. Parameters sent:{0}" +
                            "discNo = {1}{0}"
                            , Environment.NewLine, item.discNo);

                var checkExists = (from a in _trCommAddDiscRepo.GetAll()
                                   join x in getbookingDetail
                                   on a.bookingDetailID equals x.Id
                                   where item.discNo == a.addDiscNo
                                   select a);

                Logger.DebugFormat("CreateUpdateDiscountPriceComm() - Ended checking before insert TR Booking Detail Add Disc. Result = {0}", checkExists.Count());

                if (checkExists.Any())
                {
                    foreach (var disc in checkExists.ToList())
                    {
                        Logger.DebugFormat("CreateUpdateDiscountPriceComm() - Start checking table history before insert TR Comm Add Disc History. Parameters sent:{0}" +
                            "bookingDetailID = {1}{0}"
                            , Environment.NewLine, disc.bookingDetailID);

                        var checkHistory = (from A in _trCommAddDiscHistoryRepo.GetAll()
                                            orderby A.Id descending
                                            where A.bookingDetailID == disc.bookingDetailID
                                            select A).FirstOrDefault();

                        //insert into tb history
                        var dataToInsertHistory = new TR_CommAddDiscHistory
                        {
                            addDiscDesc = disc.addDiscDesc,
                            addDiscNo = disc.addDiscNo,
                            amtAddDisc = disc.amtAddDisc,
                            isAmount = disc.isAmount,
                            pctAddDisc = disc.pctAddDisc,
                            bookingDetailID = disc.bookingDetailID,
                            entityID = disc.entityID,
                            historyNo = checkHistory == null ? Convert.ToByte(0) : Convert.ToByte(checkHistory.historyNo + 1)
                        };

                        var update = disc.MapTo<TR_CommAddDisc>();

                        update.pctAddDisc = input.isAmount == true ? 0 : item.pctDisc;
                        update.amtAddDisc = input.isAmount == true ? item.amountDisc : 0;
                        update.isAmount = input.isAmount;
                        try
                        {
                            _trCommAddDiscHistoryRepo.Insert(dataToInsertHistory);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("CreateUpdateDiscountPriceComm() - Start update TR Comm Add Disc. Parameters sent:{0}" +
                                "pctAddDisc = {1}{0}" +
                                "amtAddDisc = {2}{0}" +
                                "isAmount = {3}{0}"
                                , Environment.NewLine, item.pctDisc, item.amountDisc, input.isAmount);

                            _trCommAddDiscRepo.Update(update);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("CreateUpdateDiscountPriceComm() - Ended update TR Comm Add Disc.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPriceComm() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPriceComm() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }

                    }
                }
                else
                {
                    foreach (var detail in getbookingDetail)
                    {
                        var insert = new TR_CommAddDisc
                        {
                            addDiscDesc = item.addDiscDesc,
                            addDiscNo = item.discNo,
                            amtAddDisc = input.isAmount == true ? item.amountDisc : 0,
                            isAmount = input.isAmount,
                            pctAddDisc = input.isAmount == true ? 0 : item.pctDisc,
                            bookingDetailID = detail.Id,
                            entityID = 1
                        };

                        try
                        {
                            Logger.DebugFormat("CreateUpdateDiscountPriceComm() - Start insert TR Comm Add Disc. Parameters sent:{0}" +
                                "entityID = {1}{0}" +
                                "addDiscDesc = {2}{0}" +
                                "addDiscNo = {3}{0}" +
                                "amtAddDisc = {4}{0}" +
                                "isAmount = {5}{0}" +
                                "pctAddDisc = {6}{0}" +
                                "bookingDetailID = {7}{0}"
                                , Environment.NewLine, 1, item.addDiscDesc, item.discNo, item.amountDisc, input.isAmount, item.pctDisc, detail.Id);

                            _trCommAddDiscRepo.Insert(insert);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("CreateUpdateDiscountPriceComm() - Ended insert TR Comm Add Disc.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPriceComm() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateUpdateDiscountPriceComm() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                }
            }
            Logger.Info("CreateUpdateDiscountPriceComm() - Finished.");
        }
        #endregion
    }
}
