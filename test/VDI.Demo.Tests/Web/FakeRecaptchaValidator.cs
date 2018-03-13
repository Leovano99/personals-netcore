using System.Threading.Tasks;
using VDI.Demo.Security.Recaptcha;

namespace VDI.Demo.Tests.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
