﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.GeneratePrice.Dto
{
    public class GetMsUnitByProjectClusterDto
    {
        public string unitNo { get; set; }
        public string combinedUnitNo { get; set; }
        public string unitCertCode { get; set; }
        public string remarks { get; set; }
        public string prevUnitNo { get; set; }
        public int entityID { get; set; }
        public int unitCodeID { get; set; }
        public string unitCode { get; set; }
        public int areaID { get; set; }
        public int projectID { get; set; }
        public int categoryID { get; set; }
        public int clusterID { get; set; }
        public int productID { get; set; }
        public int detailID { get; set; }
        public int zoningID { get; set; }
        public int facingID { get; set; }
        public int unitStatusID { get; set; }
        public int rentalStatusID { get; set; }
        public int termMainID { get; set; }
        public int? tokenNo { get; set; }
    }
}
