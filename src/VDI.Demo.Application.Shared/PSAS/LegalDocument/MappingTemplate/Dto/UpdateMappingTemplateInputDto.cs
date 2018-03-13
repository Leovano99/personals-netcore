using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.MappingTemplate.Dto
{
    public class UpdateMappingTemplateInputDto
    {
        public int entityID { get; set; }
        public int mappingTemplateID { get; set; }
        public int projectID { get; set; }
        public int docID { get; set; }
        public int docTemplateID { get; set; }
        public DateTime activeFrom { get; set; }
        public DateTime? activeTo { get; set; }
        public bool isTandaTerima { get; set; }
        public bool isActive { get; set; }
    }
}
