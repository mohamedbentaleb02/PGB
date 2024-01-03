using PGB.Domain.Entities;

namespace PGB.Application.IRepositories;

public interface IBannedUserRepository
{
    Task<bool> BanAsync(BannedUser banned);
    Task<bool> UnbanAsync(int id);
    Task<BannedUser> GetAsync(int userId);
    Task<List<BannedUser>> GetAllAsync();
}
