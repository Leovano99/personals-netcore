using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.Auditing.Dto;
using VDI.Demo.Dto;

namespace VDI.Demo.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);

        Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);
    }
}