using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.Migrations.PropertySystemDb.Seed
{
    public class DepartmentCreator
    {
        private readonly PropertySystemDbContext _context;

        public DepartmentCreator(PropertySystemDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateSeed();
        }

        private void CreateSeed()
        {
            List<MS_Department> InitialDepartment = new List<MS_Department>();
            List<String> listDepartment = new List<string>()
            {
                "FIN,Finance,08123456789,fin@company.com",
                "BM,Building Management,08123456788,bm@company.com",
                "CC,Call Center,08123456787,cc@company.com",
                "PSAS,PSAS,08123456786,psas@company.com",
                "BR,Bank Relation,08123456785,br@company.com",
                "PG,Product General,081234567890,pg@company.com"
            };
            foreach (var item in listDepartment)
            {
                var departmentSeparate = item.Split(',');

                var departmentPush = new MS_Department()
                {
                    departmentCode = departmentSeparate[0],
                    departmentName = departmentSeparate[1],
                    isActive = true,
                    departmentWhatsapp = departmentSeparate[2],
                    departmentEmail = departmentSeparate[3]

                };

                InitialDepartment.Add(departmentPush);
            }

            foreach (var department in InitialDepartment)
            {
                AddIfNotExists(department);
            }
        }

        private void AddIfNotExists(MS_Department department)
        {
            if (_context.MS_Department.Any(l => l.departmentCode == department.departmentCode))
            {
                return;
            }

            _context.MS_Department.Add(department);

            _context.SaveChanges();
        }
    }
}
