using Ciber.Data.Entities;
using Ciber.Models;

namespace Ciber.Services
{
    public interface IProductAppService
    {
        Task<List<ProductModel>> GetAllAsync();
        Task<ProductModel> GetByIdAsync(object? keyValues, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(Product entity);
        Task UpdateAsync(Product entity);
        Task DeleteAsync(int id);
        Task<ProductModel> GetAsync(int id);
        Task<bool> IsAmountGraterQuantityOfProductAsync(int productId, int amount);
    }
}
