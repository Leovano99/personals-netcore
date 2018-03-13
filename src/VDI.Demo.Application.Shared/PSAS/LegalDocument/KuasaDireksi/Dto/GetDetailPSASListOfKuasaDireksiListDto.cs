using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.KuasaDireksi.Dto
{
    public class GetDetailPSASListOfKuasaDireksiListDto
    {
        public int entityID { get; set; }
        public int kuasaDireksiID { get; set; }
        public int kuasaDireksiPeopleID { get; set; }
        public string kuasaDireksiCode { get; set; }
        public int projectID { get; set; }
        public int docID { get; set; }
        public string linkFileKuasaDireksi { get; set; }
        public string linkSignature { get; set; }
        public string remarks { get; set; }
        public bool isActive { get; set; }
        public string namePosition { get; set; }
    }
}
