using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.Commission.MS_Developer_Schemas.Dto;

namespace VDI.Demo.Commission.MS_Developer_Schemas
{
    public interface IMsDeveloperSchemasAppService : IApplicationService
    {
        ListResultDto<GetDeveloperSchemasListDto> GetMsDeveloperSchemas();
        ListResultDto<GetDeveloperSchemasListDto> GetAllMsDeveloperSchemaPaging();
        ListResultDto<GetDeveloperSchemasListDto> GetMsDeveloperSchemasBySchema(int SchemaID);
        ListResultDto<GetDropDownDeveloperSchemasListDto> GetDropDownMsDeveloperSchemasBySchema(int SchemaID);
        GetDeveloperSchemasListDto GetDetailMsDeveloperSchemas(int Id);
        ListResultDto<GetPropCodeListDto> GetPropCodeBySchemaID(int schemaID);
        List<GetDropDownDeveloperSchemasListDto> GetDataDeveloperSchemaByProperty(int propertyID);
        string GetPropNameByPropCode(string propCode);

        void CreateMsDeveloperSchemas(CreateMsDeveloperSchemasInputDto input);
        void UpdateMsDeveloperSchemas(UpdateMsDeveloperSchemasInputDto input);
        void DeleteMsDeveloperSchemas(int Id);
    }
}
