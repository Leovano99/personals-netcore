using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class BloodCreator
    {

        private readonly PersonalsNewDbContext _context;

        public BloodCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<LK_Blood> InitialBlood = new List<LK_Blood>();
            List<String> listBlood = new List<string>()
            {
                "0,-" ,
                "1,A"  ,
                "2,B"  ,
                "3,AB" ,
                "4,O"
            };
            foreach (var item in listBlood)
            {
                var bloodSeparate = item.Split(',');

                var bloodPush = new LK_Blood()
                {
                    bloodCode = bloodSeparate[0],
                    bloodName = bloodSeparate[1]
                };

                InitialBlood.Add(bloodPush);
            }

            foreach (var blood in InitialBlood)
            {
                AddIfNotExists(blood);
            }
        }

        private void AddIfNotExists(LK_Blood blood)
        {
            if (_context.LK_Bloods.Any(l => l.bloodCode == blood.bloodCode))
            {
                return;
            }

            _context.LK_Bloods.Add(blood);

            _context.SaveChanges();
        }
    }
}
