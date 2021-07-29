using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DatabaseContext _context;
        public ProductsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<int> AddProductAsync(Product tempProduct)
        {
            var product = new Product()
            {
                ProductName = tempProduct.ProductName,
                ProductDescription = tempProduct.ProductDescription,
                Unit = tempProduct.Unit,
                UnitPrice = tempProduct.UnitPrice
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Id;

        }

        public async Task<Product> UpdateProductAsync(int pid, Product tempProduct)
        {
            var product = new Product()
            {
                Id = pid,
                ProductName = tempProduct.ProductName,
                ProductDescription = tempProduct.ProductDescription,
                Unit = tempProduct.Unit,
                UnitPrice = tempProduct.UnitPrice
            };

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product;

        }

        public async Task DeleteProductAsync(int pid)
        {
            var product = new Product()
            {
                Id = pid
            };
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Stock> PurchaseAsync(Stock purchaseInfo)
        {
            var product = await _context.Products.FindAsync(purchaseInfo.ProductId);

            if (product != null && purchaseInfo.Type == "purchase" && purchaseInfo.Quantity >= 1)
            {
                product.Quantity += purchaseInfo.Quantity;
                purchaseInfo.TransactionDate = DateTime.Today;
                _context.Products.Update(product);
                _context.Stocks.Add(purchaseInfo);
                await _context.SaveChangesAsync();
                return purchaseInfo;
            }

            else
            {
                return null;
            }
        }

        public async Task<Stock> SaleAsync(Stock saleInfo)
        {
            var product = await _context.Products.FindAsync(saleInfo.ProductId);

            if(product != null && saleInfo.Type == "sale" && saleInfo.Quantity <= product.Quantity)
            {
                product.Quantity -= saleInfo.Quantity;
                saleInfo.TotalPrice = product.UnitPrice * saleInfo.Quantity;
                saleInfo.TransactionDate = DateTime.Today;
                _context.Products.Update(product);
                _context.Stocks.Add(saleInfo);
                await _context.SaveChangesAsync();
                return saleInfo;
            }

            return null;
            
        }

        public async Task<List<Stock>> MonthlyTransactionReportAsync(string month)
        {
            var query = DateTime.Parse(month);

            var transactions = _context.Stocks.Where(a => a.TransactionDate.Month == query.Month && a.TransactionDate.Year == query.Year).ToList();

            return transactions;
        }

        public async Task<List<Stock>> DailyTransactionReportAsync(string date)
        {
            var query = DateTime.Parse(date);

            var transactions = _context.Stocks.
                Where(a => a.TransactionDate.Date == query.Date && a.TransactionDate.Month == query.Month && a.TransactionDate.Year == query.Year).ToList();

            return transactions;
        }



    }
}
