using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.Commission.MS_Schemas.Dto;

namespace VDI.Demo.Commission.MS_Schemas
{
    public interface IMsSchemaAppService : IApplicationService
    {
        Task<PagedResultDto<GetAllMsSchemaListDto>> GetAllMsSchema(GetMsSchemaListInput input);
        List<GetMsSchemaRequirementListDto> GetMsSchemaRequirementBySchemaID(int schemaID, bool isComplete);
        List<GetLkCommTypeListDto> GetLkCommTypeBySchemaID(int schemaID, bool isComplete);
        List<GetMsStatusMemberListDto> GetMsStatusMemberBySchemaID(int schemaID, bool isComplete);
        List<GetLkPointTypeListDto> GetLkPointTypeBySchemaID(int schemaID, bool isComplete);
        List<GetMsCommPctListDto> GetMsCommPctBySchemaID(int schemaID, bool isComplete);
        List<GetLkUplineListDto> GetLkUpline(int schemaID);
        CreateOrUpdateSetSchemaInputDto GetDetailMsSchema(int schemaID);
        void CreateOrUpdateMsSchemaRequirement(CreateMsSchemaRequirementInputDto input);
        void CreateOrUpdateMsStatusMember(CreateMsStatusMemberInputDto input);
        void CreateOrUpdateLkPointType(CreateOrUpdateSetPointTypeInputDto input);
        int? CreateOrUpdateMsSchema(CreateOrUpdateSetSchemaInputDto input);
        void CreateOrUpdateLkCommType(CreateOrUpdateSetCommTypeInputDto input);
        void CreateOrUpdateMsCommPct(CreateOrUpdateMsCommPctInputDto input);
        void UpdateIsComplete(int schemaID, bool flag);
        void DeleteMsSchemaRequirement(int Id, string flag);
        void DeleteMsStatusMember(int Id, string flag);
        void DeleteLkPointType(int Id, string flag);
        void DeleteMsSchema(int Id);
        void DeleteLkCommType(int Id, string flag);
        void DeleteMsCommPct(int Id, string flag);
    }
}
