using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.PaymentMidtrans.Dto
{
    public class PaymentOnlineBookingRequest
    {
        public string payment_type { get; set; }
        public transactionDetailsDto transaction_details { get; set; }
        public customerDetailsDto customer_details { get; set; }
        public List<itemDetailsDto> item_details { get; set; }
        public bankTransferDto bank_transfer { get; set; }
        public bcaKlikPayDto bca_klikpay { get; set; }
        public bcaKlikBcaDto bca_klikbca { get; set; }
        public mandiriClickPayDto mandiri_clickpay { get; set; }
        public cimbClicksDto cimb_clicks { get; set; }
        public telkomselCashDto telkomsel_cash { get; set; }
        public indosatDompetkuDto indosat_dompetku { get; set; }
        public mandiriEcash mandiri_ecash { get; set; }
        public creditCard credit_card { get; set; }
        public cstore cstore { get; set; }
        public customExpiryDto custom_expiry { get; set; }
    }
    public class customExpiryDto
    {
        public string order_time { get; set; }
        public string expiry_duration { get; set; }
        public string unit { get; set; }
    }
    public class transactionDetailsDto
    {
        public decimal gross_amount { get; set; }
        public string order_id { get; set; }
    }
    public class customerDetailsDto
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
    }
    public class itemDetailsDto
    {
        public string id { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public string name { get; set; }
    }
    public class bankTransferDto
    {
        public string bank { get; set; }
        //public string va_number { get; set; }
    }
    public class bcaKlikPayDto
    {
        public string type { get; set; }
        public string description { get; set; }
    }
    public class bcaKlikBcaDto
    {
        public string description { get; set; }
        public string user_id { get; set; }
    }
    public class mandiriClickPayDto
    {
        public string card_number { get; set; }
        public string input1 { get; set; }
        public string input2 { get; set; }
        public string input3 { get; set; }
        public string token { get; set; }
    }
    public class cimbClicksDto
    {
        public string description { get; set; }
    }
    public class telkomselCashDto
    {
        public bool promo { get; set; }
        public int is_reversal { get; set; }
        public string customer { get; set; }
    }
    public class indosatDompetkuDto
    {
        public string msisdn { get; set; }
    }
    public class mandiriEcash
    {
        public string description { get; set; }
    }
    public class creditCard
    {
        public string token_id { get; set; }
    }
    public class cstore
    {
        public string store { get; set; }
        public string message { get; set; }
    }
}
