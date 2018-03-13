using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.LegalDocument.KuasaDireksi.Dto;

namespace VDI.Demo.PSAS.LegalDocument.KuasaDireksi
{
    public interface IPSASMsKuasaDireksiAppService : IApplicationService
    {
        PagedResultDto<GetPSASListOfKuasaDireksiListDto> GetPSASListOfKuasaDireksi(PSASListOfKuasaDireksiInputDto input);
        List<GetPSASListOfKuasaDireksiListDto> GetAllPSASListOfKuasaDireksi();
        GetDetailPSASListOfKuasaDireksiListDto GetPSASListOfKuasaDireksiByDireksiID(int id);
        List<GetPSASListOfKuasaDireksiPeopleListDto> GetPSASKuasaDireksiPeopleByKuasaDireksiID(int kuasaDireksiID);
        GetPSASKuasaDireksiAndPeopleListDto GetPSASKuasaDireksiAndPeople(int id);
        void CreateUniversalKuasaDireksi(CreateKuasaDireksiInputDto input);
        void CreateKuasaDireksiPeople(List<CreateKuasaDireksiPeopleInputDto> dataKuasaDireksiPeople);        
        void DeleteKuasaDireksi(int kuasaDireksiID);
        List<GetDetailPSASListOfKuasaDireksiListDto> GetDropdownPSASListOfKuasaDireksi(string bookCode, int docID);
    }
}
