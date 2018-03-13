using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class ReligionCreator
    {
        private readonly PersonalsNewDbContext _context;

        public ReligionCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<LK_Religion> InitialReligion = new List<LK_Religion>();
            List<String> listReligion = new List<string>()
            {
                "0,UnKnown,Others",
                "1,Islam,Islam",
                "2,Kristen,Protestant",
                "3,Katolik,Catholic",
                "4,Budha,Budhism",
                "5,Hindu,Hinduism",
                "6,Kong Hu Cu,Confucius",
                "7,Tao,Others",
                "8,No Religion,Others"
            };
            foreach (var item in listReligion)
            {
                var religionSeparete = item.Split(',');
                var religionPush = new LK_Religion()
                {
                    relCode = religionSeparete[0],
                    relName = religionSeparete[1],
                    relNameProInt = religionSeparete[2]
                };

                InitialReligion.Add(religionPush);
            }

            foreach (var religion in InitialReligion)
            {
                AddIfNotExists(religion);
            }
        }

        private void AddIfNotExists(LK_Religion religion)
        {
            if (_context.LK_Religion.Any(l => l.relCode == religion.relCode))
            {
                return;
            }
            _context.LK_Religion.Add(religion);
            _context.SaveChanges();
        }
    }
}
