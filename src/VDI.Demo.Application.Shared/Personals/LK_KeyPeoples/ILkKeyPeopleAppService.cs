using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_KeyPeoples.Dto;

namespace VDI.Demo.Personals.LK_KeyPeoples
{
    public interface ILkKeyPeopleAppService : IApplicationService
    {
        ListResultDto<GetAllLkKeyPeopleDropdwonListDto> GetAllLkKeyPeopleDropdwon();
    }
}
