using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class DeleteMsGroupSchemaRequirementInputDto
    {
        public byte reqNo { get; set; }

        public List<int> groupSchemaID { get; set; }
    }
}
