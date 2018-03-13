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

namespace VDI.Demo.PSAS.Term
{
    public class PSASTermAppService : DemoAppServiceBase, IPSASTermAppService
    {
        private readonly IRepository<TR_BookingHeader> _trBookingHeaderRepo;
        private readonly IRepository<MS_Bank> _msBankRepo;
        private readonly IRepository<MS_Term> _msTermRepo;
        private readonly IRepository<MS_TermPmt> _msTermPmtRepo;
        private readonly IRepository<LK_FinType> _lkFinTypeRepo;
        private readonly IPSASPriceAppService _iPriceAppService;
        private readonly IRepository<TR_BookingHeaderHistory> _trBookingHeaderHistory;

        public PSASTermAppService(
            IRepository<TR_BookingHeader> trBookingHeaderRepo,
            IRepository<MS_Bank> msBankRepo,
            IRepository<MS_Term> msTermRepo,
            IRepository<MS_TermPmt> msTermPmtRepo,
            IRepository<LK_FinType> lkFinTypeRepo,
            IPSASPriceAppService iPriceAppService,
            IRepository<TR_BookingHeaderHistory> trBookingHeaderHistory
            )
        {
            _trBookingHeaderRepo = trBookingHeaderRepo;
            _msBankRepo = msBankRepo;
            _msTermRepo = msTermRepo;
            _msTermPmtRepo = msTermPmtRepo;
            _lkFinTypeRepo = lkFinTypeRepo;
            _iPriceAppService = iPriceAppService;
            _trBookingHeaderHistory = trBookingHeaderHistory;
        }

        public GetPSASTermDto GetTermByBookCode(GetPSASParamsDto input)
        {

            var unitID = _iPriceAppService.GetParameter(input);

            var getData = (from bh in _trBookingHeaderRepo.GetAll()
                           join b in _msBankRepo.GetAll() on bh.KPRBankCode equals b.bankCode into l1
                           from b in l1.DefaultIfEmpty()
                           join t in _msTermRepo.GetAll() on bh.termID equals t.Id into l2
                           from t in l2.DefaultIfEmpty()
                           join pt in _msTermPmtRepo.GetAll() on t.Id equals pt.termID into l3
                           from pt in l3.DefaultIfEmpty()
                           join ft in _lkFinTypeRepo.GetAll() on pt.finTypeID equals ft.Id into l4
                           from ft in l4.DefaultIfEmpty()
                           where bh.unitID == unitID.unitID && bh.cancelDate == null
                           select new GetPSASTermDto
                           {
                               termCode = t == null ? null : t.termCode,
                               termNo = t == null ? Convert.ToInt16(0) : t.termNo,
                               remarksTerm = t == null ? null : t.remarks,
                               PPJBDue = bh.PPJBDue,
                               bankName = b == null ? null : b.bankName,
                               DPCalcType = bh.DPCalcType,
                               finType = ft == null ? null : ft.finTypeDesc,
                               finStatrtDue = pt == null ? Convert.ToInt16(0) : pt.finStartDue,
                               unitID = unitID.unitID,
                               termID = t == null ? 0 : t.Id
                           }).FirstOrDefault();

            return getData;
        }

        public List<GetMsTermByTermCodeDto> GetTermByCodeDropdown(string termCode)
        {
            var getData = (from t in _msTermRepo.GetAll()
                           where t.termCode == termCode
                           orderby t.termNo
                           select new GetMsTermByTermCodeDto
                           {
                               termID = t.Id,
                               termNo = t.termNo,
                               remarks = t.remarks
                           }).ToList();

            return getData;
        }

        public GetTermPmtByTermIdDto GetTermPmt(int termID)
        {
            var getData = (from t in _msTermRepo.GetAll()
                           join pt in _msTermPmtRepo.GetAll() on t.Id equals pt.termID
                           join ft in _lkFinTypeRepo.GetAll() on pt.finTypeID equals ft.Id
                           where t.Id == termID
                           select new GetTermPmtByTermIdDto
                           {
                               finType = ft.finTypeDesc,
                               finStatrtDue = pt.finStartDue
                           }).FirstOrDefault();

            return getData;
        }

        public void UpdateTerm(UpdateTermInputDto input)
        {
            Logger.Info("UpdateTerm() - Started.");

            Logger.DebugFormat("UpdateTerm() - Start checking before update Term in TR Booking Header. Parameters sent:{0}" +
                        "unitID = {1}{0}"
                        , Environment.NewLine, input.unitID);

            var check = (from bh in _trBookingHeaderRepo.GetAll()
                         where bh.unitID == input.unitID && bh.cancelDate == null
                         select bh).FirstOrDefault();

            Logger.DebugFormat("UpdateTerm() - Ended checking before update Term in TR Booking Header.");

            if (check != null)
            {
                //history

                var checkHistory = (from A in _trBookingHeaderHistory.GetAll()
                                    orderby A.Id descending
                                    where A.bookCode == check.bookCode
                                    select A).FirstOrDefault();

                var dataToInsertHistory = new TR_BookingHeaderHistory
                {
                    bankName = check.bankName,
                    bankNo = check.bankNo,
                    bankRekeningPemilik = check.bankRekeningPemilik,
                    BFPayTypeCode = check.BFPayTypeCode,
                    bookCode = check.bookCode,
                    bookDate = check.bookDate,
                    discBFCalcType = check.discBFCalcType,
                    cancelDate = check.cancelDate,
                    DPCalcType = check.DPCalcType,
                    entityID = check.entityID,
                    eventID = check.eventID,
                    facadeID = check.facadeID,
                    SADStatusID = check.SADStatusID,
                    scmCode = check.scmCode,
                    shopBusinessID = check.shopBusinessID,
                    isPenaltyStop = check.isPenaltyStop,
                    promotionID = check.promotionID,
                    isSK = check.isSK,
                    isSMS = check.isSMS,
                    KPRBankCode = check.KPRBankCode,
                    memberCode = check.memberCode,
                    sumberDanaID = check.sumberDanaID,
                    memberName = check.memberName,
                    nomorRekeningPemilik = check.nomorRekeningPemilik,
                    PPJBDue = check.PPJBDue,
                    psCode = check.psCode,
                    netPriceComm = check.netPriceComm,
                    NUP = check.NUP,
                    remarks = check.remarks,
                    termID = check.termID,
                    transID = check.transID,
                    termRemarks = check.termRemarks,
                    tujuanTransaksiID = check.tujuanTransaksiID,
                    unitID = check.unitID,
                    historyNo = checkHistory == null ? Convert.ToByte(0) : Convert.ToByte(checkHistory.historyNo + 1)
                };

                var update = check.MapTo<TR_BookingHeader>();

                update.termID = input.termID;

                try
                {
                    _trBookingHeaderHistory.Insert(dataToInsertHistory);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("UpdateTerm() - Start update Term in TR Booking Header. Parameters sent:{0}" +
                        "termID = {1}{0}"
                        , Environment.NewLine, input.termID);

                    _trBookingHeaderRepo.Update(update);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("UpdateTerm() - Ended update Term in TR Booking Header");

                    _iPriceAppService.GeneratePrice(input);
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateTerm() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateTerm() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }

            }
            else
            {
                throw new UserFriendlyException("Booking Cancelled!");
            }

            Logger.Info("UpdateTerm() - Finished.");
        }
    }
}
