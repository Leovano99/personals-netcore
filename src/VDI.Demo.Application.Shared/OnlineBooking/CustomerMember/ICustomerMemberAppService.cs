using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.OnlineBooking.CustomerMember.Dto;
using VDI.Demo.OnlineBooking.Transaction.Dto;

namespace VDI.Demo.OnlineBooking.CustomerMember
{
    public interface ICustomerMemberAppService : IApplicationService
    {
        Task<MemberActivationResultDto> MemberActivation(SignUpMemberInputDto input);

        Task<SignUpCustomerResultDto> SignUpCustomer(SignUpCustomerInputDto input);

        ListResultDto<ListCustomerResultDto> GetListCustomer(string name, string birthDate, string idNo);

        List<ListNationResultDto> GetNation();

        List<ListCountryResultDto> GetCountry();

        List<ListCityResultDto> GetCity(string country);

        List<ListPostCodeResultDto> GetPostCode(string city);

        DetailCustomerResultDto GetDetailCustomer(string psCode);

        Task<ResultMessageDto> UpdateCustomer(UpdateCustomerInputDto input);

        List<ListDocumentResultDto> GetDocumentByPsCode(string psCode);

        DetailCustomerMobileResultDto GetDetailCustomerMobile(string psCode);
    }
}
