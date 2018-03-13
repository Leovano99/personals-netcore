using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDI.Demo.MultiTenancy.HostDashboard.Dto;

namespace VDI.Demo.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}