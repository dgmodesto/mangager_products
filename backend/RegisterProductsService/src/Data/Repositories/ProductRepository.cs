using Data.MySqlDB.ProductTable;
using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }


        public List<Product> Get()
        {
            var result = _dbContext.Products.ToList<Product>();


            return result;
        }

        public void Save(Product entity)
        {
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
        }
    }
}
