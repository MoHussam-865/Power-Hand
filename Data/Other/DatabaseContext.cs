using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Power_Hand.Models;

namespace Power_Hand.DBContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(
            DbContextOptions<DatabaseContext> options
        ) : base(options) { }

        public DbSet<Item> Item { get; set; }
		public DbSet<Client> Client { get; set; }
		public DbSet<Invoice> Invoice { get; set; }
		public DbSet<InvoiceItem> InvoiceItem { get; set; }
        public DbSet<Emploee> Emploee { get; set; }

	}
}
