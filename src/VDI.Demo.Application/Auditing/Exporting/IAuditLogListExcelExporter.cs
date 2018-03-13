using System.Collections.Generic;
using VDI.Demo.Auditing.Dto;
using VDI.Demo.Dto;

namespace VDI.Demo.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
