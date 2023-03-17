using Ciber.Data.Entities;

namespace Ciber.Services
{
    public interface IUserAppService
    {
        Task<List<ApplicationUser>> GetAllAsync();
    }
}
