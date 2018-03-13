using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.Commission.MS_GroupSchemas.Dto;

namespace VDI.Demo.Commission.MS_GroupSchemas
{
    public interface IMsGroupSchemaAppService : IApplicationService
    {
        Task<PagedResultDto<GetAllMsGroupSchemaListDto>> GetAllMsGroupSchema(GetMsGroupSchemaListInput input, bool isComplete);
        List<ReturnMsGroupSchemaDto> CreateOrUpdateMsGroupSchema(CreateOrUpdateSetGroupSchemaInputDto input);
        void CreateOrUpdateMsGroupRequirement(CreateMsGroupSchemaRequirementInputDto input);
        List<GetDropDownSchemaByGroupSchemaIdListDto> GetDropDownSchemaByGroupSchemaId(List<int> groupSchemaID, bool isComplete);
        List<GetAllMsGroupCommPctListDto> GetAllMsGroupCommPct(List<ReturnMsGroupSchemaDto> input, string flag);
        void CreateOrUpdateMsGroupCommPct(List<CreateOrUpdateSetPercentCommGroupInputDto> input, string flag);
        void DeleteMsGroupSchema(string groupSchemaCode);
        void DeleteSchemaByGroupSchema(int Id);
        void DeleteMsGroupSchemaReq(DeleteMsGroupSchemaRequirementInputDto input);
        void DeleteMsGroupCommPct(List<int> Id, bool isStandard, string flag);
        GetDetailMsGroupSchemaListDto GetDetailMsGroupSchema(string groupSchemaCode, bool isComplete);
        List<GetMsGroupSchemaRequirementListDto> GetMsGroupRequirement(int groupSchemaID, bool isComplete);
        void BackToLatest(string groupSchemaCode);
        void UpdateIsComplete(List<int> groupSchemas);
        List<GetDropDownClusterByProjectListDto> GetDropDownClusterByProject(int projectID);

        List<GetDataMsGroupSchemaReqListDto> GetDataMsGroupSchemaReq(int schemaID, int projectID);
    }
}
