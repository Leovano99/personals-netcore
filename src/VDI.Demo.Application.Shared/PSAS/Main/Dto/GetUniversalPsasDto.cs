using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.Price.Dto;
using VDI.Demo.PSAS.Term.Dto;

namespace VDI.Demo.PSAS.Main.Dto
{
    public class GetUniversalPsasDto
    {
        public GetPSASMainUnitDetailDto psasMain { get; set; }
        public GetUniversalListDto psasPrice { get; set; }
        public List<GetPSASMainPaymentDto> psasPayment { get; set; }
        public List<GetPSASMainScheduleDto> psasSchedule { get; set; }
    }
}
