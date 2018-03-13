using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class DocumentCreator
    {
        private readonly PersonalsNewDbContext _context;

        public DocumentCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<MS_Document> InitialDocument = new List<MS_Document>();
            List<String> listDocument = new List<string>()
            {
                "ALAHIR,Akta Lahir",
                "ANIKAH,Akta Nikah",
                "APP,Akta Pendirian PT",
                "KITAS,Kitas",
                "KK,Kartu Keluarga",
                "KKP,Kartu Keanggotaan Profesi",
                "KTP,KTP",
                "KTPC,KTP Pasangan",
                "KTPP,KTP Pengurus",
                "LKEU,Laporan Keuangan",
                "NPWP,NPWP",
                "PASS,Passport",
                "PPER,Profil Perusahaan",
                "RKOR,Rekening Koran /Tabungan",
                "SCERAI,Surat Cerai",
                "SIM,SIM",
                "SIP,Surat Ijin Praktek",
                "SIUP,SIUP",
                "SKB,Surat Keterangan Bekerja",
                "SKTD,Surat Keterangan Domisili",
                "SLIP,Slip Gaji 3 Bulan",
                "SPKH,Pengesahan Mentri Kehakiman dan Ham",
                "TDP,TDP"
            };
            foreach (var item in listDocument)
            {
                var documentSeparete = item.Split(',');
                var documentPush = new MS_Document()
                {
                    documentType = documentSeparete[0],
                    documentName = documentSeparete[1]
                };

                InitialDocument.Add(documentPush);
            }

            foreach (var document in InitialDocument)
            {
                AddIfNotExists(document);
            }
        }

        private void AddIfNotExists(MS_Document document)
        {
            if (_context.MS_Document.Any(l => l.documentType == document.documentType))
            {
                return;
            }
            _context.MS_Document.Add(document);
            _context.SaveChanges();
        }
    }
}
