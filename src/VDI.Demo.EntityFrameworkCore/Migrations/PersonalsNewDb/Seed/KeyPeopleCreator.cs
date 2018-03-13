using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Migrations.PersonalsNewDb.Seed
{
    public class KeyPeopleCreator
    {
        private readonly PersonalsNewDbContext _context;

        public KeyPeopleCreator(PersonalsNewDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            var InitialKeyPeople = new LK_KeyPeople()
            {
                keyPeopleDesc = "CEO"
            };

            AddIfNotExists(InitialKeyPeople);
        }

        private void AddIfNotExists(LK_KeyPeople initialKeyPeople)
        {
            if (_context.LK_KeyPeople.Any(l => l.keyPeopleDesc == initialKeyPeople.keyPeopleDesc))
            {
                return;
            }

            _context.LK_KeyPeople.Add(initialKeyPeople);

            _context.SaveChanges();
        }
    }
}
