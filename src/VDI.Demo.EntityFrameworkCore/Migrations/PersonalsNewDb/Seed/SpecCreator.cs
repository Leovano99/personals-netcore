using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class SpecCreator
    {
        private readonly PersonalsNewDbContext _context;

        public SpecCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<LK_Spec> InitialSpec = new List<LK_Spec>();
            List<String> listSpec = new List<string>()
            {
                "0,Unknown",
                "1,Industri",
                "2,Niaga",
                "3,Perumahan"
            };
            foreach (var item in listSpec)
            {
                var specSeparete = item.Split(',');
                var specPush = new LK_Spec()
                {
                    specCode = specSeparete[0],
                    specName = specSeparete[1]
                };

                InitialSpec.Add(specPush);
            }

            foreach (var spec in InitialSpec)
            {
                AddIfNotExists(spec);
            }
        }

        private void AddIfNotExists(LK_Spec spec)
        {
            if (_context.LK_Spec.Any(l => l.specCode == spec.specCode))
            {
                return;
            }
            _context.LK_Spec.Add(spec);
            _context.SaveChanges();
        }
    }
}
