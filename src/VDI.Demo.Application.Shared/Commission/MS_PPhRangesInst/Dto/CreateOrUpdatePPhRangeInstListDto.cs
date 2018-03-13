using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_PPhRangesInst.Dto
{
    public class CreateOrUpdatePPhRangeInstListDto
    {
        public int schemaID { get; set; }
        public int pphRangeIDInst { get; set; }
        public double pphRangePct { get; set; }
        public DateTime validDate { get; set; }
        public string taxCode { get; set; }
        public bool isComplete { get; set; }
        public bool isActive { get; set; }
    }
}
