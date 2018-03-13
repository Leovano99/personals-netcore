using Abp.AspNetZeroCore.Net;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using VDI.Demo.OnlineBooking.Email.Dto;

namespace VDI.Demo.OnlineBooking.Email
{
    public class EmailAppService : DemoAppServiceBase, IEmailAppService
    {
        private IHostingEnvironment _env;

        public EmailAppService(IHostingEnvironment env)
        {

            _env = env;
        }

        private string createEmailBody(string userName, string title, string message)
        {

            var filepath = _env.ContentRootPath;
            var file = System.IO.Path.Combine(filepath, "EmailTemplate/TemplateCoba.html");

            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(file))
            {

                body = reader.ReadToEnd();

            }

            body = body.Replace("{UserName}", userName); //replacing the required things  

            body = body.Replace("{Title}", title);

            body = body.Replace("{message}", message);

            return body;

        }

        public string bodyAfterReserved(AfterReservedInputDto input)
        {

            var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "EmailTemplate/AfterReservedTemplate.html");

            string body = string.Empty;

            using (StreamReader reader = new StreamReader(file))
            {

                body = reader.ReadToEnd();

            }

            var lippoLogo = System.IO.Path.Combine(webRoot, "lippologo.png");

            body = body.Replace("{projectImage}", input.projectImage);
            body = body.Replace("{customerName}", input.customerName);
            body = body.Replace("{orderCode}", input.orderCode);
            body = body.Replace("{unitCode}", input.unitCode);
            body = body.Replace("{unitNo}", input.unitNo);
            body = body.Replace("{orderDate}", input.orderDate.ToString());
            body = body.Replace("{projectName}", input.projectName);
            body = body.Replace("{clusterName}", input.clusterName);
            body = body.Replace("{bookingFee}", input.BFAmount.ToString());
            body = body.Replace("{bankName}", input.bankName);
            body = body.Replace("{vaNumber}", input.vaNumber);
            body = body.Replace("{expiredDate}", input.expiredDate.ToString());
            body = body.Replace("{memberName}", input.memberName);
            body = body.Replace("{memberPhone}", input.memberPhone);
            body = body.Replace("{devPhone}", input.devPhone);
            body = body.Replace("{marketingOffice}", input.marketingOffice);
            body = body.Replace("{liipoLogo}", lippoLogo);

            return body;

        }

        public string Reminder2Jam(Reminder2JamInputDto input)
        {
            var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "EmailTemplate/Reminder2Jam.html");

            string body = string.Empty;

            using (StreamReader reader = new StreamReader(file))
            {
                body = reader.ReadToEnd();
            }

            var lippoLogo = System.IO.Path.Combine(webRoot, "lippologo.png");

            body = body.Replace("{projectImage}", input.projectImage);
            body = body.Replace("{customerName}", input.customerName);
            body = body.Replace("{unitCode}", input.unitCode);
            body = body.Replace("{unitNo}", input.unitNo);
            body = body.Replace("{orderDate}", input.orderDate.ToString());
            body = body.Replace("{bookingFee}", input.bookingFee.ToString());
            body = body.Replace("{bankName}", input.bankName);
            body = body.Replace("{vaNumber}", input.vaNumber);
            body = body.Replace("{memberName}", input.memberName);
            body = body.Replace("{memberPhone}", input.memberPhone);
            body = body.Replace("{devPhone}", input.devPhone);
            body = body.Replace("{marketingOffice}", input.marketingOffice);
            body = body.Replace("{projectName}", input.projectName);
            body = body.Replace("{lippoLogo}", lippoLogo);

            return body;
        }

        public string UnitExpired(UnitExpiredInputDto input)
        {
            var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "EmailTemplate/UnitExpired.html");

            string body = string.Empty;

            using (StreamReader reader = new StreamReader(file))
            {
                body = reader.ReadToEnd();
            }

            var lippoLogo = System.IO.Path.Combine(webRoot, "lippologo.png");

            body = body.Replace("{projectImage}", input.projectImage);
            body = body.Replace("{customerName}", input.customerName);
            body = body.Replace("{orderCode}", input.orderCode);
            body = body.Replace("{unitCode}", input.unitCode);
            body = body.Replace("{unitNo}", input.unitNo);
            body = body.Replace("{memberName}", input.memberName);
            body = body.Replace("{memberPhone}", input.memberPhone);
            body = body.Replace("{devPhone}", input.devPhone);
            body = body.Replace("{marketingOffice}", input.marketingOffice);
            body = body.Replace("{lippoLogo}", lippoLogo);

            return body;
        }

        public string bookingSuccess(BookingSuccessInputDto input)
        {
            var webRoot = _env.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "EmailTemplate/BookingSuccess.html");

            string body = string.Empty;

            using (StreamReader reader = new StreamReader(file))
            {
                body = reader.ReadToEnd();
            }

            var lippoLogo = System.IO.Path.Combine(webRoot, "lippologo.png");

            body = body.Replace("{projectImage}", input.projectImage);
            body = body.Replace("{customerName}", input.customerName);
            body = body.Replace("{bookDate}", input.bookDate.ToString());
            body = body.Replace("{memberName}", input.memberName);
            body = body.Replace("{memberPhone}", input.memberPhone);
            body = body.Replace("{devPhone}", input.devPhone);
            body = body.Replace("{projectName}", input.projectName);
            body = body.Replace("{lippoLogo}", lippoLogo);

            return body;
        }

        public void ConfigurationEmail(SendEmailInputDto input)
        {
            using (MailMessage mailMessage = new MailMessage())
            {

                mailMessage.From = new MailAddress("denykalpar@gmail.com");

                mailMessage.Subject = input.subject;

                mailMessage.Body = input.body; //Ini body

                mailMessage.IsBodyHtml = true;

                mailMessage.To.Add(new MailAddress(input.toAddress));

               mailMessage.Attachments.Add(new Attachment(input.pathKP, MimeTypeNames.ApplicationPdf));

                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";

                smtp.EnableSsl = true;

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                NetworkCred.UserName = "denykalpar@gmail.com";

                NetworkCred.Password = "ikhlas13";

                smtp.UseDefaultCredentials = false;

                smtp.Credentials = NetworkCred;

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.Port = 587;

                smtp.Send(mailMessage);

            }
        }
    }
}
