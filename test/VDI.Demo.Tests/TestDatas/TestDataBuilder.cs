using VDI.Demo.EntityFrameworkCore;

namespace VDI.Demo.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly DemoDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(DemoDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();
            new TestSubscriptionPaymentBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
