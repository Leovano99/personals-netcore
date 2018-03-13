using Abp.AspNetZeroCore.Net;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.NewCommDB;
using VDI.Demo.OnlineBooking.Email;
using VDI.Demo.OnlineBooking.Email.Dto;
using VDI.Demo.OnlineBooking.PaymentMidtrans.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;

namespace VDI.Demo.OnlineBooking.PaymentMidtrans
{
    public class PaymentOBAppService : DemoAppServiceBase, IPaymentOBAppService, ITransientDependency
    {
        private readonly PaymentOBConfiguration _configuration;
        private readonly IPaymentMidtransHelper _paymentMidtranshelper;
        private readonly IRepository<TR_UnitOrderHeader> _trUnitOrderHeader;
        private readonly IRepository<TR_UnitOrderDetail> _trUnitOrderDetail;
        private readonly IRepository<MS_Unit> _msUnit;
        private readonly IRepository<MS_UnitCode> _msUnitCode;
        private readonly IRepository<PERSONALS, string> _personalsRepo;
        private readonly IRepository<TR_BookingHeader> _bookingHeaderRepo;
        private readonly IRepository<MS_TujuanTransaksi> _tujuanTransaksiRepo;
        private readonly IRepository<MS_SumberDana> _sumberDanaRepo;
        private readonly IRepository<TR_ID, string> _trIDRepo;
        private readonly IRepository<MS_Schema, string> _msSchemaRepo;
        private readonly IRepository<PERSONALS_MEMBER, string> _personalsMemberRepo;
        private readonly IRepository<TR_Phone, string> _trPhoneRepo;
        private readonly PersonalsNewDbContext _personalContext;
        private readonly PropertySystemDbContext _propertySystemContext;
        private readonly NewCommDbContext _newCommContext;
        private readonly IEmailAppService _emailAppService;

        public PaymentOBAppService(PaymentOBConfiguration configuration,
            IPaymentMidtransHelper paymentMidtranshelper,
            IRepository<TR_UnitOrderHeader> trUnitOrderHeader,
            IRepository<TR_UnitOrderDetail> trUnitOrderDetail,
            IRepository<MS_Unit> msUnit,
            IRepository<MS_UnitCode> msUnitCode,
            IRepository<PERSONALS, string> personalsRepo,
            IRepository<TR_BookingHeader> bookingHeaderRepo,
            IRepository<MS_TujuanTransaksi> tujuanTransaksiRepo,
            IRepository<MS_SumberDana> sumberDanaRepo,
            IRepository<TR_ID, string> trIDRepo,
            IRepository<MS_Schema, string> msSchemaRepo,
            IRepository<PERSONALS_MEMBER, string> personalsMemberRepo,
            IRepository<TR_Phone, string> trPhoneRepo,
            PersonalsNewDbContext personalContext,
            PropertySystemDbContext propertySystemContext,
            NewCommDbContext newCommContext,
            IEmailAppService emailAppService)
        {
            _newCommContext = newCommContext;
            _propertySystemContext = propertySystemContext;
            _personalContext = personalContext;
            _trUnitOrderHeader = trUnitOrderHeader;
            _trUnitOrderDetail = trUnitOrderDetail;
            _msUnit = msUnit;
            _msUnitCode = msUnitCode;
            _personalsRepo = personalsRepo;
            _bookingHeaderRepo = bookingHeaderRepo;
            _tujuanTransaksiRepo = tujuanTransaksiRepo;
            _sumberDanaRepo = sumberDanaRepo;
            _trIDRepo = trIDRepo;
            _msSchemaRepo = msSchemaRepo;
            _personalsMemberRepo = personalsMemberRepo;
            _trPhoneRepo = trPhoneRepo;
            _paymentMidtranshelper = paymentMidtranshelper;
            _configuration = configuration;
            _emailAppService = emailAppService;
        }

        public async Task<PaymentOnlineBookingResponse> CreatePayment(PaymentOnlineBookingRequest input)
        {
            Logger.InfoFormat("CreatePaymentOnlineBooking() - Start.");
            var url = _configuration.BaseUrl.EnsureEndsWith('/') + "charge";
            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(_configuration.ServerKey));
            Logger.DebugFormat("url create payment : {0}", url);

