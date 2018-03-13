using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_KeyPeoples.Dto;

namespace VDI.Demo.Personals.TR_KeyPeoples
{
    public interface ITrKeyPeopleAppService : IApplicationService
    {
        void UpdateTrJKeyPeople(UpdateTrJKeyPeopleInputDto input);
        void DeleteTrKeyPeople(int trKeyPeopleID);
    }
}
