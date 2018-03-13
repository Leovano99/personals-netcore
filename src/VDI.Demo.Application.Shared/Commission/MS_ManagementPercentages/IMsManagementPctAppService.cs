using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Commission.MS_ManagementPercentages.Dto;

namespace VDI.Demo.Commission.MS_ManagementPercentages
{
    public interface IMsManagementPctAppService : IApplicationService
    {
        void CreateMsManagementPct(List<InputManagementPctDto> input);
        void UpdateMsManagementPct(InputManagementPctDto input);
        void DeleteMsManagementPct(int Id);
        ListResultDto<GetAllManagementPctListDto> GetMsManagementPctBySchemaId(int schemaID);
    }
}
