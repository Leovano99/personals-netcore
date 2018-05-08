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
                "NAD,Nanggroe Aceh Darussalam,Indonesia",
                "SUT,Sumatera Utara,Indonesia",
                "SBR,Sumatera Barat,Indonesia",
                "RIA,Riau,Indonesia",
                "JAM,Jambi,Indonesia",
                "SSL,Sumatera Selatan,Indonesia",
                "BKL,Bengkulu,Indonesia",
                "LMP,Lampung,Indonesia",
                "BBL,Bangka Belitung,Indonesia",
                "KPR,Kepulauan Riau,Indonesia",
                "JAK,DKI Jakarta,Indonesia",
                "JBR,Jawa Barat,Indonesia",
                "JTG,Jawa Tengah,Indonesia",
                "DIY,DI Yogyakarta,Indonesia",
                "JTM,Jawa Timur,Indonesia",
                "BTN,Banten,Indonesia",
                "BAL,Bali,Indonesia",
                "NTB,Nusa Tenggara Barat,Indonesia",
                "NTT,Nusa Tenggara Timur,Indonesia",
                "KBR,Kalimantan Barat,Indonesia",
                "KTG,Kalimantan Tengah,Indonesia",
                "KSL,Kalimantan Selatan,Indonesia",
                "KTM,Kalimantan Timur,Indonesia",
                "SUT,Sulawesi Utara,Indonesia",
                "STG,Sulawesi Tengah,Indonesia",
                "SSL,Sulawesi Selatan,Indonesia",
                "STE,Sulawesi Tenggara,Indonesia",
                "GRN,Gorontalo,Indonesia",
                "SBR,Sulawesi Barat,Indonesia",
                "MLK,Maluku,Indonesia",
                "MUT,Maluku Utara,Indonesia",
                "PBR,Papua Barat,Indonesia",
                "PPA,Papua,Indonesia"
            };
            foreach (var item in listProvince)
            {
                var provinceSeparete = item.Split(',');
                var provincePush = new MS_Province()
                {
                    provinceCode = provinceSeparete[0],
                    provinceName = provinceSeparete[1],
                    country = provinceSeparete[2]
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
