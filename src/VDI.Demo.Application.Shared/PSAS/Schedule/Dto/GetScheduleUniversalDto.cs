using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class GetScheduleUniversalDto
    {
        public double pctTax { get; set; }
        public List<GetScheduleListDto> dataSchedule { get; set; }
    }
}
