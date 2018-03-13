using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.Dto;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class CreateTrBookingDetailScheduleParamsDto
    {
        public double pctTax { get; set; }
        public GetPSASParamsDto psasParams { get; set; }
        public List<GetScheduleListDto> listSchedule { get; set; }
        //public TrBookingDetailScheduleInput inputBookingDetailSchedule { get; set; }
    }

    public class GetTrBookingDetailList
    {
        public int bookingDetailID { get; set; }
        public decimal percentage { get; set; }
        public int coCode { get; set; }
    }

    public class TrBookingDetailScheduleInput
    {
        public DateTime dueDate { get; set; }
        public string allocation { get; set; }
        public decimal totalAmount { get; set; }
    }
}
