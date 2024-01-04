using PGB.Domain.Entities;

namespace PGB.Application.IRepositories;

public interface IUserPenaltyRepository
{
    Task<bool> PostPenaltyAsync(UserPenalty userPenalty);
    Task<bool> RemovePenaltyAsync(int userId);
    Task<bool> IncrementPenaltyAsync(int userId);
    Task<int> CountPenaltyAsync(int userId);
}