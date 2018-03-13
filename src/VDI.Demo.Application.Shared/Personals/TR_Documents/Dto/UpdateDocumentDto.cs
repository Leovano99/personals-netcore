using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.TR_Documents.Dto
{
    public class UpdateDocumentDto
    {
        public string psCode { get; set; }
        public string documentType { get; set; }
        public string documentBinary { get; set; }
        public string documentBinaryNew { get; set; }
    }
}
