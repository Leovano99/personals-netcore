using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.OnlineBooking.Email.Dto;

namespace VDI.Demo.OnlineBooking.Email
{
    public interface IEmailAppService : IApplicationService
    {
        void ConfigurationEmail(SendEmailInputDto input);

        string bodyAfterReserved(AfterReservedInputDto input);

        string Reminder2Jam(Reminder2JamInputDto input);

        string UnitExpired(UnitExpiredInputDto input);

        string bookingSuccess(BookingSuccessInputDto input);
    }
}
