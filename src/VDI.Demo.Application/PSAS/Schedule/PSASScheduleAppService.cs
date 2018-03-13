using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.Pricing;
using VDI.Demo.PSAS.Dto;
using System.Linq;
using VDI.Demo.PSAS.Term.Dto;
using Abp.AutoMapper;
using System.Data;
using Abp.UI;
using VDI.Demo.PSAS.Price;
using VDI.Demo.PSAS.Schedule.Dto;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.Helper;
using Newtonsoft.Json.Linq;

namespace VDI.Demo.PSAS.Schedule
{
    public class PSASScheduleAppService : DemoAppServiceBase, IPSASScheduleAppService
    {
        private readonly IRepository<TR_BookingHeader> _trBookingHeaderRepo;
        private readonly IRepository<MS_Bank> _msBankRepo;
        private readonly IRepository<MS_Term> _msTermRepo;
        private readonly IRepository<MS_TermPmt> _msTermPmtRepo;
        private readonly IRepository<LK_FinType> _lkFinTypeRepo;
        private readonly IRepository<TR_PaymentDetailAlloc> _trPaymentDetailAllocRepo;
        private readonly IRepository<TR_PaymentHeader> _trPaymentHeaderRepo;
        private readonly IRepository<TR_PaymentDetail> _trPaymentDetailRepo;
        private readonly IRepository<MS_BankBranch> _msBankBranchRepo;
        private readonly IRepository<MS_Account> _msAccountRepo;
        private readonly IRepository<LK_PayType> _lkPayTypeRepo;
        private readonly IPSASPriceAppService _iPriceAppService;
        private readonly IRepository<LK_PayFor> _lkPayForRepo;
        private readonly IRepository<TR_BookingDetail> _trBookingDetailRepo;
        private readonly IRepository<LK_Alloc> _lkAllocRepo;
        private readonly IRepository<MS_TermDP> _msTermDpRepo;
        private readonly IRepository<TR_BookingDetailSchedule> _trBookingDetailScheduleRepo;
        private readonly IRepository<TR_BookingDetailDP> _trBookingDetailDPRepo;
        private readonly PropertySystemDbContext _context;
        private readonly IRepository<SYS_FinanceCounter> _sysFinanceCounterRepo;
        //private readonly IRepository<LK_DPCalc> _lkDpCalcRepo;
        private readonly IRepository<LK_FormulaDP> _lkFormulaDpRepo;
        private readonly IRepository<TR_DPHistory> _trDPHistoryRepo;

        public PSASScheduleAppService(
            IRepository<TR_BookingHeader> trBookingHeaderRepo,
            IRepository<MS_Bank> msBankRepo,
            IRepository<MS_Term> msTermRepo,
            IRepository<MS_TermPmt> msTermPmtRepo,
            IRepository<LK_FinType> lkFinTypeRepo,
            IRepository<TR_PaymentDetailAlloc> trPaymentDetailAllocRepo,
            IRepository<TR_PaymentHeader> trPaymentHeaderRepo,
            IRepository<TR_PaymentDetail> trPaymentDetailRepo,
            IRepository<MS_BankBranch> msBankBranchRepo,
            IRepository<MS_Account> msAccountRepo,
            IRepository<LK_PayType> lkPayTypeRepo,
            IPSASPriceAppService iPriceAppService,
            IRepository<TR_BookingDetail> trBookingDetailRepo,
            IRepository<LK_PayFor> lkPayFor,
            IRepository<LK_Alloc> lkAllocRepo,
            IRepository<MS_TermDP> msTermDpRepo,
            IRepository<TR_BookingDetailSchedule> trBookingDetailScheduleRepo,
            IRepository<TR_BookingDetailDP> trBookingDetailDPRepo,
            PropertySystemDbContext context,
            IRepository<SYS_FinanceCounter> sysFinanceCounterRepo,
            //IRepository<LK_DPCalc> lkDpCalcRepo,
            IRepository<LK_FormulaDP> lkFormulaDpRepo,
            IRepository<TR_DPHistory> trDPHistoryRepo
            )
        {
            _trBookingHeaderRepo = trBookingHeaderRepo;
            _msBankRepo = msBankRepo;
            _msTermRepo = msTermRepo;
            _msTermPmtRepo = msTermPmtRepo;
            _lkFinTypeRepo = lkFinTypeRepo;
            _trPaymentDetailAllocRepo = trPaymentDetailAllocRepo;
            _trPaymentHeaderRepo = trPaymentHeaderRepo;
            _trPaymentDetailRepo = trPaymentDetailRepo;
            _msBankBranchRepo = msBankBranchRepo;
            _msAccountRepo = msAccountRepo;
            _lkPayTypeRepo = lkPayTypeRepo;
            _iPriceAppService = iPriceAppService;
            _trBookingDetailRepo = trBookingDetailRepo;
            _lkPayForRepo = lkPayFor;
            _lkAllocRepo = lkAllocRepo;
            _msTermDpRepo = msTermDpRepo;
            _trBookingDetailScheduleRepo = trBookingDetailScheduleRepo;
            _trBookingDetailDPRepo = trBookingDetailDPRepo;
            _context = context;
            _sysFinanceCounterRepo = sysFinanceCounterRepo;
            //_lkDpCalcRepo = lkDpCalcRepo;
            _lkFormulaDpRepo = lkFormulaDpRepo;
            _trDPHistoryRepo = trDPHistoryRepo;
        }

