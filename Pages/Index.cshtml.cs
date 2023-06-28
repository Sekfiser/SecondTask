using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SecondTask.Models;

namespace SecondTask.Pages
{
    public class IndexModel : PageModel
    {
        ApplicationContext context;
        public List<TopRegionsProducts> TopRegionsProducts { get; private set; } = new();
        public IndexModel(ApplicationContext db)
        {
            context = db;
        }
        public void OnGet(int ProductCount = 2000, int ProductsCost = 50000000, int ProductsBuyers = 400)
        {

            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@ProductCount", ProductCount),
                                                                 new MySqlParameter("@ProductsCost", ProductsCost),
                                                                 new MySqlParameter("@ProductsBuyers", ProductsBuyers)};

            TopRegionsProducts = context.TopRegionsProducts.FromSqlRaw(
                "SET sql_mode ='';" +
                "SELECT ProductsStats.Id," +
                " ProductsStats.RegionName," +
                " ProductsStats.ProductName," +
                " MAX(ProductsStats.TotalCount) as TotalCount," +
                " ProductsStats.TotalBuyers," +
                " ROUND(ProductsStats.TotalCost, 2) as TotalCost " +
                "FROM (" +
                    "SELECT Region.Id, " +
                    "Region.RegionName, " +
                    "Product.ProductName, " +
                    "SUM(OrderItems.ProductCount) as TotalCount, " +
                    "COUNT(DISTINCT Person.Id) as TotalBuyers, " +
                    "SUM(Price.Cost * OrderItems.ProductCount) as TotalCost " +
                    "FROM OrderItems " +
                    "JOIN Person ON OrderItems.PersonId = Person.Id " +
                    "JOIN Price ON OrderItems.PriceId = Price.Id " +
                    "JOIN Product ON Price.ProductId = Product.Id " +
                    "JOIN Region ON Person.RegionId = Region.Id " +
                    "GROUP BY Region.Id, Product.ProductName " +
                    "HAVING SUM(OrderItems.ProductCount) >= @ProductCount AND " +
                    "SUM(Price.Cost * OrderItems.ProductCount) >= @ProductsCost AND " +
                    "COUNT(DISTINCT Person.Id) >= @ProductsBuyers " +
                    "ORDER BY TotalCount DESC, TotalCost DESC) ProductsStats " +
                    "GROUP BY ProductsStats.RegionName " +
                    "LIMIT 10;", parameters).ToList();
        }
    }
}