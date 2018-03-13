using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.Dto;
using VDI.Demo.PSAS.LegalDocument.BookingDocument.Dto;

namespace VDI.Demo.PSAS.LegalDocument.BookingDocument
{
    public interface IPSASBookingDocumentAppService : IApplicationService
    {
        void CreateBookingDocument(CreateBookingDocumentInputDto input);
        void UploadTandaTerima(UpdateBookingDocumentInputDto input);
        GetBookingDocumentListDto GetBookingDocument(GetPSASParamsDto input);
        P32DocumentDto GetDataGenerate(string docNo, int bookingHeaderID, string atasNama, int procurationType);
        GetFilePPPUListDto GetFilePPPU(GetFilePPPUInputDto input);
    }
}
