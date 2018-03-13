using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class CreateTAXTrFPHeaderInputDto
    {
        public string       entityCode          { get; set; }
        public string       coCode              { get; set; }
        public string       FPCode              { get; set; }
        public decimal      DPAmount            { get; set; }
        public string       FPBranchCode        { get; set; }
        public string       FPNo                { get; set; }
        public string       FPStatCode          { get; set; }
        public string       FPTransCode         { get; set; }
        public string       FPType              { get; set; }
        public string       FPYear              { get; set; }
        public string       NPWP                { get; set; }
        public string       accCode             { get; set; }
        public decimal      discAmount          { get; set; }
        public string       name                { get; set; }
        public int          payNo               { get; set; }
        public string       paymentCode         { get; set; }
        public string       pmtBatchNo          { get; set; }
        public string       priceType           { get; set; }
        public string       psCode              { get; set; }
        public string       rentalCode          { get; set; }
        public string       sourceCode          { get; set; }
        public DateTime     transDate           { get; set; }
        public string       transNo             { get; set; }
        public string       unitCode            { get; set; }
        public string       unitNo              { get; set; }
        public decimal      unitPriceAmt        { get; set; }
        public decimal      unitPriceVat        { get; set; }
        public string       userAddress         { get; set; }
        public decimal      vatAmt              { get; set; }
    }
}
