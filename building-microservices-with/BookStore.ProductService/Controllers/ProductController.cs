using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.ProductService.Helpers.Extensions;
using BookStore.ProductService.Models;
using BookStore.ProductService.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.ProductService.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public IActionResult Get()
        {
            var productVm = _productRepository.GetAll().ToViewModel();
            return new OkObjectResult(productVm);
        }
    }
}