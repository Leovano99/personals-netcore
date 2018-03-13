using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using VDI.Demo.MultiTenancy.Accounting.Dto;

namespace VDI.Demo.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
