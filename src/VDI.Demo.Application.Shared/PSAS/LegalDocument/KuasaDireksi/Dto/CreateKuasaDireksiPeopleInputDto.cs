using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.KuasaDireksi.Dto
{
    public class CreateKuasaDireksiPeopleInputDto
    {
        public string name { get; set; }
        public string position { get; set; }
        public string email { get; set; }
        public string noTelp { get; set; }
        public string signatureImage { get; set; }
        public bool isActive { get; set; }
        public int entityID { get; set; }
        public int kuasaDireksiID { get; set; }
        public int? officerID { get; set; }
    }
}
