using System.Threading.Tasks;
using Abp.Dependency;

namespace VDI.Demo.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}