            //List<string> test = new List<string>() { "foo", "boo", "john", "doe" };
            string delimiter = ",";
            string itemName = input.item_details.Select(i => i.name).Aggregate((i, j) => i + delimiter + j);
            string itemPrice = input.item_details.Select(i => i.price.ToString()).Aggregate((i, j) => i + delimiter + j);

            //Logger.DebugFormat("request payment midtrans. {0}" +
            //    "payment type                = {1}{0}" +
            //    "transaction details    { {0}" +
            //    "     gross amount           = {2}{0}" +
            //    "     order code             = {3}{0}" +
            //    "} {0}" +
            //    "customer details    { {0}" +
            //    "     email                  = {4}{0}" +
            //    "     first name             = {5}{0}" +
            //    "     last name              = {6}{0}" +
            //    "     phone                  = {7}{0}" +
            //    "} {0}" +
            //    "item details    { {0}" +
            //    "     name                   = {8}{0}" +
            //    "     price                  = {9}{0}" +
            //    "     quantity               = {15}{0}" +
            //    "} {0}" +
            //    "Bank Transfer    { {0}" +
            //    "     Bank                   = {10}{0}" +
            //    "} {0}" +
            //    "Custom Expiry    { {0}" +
            //    "     order time             = {12}{0}" +
            //    "     expiry duration        = {13}{0}" +
            //    "     unit                   = {14}{0}" +
            //    "} {0}",
            //Environment.NewLine, input.payment_type, input.transaction_details.gross_amount,
            //    input.transaction_details.order_id, input.customer_details.email, input.customer_details.first_name,
            //    input.customer_details.last_name, input.customer_details.phone, itemPrice, itemName,
            //    input.bank_transfer != null ? input.bank_transfer.bank:"", input.custom_expiry != null ? input.custom_expiry.order_time : "",
            //    input.custom_expiry != null ? input.custom_expiry.expiry_duration : "", input.custom_expiry != null ? input.custom_expiry.unit : "",input.item_details.Count);
            var test = JsonConvert.SerializeObject(input);
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                var request = CreateRequest(url, "Basic", authToken, client, HttpMethod.Post);
                request.Content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, MimeTypeNames.ApplicationJson);

