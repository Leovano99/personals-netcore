using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetDataMemberUplineInsertInputDto
    {
        public int id { get; set; }
        public string bookNo { get; set; }
        public string devCode { get; set; }
        public string memberCode { get; set; }
        public int entityID { get; set; }
        public int developerSchemaID { get; set; }
    }
}
