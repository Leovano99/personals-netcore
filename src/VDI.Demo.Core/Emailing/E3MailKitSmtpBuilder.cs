
using Abp.MailKit;
using Abp.Net.Mail.Smtp;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;

namespace VDI.Demo.Emailing
{
    public class E3MailKitSmtpBuilder : DefaultMailKitSmtpBuilder
    {
        private readonly IConfigurationRoot _appConfiguration;

        public E3MailKitSmtpBuilder(ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration, IConfigurationRoot appConfiguration)
        : base(smtpEmailSenderConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        protected override void ConfigureClient(SmtpClient client)
        {
            var ntlm = new SaslMechanismNtlm(_appConfiguration["App:UsernameSasl"], _appConfiguration["App:PasswordSasl"]);
            client.Authenticate(ntlm);

            base.ConfigureClient(client);
        }
    }
}
