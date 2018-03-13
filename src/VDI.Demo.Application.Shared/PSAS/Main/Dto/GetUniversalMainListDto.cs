using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Main.Dto
{
    public class GetUniversalMainListDto
    {
        public List<GetPSASMainHeaderListDto> GetAllPSASMainHeaderDto;
        public GetPSASMainListDto GetPSASMainDto;
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public string bookCode { get; set; }

    }
}
