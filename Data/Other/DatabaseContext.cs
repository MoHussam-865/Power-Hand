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

        
        public DatabaseContext(
            DbContextOptions<DatabaseContext> options
        ) : base(options) { }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceItem>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            string connectionString = new SqliteConnectionStringBuilder()
            {
                DataSource= "E:\\My Projects\\PowerHand\\Power Hand\\PowerHand.db"
            }.ToString();
            optionsBuilder.UseSqlite (connectionString);
            
            base.OnConfiguring(optionsBuilder);
        }
    }
}
