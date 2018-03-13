using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetDocumentDto
    {
        public string psCode { get; set; }
        public string documentType { get; set; }
        public string documentTypeName { get; set; }
        public string filename { get; set; }
        public string documentBinary { get; set; }
        public string LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public string CreationTime { get; set; }
        public string CreatorUserId { get; set; }
    }
}
