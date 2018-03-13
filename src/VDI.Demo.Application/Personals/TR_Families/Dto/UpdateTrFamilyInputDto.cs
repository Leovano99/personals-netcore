using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.TR_Families.Dto
{
    public class UpdateTrFamilyListDto
    {
        public string psCode { get; set; }

        public int refID { get; set; }

        public string familyName { get; set; }

        public string familyStatus { get; set; }

        public DateTime? birthDate { get; set; }

        public string occID { get; set; }
    }
}
