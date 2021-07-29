using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.Models;

namespace InventoryManagement.Repository
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task<int> AddProductAsync(Product tempProduct);
        Task<Product> UpdateProductAsync(int pid, Product tempProduct);
        Task DeleteProductAsync(int pid);
        Task<Stock> PurchaseAsync(Stock purchaseInfo);
        Task<Stock> SaleAsync(Stock saleInfo);
        Task<List<Stock>> MonthlyTransactionReportAsync(string month);
        Task<List<Stock>> DailyTransactionReportAsync(string date);
    }
}
