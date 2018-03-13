using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetDataSchemaRequirementListDto
    {
        public int reqNo { get; set; }
        public string reqName { get; set; }
        public double paid { get; set; }
        public DateTime? processDate { get; set; }
    }
}
