using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Producer.Services.Products
{
    public interface IConsumerService
    {
        Task SendEventMessageAsync(Product product);

    }
}
