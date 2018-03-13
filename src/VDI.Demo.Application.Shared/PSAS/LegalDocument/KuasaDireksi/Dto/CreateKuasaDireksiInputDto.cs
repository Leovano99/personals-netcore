using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.KuasaDireksi.Dto
{
    public class CreateKuasaDireksiInputDto
    {
        public int? kuasaDireksiID { get; set; }
        public int projectID { get; set; }
        public int docID { get; set; }
        public string linkFileKuasaDireksi { get; set; }
        public string remarks { get; set; }
        public bool isActive { get; set; }
        public List<CreateKuasaDireksiPeopleInputDto> kuasaDireksiPeople { get; set; }
    }
}
