using Ciber.Data.Entities;
using Ciber.Models;
using Ciber.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Ciber.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IRepository<ApplicationUser> _itemRepository;
        public UserAppService(IRepository<ApplicationUser> itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            
            return items;
        }
    }
}
