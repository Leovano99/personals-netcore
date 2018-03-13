using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class BankCreator
    {
        private readonly PersonalsNewDbContext _context;

        public BankCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<MS_BankPersonal> InitialBank = new List<MS_BankPersonal>();
            List<String> listBank = new List<string>()
            {
                "ABNAM,ABN AMRO BANK",
                "ARTHA,ARTHA GRAHA BANK",
                "BAJ,BANK JATIM",
                "BALI,BANK BALI",
                "BCA,BANK CENTRAL ASIA",
                "BCP,BANK CAPITAL",
                "BE,BANK EKONOMI",
                "BII,BANK INT. INDONESIA",
                "BJA,BANK JASA ARTA",
                "BJJ,BANK JASA JAKARTA",
                "BNI,BANK NEGARA INDONESIA",
                "BNIS,BNI SYARIAH",
                "BNP,BANK NUSANTARA PARAHYANGAN",
                "BPD,BANK PEMBANGUNAN DAERAH",
                "BPT,BANK BUMI PUTERA",
                "BRI,BANK RAKYAT INDONESIA",
                "BSM,SYARIAH MANDIRI BANK",
                "BTN,BANK TABUNGAN NEGARA",
                "BTPN,BANK TABUNGAN PENSIUNAN NEGARA",
                "BUANA,BANK BUANA",
                "BUKPN,BUKOPIN",
                "BUKS,BUKOPIN SYARIAH",
                "CEN,CENTURY BANK",
                "CIC,CIC BANK",
                "CIMB,CIMB NIAGA",
                "CITI,CITIBANK",
                "COMM,COMMONWEALTH BANK",
                "DANMN,DANAMON BANK",
                "DKI,BANK DKI",
                "HAG,HAGA",
                "HANA,HANA BANK",
                "HSBC,HSBC",
                "ICBC,ICBC",
                "IFI,IFI BANK",
                "INA,BANK INA",
                "JABR,BANK JABAR",
                "LIPPO,LIPPO BANK",
                "MANDR,MANDIRI BANK",
                "MAS,MASPION",
                "MAY,MAYBANK",
                "MAYA,MAYAPADA BANK",
                "MEGA,BANK MEGA",
                "MEST,MESTIKA",
                "MUA,MUAMALAT BANK",
                "NIAGA,NIAGA BANK",
                "NISP,BANK NISP",
                "NOBU,NOBU NATIONAL BANK",
                "OCBC,OCBC NISP",
                "OTH,OTHERS",
                "PANIN,PANIN BANK",
                "PERMT,PERMATA BANK",
                "RABO,RABO BANK",
                "SHN,BANK SHINTA",
                "UOB,UOB BUANA",
                "WOO,Bank Woori Indonesia",
                "YDH,YUDHA BHAKTI BANK"
            };
            foreach (var item in listBank)
            {
                var bankSeparete = item.Split(',');
                var bankPush = new MS_BankPersonal()
                {
                    bankCode = bankSeparete[0],
                    bankName = bankSeparete[1]
                };

                InitialBank.Add(bankPush);
            }

            foreach (var spec in InitialBank)
            {
                AddIfNotExists(spec);
            }
        }

        private void AddIfNotExists(MS_BankPersonal bank)
        {
            if (_context.MS_BankPersonal.Any(l => l.bankCode == bank.bankCode))
            {
                return;
            }
            _context.MS_BankPersonal.Add(bank);
            _context.SaveChanges();
        }
    }
}
