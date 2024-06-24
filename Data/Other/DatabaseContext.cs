//using System.Data.Entity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Power_Hand.Models;

namespace Power_Hand.DBContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Item> Item { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceItem> InvoiceItem { get; set; }
        public DbSet<Emploee> Emploee { get; set; }


        // needed 
        public DatabaseContext() { }

        public DatabaseContext(
            DbContextOptions<DatabaseContext> options
        ) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>()
                .HasMany(invoice => invoice.Items)
                .WithOne(invoiceItem => invoiceItem.Invoice)
                .HasForeignKey(InvoiceItem => InvoiceItem.InvoiceId);

            base.OnModelCreating(modelBuilder);
        }

        // important 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            string connectionString = new SqliteConnectionStringBuilder()
            {
                DataSource= "E:\\My Projects\\PowerHand\\Power Hand\\PowerHand.db"
            }.ToString();
            optionsBuilder.UseSqlite (connectionString);
            
            base.OnConfiguring(optionsBuilder);
        }
    }
}
