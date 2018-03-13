using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class GetAllocationListDto
    {
        public int allocID { get; set; }

        public string allocCode { get; set; }

        public bool isVat { get; set; }
    }
}
