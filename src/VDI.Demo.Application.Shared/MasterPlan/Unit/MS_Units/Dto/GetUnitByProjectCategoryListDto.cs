using System;
using System.Collections.Generic;

namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class GetUnitByProjectCategoryListDto
    {
        public string floor { get; set; }
        public List<UnitStatus> unit { get; set; }
    }

    public class UnitStatus
    {
        public int unitID { get; set; }
        public string unitNo { get; set; }
        public int? unitStatusID { get; set; }
        public string unitStatusCode { get; set; }
    }
}
