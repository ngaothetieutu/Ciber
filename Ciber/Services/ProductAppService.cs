using AutoMapper;
using Ciber.Data.Entities;
using Ciber.Models;
using Ciber.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ciber.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IRepository<Product> _itemRepository;
        private readonly IMapper _mapper;
        public ProductAppService(IRepository<Product> itemRepository,
            IMapper mapper) 
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductModel>> GetAllAsync()
        {
            var items = await _itemRepository.GetAllAsync();
           
            return _mapper.Map<List<Product>,List<ProductModel>>(items);
        }


        public async Task<ProductModel> GetByIdAsync(object? keyValues, CancellationToken cancellationToken = default)
        {

            return _mapper.Map<ProductModel>(await _itemRepository.GetByIdAsync(keyValues));
        }

        public async Task<int> CreateAsync(Product entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var result = await _itemRepository.AddAsync(entity, true);
            return result.Id;

        }
        public async Task UpdateAsync(Product entity)
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
        public async Task<ProductModel> GetAsync(int id)
        {
            return _mapper.Map<ProductModel>(await _itemRepository.GetByIdAsync(id));
        }
        public async Task<bool> IsAmountGraterQuantityOfProductAsync(int productId, int amount)
        {
            var result = await _itemRepository.Where(c => c.Id == productId).Select(c => c.Quantity).FirstOrDefaultAsync();
            if (result < amount)
                return true;
            
            return false;
        }
    }
}
