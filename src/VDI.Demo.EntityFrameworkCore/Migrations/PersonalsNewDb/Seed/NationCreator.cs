using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class NationCreator
    {
        private readonly PersonalsNewDbContext _context;

        public NationCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            List<MS_Nation> InitialNation = new List<MS_Nation>();
            List<String> listNation = new List<string>()
            {
                "1,001,Indonesian,62",
                "1,002,Korean,14",
                "1,003,Afghan,NULL",
                "1,004,Albanian,118",
                "1,005,Algerian,39",
                "1,006,American,1",
                "1,007,Andorran,NULL",
                "1,008,Angolan,NULL",
                "1,009,Antiguans,NULL",
                "1,010,Argentinean,20",
                "1,011,Armenian,NULL",
                "1,012,Australian,17",
                "1,013,Austrian,35",
                "1,014,Azerbaijani,NULL",
                "1,015,Bahamian,NULL",
                "1,016,Bahraini,NULL",
                "1,017,Bangladeshi,NULL",
                "1,018,Barbadian,NULL",
                "1,019,Barbudans,NULL",
                "1,020,Batswana,NULL",
                "1,021,Belarusian,NULL",
                "1,022,Belgian,29",
                "1,023,Belizean,163",
                "1,024,Beninese,NULL",
                "1,025,Bhutanese,NULL",
                "1,026,Bolivian,NULL",
                "1,027,Bosnian,NULL",
                "1,028,Brazilian,9",
                "1,029,British,6",
                "1,030,Bruneian,140",
                "1,031,Bulgarian,NULL",
                "1,032,Burkinabe,NULL",
                "1,033,Burmese,NULL",
                "1,034,Burundian,NULL",
                "1,035,Cambodian,NULL",
                "1,036,Cameroonian,NULL",
                "1,037,Canadian,13",
                "1,038,Cape Verdean,NULL",
                "1,039,Central African,NULL",
                "1,040,Chadian,NULL",
                "1,041,Chilean,NULL",
                "1,042,Chinese,2",
                "1,043,Colombian,NULL",
                "1,044,Comoran,NULL",
                "1,045,Congolese,NULL",
                "1,046,Costa Rican,78",
                "1,047,Croatian,NULL",
                "1,048,Cuban,NULL",
                "1,049,Cypriot,NULL",
                "1,050,Czech,NULL",
                "1,051,Danish,NULL",
                "1,052,Djibouti,NULL",
                "1,053,Dominican,NULL",
                "1,054,Dutch / Netherlander,23",
                "1,055,East Timorese,NULL",
                "1,056,Ecuadorean,NULL",
                "1,057,Egyptian,NULL",
                "1,058,Emirian,NULL",
                "1,059,Equatorial Guinean,NULL",
                "1,060,Eritrean,NULL",
                "1,061,Estonian,NULL",
                "1,062,Ethiopian,NULL",
                "1,063,Fijian,NULL",
                "1,064,Filipino,25",
                "1,065,Finnish,NULL",
                "1,066,French,7",
                "1,067,Gabonese,NULL",
                "1,068,Gambian,NULL",
                "1,069,Georgian,NULL",
                "1,070,German,5",
                "1,071,Ghanaian,NULL",
                "1,072,Greek,36",
                "1,073,Grenadian,NULL",
                "1,074,Guatemalan,NULL",
                "1,075,Guinea-Bissauan,NULL",
                "1,076,Guinean,NULL",
                "1,077,Guyanese,NULL",
                "1,078,Haitian,NULL",
                "1,079,Herzegovinian,NULL",
                "1,080,Honduran,NULL",
                "1,081,Hungarian,NULL",
                "1,082,I-Kiribati,NULL",
                "1,083,Icelander,NULL",
                "1,084,Indian,4",
                "1,085,Iranian,22",
                "1,086,Iraqi,NULL",
                "1,087,Irish,51",
                "1,088,Irish,51",
                "1,089,Israeli,NULL",
                "1,090,Italian,8",
                "1,091,Ivorian,NULL",
                "1,092,Jamaican,NUL",
                "1,093,Japanese,3",
                "1,094,Jordanian,NULL",
                "1,095,Kazakhstani,NULL",
                "1,096,Kenyan,NULL",
                "1,097,Kittian and Nevisian,NULL",
                "1,098,Kuwaiti,NULL",
                "1,099,Kyrgyz,NULL",
                "1,100,Laotian,NULL",
                "1,101,Latvian,NULL",
                "1,102,Lebanese,NULL",
                "1,103,Liberian,NULL",
                "1,104,Libyan,NULL",
                "1,105,Liechtensteiner,NULL",
                "1,106,Lithuanian,NULL",
                "1,107,Luxembourger,NULL",
                "1,108,Macedonian,NULL",
                "1,109,Malagasy,NULL",
                "1,110,Malawian,NULL",
                "1,111,Malaysian,34",
                "1,112,Maldivan,NULL",
                "1,113,Malian,NULL",
                "1,114,Maltese,NULL",
                "1,115,Marshallese,NULL",
                "1,116,Mauritanian,NULL",
                "1,117,Mauritian,NULL",
                "1,118,Mexican,12",
                "1,119,Micronesian,NULL",
                "1,120,Moldovan,NULL",
                "1,121,Monacan,NULL",
                "1,122,Mongolian,NULL",
                "1,123,Moroccan,NULL",
                "1,124,Mosotho,NULL",
                "1,125,Motswana,NULL",
                "1,126,Mozambican,NULL",
                "1,127,Namibian,NULL",
                "1,128,Nauruan,NULL",
                "1,129,Nepalese,84",
                "1,130,New Zealander,59",
                "1,131,Ni-Vanuatu,NULL",
                "1,132,Nicaraguan,NULL",
                "1,133,Nigerian,NULL",
                "1,134,Nigerien,NULL",
                "1,135,North Korean,NULL",
                "1,136,Northern Irish,NULL",
                "1,137,Norwegian,46",
                "1,138,Omani,NULL",
                "1,139,Pakistani,NULL",
                "1,140,Palauan,NULL",
                "1,141,Panamanian,NULL",
                "1,142,Papua New Guinean,NULL",
                "1,143,Paraguayan,NULL",
                "1,144,Peruvian,NULL",
                "1,145,Polish,NULL",
                "1,146,Portuguese,NULL",
                "1,147,Qatari,NULL",
                "1,148,Romanian,NULL",
                "1,149,Russian,NULL",
                "1,150,Rwandan,NULL",
                "1,151,Saint Lucian,NULL",
                "1,152,Salvadoran,NULL",
                "1,153,Samoan,NULL",
                "1,154,San Marinese,NULL",
                "1,155,Sao Tomean,NULL",
                "1,156,Saudi,NULL",
                "1,157,Scottish,NULL",
                "1,158,Senegalese,NULL",
                "1,159,Serbian,77",
                "1,160,Seychellois,NULL",
                "1,161,Sierra Leonean,NULL",
                "1,162,Singaporean,55",
                "1,163,Slovakian,NULL",
                "1,164,Slovenian,NULL",
                "1,165,Solomon Islander,NULL",
                "1,166,Somali,NULL",
                "1,167,South African,NULL",
                "1,168,South Korean,14",
                "1,169,Spanish,11",
                "1,170,Sri Lankan,60",
                "1,171,Sudanese,NULL",
                "1,172,Surinamer,NULL",
                "1,173,Swazi,NULL",
                "1,174,Swedish,33",
                "1,175,Swiss,37",
                "1,176,Syrian,NULL",
                "1,177,Taiwanese,16",
                "1,178,Tajik,NULL",
                "1,179,Tanzanian,NULL",
                "1,180,Thai,21",
                "1,181,Togolese,NULL",
                "1,182,Tongan,NULL",
                "1,183,Trinidadian or Tobagonian,NULL",
                "1,184,Tunisian,NULL",
                "1,185,Turkish,18",
                "1,186,Tuvaluan,NULL",
                "1,187,Ugandan,NULL",
                "1,188,Ukrainian,NULL",
                "1,189,Uruguayan,NULL",
                "1,190,Uzbekistani,NULL",
                "1,191,Venezuelan,49",
                "1,192,Vietnamese,NULL",
                "1,193,Welsh,NULL",
                "1,194,Welsh,NULL",
                "1,195,Yemenite,NULL",
                "1,196,Zambian,131",
                "1,197,Zimbabwean,98",
                "1,198,Hongkong,40",
                "1,199,English,NULL"
            };
            foreach (var item in listNation)
            {
                var nationSeparete = item.Split(',');
                var nationPush = new MS_Nation()
                {
                    entityCode = nationSeparete[0],
                    nationID = nationSeparete[1],
                    nationality = nationSeparete[2],
                    ppatkNationCode = nationSeparete[3] == "NULL" ? null : nationSeparete[3]
                };

                InitialNation.Add(nationPush);
            }

            foreach (var nation in InitialNation)
            {
                AddIfNotExists(nation);
            }
        }

        private void AddIfNotExists(MS_Nation nation)
        {
            if (_context.MS_Nation.Any(l => l.nationID == nation.nationID))
            {
                return;
            }
            _context.MS_Nation.Add(nation);
            _context.SaveChanges();
        }
    }
}
