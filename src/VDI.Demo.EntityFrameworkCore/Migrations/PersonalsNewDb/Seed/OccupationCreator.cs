using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class OccupationCreator
    {
        private readonly PersonalsNewDbContext _context;

        public OccupationCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<MS_Occupation> InitialOccupation = new List<MS_Occupation>();
            List<String> listOccupation = new List<string>()
            {
                "1,002,ABRI,3",
                "1,003,Apoteker,6",
                "1,004,Arsitek,7",
                "1,005,Buruh,13",
                "1,006,Dokter,7",
                "1,007,Dosen,8",
                "1,008,Guru,8",
                "1,009,Ibu Rumah Tangga,14",
                "1,010,Lain-lain,19",
                "1,011,Mahasiswa/i,15",
                "1,012,Marketing,6",
                "1,013,Pedagang,10",
                "1,014,Pegawai Negri,1",
                "1,015,Pegawai Swasta,6",
                "1,016,Pelajar,15",
                "1,017,Pendeta,18",
                "1,018,Pengacara,7",
                "1,019,Sopir,19",
                "1,020,Pengusaha / Wiraswasta,9",
                "1,021,Karyawan,6",
                "1,022,Profesional,7",
                "1,023,Seniman,19",
                "1,024,TNI AD,3"
            };
            foreach (var item in listOccupation)
            {
                var occupationSeparete = item.Split(',');
                var occupationPush = new MS_Occupation()
                {
                    entityCode = occupationSeparete[0],
                    occID = occupationSeparete[1],
                    occDesc = occupationSeparete[2],
                    ppatkOccCode = occupationSeparete[3]
                };

                InitialOccupation.Add(occupationPush);
            }

            foreach (var occupation in InitialOccupation)
            {
                AddIfNotExists(occupation);
            }
        }

        private void AddIfNotExists(MS_Occupation occupation)
        {
            if (_context.MS_Occupation.Any(l => l.occID == occupation.occID))
            {
                return;
            }
            _context.MS_Occupation.Add(occupation);
            _context.SaveChanges();
        }
    }
}
