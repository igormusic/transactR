using Microsoft.EntityFrameworkCore;
using transactR.Runtime.Accounts;

namespace transactR.Tests
{
    public class UnitOfWorkTest
    {
        private class TestContext : DbContext
        {
            public TestContext()
        {
               
            }

            public virtual DbSet<Account> Accounts { get; set; }
           
        }
    }
}
