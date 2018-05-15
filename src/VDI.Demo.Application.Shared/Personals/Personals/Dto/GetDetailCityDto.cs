using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetDetailCityDto
    {
        public string country { get; set; }
        public string provinceName { get; set; }
        public string provinceCode { get; set; }
        public string cityCode { get; set; }
        public string cityName { get; set; }
        public string postCode { get; set; }
    }
}
