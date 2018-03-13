using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Payment.BulkPayment.Dto;

namespace VDI.Demo.Payment.BulkPayment
{
    public interface IBulkPaymentAppService : IApplicationService
    {

        List<GetDataCheckUploadExcelListDto> CheckDataUploadExcelBulk(List<CheckDataUploadExcelBulkInputDto> input);
        void CreateTrPaymentBulk(CreateTrPaymentBulkInputDto input);
        void CreateUniversalBulkPayment(List<CreateUniversalBulkPaymentInputDto> input);
    }
}
