using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class GetScheduleUniversalsDto
    {
        public double pctTax { get; set; }
        public List<GetSchedulerListDto> dataSchedule { get; set; }
    }
}
