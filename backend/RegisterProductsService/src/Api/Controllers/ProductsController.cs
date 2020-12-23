using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.ViewModels;
using Application.Services;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Producer.Services.Products;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IConsumerService _consumerService;
        private readonly IProductApplicationService _productApplicationService;
        private readonly IMapper _mapper;


        public ProductsController(ILogger<ProductsController> logger, IConsumerService consumerService, IProductApplicationService productApplicationService, IMapper mapper)
        {
            _logger = logger;
            _consumerService = consumerService;
            _productApplicationService = productApplicationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            try
            {
                var products = await _productApplicationService.GetAllProducts();
                var viewModel =    _mapper.Map<List<Product>, List<ProductViewModel>>(products);
                _logger.LogInformation("Busca de produtos cadstrados");
                return Ok(viewModel);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao buscar produtos cadastrados");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Erro ao buscar produtos cadastrados");
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Products(ProductViewModel viewModel)
        {
            try
            {
                var product = _mapper.Map<ProductViewModel, Product>(viewModel);
                await _consumerService.SendEventMessageAsync(product);
                _logger.LogInformation("Produto cadstrados com sucesso.");
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar novo produto");
                return BadRequest();
            }
        }
    }
}
