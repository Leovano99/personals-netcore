using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.Dto;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class SetDPScheduleInputDto
    {
        public int? bookingHeaderID { get; set; }

        public List<GetDPScheduleListDto> listDP { get; set; }
    }
}
