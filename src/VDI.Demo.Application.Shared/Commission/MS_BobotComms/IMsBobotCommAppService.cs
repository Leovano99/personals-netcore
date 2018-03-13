using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Commission.MS_BobotComms.Dto;

namespace VDI.Demo.Commission.MS_BobotComms
{
    public interface IMsBobotCommAppService : IApplicationService
    {
        ListResultDto<MsBobotCommListDto> GetMsBobotCommByProject(int projectID);
        void CreateMsBobotComm(List<MsBobotCommListDto> input);
        void UpdateMsBobotComm(MsBobotCommListDto input);
        void DeleteMsBobotComm(int Id);
    }
}
