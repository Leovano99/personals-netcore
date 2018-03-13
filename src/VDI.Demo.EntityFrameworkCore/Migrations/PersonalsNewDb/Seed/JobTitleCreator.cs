using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class JobTitleCreator
    {
        private readonly PersonalsNewDbContext _context;

        public JobTitleCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<MS_JobTitle> InitialJobTitle = new List<MS_JobTitle>();
            List<String> listJobTitle = new List<string>()
            {
                "000,Not Defined",
                "001,Pemilik Usaha / Komisaris",
                "002,Setingkat Presdir / CEO",
                "003,Setingkat Direktur / VP/ GM",
                "004,Setingkat Manager"
            };
            foreach (var item in listJobTitle)
            {
                var jobTitleSeparete = item.Split(',');
                var jobTitlePush = new MS_JobTitle()
                {
                    jobTitleID = jobTitleSeparete[0],
                    jobTitleName = jobTitleSeparete[1]
                };

                InitialJobTitle.Add(jobTitlePush);
            }

            foreach (var jobTitle in InitialJobTitle)
            {
                AddIfNotExists(jobTitle);
            }
        }

        private void AddIfNotExists(MS_JobTitle jobTitle)
        {
            if (_context.MS_JobTitle.Any(l => l.jobTitleID == jobTitle.jobTitleID))
            {
                return;
            }
            _context.MS_JobTitle.Add(jobTitle);
            _context.SaveChanges();
        }
    }
}
