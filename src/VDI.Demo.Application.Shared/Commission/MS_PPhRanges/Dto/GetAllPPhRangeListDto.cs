using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_PPhRanges.Dto
{
    public class GetAllPPhRangeListDto
    {
        public int Id { get; set; }
        public int pphYear { get; set; }
        public decimal pphRangeHighBound { get; set; }
        public double pphRangePct { get; set; }
        public int? schemaID { get; set; }
        public bool isActive { get; set; }
    }
}
