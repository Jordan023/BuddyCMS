using System.Data.Entity;
using Buddy.CodeFirst.Models.Address;
using Buddy.CodeFirst.Models.Hour;
using Buddy.CodeFirst.Models.Invoice;
using Buddy.CodeFirst.Models.Log;
using Buddy.CodeFirst.Models.Module;
using Buddy.CodeFirst.Models.User;

namespace Buddy.CodeFirst
{
    internal class Buddy
    {
        private static void Main()
        {

        }
    }

    public class BuddyContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogType> LogTypes { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceProduct> InvoiceProducts { get; set; }
        public DbSet<Hour> Hours { get; set; }

        public BuddyContext() : base("name=DefaultConnection")
        {

        }
    }
}
