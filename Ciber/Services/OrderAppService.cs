using AutoMapper;
using Ciber.Data.Entities;
using Ciber.Models;
using Ciber.Repositories;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace Ciber.Services
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IRepository<Order> _itemRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public OrderAppService(IRepository<Order> itemRepository,
            IRepository<Category> categoryRepository,
            IMapper mapper) {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<List<OrderModel>> GetAllAsync(string productName = "")
        {
            var items = await _itemRepository.WhereIf(!string.IsNullOrEmpty(productName), c => c.ProductName.ToUpper().Contains(productName.ToUpper()))
                                             .Include(c => c.User)
                                             .Include(c => c.Product)
                                             .ToListAsync();
            var categoryDict = (await _categoryRepository.GetAllAsync()).ToDictionary(c => c.Id, c => c.Name);
            return items.Select(c => new OrderModel
            {
                Id = c.Id,
                OrderName = c.OrderName,
                OrderDate = c.OrderDate,
                UserId = c.UserId,
                ProductName = c.ProductName,
                ProductId = c.ProductId,
                Amount = c.Amount,
                CustomerName = c.User.FullName,
                CategoryName = categoryDict[c.Product.CategoryId]
            }).ToList();
        }


        public async Task<OrderModel> GetByIdAsync(object? keyValues, CancellationToken cancellationToken = default)
        {

            return _mapper.Map<OrderModel>(await _itemRepository.GetByIdAsync(keyValues));
        }

        public async Task<int> CreateAsync(Order entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var result = await _itemRepository.AddAsync(entity, true);
            return result.Id;

        }
        public async Task UpdateAsync(Order entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _itemRepository.UpdateAsync(entity, true);

        }
        public async Task DeleteAsync(int id)
        {
            
            var entity = await _itemRepository.GetByIdAsync(id);
            await _itemRepository.DeleteAsync(entity);
        }
        public async Task<OrderModel> GetAsync(int id)
        {
            return _mapper.Map<OrderModel>(await _itemRepository.GetByIdAsync(id));
        }
    }
}
