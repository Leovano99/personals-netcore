using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class CreateDocumentDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public string documentType { get; set; }
        public int? documentRef { get; set; }
        public string documentBinary { get; set; }
        public string documentPicType { get; set; }
    }
}
