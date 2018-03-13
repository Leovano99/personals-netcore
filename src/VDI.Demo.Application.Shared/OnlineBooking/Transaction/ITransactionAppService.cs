using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.OnlineBooking.PaymentMidtrans.Dto;
using VDI.Demo.OnlineBooking.Transaction.Dto;

namespace VDI.Demo.OnlineBooking.Transaction
{
    public interface ITransactionAppService : IApplicationService
    {
        InsertTrUnitReservedResultDto InsertTrUnitReserved(InsertTRUnitReservedInputDto input);

        //ResultMessageDto UpdatePsCodeTrUnitReserved(UpdateTRUnitReserved input);

        TrUnitOrderHeaderResultDto InsertTrUnitOrderHeader(CreateTransactionUniversalDto input);

        //ResultMessageDto InsertTrUnitOrderDetail(TrUnitOrderDetailInputDto input);

        //TrBookingHeaderResultDto InsertTrBookingHeader(TrBookingHeaderInputDto input);

        //List<bookingDetailIDDto> InsertTrBookingDetail(TrBookingDetailInputDto input);

        //ResultMessageDto InsertTrBookingSalesAddDisc(TrBookingSalesAddDiscInputDto input);

        //ResultMessageDto InsertAddDisc(InsertAddDiscInputDto input);

        //ResultMessageDto InsertTrBookingDetailDP(TrBookingDetailDPInputDto input);

        //ResultMessageDto InsertTrCashAddDisc(TrCashAddDiscInputDto input);

        //ResultMessageDto UpdateBFAmount(UpdateBFAmountInputDto input);

        //ResultMessageDto InsertTrBookingHeaderTerm(InsertTrBookingHeaderTermInputDto input);

        //ResultMessageDto InsertTrBookingItemPrice(InsertTrBookingHeaderTermInputDto input);

        //ResultMessageDto InsertTrBookingTax(InsertTrBookingTaxInputDto input);

        //ResultMessageDto UpdateUnitSold(InsertTrBookingHeaderTermInputDto input);

        //UpdateNetPriceResultDto UpdateNetPriceBookingHeaderDetail(UpdateBookingDetailInputDto input);

        //ResultMessageDto UpdateOrderStatusFullyPaid(UpdateOrderStatusFullyPaid input);

        void SchedulerStatusOrderExpired();

        void SchedulerTRUnitReserved();

        List<GetTrUnitReservedDto> GetTrUnitReserved(int userID);

        ResultMessageDto DeleteTrUnitReserved(int Id);

        ListDetailBookingUnitResultDto GetDetailBookingUnit(int userID, string psCode);

        ResultMessageDto InsertTransactionUniversal(CreateTransactionUniversalDto input);

        Task<PaymentOnlineBookingResponse> DoBookingMidransReq(CreateTransactionUniversalDto input);

        void DoBooking(PaymentOnlineBookingResponse input);
    }
}