        public void CreateTrBookingDetailSchedule(CreateTrBookingDetailScheduleParamsDto input)
        {
            var getParamsPSAS = _iPriceAppService.GetParameter(input.psasParams);

            var checkInputAmount = (from A in input.listSchedule
                                    join B in _lkAllocRepo.GetAll() on A.allocID equals B.Id
                                    join C in _lkPayForRepo.GetAll() on B.payForID equals C.Id
                                    where C.payForCode == "PMT"
                                    select new
                                    {
                                        A.totalAmount
                                    }).Sum(c => c.totalAmount);

            var getTotalAmount = (from x in _trBookingDetailRepo.GetAll()
                                  where x.bookingHeaderID == getParamsPSAS.bookingHeaderID
                                  group x by new { x.bookingHeaderID } into G
                                  select new
                                  {
                                      bookHeaderID = G.Key.bookingHeaderID,
                                      TotalNetNetPrice = G.Sum(d => d.netNetPrice)
                                  }).FirstOrDefault();

            if (getTotalAmount.TotalNetNetPrice == 0)
                throw new UserFriendlyException("Devide By Zero");

            if (Math.Floor(checkInputAmount) <= Math.Floor(getTotalAmount.TotalNetNetPrice * (decimal)(1 + input.pctTax) + 100) && Math.Floor(checkInputAmount) >= Math.Floor(getTotalAmount.TotalNetNetPrice * (decimal)(1 + input.pctTax) - 100))
            {
                var getBookingDetail = (from x in _trBookingDetailRepo.GetAll()
                                        where x.bookingHeaderID == getParamsPSAS.bookingHeaderID
                                        select new
                                        {
                                            bookingHeaderID = getTotalAmount.bookHeaderID,
                                            bookingDetailID = x.Id,
                                            percentage = x.netNetPrice / getTotalAmount.TotalNetNetPrice
                                        }).ToList();

                var getAllSchedule = input.listSchedule;


                int index = 0;
                List<GetScheduleListDto> getAllocCodeADJ = new List<GetScheduleListDto>();
                foreach (var tmpSch in getAllSchedule)
                {
                    //insert data baru
                    if (tmpSch.netAmount == 0 && tmpSch.VATAmount == 0 && tmpSch.schedNo == 0)
                    {
                        getAllSchedule[index].allocID = GetAllocIDbyCode(tmpSch.allocCode);
                        getAllSchedule[index].netAmount = getAllSchedule[index].totalAmount;
                        getAllSchedule[index].VATAmount = getAllSchedule[index].netAmount;
                        getAllSchedule[index].netOutstanding = getAllSchedule[index].totalAmount;
                        getAllSchedule[index].VATOutstanding = getAllSchedule[index].netAmount;
                        getAllSchedule[index].paymentAmount = 0;
                        getAllSchedule[index].totalOutstanding = getAllSchedule[index].totalAmount;
                    }

                    if (tmpSch.allocCode == "ADJ")
                    {
                        getAllocCodeADJ.Add(getAllSchedule[index]);
                    }

                    index++;
                }

                if (getBookingDetail == null)
                    throw new UserFriendlyException("Booking Detail NULL");

                var getAllDetailScheduleDB = (from x in _trBookingHeaderRepo.GetAll()
                                              join y in _trBookingDetailRepo.GetAll() on x.Id equals y.bookingHeaderID
                                              join z in _trBookingDetailScheduleRepo.GetAll() on y.Id equals z.bookingDetailID
                                              where x.Id == getParamsPSAS.bookingHeaderID
                                              select new
                                              {
                                                  ID = z.Id
                                              }).ToList();

                //delete existing data
                foreach (var delSch in getAllDetailScheduleDB)
                {
                    _trBookingDetailScheduleRepo.Delete(delSch.ID);
                }

                int schedNumber = 0;

                var getAllScheduleOrdered = getAllSchedule.OrderBy(x => x.allocID).ThenBy(x => x.dueDate);

                foreach (var schedule in getAllScheduleOrdered)
                {
                    if (schedule.allocCode == "ADJ")
                        continue;

                    schedNumber++;
                    foreach (var bookDetail in getBookingDetail)
                    {
                        var data = new TR_BookingDetailSchedule
                        {
                            allocID = schedule.allocID,
                            bookingDetailID = bookDetail.bookingDetailID,
                            dueDate = schedule.dueDate,
                            entityID = 1,
                            netAmt = schedule.netAmount * bookDetail.percentage,
                            netOut = schedule.netOutstanding * bookDetail.percentage,
                            remarks = String.IsNullOrEmpty(schedule.remarks) ? string.Empty : schedule.remarks,
                            schedNo = (short)schedNumber,
                            vatAmt = schedule.VATAmount * bookDetail.percentage,
                            vatOut = schedule.VATOutstanding * bookDetail.percentage
                        };

                        _trBookingDetailScheduleRepo.Insert(data);
                    }
                }

                foreach (var dtADJ in getAllocCodeADJ)
                {
                    schedNumber++;
                    foreach (var bookDetail in getBookingDetail)
                    {
                        var data = new TR_BookingDetailSchedule
                        {
                            allocID = dtADJ.allocID,
                            bookingDetailID = bookDetail.bookingDetailID,
                            dueDate = dtADJ.dueDate,
                            entityID = 1,
                            netAmt = dtADJ.netAmount * bookDetail.percentage,
                            netOut = dtADJ.netOutstanding * bookDetail.percentage,
                            remarks = String.IsNullOrEmpty(dtADJ.remarks) ? String.Empty : dtADJ.remarks,
                            schedNo = (short)schedNumber,
                            vatAmt = dtADJ.VATAmount * bookDetail.percentage,
                            vatOut = dtADJ.VATOutstanding * bookDetail.percentage
                        };

                        _trBookingDetailScheduleRepo.Insert(data);
                    }
                }
            }
            else
            {
                throw new UserFriendlyException("Total Amount not match!");
            }
        }

        public List<GetAllocationListDto> GetAllocationDropdown()
        {
            var getData = (from A in _lkAllocRepo.GetAll()
                           join B in _lkPayForRepo.GetAll() on A.payForID equals B.Id
                           where B.payForCode != "PEN" && A.allocCode != "DP"
                           select new GetAllocationListDto
                           {
                               allocID = A.Id,
                               allocCode = A.allocCode,
                               isVat = A.isVAT
                           }).ToList();

            return getData;
        }

        public List<GetCompanyCodeListDto> GetCompanyCodeList(GetPSASParamsDto input)
        {
            var getParamsPSAS = _iPriceAppService.GetParameter(input);

            var getCoCodeAll = (from x in _trBookingDetailRepo.GetAll()
                                where x.bookingHeaderID == getParamsPSAS.bookingHeaderID
                                group x by new { x.coCode } into G
                                select new GetCompanyCodeListDto
                                {
                                    coCode = G.Key.coCode
                                }).ToList();

            return new List<GetCompanyCodeListDto>(getCoCodeAll);
        }

