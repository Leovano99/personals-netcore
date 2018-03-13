using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.MS_Products.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Products
{
    public interface IMsProductAppService : IApplicationService
    {
        ListResultDto<GetAllProductListDto> GetAllMsProduct();
        ListResultDto<GetProductDropdownListDto> GetMsProductDropdown();
        ListResultDto<GetProductDropdownListDto> GetMsProductDropdownByProjectClusterCategory(GetMsProductDropdownByProjectClusterCategoryInputDto input);
        void CreateMsProduct(CreateMsProductDto input);
        void UpdateMsProduct(UpdateMsProductDto input);
        void DeleteMsProduct(int Id);
    }
}
