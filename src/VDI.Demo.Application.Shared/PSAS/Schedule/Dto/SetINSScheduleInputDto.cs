using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.Dto;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class SetINSScheduleInputDto
    {
        public int? bookingHeaderID { get; set; }

        public GetINSScheduleListDto dataINS { get; set; }
    }
}
