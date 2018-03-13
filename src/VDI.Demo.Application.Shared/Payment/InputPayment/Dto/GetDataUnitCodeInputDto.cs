using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GetDataUnitCodeInputDto
    {
        public List<int> listProject { get; set; }
        public int projectID { get; set; }
    }
}
