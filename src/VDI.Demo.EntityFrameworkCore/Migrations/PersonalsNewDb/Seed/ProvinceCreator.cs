using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class ProvinceCreator
    {
        private readonly PersonalsNewDbContext _context;

        public ProvinceCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<MS_Province> InitialProvince = new List<MS_Province>();
            List<String> listProvince = new List<string>()
            {
                "NAD,Nanggroe Aceh Darussalam",
                "SUT,Sumatera Utara",
                "SBR,Sumatera Barat",
                "RIA,Riau",
                "JAM,Jambi",
                "SSL,Sumatera Selatan",
                "BKL,Bengkulu",
                "LMP,Lampung",
                "BBL,Bangka Belitung",
                "KPR,Kepulauan Riau",
                "JAK,DKI Jakarta",
                "JBR,Jawa Barat",
                "JTG,Jawa Tengah",
                "DIY,DI Yogyakarta",
                "JTM,Jawa Timur",
                "BTN,Banten",
                "BAL,Bali",
                "NTB,Nusa Tenggara Barat",
                "NTT,Nusa Tenggara Timur",
                "KBR,Kalimantan Barat",
                "KTG,Kalimantan Tengah",
                "KSL,Kalimantan Selatan",
                "KTM,Kalimantan Timur",
                "SUT,Sulawesi Utara",
                "STG,Sulawesi Tengah",
                "SSL,Sulawesi Selatan",
                "STE,Sulawesi Tenggara",
                "GRN,Gorontalo",
                "SBR,Sulawesi Barat",
                "MLK,Maluku",
                "MUT,Maluku Utara",
                "PBR,Papua Barat",
                "PPA,Papua"
            };
            foreach (var item in listProvince)
            {
                var provinceSeparete = item.Split(',');
                var provincePush = new MS_Province()
                {
                    provinceCode = provinceSeparete[0],
                    provinceName = provinceSeparete[1]
                };

                InitialProvince.Add(provincePush);
            }

            foreach (var province in InitialProvince)
            {
                AddIfNotExists(province);
            }

        }

        private void AddIfNotExists(MS_Province province)
        {
            if (_context.MS_Province.Any(l => l.provinceCode == province.provinceCode))
            {
                return;
            }
            _context.MS_Province.Add(province);
            _context.SaveChanges();
        }
    }
}
