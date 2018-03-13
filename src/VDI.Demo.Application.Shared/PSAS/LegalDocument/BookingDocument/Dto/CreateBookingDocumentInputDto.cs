using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.BookingDocument.Dto
{
    public class CreateBookingDocumentInputDto
    {
        public int entityID { get; set; }

        public string bookCode { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }

        public int projectID { get; set; }
        public int docID { get; set; }
        public string docNo { get; set; }
        public string oldDocNo { get; set; }
        public DateTime docDate { get; set; }
        public string remarks { get; set; } //Di FE default yg akan di isi auto-generate PPPU
        public string tandaTerimaNo { get; set; }
        public DateTime? tandaTerimaDate { get; set; }
        public string tandaTerimaBy { get; set; }
        public string tandaTerimaFile { get; set; }

        public string procurationName { get; set; }
        public int procurationType { get; set; }
        public string signatureImage1 { get; set; }
        public string signatureImage2 { get; set; }
    }
}
