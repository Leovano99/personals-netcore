using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetIDNumberDto
    {
        public string psCode { get; set; }
        public int refID { get; set; }
        public string idType { get; set; }
        public string idTypeName { get; set; }
        public string idNo { get; set; }
        public DateTime? expiredDate { get; set; }
        public string LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public string CreationTime { get; set; }
        public string CreatorUserId { get; set; }
    }
}
