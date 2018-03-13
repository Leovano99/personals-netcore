using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.Personals.Personals.Dto;

namespace VDI.Demo.Personals.Personals
{
    public interface IPersonalAppService : IApplicationService
    {
        Task<PagedResultDto<GetPersonalsByKeywordList>> GetPersonalsByKeyword(GetPersonalsByKeywordInputDto input);
        List<GetPersonalsByKeywordList> GetPersonalsByAdvanceSearch(GetPersonalsByAdvanceSearchInputDto input);

        string generatePsCode();

        Task CreateKeyPeople(List<CreateKeyPeopleDto> inputs);
        Task CreatePersonal(CreatePersonalDto input);
        Task CreateBankAccount(List<CreateBankAccountDto> inputs);
        Task CreateCompany(List<CreateCompanyDto> inputs);
        Task CreateDocument(List<CreateDocumentDto> inputs);
        Task CreateFamily(List<CreateFamilyDto> inputs);
        Task CreateIDNumber(List<CreateIDNumberDto> inputs);
        JObject CreateMember(CreateMemberDto input);

        Task UpdatePersonal(CreatePersonalDto input);
        Task CreateOrUpdatePhone(List<CreatePhoneDto> inputs);
        Task CreateOrUpdateEmail(List<CreateEmailDto> inputs);
        Task CreateOrUpdateAddress(List<CreateAddressDto> inputs);

        GetUniversalPersonalDto GetUniversalPersonal(string psCode);
        GetPersonalDto GetPersonalByPsCode(string psCode);
        GetContactDto GetContactByPsCode(string psCode);
        List<GetPhoneDto> GetPhoneByPsCode(string psCode);
        List<GetEmailDto> GetEmailByPsCode(string psCode);
        List<GetAddressDto> GetAddressByPsCode(string psCode);
        List<GetKeyPeopleDto> GetKeyPeopleByPsCode(string psCode);
        List<GetBankAccountDto> GetBankAccountByPsCode(string psCode);
        List<GetDocumentDto> GetDocumentByPsCode(string psCode);
        List<GetCompanyDto> GetCompanyByPsCode(string psCode);
        List<GetIDNumberDto> GetIDNumberByPsCode(string psCode);
        List<GetFamilyDto> GetFamilyByPsCode(string psCode);
        List<GetMemberDto> GetMemberByPsCode(string psCode);

        ListResultDto<GetAllMsSchemaDropdownList> GetAllMsSchemaDropdown();
        GetAvailableMemberSchemaByScmCodeAndPsCode GetAvailableMemberSchemaByScmCodeAndPsCode(GetAvailableMemberSchemaByScmCodeAndPsCode input);
    }
}
