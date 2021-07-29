using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManagement.Models;
using InventoryManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        //GET: api/products
        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productsRepository.GetAllProductsAsync();
            return Ok(products);
        }

        //GET: api/products/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productsRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        //GET: api/products/report/monthly/2021-07-20
        [HttpGet("report/monthly/{month}")]
        public async Task<IActionResult> MonthlyReport(string month)
        {
            var transactions = await _productsRepository.MonthlyTransactionReportAsync(month);


            if (transactions == null)
            {
                return NotFound();
            }

            return Ok(transactions);
        }

        //GET: api/products/report/daily/2021-07-20
        [HttpGet("report/daily/{date}")]
        public async Task<IActionResult> DailyReport(string date)
        {
            var transactions = await _productsRepository.DailyTransactionReportAsync(date);

            if (transactions == null)
            {
                return NotFound();
            }

            return Ok(transactions);
        }

        //POST: api/products
        [HttpPost("")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            var pid = await _productsRepository.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new {id = pid, controller="products"}, pid);
        }

        //POST: api/products/purchase
        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseProduct(Stock purchaseInfo)
        {
            var purchase = await _productsRepository.PurchaseAsync(purchaseInfo);
            if (purchase == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(purchase);
            }
        }

        //POST: api/products//sale
        [HttpPost("sale")]
        public async Task<IActionResult> SaleProduct(Stock saleInfo)
        {
            var sale = await _productsRepository.SaleAsync(saleInfo);
            if (sale == null)
            {
                return BadRequest();
            }
           
            return Ok(sale);
            
        }

        //PUT: api/products
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product temProduct)
        {
            var product = await _productsRepository.UpdateProductAsync(id, temProduct);
            return Ok(product);
        }

        //Delete: api/products/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productsRepository.DeleteProductAsync(id);
            return Ok();
        }
        
    }
}




