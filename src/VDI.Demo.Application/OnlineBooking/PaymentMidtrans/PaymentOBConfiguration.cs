using Abp.Dependency;
using Abp.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Configuration;

namespace VDI.Demo.OnlineBooking.PaymentMidtrans
{
    public class PaymentOBConfiguration : ITransientDependency
    {
            private readonly IConfigurationRoot _appConfiguration;

            public string Environment => _appConfiguration["Payment:Midtrans:Environment"];

            public string BaseUrl => _appConfiguration["Payment:Midtrans:BaseUrl"].EnsureEndsWith('/');

            public string reqToken => _appConfiguration["Payment:Midtrans:reqToken"];

            public string ServerKey => _appConfiguration["Payment:Midtrans:ServerKey"];

            public string ClientKey => _appConfiguration["Payment:Midtrans:ClientKey"];

            public string ApiPdfUrl => _appConfiguration["App:apiPdfUrl"];

            public PaymentOBConfiguration(IAppConfigurationAccessor configurationAccessor)
            {
                _appConfiguration = configurationAccessor.Configuration;
            }
        
    }
}
