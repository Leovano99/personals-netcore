using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.KuasaDireksi.Dto
{
    public class GetPSASListOfKuasaDireksiListDto
    {
        public int kuasaDireksiID { get; set; }
        public string kuasaDireksiCode { get; set; }
        public int docID { get; set; }
        public string docCode { get; set; }
        public int entityID { get; set; }
        public int projectID { get; set; }
        public string projectCode { get; set; }
        public string projectName { get; set; }
        public string remarks { get; set; }
        public string suratKuasaImage { get; set; }
        public bool isActive { get; set; }
    }
}