        public GetScheduleUniversalDto GetOriginalSchedule(int? bookingHeaderID)
        {
            List<GetScheduleListDto> dataSchedule = new List<GetScheduleListDto>();

            short schecNo = 1;

            //get sebagian data BF
            var dataBF = (from bh in _trBookingHeaderRepo.GetAll()
                          join bd in _trBookingDetailRepo.GetAll() on bh.Id equals bd.bookingHeaderID
                          where bh.Id == bookingHeaderID
                          group bd by new
                          {
                              bh.bookDate,
                              bd.bookingHeaderID,
                              bd.pctTax
                          } into G
                          select new
                          {
                              dueDate = G.Key.bookDate,
                              totalAmount = G.Sum(X => X.BFAmount),
                              sellingPrice = G.Sum(x => x.netNetPrice),
                              G.Key.pctTax
                          }).FirstOrDefault();

            //harga jual
            var sellingPrice = dataBF.sellingPrice * (decimal)(1 + dataBF.pctTax);

            //data BF untuk di push
            var dataBFFinal = new GetScheduleListDto
            {
                dueDate = dataBF.dueDate,
                allocCode = "BF",
                allocID = GetAllocIDbyCode("BF"),
                totalAmount = dataBF.totalAmount,
                netAmount = dataBF.totalAmount / (decimal)(1 + dataBF.pctTax),
                VATAmount = (dataBF.totalAmount / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax),
                netOutstanding = dataBF.totalAmount / (decimal)(1 + dataBF.pctTax),
                VATOutstanding = (dataBF.totalAmount / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax),
                paymentAmount = 0,
                totalOutstanding = dataBF.totalAmount,
                schedNo = schecNo
            };

            //push data BF
            dataSchedule.Add(dataBFFinal);


            //DP
            var dataDP = (from bh in _trBookingHeaderRepo.GetAll()
                          join bd in _trBookingDetailRepo.GetAll() on bh.Id equals bd.bookingHeaderID
                          join bdd in _trBookingDetailDPRepo.GetAll() on bd.Id equals bdd.bookingDetailID
                          join dc in _lkFormulaDpRepo.GetAll() on bdd.formulaDPID equals dc.Id into l1
                          from dc in l1.DefaultIfEmpty()
                          where bh.Id == bookingHeaderID
                          select new
                          {
                              bdd.DPAmount,
                              bdd.DPPct,
                              bdd.daysDue,
                              bdd.monthsDue,
                              bdd.dpNo,
                              dpCalcID = bdd.dpCalcID == null ? null : bdd.dpCalcID
                          } into bdd
                          group bdd by new
                          {
                              bdd.DPAmount,
                              bdd.DPPct,
                              bdd.daysDue,
                              bdd.monthsDue,
                              bdd.dpNo,
                              bdd.dpCalcID
                          } into G
                          select new
                          {
                              G.Key.dpNo,
                              G.Key.DPAmount,
                              G.Key.DPPct,
                              G.Key.daysDue,
                              G.Key.monthsDue,
                              G.Key.dpCalcID
                          }).OrderBy(x => x.dpNo).ToList();



            int countDP = dataDP.Count;

            decimal totalDP = 0;

            //selling
            var calcTypeID4 = (from A in _lkFormulaDpRepo.GetAll()
                               where A.formulaDPType == "2"
                               select A.Id).FirstOrDefault();

            var DPFinal = new GetScheduleListDto();

            for (var i = 0; i < countDP; i++)
            {
                schecNo++;

                var DPValue = dataDP[i].DPPct != 0 ? sellingPrice * (decimal)dataDP[i].DPPct : dataDP[i].DPAmount;


                //first DP
                DPFinal = new GetScheduleListDto
                {
                    dueDate = dataDP[0].daysDue != 0 ? dataBFFinal.dueDate.AddDays(dataDP[0].daysDue) : dataBFFinal.dueDate.AddMonths((int)dataDP[0].monthsDue),
                    allocCode = "DP",
                    allocID = GetAllocIDbyCode("DP"),
                    totalAmount = dataDP[i].dpCalcID == calcTypeID4 ? DPValue - dataBFFinal.totalAmount : DPValue,
                    netAmount = dataDP[i].dpCalcID == calcTypeID4 ? (DPValue - dataBFFinal.totalAmount) / (decimal)(1 + dataBF.pctTax) : DPValue / (decimal)(1 + dataBF.pctTax),
                    VATAmount = dataDP[i].dpCalcID == calcTypeID4 ? ((DPValue - dataBFFinal.totalAmount) / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax) : (DPValue / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax),
                    netOutstanding = dataDP[i].dpCalcID == calcTypeID4 ? (DPValue - dataBFFinal.totalAmount) / (decimal)(1 + dataBF.pctTax) : DPValue / (decimal)(1 + dataBF.pctTax),
                    VATOutstanding = dataDP[i].dpCalcID == calcTypeID4 ? ((DPValue - dataBFFinal.totalAmount) / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax) : (DPValue / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax),
                    paymentAmount = 0,
                    totalOutstanding = dataDP[i].dpCalcID == calcTypeID4 ? DPValue - dataBFFinal.totalAmount : DPValue,
                    schedNo = schecNo
                };

                //DP selain pertama
                if (i != 0)
                {
                    DPFinal.dueDate = dataDP[i].daysDue != 0 ? dataSchedule[dataSchedule.Count - 1].dueDate.AddDays(dataDP[i].daysDue) : dataSchedule[dataSchedule.Count - 1].dueDate.AddMonths((int)dataDP[i].monthsDue);
                }



                totalDP += DPFinal.totalAmount;

                dataSchedule.Add(DPFinal);
            }

            //INS
            var j = 0;

            //get data INS
            var dataINS = (from bh in _trBookingHeaderRepo.GetAll()
                           join t in _msTermPmtRepo.GetAll() on bh.termID equals t.termID
                           join ft in _lkFinTypeRepo.GetAll() on t.finTypeID equals ft.Id
                           where bh.Id == bookingHeaderID
                           select new
                           {
                               ft.finTimes,
                               t.finStartDue,
                               t.finStartM
                           }).FirstOrDefault();

            //looping untuk mengasih schedNo dll
            while (j < dataINS.finTimes)
            {
                schecNo++;

                var dataINSFinal = new GetScheduleListDto();

                //INS Selain pertama
                dataINSFinal = new GetScheduleListDto
                {
                    dueDate = dataSchedule[dataSchedule.Count - 1].dueDate.AddMonths(1),
                    allocCode = "INS",
                    allocID = GetAllocIDbyCode("INS"),
                    totalAmount = (sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes,
                    netAmount = ((sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes) / (decimal)(1 + dataBF.pctTax),
                    VATAmount = (((sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes) / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax),
                    netOutstanding = ((sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes) / (decimal)(1 + dataBF.pctTax),
                    VATOutstanding = (((sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes) / (decimal)(1 + dataBF.pctTax)) * (decimal)(dataBF.pctTax),
                    paymentAmount = 0,
                    totalOutstanding = (sellingPrice - totalDP - dataBFFinal.totalAmount) / dataINS.finTimes,
                    schedNo = schecNo
                };

                //ins pertama
                if (j == 0)
                {
                    dataINSFinal.dueDate = dataINS.finStartDue != 0 ? dataSchedule[dataSchedule.Count - 1].dueDate.AddDays(dataINS.finStartDue) : dataBFFinal.dueDate.AddMonths((int)dataINS.finStartM);
                }
                dataSchedule.Add(dataINSFinal);
                j++;
            }
            //dataResult.OrderBy(x => x.schedNo);

            var dataResult = new GetScheduleUniversalDto
            {
                pctTax = dataBF.pctTax,
                dataSchedule = dataSchedule
            };

            return dataResult;
        }

        public GetPSASScheduleHeaderDto GetPSASScheduleHeader(GetPSASParamsDto input)
        {
            var getParamsPSAS = _iPriceAppService.GetParameter(input);

            var getPSASScheduleHeader = (from a in _trBookingHeaderRepo.GetAll()
                                         join b in _trBookingDetailRepo.GetAll() on a.Id equals b.bookingHeaderID
                                         join c in _msTermRepo.GetAll() on a.termID equals c.Id
                                         where a.Id == getParamsPSAS.bookingHeaderID
                                         group b by new
                                         {
                                             a.bookCode,
                                             a.unitID,
                                             a.termID,
                                             b.pctTax,
                                             c.termCode,
                                             c.remarks
                                         } into G
                                         select new GetPSASScheduleHeaderDto
                                         {
                                             term = G.Key.termCode + " - " + G.Key.remarks,
                                             totalNetAmount = G.Sum(d => d.netNetPrice),
                                             totalVATAmount = G.Sum(d => d.netNetPrice) * (decimal)G.Key.pctTax,

                                         }).FirstOrDefault();
            return getPSASScheduleHeader;
        }

        public GetScheduleUniversalDto GetPSASScheduleDetail(GetPSASScheduleParamDto input)
        {
            var dataResult = new GetScheduleUniversalDto();

            var getInput = new GetPSASParamsDto()
            {
                bookCode = input.bookCode,
                unitCode = input.unitCode,
                unitNo = input.unitNo
            };
            var getParamsPSAS = _iPriceAppService.GetParameter(getInput);

            var checkScheduleData = (from x in _trBookingDetailScheduleRepo.GetAll()
                                     join a in _trBookingDetailRepo.GetAll() on x.bookingDetailID equals a.Id
                                     join b in _trBookingHeaderRepo.GetAll() on a.bookingHeaderID equals b.Id
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
                                         x.vatOut
                                     });

            var dataAlloc = _lkAllocRepo.GetAllList();

            List<GetScheduleListDto> dataSchedule = new List<GetScheduleListDto>();

            if (checkScheduleData.Any())
            {
                var dataSchedules = (from x in checkScheduleData
                                     where (input.coCode == "All") || (input.coCode != "All" && x.coCode == input.coCode)
                                     group x by new
                                     {
                                         x.schedNo,
                                         x.dueDate,
                                         x.allocID,
                                         x.remarks
                                     } into G
                                     select new GetScheduleListDto
                                     {
                                         allocID = G.Key.allocID,
                                         allocCode = (from da in dataAlloc where da.Id == G.Key.allocID select da.allocCode).FirstOrDefault(),
                                         netAmount = G.Sum(d => d.netAmt),
                                         VATAmount = G.Sum(d => d.vatAmt),
                                         totalAmount = G.Sum(d => d.netAmt) + G.Sum(d => d.vatAmt),
                                         netOutstanding = G.Sum(d => d.netOut),
                                         VATOutstanding = G.Sum(d => d.vatOut),
                                         totalOutstanding = G.Sum(d => d.netOut) + G.Sum(d => d.vatOut),
                                         dueDate = G.Key.dueDate,
                                         schedNo = G.Key.schedNo,
                                         remarks = G.Key.remarks,
                                         paymentAmount = (G.Sum(d => d.netAmt) + G.Sum(d => d.vatAmt)) - (G.Sum(d => d.netOut) + G.Sum(d => d.vatOut))
                                     }).ToList();

                foreach (var dataSched in dataSchedules)
                {
                    var dataPayment = (from pda in _trPaymentDetailAllocRepo.GetAll()
                                       join pd in _trPaymentDetailRepo.GetAll() on pda.paymentDetailID equals pd.Id
                                       join ph in _trPaymentHeaderRepo.GetAll() on pd.paymentHeaderID equals ph.Id
                                       join pt in _lkPayTypeRepo.GetAll() on pd.payTypeID equals pt.Id
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

                    var dataSchedPush = new GetScheduleListDto
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
                        dataPayment = dataPayment
                    };

                    dataSchedule.Add(dataSchedPush);
                }

                dataSchedule.OrderBy(x => x.allocID).ThenBy(x => x.dueDate);

                dataResult = new GetScheduleUniversalDto
                {
                    pctTax = checkScheduleData.FirstOrDefault().pctTax,
                    dataSchedule = dataSchedule
                };

            }
            else
            {
                var ori = GetOriginalSchedule(getParamsPSAS.bookingHeaderID);

                dataSchedule = ori.dataSchedule;

                dataResult = new GetScheduleUniversalDto
                {
                    pctTax = ori.pctTax,
                    dataSchedule = dataSchedule
                };

                var paramsUnit = new GetPSASParamsDto
                {
                    bookCode = input.bookCode,
                    unitCode = input.unitCode,
                    unitNo = input.unitNo
                };
                var paramsToInsert = new CreateTrBookingDetailScheduleParamsDto
                {
                    psasParams = paramsUnit,
                    listSchedule = dataSchedule
                };

                CreateTrBookingDetailSchedule(paramsToInsert);
            }
            return dataResult;
        }

        private int GetAllocIDbyCode(string allocCode)
        {
            var allocID = (from a in _lkAllocRepo.GetAll()
                           where a.allocCode == allocCode
                           select a.Id).FirstOrDefault();

            return allocID;
        }

        public void RegenerateSchedule(GetPSASParamsDto input)
        {
            var getParamsPSAS = _iPriceAppService.GetParameter(input);

            var getTotalAmount = (from x in _trBookingDetailRepo.GetAll()
                                  where x.bookingHeaderID == getParamsPSAS.bookingHeaderID
                                  group x by new { x.bookingHeaderID } into G
                                  select new
                                  {
                                      bookHeaderID = G.Key.bookingHeaderID,
                                      TotalNetNetPrice = G.Sum(d => d.netNetPrice)
                                  }).FirstOrDefault();

            if (getTotalAmount.TotalNetNetPrice == 0)
                throw new UserFriendlyException("Devide By Zero");

            var getBookingDetail = (from x in _trBookingDetailRepo.GetAll()
                                    where x.bookingHeaderID == getParamsPSAS.bookingHeaderID
                                    select new
                                    {
                                        bookingHeaderID = getTotalAmount.bookHeaderID,
                                        bookingDetailID = x.Id,
                                        percentage = x.netNetPrice / getTotalAmount.TotalNetNetPrice
                                    }).ToList();

            var dataOri = GetOriginalSchedule(getParamsPSAS.bookingHeaderID);

            if (getBookingDetail == null)
                throw new UserFriendlyException("Booking Detail NULL");

            //get data booking schedule by booking detail
            var getAllDetailScheduleDB = (from x in _trBookingHeaderRepo.GetAll()
                                          join y in _trBookingDetailRepo.GetAll() on x.Id equals y.bookingHeaderID
                                          join z in _trBookingDetailScheduleRepo.GetAll() on y.Id equals z.bookingDetailID
                                          where x.Id == getParamsPSAS.bookingHeaderID
                                          select new
                                          {
                                              ID = z.Id
                                          }).ToList();

            //delete existing data
            foreach (var delSch in getAllDetailScheduleDB)
            {
                _trBookingDetailScheduleRepo.Delete(delSch.ID);
            }

            //Data payment
            var getDataPayment = (from x in _trPaymentDetailAllocRepo.GetAll()
                                  join a in _trPaymentDetailRepo.GetAll() on x.paymentDetailID equals a.Id
                                  join b in _trPaymentHeaderRepo.GetAll() on a.paymentHeaderID equals b.Id
                                  join c in _trBookingHeaderRepo.GetAll() on b.bookingHeaderID equals c.Id
                                  join d in _lkPayForRepo.GetAll() on b.payForID equals d.Id
                                  where c.Id == getParamsPSAS.bookingHeaderID && d.payForCode == "PMT"
                                  group x by 1 into G
                                  select new
                                  {
                                      netAmt = G.Sum(x => x.netAmt),
                                      VATAmt = G.Sum(x => x.vatAmt)
                                  }).FirstOrDefault();

            var totalPayment = getDataPayment != null ? getDataPayment.netAmt + getDataPayment.VATAmt : 0;

            //Calculate data schedule

            int schedNumber = 0;

            decimal lastPayment = totalPayment;

            foreach (var schedule in dataOri.dataSchedule)
            {
                List<TR_BookingDetailSchedule> dataFinal = new List<TR_BookingDetailSchedule>();
                var data = new TR_BookingDetailSchedule();



                schedNumber++;

                foreach (var bookDetail in getBookingDetail)
                {
                    data = new TR_BookingDetailSchedule
                    {
                        allocID = schedule.allocID,
                        bookingDetailID = bookDetail.bookingDetailID,
                        dueDate = schedule.dueDate,
                        entityID = 1,
                        netAmt = schedule.netAmount * bookDetail.percentage,
                        netOut = schedule.netAmount * bookDetail.percentage,
                        remarks = String.IsNullOrEmpty(schedule.remarks) ? string.Empty : schedule.remarks,
                        schedNo = (short)schedNumber,
                        vatAmt = schedule.VATAmount * bookDetail.percentage,
                        vatOut = schedule.VATAmount * bookDetail.percentage,
                    };

                    dataFinal.Add(data);
                }
                var dataReturn = (from A in dataFinal select A).OrderBy(x => x.schedNo).ToList();
                _context.BulkInsert(_trBookingDetailScheduleRepo, dataReturn);
            }
        }

        public void RecalculateBalance(GetPSASParamsDto input)
        {
            var getParamsPSAS = _iPriceAppService.GetParameter(input);

            //check payment (total payment untuk 1 unit)
            var checkPayment = (from x in _trPaymentDetailAllocRepo.GetAll()
                                join a in _trPaymentDetailRepo.GetAll() on x.paymentDetailID equals a.Id
                                join b in _trPaymentHeaderRepo.GetAll() on a.paymentHeaderID equals b.Id
                                where b.bookingHeaderID == getParamsPSAS.bookingHeaderID
                                group x by 1 into G
                                select new
                                {
                                    netAmt = G.Sum(x => x.netAmt),
                                    VATAmt = G.Sum(x => x.vatAmt)
                                }).FirstOrDefault();

            var totalCheckPayment = checkPayment != null ? checkPayment.netAmt + checkPayment.VATAmt : 0;

            //check selling price
            var checkSellingPrice = (from bh in _trBookingHeaderRepo.GetAll()
                                     join bd in _trBookingDetailRepo.GetAll() on bh.Id equals bd.bookingHeaderID
                                     where bh.Id == getParamsPSAS.bookingHeaderID
                                     group bd by new
                                     {
                                         bd.pctTax
                                     } into G
                                     select new
                                     {
                                         sellingPrice = G.Sum(x => x.netNetPrice),
                                         G.Key.pctTax
                                     }).FirstOrDefault();

            //harga jual
            var sellingPrice = checkSellingPrice.sellingPrice * (decimal)(1 + checkSellingPrice.pctTax);

            if (Math.Floor(totalCheckPayment) <= Math.Floor(sellingPrice) + 100)
            {
                var getDataPayFor = (from a in _trPaymentHeaderRepo.GetAll()
                                     where a.bookingHeaderID == getParamsPSAS.bookingHeaderID
                                     select a.payForID).Distinct().ToList();

                foreach (var payFor in getDataPayFor)
                {
                    //per payFor
                    var getDataPayment = (from x in _trPaymentDetailAllocRepo.GetAll()
                                          join a in _trPaymentDetailRepo.GetAll() on x.paymentDetailID equals a.Id
                                          join b in _trPaymentHeaderRepo.GetAll() on a.paymentHeaderID equals b.Id
                                          join c in _trBookingHeaderRepo.GetAll() on b.bookingHeaderID equals c.Id
                                          join d in _lkPayForRepo.GetAll() on b.payForID equals d.Id
                                          orderby a.dueDate
                                          where c.Id == getParamsPSAS.bookingHeaderID && b.payForID == payFor
                                          group x by new
                                          {
                                              b.payForID
                                          } into G
                                          select new
                                          {
                                              payForId = G.Key.payForID,
                                              netAmt = G.Sum(x => x.netAmt),
                                              VATAmt = G.Sum(x => x.vatAmt)
                                          }).ToList();



                    //update outstanding schedule
                    foreach (var paymentPayFor in getDataPayment)
                    {
                        //schedule per schedNo
                        var getSchedulePayFor = (from a in _trBookingDetailScheduleRepo.GetAll()
                                                 join b in _lkAllocRepo.GetAll() on a.allocID equals b.Id
                                                 join c in _lkPayForRepo.GetAll() on b.payForID equals c.Id
                                                 join d in _trBookingDetailRepo.GetAll() on a.bookingDetailID equals d.Id
                                                 where d.bookingHeaderID == getParamsPSAS.bookingHeaderID && c.Id == paymentPayFor.payForId
                                                 orderby a.schedNo
                                                 group a by new
                                                 {
                                                     a.schedNo
                                                 } into G
                                                 select new
                                                 {
                                                     G.Key.schedNo,
                                                     netAmt = G.Sum(x => x.netAmt),
                                                     VATAmt = G.Sum(x => x.vatAmt),
                                                     netOut = G.Sum(x => x.netOut),
                                                     vatOut = G.Sum(x => x.vatOut)
                                                 }).ToList();

                        var totalPayment = paymentPayFor.netAmt;

                        decimal lastPayment = totalPayment;

                        foreach (var schedule in getSchedulePayFor)
                        {
                            var data = new TR_BookingDetailSchedule();

                            var getSchedulePerSchedNo = (from a in _trBookingDetailScheduleRepo.GetAll()
                                                         join b in _lkAllocRepo.GetAll() on a.allocID equals b.Id
                                                         join c in _lkPayForRepo.GetAll() on b.payForID equals c.Id
                                                         join d in _trBookingDetailRepo.GetAll() on a.bookingDetailID equals d.Id
                                                         where d.bookingHeaderID == getParamsPSAS.bookingHeaderID && c.Id == paymentPayFor.payForId && a.schedNo == schedule.schedNo
                                                         select a).ToList();

                            //outstanding = 0
                            if (lastPayment >= schedule.netAmt && schedule.netOut == 0)
                            {
                                lastPayment = lastPayment - schedule.netAmt;
                            }


                            else if (lastPayment >= schedule.netAmt && schedule.netOut != 0)
                            {
                                lastPayment = lastPayment - schedule.netAmt;

                                foreach (var bookDetail in getSchedulePerSchedNo)
                                {
                                    data = bookDetail.MapTo<TR_BookingDetailSchedule>();

                                    data.netOut = 0;
                                    data.vatOut = 0;

                                    _trBookingDetailScheduleRepo.Update(data);
                                }
                            }
                            else if (lastPayment <= schedule.netAmt && lastPayment > 0)
                            {
                                lastPayment = lastPayment - schedule.netAmt;
                                var lastPaymentPositif = lastPayment * -1;

                                var sumAmt = getSchedulePerSchedNo.Sum(x => x.netAmt);

                                foreach (var bookDetail in getSchedulePerSchedNo)
                                {
                                    var percentage = bookDetail.netAmt / sumAmt;

                                    data = bookDetail.MapTo<TR_BookingDetailSchedule>();

                                    data.netOut = lastPaymentPositif * percentage;
                                    data.vatOut = (lastPaymentPositif * percentage) * (decimal)(checkSellingPrice.pctTax);

                                    _trBookingDetailScheduleRepo.Update(data);
                                }
                            }
                            //payment = 0
                            else
                            {
                                foreach (var bookDetail in getSchedulePerSchedNo)
                                {
                                    data = bookDetail.MapTo<TR_BookingDetailSchedule>();

                                    data.netOut = bookDetail.netAmt;
                                    data.vatOut = bookDetail.vatAmt;

                                    _trBookingDetailScheduleRepo.Update(data);
                                }
                            }

                        }
                    }

                    //per payment
                    var getPayment = (from x in _trPaymentDetailAllocRepo.GetAll()
                                      join a in _trPaymentDetailRepo.GetAll() on x.paymentDetailID equals a.Id
                                      join b in _trPaymentHeaderRepo.GetAll() on a.paymentHeaderID equals b.Id
                                      join c in _trBookingHeaderRepo.GetAll() on b.bookingHeaderID equals c.Id
                                      join d in _lkPayForRepo.GetAll() on b.payForID equals d.Id
                                      where c.Id == getParamsPSAS.bookingHeaderID && b.payForID == payFor
                                      group x by new
                                      {
                                          x.paymentDetailID,
                                          b.payForID,
                                          a.ket,
                                          b.paymentDate
                                      } into G
                                      select new
                                      {
                                          G.Key.paymentDetailID,
                                          G.Key.ket,
                                          G.Key.payForID,
                                          netAmt = G.Sum(x => x.netAmt),
                                          VATAmt = G.Sum(x => x.vatAmt),
                                          G.Key.paymentDate
                                      }).OrderBy(x => x.paymentDate).ToList();

                    //update payment
                    var lastSchedNo = 1;
                    foreach (var payment in getPayment)
                    {
                        //schedule per schedNo (per schedule)
                        var getSchedule = (from a in _trBookingDetailScheduleRepo.GetAll()
                                           join b in _lkAllocRepo.GetAll() on a.allocID equals b.Id
                                           join c in _lkPayForRepo.GetAll() on b.payForID equals c.Id
                                           join d in _trBookingDetailRepo.GetAll() on a.bookingDetailID equals d.Id
                                           where d.bookingHeaderID == getParamsPSAS.bookingHeaderID && c.Id == payment.payForID
                                           group a by new
                                           {
                                               a.schedNo
                                           } into G
                                           select new
                                           {
                                               G.Key.schedNo,
                                               netAmt = G.Sum(x => x.netAmt),
                                               VATAmt = G.Sum(x => x.vatAmt),
                                               totalAmount = G.Sum(x => x.netAmt) + G.Sum(x => x.vatAmt),
                                               netOut = (decimal)G.Sum(x => x.netOut),
                                               vatOut = (decimal)G.Sum(x => x.vatOut)
                                           }).OrderBy(x => x.schedNo).ToList();

                        var totalPaymentPerPaymentDetail = payment.VATAmt + payment.netAmt;

                        decimal lastPaymentPerPaymentDetail = totalPaymentPerPaymentDetail;

                        var paymentAllocForDelete = (from a in _trPaymentDetailAllocRepo.GetAll()
                                                     where a.paymentDetailID == payment.paymentDetailID
                                                     select a).ToList();
                        var countToDelete = 0;

                        foreach (var schedule in getSchedule)
                        {
                            if (lastPaymentPerPaymentDetail > 0 && lastSchedNo <= schedule.schedNo && schedule.netAmt - schedule.netOut != 0)
                            {
                                lastPaymentPerPaymentDetail = lastPaymentPerPaymentDetail - schedule.totalAmount;

                                bool isMinus = false;
                                decimal? totalWriteOffNet = 0;
                                decimal? totalWriteOffVat = 0;
                                decimal? totalWriteOff = 0;
                                if (lastPaymentPerPaymentDetail < 0)
                                {
                                    totalWriteOffNet = (from x in getPayment
                                                        where x.ket.Contains("Write Off")
                                                        select new
                                                        {
                                                            netAmt = x.netAmt
                                                        }).Sum(x => x.netAmt);

                                    totalWriteOffVat = (from x in getPayment
                                                        where x.ket.Contains("Write Off")
                                                        select new
                                                        {
                                                            vatAmt = x.VATAmt
                                                        }).Sum(x => x.vatAmt);
                                    totalWriteOff = totalWriteOffNet + totalWriteOffVat;

                                    if (lastPaymentPerPaymentDetail + totalWriteOff < 0)
                                    {
                                        isMinus = true;
                                    }
                                    else
                                    {
                                        isMinus = true;
                                        totalWriteOff = lastPaymentPerPaymentDetail * -1;
                                        totalWriteOffNet = totalWriteOff / (decimal)1.1;
                                        totalWriteOffVat = (decimal)0.1 * totalWriteOff;
                                    }
                                }

                                var dataInsertPDA = new TR_PaymentDetailAlloc();
                                if (isMinus)
                                {
                                    dataInsertPDA = new TR_PaymentDetailAlloc
                                    {
                                        schedNo = schedule.schedNo,
                                        paymentDetailID = payment.paymentDetailID,
                                        netAmt = schedule.netAmt - schedule.netOut - (decimal)totalWriteOffNet,
                                        vatAmt = schedule.VATAmt - schedule.vatOut - (decimal)totalWriteOffVat,
                                        entityID = 1,
                                    };
                                }
                                else
                                {
                                    dataInsertPDA = new TR_PaymentDetailAlloc
                                    {
                                        schedNo = schedule.schedNo,
                                        paymentDetailID = payment.paymentDetailID,
                                        netAmt = schedule.netAmt - schedule.netOut,
                                        vatAmt = schedule.VATAmt - schedule.vatOut,
                                        entityID = 1,
                                    };
                                }

                                _trPaymentDetailAllocRepo.Insert(dataInsertPDA);

                                countToDelete++;
                                lastSchedNo = schedule.schedNo + 1;
                            }
                        }
                        if (countToDelete > 0)
                        {
                            foreach (var itemDelete in paymentAllocForDelete)
                            {
                                _trPaymentDetailAllocRepo.Delete(itemDelete);
                            }
                        }
                        //edit amt jadi 0
                        else if (countToDelete == 0 && !payment.ket.Contains("Write Off"))
                        {
                            foreach (var itemDelete in paymentAllocForDelete)
                            {
                                itemDelete.MapTo<TR_PaymentDetailAlloc>();
                                itemDelete.netAmt = 0;
                                itemDelete.vatAmt = 0;

                                _trPaymentDetailAllocRepo.Update(itemDelete);
                            }
                        }

                    }
                }
            }

            else
            {
                throw new UserFriendlyException("Your Payment More Than Selling Price!");
            }
        }

        public int CreateTrPaymentHeader(CreateTrPaymentHeaderInputDto input)
        {
            var inputGenerateTransNo = new GenerateTransNoInputDto
            {
                accID = input.accountID,
                entityID = 1
            };

            var transNo = GenerateTransNo(inputGenerateTransNo);

            Logger.Info("CreateTrPaymentHeader() - Started.");
            var data = new TR_PaymentHeader
            {
                accountID = input.accountID == 0 ? 958 : input.accountID,
                bookingHeaderID = input.bookingHeaderID,
                clearDate = DateTime.Now,
                combineCode = "1",
                entityID = 1,
                paymentDate = DateTime.Now,
                transNo = transNo.GetValue("transNo").ToString(),
                payForID = input.payForID,
                controlNo = transNo.GetValue("transNo").ToString(), //sama kaya transNo,
                ket = "Write Off",
                isSMS = false
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
                    "isSMS = {11}{0}"
                    , Environment.NewLine, 1, input.accountID, input.bookingHeaderID, DateTime.Now, 1, DateTime.Now, transNo, input.payForID, transNo, "Write Off", false);

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

        public int CreateTrPaymentDetail(CreateTrPaymentDetailInputDto input)
        {
            var getPayTypeID = (from a in _lkPayTypeRepo.GetAll()
                                where a.payTypeCode == "ADJ"
                                select a.Id).FirstOrDefault();

            var data = new TR_PaymentDetail
            {
                entityID = 1,
                bankName = "-",
                chequeNo = "-",
                dueDate = DateTime.Now,
                ket = "Write Off",
                othersTypeCode = "ADW",
                paymentHeaderID = input.paymentHeaderID,
                payNo = input.payNo,
                payTypeID = getPayTypeID,
                status = "C",
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
                    , Environment.NewLine, 1, "-", "-", DateTime.Now, "ADW", input.paymentHeaderID, input.payNo, getPayTypeID, "C", "Write Off");

                var paymentDetailID = _trPaymentDetailRepo.InsertAndGetId(data);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("CreateTrPaymentDetail() - Ended insert TR Payment Detail.");
                Logger.Info("CreateTrPaymentDetail() - Finished.");
                return paymentDetailID;

            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreateTrPaymentDetail() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateTrPaymentDetail() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        public void CreateTrPaymentDetailAlloc(CreateTrPaymentDetailAllocInputDto input)
        {
            var data = new TR_PaymentDetailAlloc
            {
                entityID = 1,
                netAmt = input.netAmt,
                paymentDetailID = input.paymentDetailID,
                schedNo = input.schedNo,
                vatAmt = input.vatAmt
            };

            try
            {
                _trPaymentDetailAllocRepo.Insert(data);
                CurrentUnitOfWork.SaveChanges();
            }
            catch (DataException ex)
            {
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        public JObject GenerateTransNo(GenerateTransNoInputDto input)
        {
            JObject obj = new JObject();
            string transNo = "";
            var year = DateTime.Now.Year.ToString();
            string accCode = (from A in _msAccountRepo.GetAll()
                              where A.Id == input.accID
                              select A.accCode).FirstOrDefault();

            var checkSysFinance = (from sfc in _sysFinanceCounterRepo.GetAll()
                                   where sfc.accID == input.accID && sfc.entityID == input.entityID && sfc.year == year
                                   select sfc);
            //kalau belum ada
            if (!checkSysFinance.Any())
            {
                var dataToInsert = new SYS_FinanceCounter
                {
                    accID = input.accID == 0 ? 958 : input.accID,
                    entityID = input.entityID,
                    year = year,
                    transNo = 1,
                    TTBGNo = 0,
                    ADJNo = 0,
                    pmtBatchNo = 0
                };

                _sysFinanceCounterRepo.Insert(dataToInsert);

                //generate transNo
                int i = 1;
                string incNo1 = i.ToString("D6");

                transNo = accCode + "/" + year + "/" + incNo1;

            }
            else
            {
                var getDataSysFinance = checkSysFinance.FirstOrDefault();

                var update = getDataSysFinance.MapTo<SYS_FinanceCounter>();

                update.transNo = getDataSysFinance.transNo + 1;

                _sysFinanceCounterRepo.Update(update);

                //generate transNo
                string incNo = getDataSysFinance.transNo.ToString("D6");

                transNo = accCode + "/" + year + "/" + incNo;
            }
            obj.Add("transNo", transNo);
            return obj;
        }

        public void WriteOff(GetPSASParamsDto input)
        {
            var getParamsPSAS = _iPriceAppService.GetParameter(input);

            //get data schedule per schedNo
            var getSchedulePerSchedNo = (from a in _trBookingDetailScheduleRepo.GetAll()
                                         join b in _lkAllocRepo.GetAll() on a.allocID equals b.Id
                                         join c in _lkPayForRepo.GetAll() on b.payForID equals c.Id
                                         join d in _trBookingDetailRepo.GetAll() on a.bookingDetailID equals d.Id
                                         join e in _lkAllocRepo.GetAll() on a.allocID equals e.Id
                                         where d.bookingHeaderID == getParamsPSAS.bookingHeaderID && b.allocCode != "PEN"
                                         orderby a.schedNo
                                         group a by new
                                         {
                                             d.bookingHeaderID,
                                             a.allocID,
                                             b.payForID,
                                             a.schedNo
                                         } into G
                                         select new
                                         {
                                             G.Key.bookingHeaderID,
                                             G.Key.allocID,
                                             G.Key.payForID,
                                             G.Key.schedNo,
                                             netAmt = G.Sum(x => x.netAmt),
                                             VATAmt = G.Sum(x => x.vatAmt),
                                             netOut = G.Sum(x => x.netOut),
                                             VATOut = G.Sum(x => x.vatOut),
                                             totalOut = G.Sum(x => x.netOut) + G.Sum(x => x.vatOut)
                                         }).ToList();

            var lastSchedNo = (from A in getSchedulePerSchedNo
                               select A.schedNo).TakeLast(1).FirstOrDefault();

            var needToUpdate = (from A in getSchedulePerSchedNo
                                where ((A.totalOut <= 100 && A.totalOut >= -100) && A.schedNo != lastSchedNo && A.totalOut != 0) || ((A.totalOut <= 10000 && A.totalOut >= -10000) && A.schedNo == lastSchedNo && A.totalOut != 0)
                                select A).ToList();

            var accID = (from A in _trPaymentHeaderRepo.GetAll()
                         join B in _msAccountRepo.GetAll() on A.accountID equals B.Id
                         where A.bookingHeaderID == getParamsPSAS.bookingHeaderID
                         select B.Id).FirstOrDefault();

            var payForID = (from A in _lkPayForRepo.GetAll()
                            where A.payForCode == "PMT"
                            select A.Id).FirstOrDefault();
            //in the middle
            if (needToUpdate.Any())
            {
                foreach (var item in needToUpdate)
                {
                    //insert payment header
                    var dataPaymentHeader = new CreateTrPaymentHeaderInputDto
                    {
                        accountID = accID,
                        bookingHeaderID = getParamsPSAS.bookingHeaderID,
                        payForID = payForID
                    };

                    var paymentHeaderID = CreateTrPaymentHeader(dataPaymentHeader);

                    //insert payment detail
                    var dataPaymentDetail = new CreateTrPaymentDetailInputDto
                    {
                        paymentHeaderID = paymentHeaderID,
                        payNo = 1
                    };

                    var paymentDetailID = CreateTrPaymentDetail(dataPaymentDetail);

                    //insert payment detail alloc
                    var dataPaymentDetailAlloc = new CreateTrPaymentDetailAllocInputDto
                    {
                        schedNo = item.schedNo,
                        entityID = 1,
                        netAmt = item.netOut,
                        vatAmt = item.VATOut,
                        paymentDetailID = paymentDetailID
                    };

                    CreateTrPaymentDetailAlloc(dataPaymentDetailAlloc);

                    //update
                    var getDataSchedulePerDetail = (from A in _trBookingDetailScheduleRepo.GetAll()
                                                    join B in _trBookingDetailRepo.GetAll() on A.bookingDetailID equals B.Id
                                                    where A.schedNo == item.schedNo && B.bookingHeaderID == item.bookingHeaderID
                                                    select A).ToList();

                    foreach (var updateOut in getDataSchedulePerDetail)
                    {
                        var dataBookingDetailSchedule = updateOut.MapTo<TR_BookingDetailSchedule>();

                        dataBookingDetailSchedule.netOut = 0;
                        dataBookingDetailSchedule.vatOut = 0;

                        _trBookingDetailScheduleRepo.Update(dataBookingDetailSchedule);
                    }
                }
            }
            else
            {
                throw new UserFriendlyException("There in no data to write off!");
            }
        }

        public void SetDP(SetDPScheduleInputDto input)
        {
            var getDataDPLama = (from A in _trBookingHeaderRepo.GetAll()
                                 join B in _trBookingDetailRepo.GetAll() on A.Id equals B.bookingHeaderID
                                 join C in _trBookingDetailDPRepo.GetAll() on B.Id equals C.bookingDetailID
                                 where A.Id == input.bookingHeaderID
                                 select new
                                 {
                                     B.bookingHeaderID,
                                     C.Id
                                 } into C
                                 group C by new
                                 {
                                     C.bookingHeaderID,
                                     C.Id
                                 } into G
                                 select new
                                 {
                                     G.Key.bookingHeaderID,
                                     G.Key.Id
                                 }).ToList();

            var getDataDPLamaToInserts = (from A in _trBookingDetailDPRepo.GetAll()
                                          join B in getDataDPLama on A.Id equals B.Id
                                          select A).ToList();

            //insert to history table
            foreach (var dataDPLamaToInsert in getDataDPLamaToInserts)
            {
                var dataHistory = new TR_DPHistory
                {
                    DPAmount = dataDPLamaToInsert.DPAmount,
                    DPPct = dataDPLamaToInsert.DPPct,
                    dpNo = dataDPLamaToInsert.dpNo,
                    dpCalcID = dataDPLamaToInsert.dpCalcID,
                    daysDue = dataDPLamaToInsert.daysDue,
                    monthsDue = dataDPLamaToInsert.monthsDue,
                    bookingDetailID = dataDPLamaToInsert.bookingDetailID,
                    isSetting = true,
                    entityID = 1,
                };
                _trDPHistoryRepo.Insert(dataHistory);
            }

            //delete
            foreach (var dataDPLamaToDelete in getDataDPLama)
            {
                _trBookingDetailDPRepo.Delete(dataDPLamaToDelete.Id);
            }

            var getBookingDetailID = (from A in _trBookingDetailRepo.GetAll()
                                      where A.bookingHeaderID == input.bookingHeaderID
                                      select A.Id).ToList();

            foreach (var dataDP in input.listDP)
            {
                foreach (var bookingDetailID in getBookingDetailID)
                {
                    var data = new TR_BookingDetailDP
                    {
                        DPAmount = dataDP.DPAmount,
                        DPPct = dataDP.DPPct,
                        dpNo = dataDP.dpNo,
                        dpCalcID = dataDP.DpCalcID == 0 ? null : dataDP.DpCalcID,
                        daysDue = dataDP.daysDue,
                        monthsDue = dataDP.monthsDue,
                        bookingDetailID = bookingDetailID,
                        isSetting = true,
                        entityID = 1,
                    };

                    _trBookingDetailDPRepo.Insert(data);
                }
            }
        }

        public SetDPScheduleInputDto GetDPSchedule(GetPSASParamsDto input)
        {
            var getParamsPSAS = _iPriceAppService.GetParameter(input);

            var getDataDP = (from A in _trBookingHeaderRepo.GetAll()
                             join B in _trBookingDetailRepo.GetAll() on A.Id equals B.bookingHeaderID
                             join C in _trBookingDetailDPRepo.GetAll() on B.Id equals C.bookingDetailID
                             join D in _lkFormulaDpRepo.GetAll() on C.formulaDPID equals D.Id into l1
                             from D in l1.DefaultIfEmpty()
                             where A.Id == getParamsPSAS.bookingHeaderID
                             select new
                             {
                                 B.bookingHeaderID,
                                 C.dpNo,
                                 C.DPPct,
                                 C.DPAmount,
                                 dpCalcID = C.formulaDPID == null ? null : C.formulaDPID,
                                 DPCalcDesc = D.formulaDPDesc == null ? null : D.formulaDPDesc,
                                 C.daysDue,
                                 C.monthsDue
                             } into C
                             group C by new
                             {
                                 C.bookingHeaderID,
                                 C.dpNo,
                                 C.DPPct,
                                 C.DPAmount,
                                 C.dpCalcID,
                                 C.DPCalcDesc,
                                 C.daysDue,
                                 C.monthsDue
                             } into G
                             select new GetDPScheduleListDto
                             {
                                 DPAmount = G.Key.DPAmount,
                                 DPPct = G.Key.DPPct,
                                 dpNo = G.Key.dpNo,
                                 DpCalcID = G.Key.dpCalcID,
                                 DPCalcDesc = G.Key.DPCalcDesc,
                                 daysDue = G.Key.daysDue,
                                 monthsDue = G.Key.monthsDue
                             }
                             ).ToList();

            var result = new SetDPScheduleInputDto
            {
                bookingHeaderID = getParamsPSAS.bookingHeaderID,
                listDP = getDataDP
            };

            return result;
        }

        public void SetINS(SetINSScheduleInputDto input)
        {
            Logger.Info("SetINS() - Started.");

            var getDataINS = (from A in _trBookingHeaderRepo.GetAll()
                              join B in _msTermPmtRepo.GetAll() on A.termID equals B.termID
                              where A.Id == input.bookingHeaderID
                              select B).FirstOrDefault();

            var edit = getDataINS.MapTo<MS_TermPmt>();

            edit.isSetting = true;
            edit.finStartDue = input.dataINS.finStartD;
            edit.finStartM = input.dataINS.finStartM;

            try
            {
                Logger.DebugFormat("SetINS() - Start update data installment. Parameters sent:{0}" +
                        "isSetting      = {1}{0}" +
                        "finStartDue    = {2}{0}" +
                        "finStartM      = {3}"
                        , Environment.NewLine, true, input.dataINS.finStartD, input.dataINS.finStartM);

                _msTermPmtRepo.Update(edit);

                Logger.DebugFormat("SetINS() - End update discount.");
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("SetINS() ERROR DbException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("SetINS() ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }

            Logger.Info("SetINS() - Finished.");
        }

        public SetINSScheduleInputDto GetINSSchedule(GetPSASParamsDto input)
        {
            var getParamsPSAS = _iPriceAppService.GetParameter(input);

            var getDataINS = (from A in _trBookingHeaderRepo.GetAll()
                              join B in _msTermPmtRepo.GetAll() on A.termID equals B.termID
                              where A.Id == getParamsPSAS.bookingHeaderID
                              select new GetINSScheduleListDto
                              {
                                  finStartD = B.finStartDue,
                                  finStartM = B.finStartM
                              }).FirstOrDefault();

            var result = new SetINSScheduleInputDto
            {
                bookingHeaderID = getParamsPSAS.bookingHeaderID,
                dataINS = getDataINS
            };

            return result;
        }

        public List<GetFormulaListDto> GetFormulaDropdown()
        {
            var getDataDpCalc = (from A in _lkFormulaDpRepo.GetAll()
                                 //where A.formulaDPType == "3" || A.DPCalcType == "4"
                                 select new GetFormulaListDto
                                 {
                                     dpCalcID = A.Id,
                                     DPCalcType = A.formulaDPType,
                                     DPCalcDesc = A.formulaDPDesc
                                 }).ToList();

            return getDataDpCalc;
        }
    }
}
