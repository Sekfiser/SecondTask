using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace SecondTask.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<TopRegionsProducts> TopRegionsProducts { get; set; } = null;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options) 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=SecondTask;",
            new MySqlServerVersion(new Version(8, 0, 30)));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Region> regions = new List<Region>();
            List<Product> products = new List<Product>();
            List<Price> prices = new List<Price>();
            List<Person> people = new List<Person>();
            List<OrderItem> orders = new List<OrderItem>();

            var rnd = new Random();

            for (int i = 1; i <= 20;  i++)
            {
                regions.Add(new Region { Id = i, RegionName = $"Регион {i}"});
            }


            for (int i = 1; i <= 15;  i++)
            {
                products.Add(new Product { Id = i, ProductName = $"Продукт {i}"});
            }

            int j = 1;
            foreach (var product in products)
            {
                for (int i = 1; i <= 20; i++)
                {
                    prices.Add(new Price { Id = j, Cost = rnd.Next(1000, 50000), ProductId = product.Id});
                    j++;
                }
            }

            j = 1;
            foreach (var region in regions)
            {
                int peopleCount = rnd.Next(200, 4000);
                for (int i = 1; i <= peopleCount; i++) 
                {
                    people.Add(new Person { Id = j, RegionId =  region.Id, PersonName = $"Человек {j}"});
                    j++;
                }
            }

            j = 1;
            foreach (var person in people)
            {
                int orderCount = rnd.Next(1, 5);
                for (int i = 1; i <= orderCount; i++)
                {
                    int productCount = rnd.Next(1, 15);
                    int rndPriceId = rnd.Next(1, prices.Count);
                    orders.Add(new OrderItem { Id = j, PersonId = person.Id, PriceId = rndPriceId, ProductCount = productCount, DateHandling = DateTime.Now});
                    j++;
                }
            }

            modelBuilder.Entity<Region>().HasData(regions);
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Price>().HasData(prices);
            modelBuilder.Entity<Person>().HasData(people);
            modelBuilder.Entity<OrderItem>().HasData(orders);
        }
    }
}
