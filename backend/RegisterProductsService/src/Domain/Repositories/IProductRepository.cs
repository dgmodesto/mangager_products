using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {
        public List<Product> Get();
        public void Save(Product entity);
    }
}
