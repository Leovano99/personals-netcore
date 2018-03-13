using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_IDs.Dto;

namespace VDI.Demo.Personals.TR_IDs
{
    public interface ITrIDAppService : IApplicationService
    {
        void UpdateTrID(UpdateTrIDInputDto input);
        void DeleteTrID(DeleteTrIDInputDto input);
    }
}
