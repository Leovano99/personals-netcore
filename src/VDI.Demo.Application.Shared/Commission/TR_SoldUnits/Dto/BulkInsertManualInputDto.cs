using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class BulkInsertManualInputDto
    {
        public List<TR_SoldUnitListDto> data { get; set; }
        public List<TR_SoldRequirementListDto> dataReq { get; set; }
        public int limitAsUplineNo { get; set; }
    }
}
