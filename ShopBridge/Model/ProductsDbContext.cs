using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Model.Products;

namespace ShopBridge.Model
{
    public class ProductsDbContext:DbContext
    {
        public DbSet<InventoryDetails> InventoryDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "ShopBridgeDB.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
  

    }
}
