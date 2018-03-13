using Abp.Auditing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VDI.Demo.Configuration;

namespace VDI.Demo.Web.Controllers
{
    public class HomeController : DemoControllerBase
    {
        private readonly IConfigurationRoot _appConfiguration;

        public HomeController(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        [DisableAuditing]
        public IActionResult Index()
        {
            return Redirect(_appConfiguration["App:VirtualDirectory"] + "/swagger");
        }
    }
}
