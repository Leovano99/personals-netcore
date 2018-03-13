using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.Migrations.PropertySystemDb.Seed
{
    public class RentalStatusCreator
    {
        private readonly PropertySystemDbContext _context;

        public RentalStatusCreator(PropertySystemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateSeed();
        }

        private void CreateSeed()
        {
            List<LK_RentalStatus> InitialRentalStatus = new List<LK_RentalStatus>();
            List<String> listStatusRental = new List<string>()
            {
                "A,Available For Rent",
                "C,Casual Leasing",
                "N,Not Defined",
                "P,Casual Parking",
                "R,Rent Out"
            };
            foreach (var item in listStatusRental)
            {
                var rentalStatusSeparate = item.Split(',');

                var rentalStatusPush = new LK_RentalStatus()
                {
                    rentalStatusCode = rentalStatusSeparate[0],
                    rentalStatusName = rentalStatusSeparate[1]
                };

                InitialRentalStatus.Add(rentalStatusPush);
            }

            foreach (var rentalStatus in InitialRentalStatus)
            {
                AddIfNotExists(rentalStatus);
            }
        }

        private void AddIfNotExists(LK_RentalStatus rentalStatus)
        {
            if (_context.LK_RentalStatus.Any(l => l.rentalStatusCode == rentalStatus.rentalStatusCode))
            {
                return;
            }

            _context.LK_RentalStatus.Add(rentalStatus);

            _context.SaveChanges();
        }
    }
}
