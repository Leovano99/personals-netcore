using Abp.AspNetCore.Mvc.ViewComponents;

namespace VDI.Demo.Web.Public.Views
{
    public abstract class DemoViewComponent : AbpViewComponent
    {
        protected DemoViewComponent()
        {
            LocalizationSourceName = DemoConsts.LocalizationSourceName;
        }
    }
}