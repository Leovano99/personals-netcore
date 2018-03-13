using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class BankTypeCreator
    {

        private readonly PersonalsNewDbContext _context;

        public BankTypeCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<LK_BankType> InitialBankType = new List<LK_BankType>();
            List<String> listBankType = new List<string>()
            {
                "0,UnKnown",
                "1,Pribadi / Sendiri",
                "2,Adik Kandung",
                "3,Anak Kandung",
                "4,Ayah Kandung",
                "5,Ibu Kandung",
                "6,Istri / Suami",
                "7,Kakak Kandung"
            };
            foreach (var item in listBankType)
            {
                var bankTypeSeparate = item.Split(',');

                var bankTypePush = new LK_BankType()
                {
                    bankType = bankTypeSeparate[0],
                    bankTypeName = bankTypeSeparate[1]
                };

                InitialBankType.Add(bankTypePush);
            }

            foreach (var bankType in InitialBankType)
            {
                AddIfNotExists(bankType);
            }
        }

        private void AddIfNotExists(LK_BankType bankType)
        {
            if (_context.LK_bankType.Any(l => l.bankType == bankType.bankType))
            {
                return;
            }

            _context.LK_bankType.Add(bankType);

            _context.SaveChanges();
        }
    }
}
