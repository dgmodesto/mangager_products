using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductApplicationService : IProductApplicationService
    {
        private const int MaxRetries = 3;
        private readonly IProductRepository _productRepository;
        private readonly IDistributedCache _cache;
        private readonly AsyncRetryPolicy<List<Product>> _retryPolicy;
        private static readonly Random _random = new Random();

        public ProductApplicationService(IProductRepository productRepository, IDistributedCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
            _retryPolicy = Policy<List<Product>>.Handle<HttpRequestException>(exception =>
            {
                return exception.Message != "This a fake request exception";
            }).WaitAndRetryAsync(MaxRetries, time => TimeSpan.FromSeconds(5));
        }

        public async Task<List<Product>> GetAllProducts()
        {

            return await _retryPolicy.ExecuteAsync(async () =>
            {

                if (_random.Next(1, 3) == 0)
                    throw new HttpRequestException("This is fake request exception");
                try
                {
                    List<Product> listProducts;

                    string cacheProduct = await _cache.GetStringAsync("productResult");
                    if (cacheProduct != null)
                    {
                        listProducts = JsonConvert.DeserializeObject<List<Product>>(cacheProduct);

                    }
                    else
                    {
                        listProducts = _productRepository.Get();

                        var stringResult = JsonConvert.SerializeObject(listProducts);
                        await _cache.SetStringAsync("productResult", stringResult);
                    }
                    return await Task.FromResult(listProducts);

                }
                catch (Exception)
                {

                    var listProducts = _productRepository.Get();
                    return await Task.FromResult(listProducts);

                }






            });



        }
    }
}
