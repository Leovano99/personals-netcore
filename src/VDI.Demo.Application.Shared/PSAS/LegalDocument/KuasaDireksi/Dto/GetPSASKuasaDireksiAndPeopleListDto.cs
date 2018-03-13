using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.KuasaDireksi.Dto
{
    public class GetPSASKuasaDireksiAndPeopleListDto
    {
        public GetDetailPSASListOfKuasaDireksiListDto GetDetailKuasaDireksi { get; set; }
        public List<GetPSASListOfKuasaDireksiPeopleListDto> GetDetailKuasaDireksiPeople { get; set; }
    }
}
