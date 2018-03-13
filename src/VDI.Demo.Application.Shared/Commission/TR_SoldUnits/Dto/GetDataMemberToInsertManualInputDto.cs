using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetDataMemberToInsertManualInputDto
    {
        public TR_SoldUnitListDto input { get; set; }
        public List<TR_SoldRequirementListDto> inputRequirement { get; set; }
        public int limitAsUplineNo { get; set; }
    }
}
