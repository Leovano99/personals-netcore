using VDI.Demo.Dto;

namespace VDI.Demo.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}