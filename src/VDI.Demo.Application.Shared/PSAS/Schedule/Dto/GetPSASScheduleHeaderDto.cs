using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class GetPSASScheduleHeaderDto
    {
        public string term { get; set; }
        public decimal totalNetAmount { get; set; }
        public decimal totalVATAmount { get; set; }
    }
}
