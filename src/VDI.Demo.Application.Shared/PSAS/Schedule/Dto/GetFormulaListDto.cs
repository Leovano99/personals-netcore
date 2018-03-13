using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.Dto;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class GetFormulaListDto
    {
        public int dpCalcID { get; set; }

        public string DPCalcType { get; set; }

        public string DPCalcDesc { get; set; }
    }
}
