using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.MappingTemplate.Dto
{
    public class CheckDataUpdateIsExistListDto
    {
        public int? mappingTemplateID { get; set; }
        public bool isExist { get; set; }
        public DateTime? activeToOldData { get; set; }
        public DateTime? activeTo { get; set; }
    }
}
