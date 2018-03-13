using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class GetPSASScheduleParamDto
    {
        public string bookCode { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public string coCode { get; set; }
    }
}
