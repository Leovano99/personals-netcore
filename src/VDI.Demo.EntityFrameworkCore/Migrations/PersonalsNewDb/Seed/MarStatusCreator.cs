using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class MarStatusCreator
    {
        private readonly PersonalsNewDbContext _context;

        public MarStatusCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<LK_MarStatus> InitialMarStatus = new List<LK_MarStatus>();
            List<String> listMarStatus = new List<string>()
            {
                "0,UnKnown",
                "M,Married",
                "S,Single",
                "W,Widow"
            };
            foreach (var item in listMarStatus)
            {
                var marStatusSeparate = item.Split(',');

                var marStatusPush = new LK_MarStatus()
                {
                    marStatus = marStatusSeparate[0],
                    marStatusName = marStatusSeparate[1]
                };

                InitialMarStatus.Add(marStatusPush);
            }

            foreach (var marStatus in InitialMarStatus)
            {
                AddIfNotExists(marStatus);
            }
        }

        private void AddIfNotExists(LK_MarStatus marStatus)
        {
            if (_context.LK_MarStatus.Any(l => l.marStatus == marStatus.marStatus))
            {
                return;
            }

            _context.LK_MarStatus.Add(marStatus);

            _context.SaveChanges();
        }
    }
}
