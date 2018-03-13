using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VDI.Demo.EntityFrameworkCore;

namespace VDI.Demo.Tax
{
    public class FpLkFpTransCodeAppService : DemoAppServiceBase, IFpLkFpTransCodeAppService
    {
        private readonly TAXDbContext _context;

        public FpLkFpTransCodeAppService(TAXDbContext context)
        {
            _context = context;
        }

        public List<string> GetFPTransCodeDropdown()
        {
            var result = (from x in _context.FP_LK_FPTransCode
                          select x.FPTransCode).ToList();
            
            return result;
        }
    }
}
