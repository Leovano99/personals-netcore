using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class FamilyStatus
    {

        private readonly PersonalsNewDbContext _context;

        public FamilyStatus(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<LK_FamilyStatus> InitialBankType = new List<LK_FamilyStatus>();
            List<String> listFamilyStatus = new List<string>()
            {
                "0,UnKnown",
                "1,Anak",
                "2,Kepala Keluarga",
                "3,Istri Kepala Keluarga",
                "4,Adik",
                "5,Saudara",
                "6,Ibu",
                "7,Ayah"
            };
            foreach (var item in listFamilyStatus)
            {
                var familyStatusSeparate = item.Split(',');

                var familyStatusPush = new LK_FamilyStatus()
                {
                    famStatus = familyStatusSeparate[0],
                    famStatusName = familyStatusSeparate[1]
                };

                InitialBankType.Add(familyStatusPush);
            }

            foreach (var familyStatus in InitialBankType)
            {
                AddIfNotExists(familyStatus);
            }
        }

        private void AddIfNotExists(LK_FamilyStatus familyStatus)
        {
            if (_context.LK_FamilyStatus.Any(l => l.famStatus == familyStatus.famStatus))
            {
                return;
            }

            _context.LK_FamilyStatus.Add(familyStatus);

            _context.SaveChanges();
        }
    }
}
