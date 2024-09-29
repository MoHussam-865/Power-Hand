//using System.Data.Entity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;


namespace MyDatabase
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Item> Item { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceItem> InvoiceItem { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Post> Post { get; set; }


        // needed 
        public DatabaseContext() { }

        public DatabaseContext(
            DbContextOptions<DatabaseContext> options
        ) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasIndex(e => e.Name);
            modelBuilder.Entity<Employee>().HasIndex(e => e.Password);
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Settings)
                .WithOne(setting => setting.Employee)
                .HasForeignKey(setting => setting.EmployeeId);

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
                DataSource = $"C:\\Projects\\MyMainServer\\MyDatabase\\PowerHand.db"
            }.ToString();
            optionsBuilder.UseSqlite(connectionString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
