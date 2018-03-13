using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Tax
{
    public interface IFpLkFpTransCodeAppService : IApplicationService
    {
        List<string> GetFPTransCodeDropdown();
    }
}
