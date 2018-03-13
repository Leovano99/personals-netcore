using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Schedule.Dto
{
    public class CreateTrBookingDetailScheduleInput
    {
        public int ID { get; set; }
        public int allocID { get; set; }
        public int bookingDetailID { get; set; }
        public DateTime dueDate { get; set; }
        public int entityID { get; set; }
        public decimal netAmt { get; set; }
        public decimal netOut { get; set; }
        public string remarks { get; set; }
        public int schedNo { get; set; }
        public decimal vatAmt { get; set; }
        public decimal vatOut { get; set; }
    }
}
