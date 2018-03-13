using Abp.Application.Services;
using VDI.Demo.Dto;
using VDI.Demo.Logging.Dto;

namespace VDI.Demo.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
