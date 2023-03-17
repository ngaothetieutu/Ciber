using Ciber.Data.Entities;
using Ciber.Models;

namespace Ciber.Services
{
    public partial interface IOrderAppService
    {
        Task<List<OrderModel>> GetAllAsync(string productName = "");
        Task<OrderModel> GetByIdAsync(object? keyValues, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(Order entity);
        Task UpdateAsync(Order entity);
        Task DeleteAsync(int id);
        Task<OrderModel> GetAsync(int id);
    }
}
