using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_PPhRangesInst.Dto
{
    public class GetAllPPhRangeInstListDto
    {
        public int Id { get; set; }
        public DateTime validDate { get; set; }
        public double pphRangePct { get; set; }
        public string taxCode { get; set; }
        public bool isActive { get; set; }
    }
}
