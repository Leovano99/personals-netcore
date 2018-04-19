using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class PhoneTypeCreator
    {
        private readonly PersonalsNewDbContext _context;

        public PhoneTypeCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<LK_PhoneType> InitialPhoneType = new List<LK_PhoneType>();
            List<String> listPhoneType = new List<string>()
            {
                "1,Home Phone,Residential Phone",
                "2,Business Phone,Mobile Phone",
                "3,Hand Phone,Mobile Phone",
                "4,Fax,NULL",
                "5,Bill Phone,Residential Phone",
                "6,Invalid Phone Number,Mobile Phone"
            };
            foreach (var item in listPhoneType)
            {
                var phoneTypeSeparate = item.Split(',');

                var phoneTypePush = new LK_PhoneType()
                {
                    phoneType = phoneTypeSeparate[0],
                    phoneTypeName = phoneTypeSeparate[1],
                    phoneTypeNameProInt = phoneTypeSeparate[2]
                };

                InitialPhoneType.Add(phoneTypePush);
            }

            foreach (var phoneType in InitialPhoneType)
            {
                AddIfNotExists(phoneType);
            }
        }

        private void AddIfNotExists(LK_PhoneType phoneType)
        {
            if (_context.LK_PhoneType.Any(l => l.phoneType == phoneType.phoneType))
            {
                return;
            }

            _context.LK_PhoneType.Add(phoneType);

            _context.SaveChanges();
        }
    }
}
