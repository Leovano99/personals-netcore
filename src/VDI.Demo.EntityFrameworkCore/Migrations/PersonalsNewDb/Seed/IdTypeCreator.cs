using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class IdTypeCreator
    {
        private readonly PersonalsNewDbContext _context;

        public IdTypeCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<LK_IDType> InitialIdType = new List<LK_IDType>();
            List<String> listIdType = new List<string>()
            {
                "1,KTP",
                "2,SIM",
                "3,Passport",
                "4,NIK",
                "5,KITAS",
                "6,UPH Card / Lippo Group ID Card",
                "7,Tanda Daftar Perusahaan"
            };
            foreach (var item in listIdType)
            {
                var idTypeSeparate = item.Split(',');

                var bloodPush = new LK_IDType()
                {
                    idType = idTypeSeparate[0],
                    idTypeName = idTypeSeparate[1]
                };

                InitialIdType.Add(bloodPush);
            }

            foreach (var blood in InitialIdType)
            {
                AddIfNotExists(blood);
            }
        }

        private void AddIfNotExists(LK_IDType idType)
        {
            if (_context.LK_IDType.Any(l => l.idType == idType.idType))
            {
                return;
            }

            _context.LK_IDType.Add(idType);

            _context.SaveChanges();
        }
    }
}
