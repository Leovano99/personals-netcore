using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class AddrType
    {

        private readonly PersonalsNewDbContext _context;

        public AddrType(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<LK_AddrType> InitialAddrType = new List<LK_AddrType>();
            List<String> listAddrStatus = new List<string>()
            {
                "1,ID",
                "2,Billing",
                "3,Corress",
                "4,NPWP",
                "5,Domisili"
            };
            foreach (var item in listAddrStatus)
            {
                var AddrTypeSeparate = item.Split(',');

                var AddrTypePush = new LK_AddrType()
                {
                    addrType = AddrTypeSeparate[0],
                    addrTypeName = AddrTypeSeparate[1]
                };

                InitialAddrType.Add(AddrTypePush);
            }

            foreach (var addrType in InitialAddrType)
            {
                AddIfNotExists(addrType);
            }
        }

        private void AddIfNotExists(LK_AddrType addrType)
        {
            if (_context.LK_AddrType.Any(l => l.addrType == addrType.addrType))
            {
                return;
            }

            _context.LK_AddrType.Add(addrType);

            _context.SaveChanges();
        }
    }
}
