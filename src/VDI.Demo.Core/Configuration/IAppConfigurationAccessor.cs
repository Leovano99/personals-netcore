using Microsoft.Extensions.Configuration;

namespace VDI.Demo.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
