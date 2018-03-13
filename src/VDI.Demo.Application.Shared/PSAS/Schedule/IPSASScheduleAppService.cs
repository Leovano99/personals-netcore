using Abp.Application.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.PSAS.Dto;
using VDI.Demo.PSAS.Price.Dto;
using VDI.Demo.PSAS.Schedule.Dto;

namespace VDI.Demo.PSAS.Schedule
{
    public interface IPSASScheduleAppService : IApplicationService
    {
        List<GetAllocationListDto> GetAllocationDropdown();
        List<GetCompanyCodeListDto> GetCompanyCodeList(GetPSASParamsDto input);
        GetPSASScheduleHeaderDto GetPSASScheduleHeader(GetPSASParamsDto input);
        GetScheduleUniversalDto GetOriginalSchedule(int? bookingHeaderID);
        GetScheduleUniversalDto GetPSASScheduleDetail(GetPSASScheduleParamDto input);
        void CreateTrBookingDetailSchedule(CreateTrBookingDetailScheduleParamsDto input);

        void RegenerateSchedule(GetPSASParamsDto input);
        void RecalculateBalance(GetPSASParamsDto input);
        void WriteOff(GetPSASParamsDto input);

        int CreateTrPaymentHeader(CreateTrPaymentHeaderInputDto input);
        JObject GenerateTransNo(GenerateTransNoInputDto input);

        int CreateTrPaymentDetail(CreateTrPaymentDetailInputDto input);
        void CreateTrPaymentDetailAlloc(CreateTrPaymentDetailAllocInputDto input);

        void SetDP(SetDPScheduleInputDto input);
        SetDPScheduleInputDto GetDPSchedule(GetPSASParamsDto input);

        void SetINS(SetINSScheduleInputDto input);
        SetINSScheduleInputDto GetINSSchedule(GetPSASParamsDto input);

        List<GetFormulaListDto> GetFormulaDropdown();
    }
}
