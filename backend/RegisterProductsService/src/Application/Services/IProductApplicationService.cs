using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IProductApplicationService
    {
       Task<List<Product>> GetAllProducts();
    }
}
