using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_Emails.Dto;

namespace VDI.Demo.Personals.TR_Emails
{
    public interface ITrEmailAppService : IApplicationService
    {
        void UpdateEmail(List<GetUpdateEmailInputDto> inputs);
        void DeleteEmail(GetDeleteEmailInputDto input);
    }
}
