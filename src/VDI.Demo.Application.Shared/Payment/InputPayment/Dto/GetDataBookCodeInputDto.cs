using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GetDataBookCodeInputDto
    {
        public List<int> listProject  { get; set; }
        public int accountID { get; set; }
        public int projectID { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string bookCode { get; set; }
        public string psCode { get; set; }
        public int unitID { get; set; }
    }
}
