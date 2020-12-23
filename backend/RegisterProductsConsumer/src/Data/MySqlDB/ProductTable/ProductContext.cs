

using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.MySqlDB.ProductTable
{
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }


    }
}
