using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_PPhRanges.Dto
{
    public class CreateOrUpdatePPhRangeListDto
    {
        public int schemaID { get; set; }
        public int? pphRangeID { get; set; }
        public int pphYear { get; set; }
        public decimal pphRangeHighBound { get; set; }
        public double pphRangePct { get; set; }
        public string tax_code { get; set; }
        public string tax_code_non_npwp { get; set; }
        public string pphRangePct_non_npwp { get; set; }
        public bool isActive { get; set; }

    }
}
