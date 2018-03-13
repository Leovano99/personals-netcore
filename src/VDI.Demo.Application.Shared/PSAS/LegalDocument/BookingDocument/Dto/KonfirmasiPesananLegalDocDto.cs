using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.LegalDocument.BookingDocument.Dto
{
    public class KonfirmasiPesananLegalDocDto
    {
        public string UnitCode { get; set; }
        public string UnitNo { get; set; }
        public string OrderCode { get; set; }
        public string BookCode { get; set; }
        public string imageProject { get; set; }
        public string kodePelanggan { get; set; }
        public string tanggalBooking { get; set; }
        public string psName { get; set; }
        public string noIdentitas { get; set; }
        public string birthDate { get; set; }
        public string noNPWP { get; set; }
        public string noHpPembeli { get; set; }
        public string email { get; set; }
        public string tower { get; set; }
        public string lantai { get; set; }
        public string noUnitHighrise { get; set; }
        public string luas { get; set; }
        public string tipe { get; set; }
        public string renovation { get; set; }
        public string cluster { get; set; }
        public string roadName { get; set; }
        public string noUnitLanded { get; set; }
        public string luasLanded { get; set; }
        public string tipeLanded { get; set; }
        public string renovationLanded { get; set; }
        public string hargaJual { get; set; }
        public string namaBank { get; set; }
        public string caraPembayaran { get; set; }
        public string noRekening { get; set; }
        public string tujuanTransaksi { get; set; }
        public string sumberDanaPembelian { get; set; }
        public string lblHandOver { get; set; }
        public string lblGracePeriod { get; set; }
        public string rekComName { get; set; }
        public List<listBankDto> listBank { get; set; }
        public List<unitDto> listUnit { get; set; }
        public string namaBankVA { get; set; }
        public string noAccVA { get; set; }
        public string bfAmount { get; set; }
        public string noDealCloser { get; set; }
        public string noHp { get; set; }
        public string namaDealCloser { get; set; }
        public string ilustrasiPembayaran { get; set; }
        public string imageLippo { get; set; }
        public int unitID { get; set; }

        public class listBankDto
        {
            public string bankName { get; set; }
            public string noVA { get; set; }
        }

        public class unitDto
        {
            public string UnitCode { get; set; }
            public string UnitNo { get; set; }
            public string luas { get; set; }
            public string tipe { get; set; }
            public string renovation { get; set; }
            public string cluster { get; set; }
            public string category { get; set; }
        }
    }
}
