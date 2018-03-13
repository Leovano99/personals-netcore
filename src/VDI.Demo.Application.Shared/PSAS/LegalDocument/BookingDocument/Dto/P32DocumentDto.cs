using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.OnlineBooking.PaymentMidtrans.Dto;

namespace VDI.Demo.PSAS.LegalDocument.BookingDocument.Dto
{
    public class P32DocumentDto
    {
        public string Block { get; set; }
        public string Tower { get; set; }
        public string Handphone { get; set; }
        public string Email { get; set; }
        public string NoKTP { get; set; }
        public string AlamatKTP { get; set; }
        public string AlamatCorrespondence { get; set; }
        public string Nama { get; set; }
        public string NoNPWP { get; set; }
        public string UnitNo { get; set; }
        public string UnitCode { get; set; }
        public string DocumentType { get; set; }
        public string HandOverPeriod { get; set; }
        public string GracePeriod { get; set; }

        //Cicilan
        public string SpecialCicilan { get; set; }
        public string SpecialCicilanAmount { get; set; }
        public string VirtualAccountNumber { get; set; }
        public string CompanyName { get; set; }
        public string BookCode { get; set; }

        public string TypeOfProcuration { get; set; }
        public string NameOfProcuration { get; set; }
        public string PPNo { get; set; }
        public string PPDate { get; set; }
        public string OrderCode { get; set; }
        public string DocNo { get; set; }
        public string DocDate { get; set; }
        public string PrintDay { get; set; }
        public string PrintDate { get; set; }
        public string KadasterErrorMessage { get; set; }
        public string TandaTerimaNo { get; set; }

        public bool isTandaTerima { get; set; }
        public string signatureImage1 { get; set; }
        public string signatureImage2 { get; set; }

        public KonfirmasiPesananDto KP { get; set; }
    }
}
