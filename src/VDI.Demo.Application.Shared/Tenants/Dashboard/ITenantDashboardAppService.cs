using Abp.Application.Services;
using VDI.Demo.Tenants.Dashboard.Dto;

namespace VDI.Demo.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();

        GetDashboardDataOutput GetDashboardData(GetDashboardDataInput input);

        GetSalesSummaryOutput GetSalesSummary(GetSalesSummaryInput input);

        GetWorldMapOutput GetWorldMap(GetWorldMapInput input);

        GetGeneralStatsOutput GetGeneralStats(GetGeneralStatsInput input);
    }
}
