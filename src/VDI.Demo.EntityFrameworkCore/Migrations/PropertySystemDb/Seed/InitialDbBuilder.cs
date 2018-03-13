using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.EntityFrameworkCore;

namespace VDI.Demo.Migrations.PropertySystemDb.Seed
{
    public class InitialDbBuilder
    {
        private readonly PropertySystemDbContext _context;

        public InitialDbBuilder(PropertySystemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new RentalStatusCreator(_context).Create();

            new DepartmentCreator(_context).Create();

            new CountryCreator(_context).Create();

            new DetailCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
