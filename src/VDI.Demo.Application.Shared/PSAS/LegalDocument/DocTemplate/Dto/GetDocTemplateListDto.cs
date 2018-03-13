using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.DocTemplate.Dto
{
    public class GetDocTemplateListDto
    {
        public int docTemplateID { get; set; }
        public string docTemplateCode { get; set; }
        public string docTemplateName { get; set; }
        public string docCode { get; set; }
        public string linkFileDoc { get; set; }
    }
}
