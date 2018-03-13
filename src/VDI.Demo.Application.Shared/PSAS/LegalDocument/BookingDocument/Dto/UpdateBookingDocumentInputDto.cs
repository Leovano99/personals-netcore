using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.BookingDocument.Dto
{
    public class UpdateBookingDocumentInputDto
    {
        public int trBookingDocumentID { get; set; }
        public string tandaTerimaNo { get; set; }
        public DateTime? tandaTerimaDate { get; set; }
        public string tandaTerimaBy { get; set; }
        public string tandaTerimaFile { get; set; }
    }
}
