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
    public class PSASPaymentAppService : DemoAppServiceBase, IPSASPaymentAppService
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

        public PSASPaymentAppService(
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
            IRepository<LK_PayFor> lkPayFor
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
            _lkPayForRepo = lkPayFor;
        }

        public List<GetPSASPaymentDto> GetOtherPaymentByBookCode(GetPSASParamsDto input)
        {
            var bhID = _iPriceAppService.GetParameter(input);

            var getData = (from pda in _trPaymentDetailAllocRepo.GetAll()
                           join pd in _trPaymentDetailRepo.GetAll() on new { S1 = pda.paymentDetailID, S2 = "C" } equals new { S1 = pd.Id, S2 = pd.status }// into l1
                           //from pd in l1.DefaultIfEmpty()
                           join ph in _trPaymentHeaderRepo.GetAll() on new { S1 = pd.paymentHeaderID, S2 = bhID.bookingHeaderID } equals new { S1 = ph.Id, S2 = ph.bookingHeaderID } into l2
                           from ph in l2.DefaultIfEmpty()
                           join pf in _lkPayForRepo.GetAll() on ph.payForID equals pf.Id
                           join b in _msBankRepo.GetAll() on pd.bankName equals b.bankCode into l3
                           from b in l3.DefaultIfEmpty()
                           join bb in _msBankBranchRepo.GetAll() on b.Id equals bb.bankID into l4
                           from bb in l4.DefaultIfEmpty()
                           join a in _msAccountRepo.GetAll() on ph.accountID equals a.Id into l5
                           from a in l5.DefaultIfEmpty()
                           join pt in _lkPayTypeRepo.GetAll() on pd.payTypeID equals pt.Id into l6
                           from pt in l6.DefaultIfEmpty()
                           where pf.payForCode == "OTP"
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
                                pda.vatAmt
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
                               pda.bankBranchName
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
                               vatAmount = G.Sum(x => x.vatAmt)
                           }).ToList();

            List<GetPSASPaymentDto> listResult = new List<GetPSASPaymentDto>();

            foreach (var item in getData)
            {
                TypeDto type = new TypeDto
                {
                    payFor = item.payForCode,
                    payType = item.payTypeCode,
                    otherType = item.othersTypeCode
                };

                GetPSASPaymentDto result = new GetPSASPaymentDto
                {
                    bankName = item.bankName,
                    bankBranch = item.bankBranchName,
                    account = item.accCode,
                    transNo = item.transNo,
                    PMTDate = item.paymentDate,
                    clearDate = item.clearDate,
                    type = type,
                    netAmount = item.netAmount,
                    vatAmt = item.vatAmount
                };

                listResult.Add(result);
            }
            return listResult;
        }

        public List<GetPSASPaymentDto> GetPaymentByBookCode(GetPSASParamsDto input)
        {
            var bhID = _iPriceAppService.GetParameter(input);

            var getData = (from pda in _trPaymentDetailAllocRepo.GetAll()
                           join pd in _trPaymentDetailRepo.GetAll() on new { S1 = pda.paymentDetailID, S2 = "C" } equals new { S1 = pd.Id, S2 = pd.status } //into l1
                           //from pd in l1.DefaultIfEmpty()
                           join ph in _trPaymentHeaderRepo.GetAll() on new { S1 = pd.paymentHeaderID, S2 = bhID.bookingHeaderID } equals new { S1 = ph.Id, S2 = ph.bookingHeaderID } //into l2
                           //from ph in l2.DefaultIfEmpty()
                           join pf in _lkPayForRepo.GetAll() on ph.payForID equals pf.Id
                           join b in _msBankRepo.GetAll() on pd.bankName equals b.bankCode into l3
                           from b in l3.DefaultIfEmpty()
                           join bb in _msBankBranchRepo.GetAll() on b.Id equals bb.bankID into l4
                           from bb in l4.DefaultIfEmpty()
                           join a in _msAccountRepo.GetAll() on ph.accountID equals a.Id into l5
                           from a in l5.DefaultIfEmpty()
                           join pt in _lkPayTypeRepo.GetAll() on pd.payTypeID equals pt.Id into l6
                           from pt in l6.DefaultIfEmpty()
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
                               pda.vatAmt
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
                               pda.bankBranchName
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
                               vatAmount = G.Sum(x => x.vatAmt)
                           }).ToList();

            List<GetPSASPaymentDto> listResult = new List<GetPSASPaymentDto>();

            foreach (var item in getData)
            {
                TypeDto type = new TypeDto
                {
                    payFor = item.payForCode,
                    payType = item.payTypeCode,
                    otherType = item.othersTypeCode
                };

                GetPSASPaymentDto result = new GetPSASPaymentDto
                {
                    bankName = item.bankName,
                    bankBranch = item.bankBranchName,
                    account = item.accCode,
                    transNo = item.transNo,
                    PMTDate = item.paymentDate,
                    clearDate = item.clearDate,
                    type = type,
                    netAmount = item.netAmount,
                    vatAmt = item.vatAmount
                };

                listResult.Add(result);
            }
            return listResult;
        }
    }
}