                Logger.InfoFormat("CreatePaymentOnlineBooking() - End.");
                return await ReadResponse<PaymentOnlineBookingResponse>(url, client, request);
            }
        }

        public async Task<RequestTokenResultDto> RequestToken(RequestTokenInputDto input)
        {
            Logger.InfoFormat("RequestToken() - Start.");
            var url = _configuration.reqToken;
            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(_configuration.ServerKey));
            Logger.DebugFormat("url create payment : {0}", url);

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                var request = CreateRequest(url, "Basic", authToken, client, HttpMethod.Post);
                request.Content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, MimeTypeNames.ApplicationJson);

                Logger.InfoFormat("RequestToken() - End.");
                return await ReadResponseReqToken<RequestTokenResultDto>(url, client, request);
            }
        }

        private async Task<RequestTokenResultDto> ReadResponseReqToken<T>(string url, HttpClient client, HttpRequestMessage request)
        {
            Logger.InfoFormat("ReadResponsePayment() - Start.");
            var response = await client.SendAsync(request);
            Logger.InfoFormat("status payment midtrans : success ==> {0}", response.IsSuccessStatusCode);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Logger.Error($"Could not complete Paypal payment (url: {url}). Error: {error}");
                throw new UserFriendlyException(L("PaymentCouldNotCompleted"));
            }

            var success = await response.Content.ReadAsStringAsync();
            var responseData = _paymentMidtranshelper.ValidateResponseToken(JsonConvert.DeserializeObject<RequestTokenResultDto>(success));
            
            Logger.InfoFormat("ReadResponsePayment() - End.");

            return responseData;
        }

        private async Task<PaymentOnlineBookingResponse> ReadResponse<T>(string url, HttpClient client, HttpRequestMessage request)
        {
            Logger.InfoFormat("ReadResponsePayment() - Start.");
            var response = await client.SendAsync(request);
            Logger.InfoFormat("status payment midtrans : success ==> {0}",response.IsSuccessStatusCode);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Logger.Error($"Could not complete Paypal payment (url: {url}). Error: {error}");
                throw new UserFriendlyException(L("PaymentCouldNotCompleted"));
            }

            var success = await response.Content.ReadAsStringAsync();
            var responseData = _paymentMidtranshelper.ValidateResponseStatus(JsonConvert.DeserializeObject<PaymentOnlineBookingResponse>(success));
            Logger.DebugFormat("response payment midtrans. {0}" +
                "status code            = {1}{0}" +
                "status message         = {2}{0}" +
                "transaction id         = {3}{0}" +
                "transaction time       = {4}{0}" +
                "transaction status     = {5}{0}" +
                "payment type           = {6}{0}" +
                "gross amount           = {7}{0}" +
                "order id               = {8}{0}" +
                "error message          = {9}{0}" +
                "fraud status           = {10}{0}" +
                "signature key          = {11}{0}" +
                "approval code          = {12}{0}" +
                "billerKey|billerCode   = {13}{0}",
            Environment.NewLine, responseData.status_code, responseData.status_message,
                responseData.transaction_id, responseData.transaction_time,responseData.transaction_status,
                responseData.payment_type,responseData.gross_amount,responseData.order_id,
                responseData.error_messages,responseData.fraud_status,responseData.signature_key,
                responseData.approval_code,responseData.bill_key+"|"+responseData.biller_code);

            Logger.InfoFormat("ReadResponsePayment() - End.");

            return responseData;
        }

        private static HttpRequestMessage CreateRequest(string url, string authSchema, string authToken, HttpClient client, HttpMethod method)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.Authorization = new AuthenticationHeaderValue(authSchema, authToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeTypeNames.ApplicationJson));
            return request;
        }

        public async Task<PaymentOnlineBookingResponse> CheckPaymentStatus(string orderCode)
        {
            Logger.InfoFormat("CheckPaymentStatus() - Start.");
            var url = _configuration.BaseUrl.EnsureEndsWith('/') + orderCode + "/status";
            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(_configuration.ServerKey));
            Logger.DebugFormat("url create payment : {0}", url);
            Logger.DebugFormat("CheckPaymentStatus() : Param Sent: orderCode = {0}", orderCode);

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                var request = CreateRequest(url, "Basic", authToken, client, HttpMethod.Get);

                Logger.InfoFormat("CheckPaymentStatus() - End.");
                return await ReadResponse<PaymentOnlineBookingResponse>(url, client, request);
            }
        }

        public void PaymentNotification(PaymentOnlineBookingResponse data)
        {
            Logger.InfoFormat("PaymentNotification() - Start.");
            //var responseData = _paymentMidtranshelper.ValidateResponseStatus(JsonConvert.DeserializeObject<PaymentOnlineBookingResponse>(data));

            Logger.DebugFormat("response payment midtrans. {0}" +
                "status code            = {1}{0}" +
                "status message         = {2}{0}" +
                "transaction id         = {3}{0}" +
                "transaction time       = {4}{0}" +
                "transaction status     = {5}{0}" +
                "payment type           = {6}{0}" +
                "gross amount           = {7}{0}" +
                "order id               = {8}{0}" +
                "error message          = {9}{0}" +
                "fraud status           = {10}{0}" +
                "signature key          = {11}{0}" +
                "approval code          = {12}{0}" +
                "billerKey|billerCode   = {13}{0}",
            Environment.NewLine, data.status_code, data.status_message,
                data.transaction_id, data.transaction_time, data.transaction_status,
                data.payment_type, data.gross_amount, data.order_id,
                data.error_messages, data.fraud_status, data.signature_key,
                data.approval_code, data.bill_key + "|" + data.biller_code);
            Logger.InfoFormat("PaymentNotification() - End.");

        }

        //public async Task<ReqTokenResultDto> ReqToken(ReqTokenInputDto input)
        //{
        //    var url = "https://app.sandbox.midtrans.com/snap/v1/transactions";
        //    var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(_configuration.ServerKey));

            
        //    using (var client = new HttpClient())
        //    {
        //        client.Timeout = TimeSpan.FromMinutes(10);
        //        var request = CreateRequest(url, "Basic", authToken, client, HttpMethod.Post);
        //        request.Content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, MimeTypeNames.ApplicationJson);

        //        Logger.InfoFormat("CreatePaymentOnlineBooking() - End.");
        //        return await ReadResponse<PaymentOnlineBookingResponse>(url, client, request);
        //    }
        //}


        public void PaymentError(PaymentOnlineBookingResponse data)
        {
            Logger.InfoFormat("PaymentNotification() - Start.");
            //var responseData = _paymentMidtranshelper.ValidateResponseStatus(JsonConvert.DeserializeObject<PaymentOnlineBookingResponse>(data));

            Logger.DebugFormat("response payment midtrans. {0}" +
                "status code            = {1}{0}" +
                "status message         = {2}{0}" +
                "transaction id         = {3}{0}" +
                "transaction time       = {4}{0}" +
                "transaction status     = {5}{0}" +
                "payment type           = {6}{0}" +
                "gross amount           = {7}{0}" +
                "order id               = {8}{0}" +
                "error message          = {9}{0}" +
                "fraud status           = {10}{0}" +
                "signature key          = {11}{0}" +
                "approval code          = {12}{0}" +
                "billerKey|billerCode   = {13}{0}",
            Environment.NewLine, data.status_code, data.status_message,
                data.transaction_id, data.transaction_time, data.transaction_status,
                data.payment_type, data.gross_amount, data.order_id,
                data.error_messages, data.fraud_status, data.signature_key,
                data.approval_code, data.bill_key + "|" + data.biller_code);
            Logger.InfoFormat("PaymentError() - End.");
        }
        //public KonfirmasiPesananDto PaymentFinish(PaymentOnlineBookingResponse data)
        //{
        //    Logger.InfoFormat("PaymentFinish() - Start.");
            
        //    var dataKP = (from x in _propertySystemContext.TR_UnitOrderHeader
        //                  join a in _propertySystemContext.TR_UnitOrderDetail on x.Id equals a.UnitOrderHeaderID
        //                  join b in _propertySystemContext.MS_Unit on a.unitID equals b.Id
        //                  join d in _personalContext.PERSONAL.ToList() on x.psCode equals d.psCode
        //                  join e in _propertySystemContext.TR_BookingHeader on a.bookingHeaderID equals e.Id
        //                  join f in _propertySystemContext.MS_TujuanTransaksi on e.tujuanTransaksiID equals f.Id
        //                  join g in _propertySystemContext.MS_SumberDana on e.sumberDanaID equals g.Id
        //                  join h in _personalContext.TR_ID.ToList() on x.psCode equals h.psCode into iden
        //                  from h in iden.DefaultIfEmpty()
        //                  join i in _newCommContext.MS_Schema.ToList() on e.scmCode equals i.scmCode
        //                  join j in _personalContext.PERSONALS_MEMBER.ToList() on new { e.memberCode, i.scmCode } equals new { j.memberCode, j.scmCode }
        //                  join k in _personalContext.PERSONAL.ToList() on j.psCode equals k.psCode
        //                  join l in _personalContext.TR_Phone.ToList() on k.psCode equals l.psCode into phone
        //                  from l in phone.DefaultIfEmpty()
        //                  join m in _propertySystemContext.MS_Project on b.projectID equals m.Id
        //                  join p in _propertySystemContext.MS_Detail on b.detailID equals p.Id
        //                  join s in _propertySystemContext.MS_Term on e.termID equals s.Id
        //                  where x.Id == Convert.ToInt32(data.order_id) && new string[] { "1", "5", "7" }.Contains(h.idType)
        //                  select new KonfirmasiPesananDto
        //                  {
        //                      orderCode = x.orderCode,
        //                      kodePelanggan = x.psCode,
        //                      tanggalBooking = e.bookDate.ToString(),
        //                      psName = x.psName,
        //                      birthDate = d.birthDate.ToString(),
        //                      noHpPembeli = x.psPhone,
        //                      noIdentitas = (h == null ? "-" : h.idNo),
        //                      noNPWP = d.NPWP,
        //                      email = x.psEmail,
        //                      BookCode = e.bookCode,
        //                      hargaJual = a.sellingPrice.ToString(),
        //                      bfAmount = a.BFAmount.ToString(),
        //                      imageProject = m.image,
        //                      noHp = l.number,
        //                      noDealCloser = k.psCode,
        //                      namaDealCloser = k.name,
        //                      caraPembayaran = s.remarks,
        //                      tujuanTransaksi = f.tujuanTransaksiName,
        //                      sumberDanaPembelian = g.sumberDanaName,
        //                      namaBank = e.bankRekeningPemilik,
        //                      noRekening = e.nomorRekeningPemilik,
        //                      unitID = e.unitID
        //                  }).FirstOrDefault();

        //    var dataUnit = (from a in _propertySystemContext.MS_Unit
        //                    join b in _propertySystemContext.MS_Project on a.projectID equals b.Id
        //                    join c in _propertySystemContext.MS_Cluster on a.clusterID equals c.Id
        //                    join d in _propertySystemContext.MS_UnitItem on a.Id equals d.unitID
        //                    join e in _propertySystemContext.MS_Detail on a.detailID equals e.Id
        //                    join f in _propertySystemContext.MS_Category on a.categoryID equals f.Id
        //                    join g in _propertySystemContext.MS_UnitCode on a.unitCodeID equals g.Id
        //                    join h in _propertySystemContext.TR_BookingHeader on a.Id equals h.unitID
        //                    join i in _propertySystemContext.TR_BookingItemPrice on new { bookingHeaderID = h.Id, itemID = d.itemID } equals new { bookingHeaderID = i.bookingHeaderID, itemID = i.itemID }
        //                    join j in _propertySystemContext.MS_Renovation on i.renovCode equals j.renovationCode
        //                    join k in _propertySystemContext.TR_UnitOrderDetail on new { h.unitID, renovID = j.Id } equals new { k.unitID, k.renovID }
        //                    group d by new
        //                    {
        //                        d.unitID,
        //                        a.unitNo,
        //                        g.unitCode,
        //                        c.clusterName,
        //                        f.categoryName,
        //                        e.detailName,
        //                        j.renovationName
        //                    } into G
        //                    where G.Key.unitID == dataKP.unitID
        //                    select new unitDto
        //                    {
        //                        UnitNo = G.Key.unitNo,
        //                        UnitCode = G.Key.unitCode.Contains("-") ? G.Key.unitCode : null,
        //                        category = G.Key.categoryName,
        //                        cluster = G.Key.clusterName,
        //                        luas = G.Sum(d => d.area).ToString(),
        //                        renovation = G.Key.renovationName,
        //                        tipe = G.Key.detailName
        //                    }).ToList();

        //    dataKP.listUnit = dataUnit;

        //    var dataBank = (from bank in _propertySystemContext.MS_BankOLBooking
        //                    join unit in _propertySystemContext.MS_Unit on new { bank.projectID, bank.clusterID } equals new { unit.projectID, unit.clusterID }
        //                    join header in _propertySystemContext.TR_BookingHeader on unit.Id equals header.unitID
        //                    where unit.Id == dataKP.unitID
        //                    select new listBankDto
        //                    {
        //                        bankName = bank.bankName,
        //                        noVA = bank.bankRekNo
        //                    }).ToList();

        //    dataKP.listBank = dataBank;

        //    //var dataIlustrasi = (from a in _propertySystemContext.TR_BookingHeaderTerm
        //    //                     join c in _propertySystemContext.TR_BookingHeader on new { a.bookingHeaderID, a.termID } equals new { bookingHeaderID = c.Id, c.termID }
        //    //                     join d in _propertySystemContext.TR_BookingDetail on c.Id equals d.bookingHeaderID
        //    //                     where c.unitID == dataKP.unitID
        //    //                     group d by new
        //    //                     {
        //    //                         a.remarks,
        //    //                         c.bookDate,
        //    //                         c.Id,
        //    //                         termID = a.Id
        //    //                     } into G
        //    //                     select new listIlustrasiPembayaran
        //    //                     {
        //    //                         termName = G.Key.remarks,
        //    //                         bookingFee = G.Sum(d => d.BFAmount),
        //    //                         tglJatuhTempo = G.Key.bookDate,
        //    //                         termID = G.Key.termID
        //    //                     }).ToList();

        //    //var dataDP = (from a in _propertySystemContext.TR_BookingHeader
        //    //              join b in _propertySystemContext.MS_TermDP on a.termID equals b.termID
        //    //              where a.unitID == dataKP.unitID
        //    //              group b by new
        //    //              {
        //    //                  a.bookDate,
        //    //                  b.DPNo,
        //    //                  b.daysDue
        //    //              } into G
        //    //              select new listDpDto
        //    //              {
        //    //                  amount = G.Sum(a => a.DPAmount),
        //    //                  tglJatuhTempo = G.Key.bookDate.AddDays(G.Key.daysDue),
        //    //                  DPNo = G.Key.DPNo
        //    //              }
        //    //              ).ToList();

        //    //var datacicilan = (from a in _propertySystemContext.TR_BookingItemPrice
        //    //                   join b in _propertySystemContext.TR_BookingHeader on a.termID equals b.termID
        //    //                   join c in _propertySystemContext.TR_BookingSalesDisc on b.Id equals c.bookingHeaderID
        //    //                   join d in _propertySystemContext.TR_BookingDetail on b.Id equals d.bookingHeaderID
        //    //                   join e in _propertySystemContext.TR_BookingDetailAddDisc on d.Id equals e.bookingDetailID
        //    //                   join f in _propertySystemContext.MS_TermPmt on a.termID equals f.termID
        //    //                   join g in _propertySystemContext.TR_BookingHeaderTerm on a.termID equals g.termID
        //    //                   join h in _propertySystemContext.LK_FinType on f.finTypeID equals h.Id
        //    //                   group new { a, c, e } by new
        //    //                   {
        //    //                       a.termID,
        //    //                       b.unitID,
        //    //                       c.bookingHeaderID,
        //    //                       d.Id,
        //    //                       h.finTimes,
        //    //                       b.bookDate
        //    //                   } into G
        //    //                   where G.Key.unitID == dataKP.unitID
        //    //                   select new
        //    //                   {
        //    //                       //totalGP = G.Sum(gp => gp.a.grossPrice),
        //    //                       //totalSD = G.Sum(sd => sd.c.pctDisc),
        //    //                       //totalAD = G.Sum(ad => ad.e.pctAddDisc),
        //    //                       finTimes = G.Key.finTimes,
        //    //                       totalAmount = G.Sum(gp => gp.a.grossPrice) - 
        //    //                                    ((G.Sum(gp => gp.a.grossPrice) - (decimal)(1 - G.Sum(sd => sd.c.pctDisc)))+
        //    //                                    (G.Sum(gp => gp.a.grossPrice) - (decimal)(1 - G.Sum(ad => ad.e.pctAddDisc)))),
        //    //                       bookDate = G.Key.bookDate
        //    //                   }).Distinct().ToList();

        //    //var arrCicilan = new List<listCicilanDto>();

        //    //var cicilanNo = 0;
        //    //foreach (var item in datacicilan)
        //    //{
        //    //    //var totalAmount = item.totalGP - ((item.totalGP - (decimal)(1 - item.totalSD)) + (item.totalGP - (decimal)(1 - item.totalAD)));
        //    //    for(var i=0; i<= item.finTimes; i++)
        //    //    {
        //    //        var datacicilans = new listCicilanDto
        //    //        {
        //    //            amount = item.totalAmount / item.finTimes,
        //    //            cicilanNo = i,
        //    //            tglJatuhTempo = item.bookDate.AddDays(14).AddDays(30)
        //    //        };
        //    //        arrCicilan.Add(datacicilans);
        //    //    }

        //    //}
        //    ////GIMANA CARANYAA!!!!    
        //    //foreach (var item in dataIlustrasi)
        //    //{
        //    //    item.listDP = dataDP;
        //    //}

        //    //dataKP.ilustrasiPembayaran = dataIlustrasi;

        //    string json = JsonConvert.SerializeObject(dataKP);

        //    Logger.InfoFormat(json);

        //    var getProjectInfo = (from project in _propertySystemContext.MS_Project
        //                          join info in _propertySystemContext.MS_ProjectInfo on project.Id equals info.projectID into a
        //                          from projectInfo in a.DefaultIfEmpty()
        //                          join unit in _propertySystemContext.MS_Unit on project.Id equals unit.projectID
        //                          where unit.Id == dataKP.unitID
        //                          orderby projectInfo.CreationTime descending
        //                          select new
        //                          {
        //                              project.projectName,
        //                              projectInfo.projectMarketingOffice,
        //                              projectInfo.projectMarketingPhone
        //                          }).FirstOrDefault();

        //    Logger.InfoFormat(JsonConvert.SerializeObject(getProjectInfo));


        //    var sendEmail = new BookingSuccessInputDto
        //    {
        //        bookDate = DateTime.Now,
        //        customerName = dataKP.psName,
        //        devPhone = getProjectInfo.projectMarketingPhone != null ? getProjectInfo.projectMarketingPhone : "-",
        //        memberName = dataKP.namaDealCloser,
        //        memberPhone = dataKP.noHp,
        //        projectImage = dataKP.imageProject,
        //        projectName = getProjectInfo.projectName
        //    };
        //    var body = _emailAppService.bookingSuccess(sendEmail);

        //    //using (var client = new HttpClient())
        //    //{
        //    //    client.Timeout = TimeSpan.FromMinutes(10);
        //    //    var url = _configuration.ApiPdfUrl.EnsureEndsWith('/') + "api/Pdf/KonfirmasiPesananPdf";

        //    //    var request = new HttpRequestMessage(HttpMethod.Post, url);
        //    //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeTypeNames.ApplicationJson));
        //    //    request.Content = new StringContent(JsonConvert.SerializeObject(dataKP), Encoding.UTF8, MimeTypeNames.ApplicationJson);

        //    //    var response = await ReadResponse(url, client, request);
        //    //    //response.Replace(@"\", null);
        //    //    //response.Replace(@"\\", null);

        //    //    Logger.InfoFormat(response);

        //    //    var email = new SendEmailInputDto
        //    //    {
        //    //        body = body,
        //    //        toAddress = dataKP.email,
        //    //        subject = "Konfirmasi Pemesanan Unit" + dataUnit.FirstOrDefault().UnitCode + " " + dataUnit.FirstOrDefault().UnitNo,
        //    //        pathKP = response
        //    //    };

        //    //    _emailAppService.ConfigurationEmail(email);
        //    //}

        //    using (var client = new WebClient())
        //    {
        //        var url = _configuration.ApiPdfUrl.EnsureEndsWith('/') + "api/Pdf/KonfirmasiPesananPdf";
        //        client.Headers.Add("Content-Type:application/json");
        //        client.Headers.Add("Accept:application/json");
        //        var result = client.UploadString(url, JsonConvert.SerializeObject(dataKP));
        //        var trimResult= result.Replace(@"\\", @"\").Trim(new char[1] { '"' });
        //        Logger.InfoFormat(trimResult);

        //        var email = new SendEmailInputDto
        //        {
        //            body = body,
        //            toAddress = dataKP.email,
        //            subject = "Konfirmasi Pemesanan Unit" + dataUnit.FirstOrDefault().UnitCode + " " + dataUnit.FirstOrDefault().UnitNo,
        //            pathKP = trimResult
        //        };

        //        _emailAppService.ConfigurationEmail(email);
        //    }

        //    return dataKP;

        //}

        private async Task<string> ReadResponse(string url, HttpClient client, HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);
            var success = await response.Content.ReadAsStringAsync();
            var trimString = success.Replace(@"\\", @"\").Trim(new char[1] { '"' });
            
            return trimString;
        }
    }
}
