using Abp.Application.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Dto;
using VDI.Demo.MasterPlan.Unit.MS_Units.Dto;
using VDI.Demo.Payment.InputPayment.Dto;
using VDI.Demo.PSAS.Schedule.Dto;

namespace VDI.Demo.Payment.InputPayment
{
    public interface IInputPaymentAppService : IApplicationService
    {
        List<GetDataTransNoListDto> GetDataLookupTransNo(GetDataTransNoInputDto input);

        List<GetDataBookCodeListDto> GetDataBookCode(GetDataBookCodeInputDto input);

        List<GetDataPersonalsListDto> GetDataLookupPersonals(string filter);
        
        List<GetDropdownUnitNoListDto> GetDropdownUnitNoByUnitCodeID(int unitCodeID);

        List<GetDropdownUnitCodeListDto> GetDropdownUnitCode(GetDataUnitCodeInputDto input);

        GetDataUnitInfoListDto GetDataUnitInfo(int bookingHeaderID);

        GetDataClientInfoListDto GetClientInfo(int bookingHeaderID);

        int CreateTrPaymentHeader(CreatePaymentHeaderInputDto input);

        int CreateTrPaymentDetail(CreatePaymentDetailInputDto input);

        void CreateTrPaymentDetailAlloc(CreatePaymentDetailAllocInputDto input);

        List<GetDataSchedulePaymentListDto> GetDataSchedulePayment(GetDataSchedulePaymentInputDto input);

        List<GetDropdownAllocationLkItemListDto> GetDropdownAllocationLkItem(GetDropdownAllocationLkItemInputDto input);

        void CreateInputPaymentUniversal(CreateInputPaymentUniversalInputDto input);

        void CreateAccountingTrPaymentDetailJournal(CreateAccountingTrPaymentDetailJournalInputDto input);

        JObject GenerateJurnalCode(GenerateJurnalInputDto input);

        void CreateTrJournal(CreateTrJournalInputDto input);

        void CreateTAXTrFPHeader(CreateTAXTrFPHeaderInputDto input);

        void CreateTAXTrFPDetail(CreateTAXTrFPDetailInputDto input);

        JObject GenerateTransNoWithoutCheckSys(GenerateTransNoInputDto input);

        List<GetProjectByAccountListDto> GetProjectListByAccount(int accountID);

        GetDataPrintORByTransNoListDto GetDataPrintORByTransNo(string transNo);

        JObject GetDataToPrintOR(PrintORDto input);
    }
}
