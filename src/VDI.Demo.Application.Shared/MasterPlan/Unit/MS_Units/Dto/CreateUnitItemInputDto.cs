using System;
using System.Collections.Generic;

namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class CreateUnitItemInputDto
    {
        public int unitID { get; set; }
        public List<UnitItemDto> unitItem { get; set; }
    }
}
