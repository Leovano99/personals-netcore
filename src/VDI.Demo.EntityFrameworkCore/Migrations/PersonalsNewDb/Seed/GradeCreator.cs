using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class GradeCreator
    {

        private readonly PersonalsNewDbContext _context;

        public GradeCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<LK_Grade> InitialGrade = new List<LK_Grade>();
            List<String> listGrade = new List<string>()
            {
                "0,UnKnown",
                "1,Ordinary",
                "2,VIP"
            };
            foreach (var item in listGrade)
            {
                var gradeSeparate = item.Split(',');

                var gradePush = new LK_Grade()
                {
                    gradeCode = gradeSeparate[0],
                    gradeName = gradeSeparate[1]
                };

                InitialGrade.Add(gradePush);
            }

            foreach (var grade in InitialGrade)
            {
                AddIfNotExists(grade);
            }
        }

        private void AddIfNotExists(LK_Grade grade)
        {
            if (_context.LK_Grade.Any(l => l.gradeCode == grade.gradeCode))
            {
                return;
            }

            _context.LK_Grade.Add(grade);

            _context.SaveChanges();
        }
    }
}
