using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.EntityFrameworkCore;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class InitialDbBuilder
    {
        private readonly PersonalsNewDbContext _context;

        public InitialDbBuilder(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new AddrType(_context).Create();
            new BloodCreator(_context).Create();
            new CountryCreator(_context).Create();
            new SpecCreator(_context).Create();
            new ReligionCreator(_context).Create();
            new BankCreator(_context).Create();
            new DocumentCreator(_context).Create();
            new BankTypeCreator(_context).Create();
            new FamilyStatus(_context).Create();
            new GradeCreator(_context).Create();
            new IdTypeCreator(_context).Create();
            new KeyPeopleCreator(_context).Create();
            new MarStatusCreator(_context).Create();
            new PhoneTypeCreator(_context).Create();
            new JobTitleCreator(_context).Create();
            new NationCreator(_context).Create();
            new OccupationCreator(_context).Create();
            //new DefaultSettingsCreator(_context).Create();
            //new CityCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
