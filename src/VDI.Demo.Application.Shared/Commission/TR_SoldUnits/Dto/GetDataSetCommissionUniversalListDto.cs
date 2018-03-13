using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetDataSetCommissionUniversalListDto
    {
        public GetDataDealCloserDto dataDealCloser { get; set; }
        public List<GetDataSchemaRequirementListDto> dataRequirement { get; set; }
        public List<GetDataAllMemberListDto> dataMember { get; set; }
    }
}
