using Microsoft.EntityFrameworkCore;

namespace apn_promise_recruiting_task.Model
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderITem> OrderITems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public string DbPath { get; }

        public ApplicationDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "orders.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Laptop", Price = 2500, Currency = "PLN" },
                new Product { ProductId = 2, Name = "Klawiatura", Price = 120, Currency = "PLN" },
                new Product { ProductId = 3, Name = "Mysz", Price = 90, Currency = "PLN" },
                new Product { ProductId = 4, Name = "Monitor", Price = 1000, Currency = "PLN" },
                new Product { ProductId = 5, Name = "Kaczka debuggująca", Price = 66, Currency = "PLN" }
            );
        }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required int Price { get; set; }
        public required string Currency { get; set; }
    }

    public class OrderITem
    {
        public int OrderITemId { get; set; }
        public int ProductId { get; set; }
        public required Product Product { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public required List<OrderITem> Orders { get; set; }
    }
}
