using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.Migrations.PropertySystemDb.Seed
{
    public class CountryCreator
    {
        private readonly PropertySystemDbContext _context;

        public CountryCreator(PropertySystemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateSeed();
        }

        private void CreateSeed()
        {
            List<MS_Country> InitialCountry = new List<MS_Country>();
            List<String> listCountry = new List<string>()
            {
                "INA,Indonesia"
            };
            foreach (var item in listCountry)
            {
                var countrySeparate = item.Split(',');

                var countryPush = new MS_Country()
                {
                    countryCode = countrySeparate[0]

                };

                InitialCountry.Add(countryPush);
            }

            foreach (var country in InitialCountry)
            {
                AddIfNotExists(country);
            }
        }

        private void AddIfNotExists(MS_Country country)
        {
            if (_context.MS_Country.Any(l => l.countryCode == country.countryCode))
            {
                return;
            }

            _context.MS_Country.Add(country);

            _context.SaveChanges();
        }
    }
}
