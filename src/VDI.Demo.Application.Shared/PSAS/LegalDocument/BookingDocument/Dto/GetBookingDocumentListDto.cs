using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.BookingDocument.Dto
{
    public class GetBookingDocumentListDto
    {
        public string bookCode { get; set; }
        public int projectID { get; set; }
        public string projectName { get; set; }
        public string clusterName { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public DateTime bookingDate { get; set; }
        public List<BookingDocumentListDto> bookingDoc { get; set; }

    }

    public class BookingDocumentListDto
    {
        public int bookingDocumentId { get; set; }
        public string docCode { get; set; }
        public string docName { get; set; }
        public string docNo { get; set; }
        public DateTime docDate { get; set; }
        public string remarks { get; set; }
        public bool isActive { get; set; }
        public bool isTandaTerima { get; set; }
        public string fileTandaTerima { get; set; }
    }
}
