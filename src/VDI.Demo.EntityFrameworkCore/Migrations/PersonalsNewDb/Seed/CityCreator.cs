﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class CityCreator
    {
        private readonly PersonalsNewDbContext _context;

        public CityCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<MS_City> InitialCity = new List<MS_City>();
            List<String> listCity = new List<string>()
            {
                "1,0001,Ambarawa,Indonesia,ABW ",
                "1,0002,Amboina,Indonesia,AMB",
                "1,0003,Amlapura,Indonesia,AMP",
                "1,0004,Amuntai,Indonesia,AMT",
                "1,0005,Argamakmur,Indonesia,AGM",
                "1,0006,Atambua,Indonesia,ATB",
                "1,0007,Bagansiapiapi,Indonesia,BGN",
                "1,0008,Bajawa,Indonesia,BJW",
                "1,0009,Balige,Indonesia,BLG",
                "1,0010,Balikpapan,Indonesia,BPP",
                "1,0011,Bandaaceh,Indonesia,BAC",
                "1,0012,Bandarlampung,Indonesia,BLG",
                "1,0013,Bandung,Indonesia,BDG      ",
                "1,0014,Bangkalan,Indonesia,BKL    ",
                "1,0015,Bangkinang,Indonesia,BKG   ",
                "1,0016,Bangko,Indonesia,BGK       ",
                "1,0017,Bangli,Indonesia,BGL       ",
                "1,0018,Banjar,Indonesia,BJR       ",
                "1,0019,Banjarbaru,Indonesia,BJB   ",
                "1,0020,Banjarmasin,Indonesia,BJM  ",
                "1,0021,Banjarnegara,Indonesia,BJN ",
                "1,0022,Bantaeng,Indonesia,BTG     ",
                "1,0023,Bantul,Indonesia,BTL       ",
                "1,0024,Banyuwangi,Indonesia,BYI   ",
                "1,0025,Barabai,Indonesia,BRB      ",
                "1,0026,Barru,Indonesia,BRU        ",
                "1,0027,Batam,Indonesia,BTM        ",
                "1,0028,Batang,Indonesia,BTA       ",
                "1,0029,Batu,Indonesia,BAT         ",
                "1,0030,Baturaja,Indonesia,BTR     ",
                "1,0031,Batusangkar,Indonesia,BTS  ",
                "1,0032,Baubau,Indonesia,BAU       ",
                "1,0033,Bekasi,Indonesia,BKS       ",
                "1,0034,Bengkalis,Indonesia,BLS    ",
                "1,0035,Bengkulu,Indonesia,BLU     ",
                "1,0036,Benteng,Indonesia,BEN      ",
                "1,0037,Biak,Indonesia,BIK         ",
                "1,0038,Bima,Indonesia,BIM         ",
                "1,0039,Binjai,Indonesia,BIN       ",
                "1,0040,Bireuen,Indonesia,BRN      ",
                "1,0041,Bitung,Indonesia,BIT       ",
                "1,0042,Blitar,Indonesia,BLI       ",
                "1,0043,Blora,Indonesia,BLO        ",
                "1,0044,Bogor,Indonesia,BGR        ",
                "1,0045,Bojonegoro,Indonesia,BJN   ",
                "1,0046,Bondowoso,Indonesia,BWS    ",
                "1,0047,Bontang,Indonesia,BON      ",
                "1,0048,Boyolali,Indonesia,BYO     ",
                "1,0049,Brebes,Indonesia,BRS       ",
                "1,0050,Bukittinggi,Indonesia,BKT  ",
                "1,0051,Bulukumba,Indonesia,BKB    ",
                "1,0052,Buntok,Indonesia,BUT       ",
                "1,0053,Cepu,Indonesia,CEP         ",
                "1,0054,Ciamis,Indonesia,CMS       ",
                "1,0055,Cianjur,Indonesia,CJR      ",
                "1,0056,Cibinong,Indonesia,CBN     ",
                "1,0057,Cilacap,Indonesia,CLP      ",
                "1,0058,Cilegon,Indonesia,CLG      ",
                "1,0059,Cimahi,Indonesia,CMH       ",
                "1,0060,Cirebon,Indonesia,CRB      ",
                "1,0061,Curup,Indonesia,CRP        ",
                "1,0062,Demak,Indonesia,DEM        ",
                "1,0063,Denpasar,Indonesia,DEN     ",
                "1,0064,Depok,Indonesia,DPK        ",
                "1,0065,Dili,Indonesia,DIL         ",
                "1,0066,Dompu,Indonesia,DMP        ",
                "1,0067,Dumai,Indonesia,DUM        ",
                "1,0068,Ende,Indonesia,END         ",
                "1,0069,Enrekang,Indonesia,ERK     ",
                "1,0070,Fakfak,Indonesia,FFK       ",
                "1,0071,Garut,Indonesia,GAR        ",
                "1,0072,Gianyar,Indonesia,GYR      ",
                "1,0073,Gombong,Indonesia,GBG      ",
                "1,0074,Gorontalo,Indonesia,GRT    ",
                "1,0075,Gresik,Indonesia,GRE       ",
                "1,0076,Gunungsitoli,Indonesia,GSL ",
                "1,0077,Indramayu,Indonesia,IDM    ",
                "1,0078,Jakarta,Indonesia,JKT      ",
                "1,0079,Jambi,Indonesia,JAM        ",
                "1,0080,Jayapura,Indonesia,JYP     ",
                "1,0081,Jember,Indonesia,JEM       ",
                "1,0082,Jeneponto,Indonesia,JNP    ",
                "1,0083,Jepara,Indonesia,JPR       ",
                "1,0084,Jombang,Indonesia,JOM      ",
                "1,0085,Kabanjahe,Indonesia,KBJ    ",
                "1,0086,Kalabahi,Indonesia,KLB     ",
                "1,0087,Kalianda,Indonesia,KLD     ",
                "1,0088,Kandangan,Indonesia,KDG    ",
                "1,0089,Karanganyar,Indonesia,KRY  ",
                "1,0090,Karawang,Indonesia,KRW     ",
                "1,0091,Kasungan,Indonesia,KSG     ",
                "1,0092,Kayuagung,Indonesia,KYA    ",
                "1,0093,Kebumen,Indonesia,KBM      ",
                "1,0094,Kediri,Indonesia,KDR       ",
                "1,0095,Kefamenanu,Indonesia,KFM   ",
                "1,0096,Kendal,Indonesia,KDL       ",
                "1,0097,Kendari,Indonesia,KDR      ",
                "1,0098,Kertosono,Indonesia,KRS    ",
                "1,0099,Ketapang,Indonesia,KTP     ",
                "1,0100,Kisaran,Indonesia,KSR      ",
                "1,0101,Klaten,Indonesia,KLA       ",
                "1,0102,Kolaka,Indonesia,KLK       ",
                "1,0103,Kotabarupulaulaut,Indonesia,KBP",
                "1,0104,Kotabumi,Indonesia,KBU         ",
                "1,0105,Kotajantho,Indonesia,KJT       ",
                "1,0106,Kotamobagu,Indonesia,KMB       ",
                "1,0107,Kualakapuas,Indonesia,KLP      ",
                "1,0108,Kualakurun,Indonesia,KLR       ",
                "1,0109,Kualapembuang,Indonesia,KPB    ",
                "1,0110,Kualatungkal,Indonesia,KLT     ",
                "1,0111,Kudus,Indonesia,KDS            ",
                "1,0112,Kuningan,Indonesia,KNG         ",
                "1,0113,Kupang,Indonesia,KPG           ",
                "1,0114,Kutacane,Indonesia,KTC         ",
                "1,0115,Kutoarjo,Indonesia,KTJ         ",
                "1,0116,Lahat,Indonesia,LHT            ",
                "1,0117,Lamongan,Indonesia,LMG         ",
                "1,0118,Langsa,Indonesia,LGS           ",
                "1,0119,Larantuka,Indonesia,LRT        ",
                "1,0120,Lawang,Indonesia,LAW           ",
                "1,0121,Lhoseumawe,Indonesia,LHO       ",
                "1,0122,Limboto,Indonesia,LBT          ",
                "1,0123,Lubukbasung,Indonesia,LKB      ",
                "1,0124,Lubuklinggau,Indonesia,LKK     ",
                "1,0125,Lubukpakam,Indonesia,LKP       ",
                "1,0126,Lubuksikaping,Indonesia,LKS    ",
                "1,0127,Lumajang,Indonesia,LMJ         ",
                "1,0128,Luwuk,Indonesia,LUW            ",
                "1,0129,Madiun,Indonesia,MDN           ",
                "1,0130,Magelang,Indonesia,MGL         ",
                "1,0131,Magetan,Indonesia,MGT          ",
                "1,0132,Majalengka,Indonesia,MJL       ",
                "1,0133,Majene,Indonesia,MJN           ",
                "1,0134,Makale,Indonesia,MKL           ",
                "1,0135,Makassar,Indonesia,MKS         ",
                "1,0136,Malang,Indonesia,MLG           ",
                "1,0137,Mamuju,Indonesia,MMJ           ",
                "1,0138,Manado,Indonesia,MNA           ",
                "1,0139,Manna,Indonesia,MAN            ",
                "1,0140,Manokwari,Indonesia,MKW        ",
                "1,0141,Marabahan,Indonesia,MRB        ",
                "1,0142,Maros,Indonesia,MRS            ",
                "1,0143,Martapura,Indonesia,MTP        ",
                "1,0144,Masohi,Indonesia,MSH           ",
                "1,0145,Mataram,Indonesia,MTM          ",
                "1,0146,Maumere,Indonesia,MMR          ",
                "1,0147,Medan,Indonesia,MDN            ",
                "1,0148,Mempawah,Indonesia,MPW         ",
                "1,0149,Mentok,Indonesia,MTK           ",
                "1,0150,Merauke,Indonesia,MRK          ",
                "1,0151,Metro,Indonesia,MTR            ",
                "1,0152,Meulaboh,Indonesia,MEU         ",
                "1,0153,Mojokerto,Indonesia,MJK        ",
                "1,0154,Muarabulian,Indonesia,MRB      ",
                "1,0155,Muarabungo,Indonesia,MRG       ",
                "1,0156,Muaraenim,Indonesia,MRN        ",
                "1,0157,Muarateweh,Indonesia,MTW       ",
                "1,0158,Muarosijunjung,Indonesia,MRJ   ",
                "1,0159,Muntilan,Indonesia,MTL         ",
                "1,0160,Nabire,Indonesia,NBR           ",
                "1,0161,Negara,Indonesia,NGR           ",
                "1,0162,Nganjuk,Indonesia,NGJ          ",
                "1,0163,Ngawi,Indonesia,NGA            ",
                "1,0164,Pacitan,Indonesia,PCT          ",
                "1,0165,Padang,Indonesia,PDG           ",
                "1,0166,Padangpanjang,Indonesia,PPG    ",
                "1,0167,Padangsidempuan,Indonesia,PDS  ",
                "1,0168,Pagaralam,Indonesia,PGR        ",
                "1,0169,Painan,Indonesia,PAN           ",
                "1,0170,Palangkaraya,Indonesia,PLG     ",
                "1,0171,Palembang,Indonesia,PLB        ",
                "1,0172,Palopo,Indonesia,PLP           ",
                "1,0173,Palu,Indonesia,PAL             ",
                "1,0174,Pamekasan,Indonesia,PMK        ",
                "1,0175,Pandan,Indonesia,PDA           ",
                "1,0176,Pandeglang,Indonesia,PDG       ",
                "1,0177,Pangkajene,Indonesia,PKJ       ",
                "1,0178,Pangkajenesidenreng,Indonesia,PKS",
                "1,0179,Pangkalanbun,Indonesia,PKL       ",
                "1,0180,Pangkalpinang,Indonesia,PKP      ",
                "1,0181,Panyabungan,Indonesia,PYB        ",
                "1,0182,Pare,Indonesia,PRE               ",
                "1,0183,Parepare,Indonesia,PAR           ",
                "1,0184,Pariaman,Indonesia,PRM           ",
                "1,0185,Pasuruan,Indonesia,PSR           ",
                "1,0186,Pati,Indonesia,PAT               ",
                "1,0187,Payakumbuh,Indonesia,PAY         ",
                "1,0188,Pekalongan,Indonesia,PGA         ",
                "1,0189,Pekanbaru,Indonesia,PBR          ",
                "1,0190,Pemalang,Indonesia,PML           ",
                "1,0191,Pematangsiantar,Indonesia,PST    ",
                "1,0192,Pendopo,Indonesia,PDP            ",
                "1,0193,Pinrang,Indonesia,PIN            ",
                "1,0194,Pleihari,Indonesia,PLE           ",
                "1,0195,Polewali,Indonesia,PLW           ",
                "1,0196,Pondokgede,Indonesia,PKG         ",
                "1,0197,Ponorogo,Indonesia,PNG           ",
                "1,0198,Pontianak,Indonesia,PTK          ",
                "1,0199,Poso,Indonesia,PSO               ",
                "1,0200,Prabumulih,Indonesia,PRB         ",
                "1,0201,Praya,Indonesia,PRY              ",
                "1,0202,Probolinggo,Indonesia,PRL        ",
                "1,0203,Purbalingga,Indonesia,PBL        ",
                "1,0204,Purukcahu,Indonesia,PRC          ",
                "1,0205,Purwakarta,Indonesia,PWK         ",
                "1,0206,Purwodadigrobogan,Indonesia,PWD  ",
                "1,0207,Purwokerto,Indonesia,PWA         ",
                "1,0208,Purworejo,Indonesia,PWO          ",
                "1,0209,Putussibau,Indonesia,PTS         ",
                "1,0210,Raha,Indonesia,RAH               ",
                "1,0211,Rangkasbitung,Indonesia,RGS      ",
                "1,0212,Rantau,Indonesia,RTU             ",
                "1,0213,Rantauprapat,Indonesia,RTP       ",
                "1,0214,Rantepao,Indonesia,RTA           ",
                "1,0215,Rembang,Indonesia,RMB            ",
                "1,0216,Rengat,Indonesia,RGT             ",
                "1,0217,Ruteng,Indonesia,RTG             ",
                "1,0218,Sabang,Indonesia,SBG             ",
                "1,0219,Salatiga,Indonesia,STG           ",
                "1,0220,Samarinda,Indonesia,SMD          ",
                "1,0221,Sampang,Indonesia,SPG            ",
                "1,0222,Sampit,Indonesia,SPT             ",
                "1,0223,Sanggau,Indonesia,SGU            ",
                "1,0224,Sawahlunto,Indonesia,SWH         ",
                "1,0225,Sekayu,Indonesia,SKY             ",
                "1,0226,Selong,Indonesia,SEL             ",
                "1,0227,Semarang,Indonesia,SMG           ",
                "1,0228,Sengkang,Indonesia,SGK           ",
                "1,0229,Serang,Indonesia,SRG             ",
                "1,0230,Serui,Indonesia,SER              ",
                "1,0231,Sibolga,Indonesia,SBL            ",
                "1,0232,Sidikalang,Indonesia,SDK         ",
                "1,0233,Sidoarjo,Indonesia,SDR           ",
                "1,0234,Sigli,Indonesia,SGL              ",
                "1,0235,Singaparna,Indonesia,SGP         ",
                "1,0236,Singaraja,Indonesia,SRJ          ",
                "1,0237,Singkawang,Indonesia,SGW         ",
                "1,0238,Sinjai,Indonesia,SJI             ",
                "1,0239,Sintang,Indonesia,SNT            ",
                "1,0240,Situbondo,Indonesia,SBD          ",
                "1,0241,Slawi,Indonesia,SLW              ",
                "1,0242,Sleman,Indonesia,SLM             ",
                "1,0243,Soasiu,Indonesia,SOS             ",
                "1,0244,Soe,Indonesia,SOE                ",
                "1,0245,Solo,Indonesia,SOL               ",
                "1,0246,Solok,Indonesia,SLK              ",
                "1,0247,Soreang,Indonesia,SRE            ",
                "1,0248,Sorong,Indonesia,SOR             ",
                "1,0249,Sragen,Indonesia,SRA             ",
                "1,0250,Stabat,Indonesia,STA             ",
                "1,0251,Subang,Indonesia,SUB             ",
                "1,0252,Sukabumi,Indonesia,SBM           ",
                "1,0253,Sukoharjo,Indonesia,SKH          ",
                "1,0254,Sumbawabesar,Indonesia,SBB       ",
                "1,0255,Sumber,Indonesia,SBR             ",
                "1,0256,Sumedang,Indonesia,SMD           ",
                "1,0257,Sumenep,Indonesia,SMP            ",
                "1,0258,Sungailiat,Indonesia,SGT         ",
                "1,0259,Sungaipenuh,Indonesia,SPH        ",
                "1,0260,Sungguminasa,Indonesia,SGM       ",
                "1,0261,Surabaya,Indonesia,SBY           ",
                "1,0262,Tabanan,Indonesia,TBN            ",
                "1,0263,Tahuna,Indonesia,THN             ",
                "1,0264,Takalar,Indonesia,TKL            ",
                "1,0265,Takengon,Indonesia,TKG           ",
                "1,0266,Tamianglayang,Indonesia,TML      ",
                "1,0267,Tanahgrogot,Indonesia,TNG        ",
                "1,0268,Tangerang,Indonesia,TGR          ",
                "1,0269,Tanjungbalai,Indonesia,TBL       ",
                "1,0270,Tanjungenim,Indonesia,TGM        ",
                "1,0271,Tanjungpandan,Indonesia,TPD      ",
                "1,0272,Tanjungpinang,Indonesia,TPN      ",
                "1,0273,Tanjungredep,Indonesia,TRD       ",
                "1,0274,Tanjungselor,Indonesia,TSL       ",
                "1,0275,Tanjungtabalong,Indonesia,TTL    ",
                "1,0276,Tapaktuan,Indonesia,TPT          ",
                "1,0277,Tarakan,Indonesia,TRK            ",
                "1,0278,Tarutung,Indonesia,TRT           ",
                "1,0279,Tasikmalaya,Indonesia,TML        ",
                "1,0280,Tebingtinggi,Indonesia,TTI       ",
                "1,0281,Tegal,Indonesia,TGL              ",
                "1,0282,Temanggung,Indonesia,TMG         ",
                "1,0283,Tembilahan,Indonesia,TBN         ",
                "1,0284,Tenggarong,Indonesia,TGG         ",
                "1,0285,Ternate,Indonesia,TNT            ",
                "1,0286,Tolitoli,Indonesia,TTL           ",
                "1,0287,Tondano,Indonesia,TDN            ",
                "1,0288,Trenggalek,Indonesia,TRG         ",
                "1,0289,Tual,Indonesia,TUA               ",
                "1,0290,Tuban,Indonesia,TUB              ",
                "1,0291,Tulungagung,Indonesia,TLG        ",
                "1,0292,Ujungberung,Indonesia,UBG        ",
                "1,0293,Ungaran,Indonesia,UGR            ",
                "1,0294,Waikabubak,Indonesia,WKB         ",
                "1,0295,Waingapu,Indonesia,WGP           ",
                "1,0296,Watampone,Indonesia,WTP          ",
                "1,0297,Watansoppeng,Indonesia,WSP       ",
                "1,0298,Wates,Indonesia,WAT              ",
                "1,0299,Wonogiri,Indonesia,WNG           ",
                "1,0300,Wonosari,Indonesia,WNS           ",
                "1,0301,Wonosobo,Indonesia,WNO           ",
                "1,0302,Yogyakarta,Indonesia,YGY         ",
                "1,0303,Jakarta Barat,Indonesia,JBR      ",
                "1,0304,Jakarta Pusat,Indonesia,JPT      ",
                "1,0305,Jakarta Utara,Indonesia,JUT      ",
                "1,0306,Jakarta Selatan,Indonesia,JSL    ",
                "1,0307,Jakarta Timur,Indonesia,JTM      ",
                "1,0308,Singapore,Singapore,SGP          ",
                "1,0309,New York,United States of America,NYK",
                "1,0310,Timika,Indonesia,TMK                 ",
                "1,0311,Bangka,Indonesia,BGA                 ",
                "1,0312,Ontario,Canada,NULL                  ",
                "1,0313,Sulawesi Utara,Indonesia,NULL        ",
                "1,0314,Seria,Brunei Darusalam,NULL          ",
                "1,0315,Bandar seri Begawan,Brunei Darusalam,NULL",
                "1,0316,Nusa Dua,Indonesia,NULL                  ",
                "1,0317,Surakarta,Indonesia,NULL                 ",
                "1,0318,Gowa,Indonesia,NULL                      ",
                "1,0319,Bandar Bukit Tinggi,Malaysia,NULL        ",
                "1,0320,Johor Bahru,Malaysia,NULL                ",
                "1,0321,Attadale Perth,Australia,NULL            ",
                "1,0322,Shiu Fai Terrace,Hongkong,NULL           ",
                "1,0323,Mumbai,India,NULL                        ",
                "1,0324,Ampang,Malaysia,NULL                     ",
                "1,0325,Mimika,Indonesia,MMK                     ",
                "1,0326,Bali,Indonesia,BAL                       ",
                "1,0327,Ambon,Indonesia,ABN                      ",
                "1,0328,Waringin Timur,Indonesia,NULL            ",
                "1,0329,Luwu Timur,Indonesia,NULL                ",
                "1,0330,Luwu Utara,Indonesia,NULL                ",
                "1,0331,Wajo,Indonesia,NULL                      ",
                "1,0332,Toba Samosir,Indonesia,NULL              ",
                "1,0333,Nunukan,Indonesia,NULL                   ",
                "1,0334,Kepulauan Selayar,Indonesia,NULL         ",
                "1,0335,Maluku Tenggara,Indonesia,NULL           ",
                "1,0336,Kuala Lumpur,Malaysia,NULL               ",
                "1,0337,Semenyih,Malaysia,SEM                    ",
                "1,0338,Cikarang,Indonesia,CIK                   ",
                "1,0339,Lombok Barat,Indonesia,NULL              ",
                "1,0340,Nusa Tenggara Timur,Indonesia,NULL       ",
                "1,0341,Asmat,Indonesia,NULL                     ",
                "1,0342,Labuhanbatu,Indonesia,NULL               ",
                "1,0343,Belitung,Indonesia,NULL                  ",
                "1,0344,Toraja Utara,Indonesia,NULL              ",
                "1,0345,Bone,Indonesia,NULL                      ",
                "1,0346,Lombok Timur,Indonesia,NULL              ",
                "1,0347,Sinzig,German,NULL                       ",
                "1,0348,Yahukimo,Indonesia,NULL                  ",
                "1,0349,Kepulauan Talaud,Indonesia,NULL          ",
                "1,0350,Minahasa,Indonesia,NULL                  ",
                "1,0351,Kepulauan Sangihe,Indonesia,NULL         ",
                "1,0352,Tidore Kepulauan,Indonesia,NULL          ",
                "1,0353,Tomohon,Indonesia,NULL                   ",
                "1,0354,Halmahera Utara,Indonesia,NULL           ",
                "1,0355,Ogan Ilir,Indonesia,NULL                 ",
                "1,0356,Halmahera Selatan,Indonesia,NULL         ",
                "1,0357,Minahasa Selatan,Indonesia,NULL          ",
                "1,0359,Minahasa Utara,Indonesia,NULL            ",
                "1,0360,Halmahera Timur ,Indonesia,NULL          ",
                "1,0361,Raja Ampat,Indonesia,NULL                ",
                "1,0362,Maluku Utara,Indonesia,NULL              ",
                "1,0363,Kalimantan Barat,Indonesia,NULL          ",
                "1,0364,Bolaang Mongondow Utara,Indonesia,NULL   ",
                "1,0365,Minahasa Tenggara,Indonesia,NULL         ",
                "1,0366,Bolaang Mongondow Timur,Indonesia,NULL   ",
                "1,0367,Tana Toraja,Indonesia,NULL               ",
                "1,0368,Tambrauw,Indonesia,NULL                  ",
                "1,0369,Bombana,Indonesia,NULL                   ",
                "1,0370,Miami,United States of America,MIA       ",
                "1,0371,Copenhagen,Denmark,CPH                   ",
                "1,0372,Chicago,United States of America,CHI     ",
                "1,0373,Bangkok,Thailand,BKK                     ",
                "1,0374,Badung,Indonesia,NULL                    ",
                "1,0375,London,United Kingdom,LDN                ",
                "1,0376,Buol,Indonesia,NULL                      ",
                "1,0377,Paris,France,NULL                        ",
                "1,0378,Banyumas,Indonesia,NULL                  ",
                "1,0379,Deli Serdang,Indonesia,NULL              ",
                "1,0380,Manggarai,Indonesia,NULL                 ",
                "1,0381,Tapanuli Tengah,Indonesia,NULL           ",
                "1,0382,Rome,Italy,NULL"
            };
            foreach (var item in listCity)
            {
                var citySeparete = item.Split(',');
                var cityPush = new MS_City()
                {
                    entityCode = citySeparete[0],
                    cityCode = citySeparete[1],
                    cityName = citySeparete[2],
                    country = citySeparete[3],
                    cityAbbreviation = citySeparete[4]
                };

                InitialCity.Add(cityPush);
            }

            foreach (var city in InitialCity)
            {
                AddIfNotExists(city);
            }
        }

        private void AddIfNotExists(MS_City city)
        {
            if (_context.MS_City.Any(l => l.cityCode == city.cityCode))
            {
                return;
            }
            _context.MS_City.Add(city);
            _context.SaveChanges();
        }
    }
}
