using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Commission.MS_PPhRanges.Dto;

namespace VDI.Demo.Commission.MS_PPhRanges
{
    public interface IMsPPhRangeAppService : IApplicationService
    {
        ListResultDto<CreateOrUpdatePPhRangeListDto> GetMsPPhRangeBySchemaId(int schemaID);
        void CreateMsPPhRange(List<CreateOrUpdatePPhRangeListDto> input);
        void UpdateMsPPhRange(CreateOrUpdatePPhRangeListDto input);
        void DeleteMsPPhRange(int Id);
    }
}
