using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.OnlineBooking.BookingHistory.Dto;

namespace VDI.Demo.OnlineBooking.BookingHistory
{
    public interface IBookingHistoryAppService : IApplicationService
    {
        DetailBookingHistoryResultDto GetDetailBookingHistory(int orderID);

        ListResultDto<ListBookingHistoryResultDto> GetListBookingHistory(string memberCode);

        List<ListBookingHistoryResultDto> SearchingBookingHistoryMobile(SearchingBookingHistoryInputDto input);
    }
}
