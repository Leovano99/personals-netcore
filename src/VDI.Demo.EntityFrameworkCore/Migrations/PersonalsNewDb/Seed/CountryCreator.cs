using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class CountryCreator
    {

        private readonly PersonalsNewDbContext _context;

        public CountryCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<LK_Country> InitialCountry = new List<LK_Country>();
            List<String> listCountry = new List<string>()
            {
                "-,1,NULL",
                "Australia,12,17",
                "Belanda,2,23",
                "Brunei Darusalam,16,140",
                "Canada,15,13",
                "China,11,2",
                "Denmark,20,1",
                "France,13,7",
                "German,19,NULL",
                "Hongkong,17,40",
                "India,7,4",
                "Indonesia,0,62",
                "Italy,14,8",
                "Jepang,3,3",
                "Korea,6,14",
                "Libya,18,69",
                "Malaysia,5,34",
                "Singapore,4,55",
                "Thailand,10,21",
                "United Kingdom,9,6",
                "United States of America,8,1"
            };
            foreach (var item in listCountry)
            {
                var countrySeparate = item.Split(',');

                var countryPush = new LK_Country()
                {
                    country = countrySeparate[0],
                    urut = Int32.Parse(countrySeparate[1]),
                    ppatkCountryCode = countrySeparate[2] == "NULL" ? null : countrySeparate[2]
                };

                InitialCountry.Add(countryPush);
            }

            foreach (var country in InitialCountry)
            {
                AddIfNotExists(country);
            }
        }

        private void AddIfNotExists(LK_Country country)
        {
            if (_context.LK_Country.Any(l => l.country == country.country))
            {
                return;
            }

            _context.LK_Country.Add(country);

            _context.SaveChanges();
        }
    }
}
