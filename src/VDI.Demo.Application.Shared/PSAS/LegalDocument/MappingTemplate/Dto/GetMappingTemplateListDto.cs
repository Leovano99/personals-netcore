using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;

namespace VDI.Demo.PSAS.LegalDocument.MappingTemplate.Dto
{
    public class GetMappingTemplateListDto
    {
        public int entityID { get; set; }
        public int mappingTemplateID { get; set; }
        public string mappingTemplateCode { get; set; }
        public int projectID { get; set; }
        public string projectName { get; set; }
        public int docID { get; set; }
        public string docCode { get; set; }
        public int docTemplateID { get; set; }
        public string docTemplateName { get; set; }
        public DateTime activeFrom { get; set; }
        public DateTime? activeTo { get; set; }
        public string period { get; set; }
        public bool isActive { get; set; }
        public bool isTandaTerima { get; set; }
    }
}
