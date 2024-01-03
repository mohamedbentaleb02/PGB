using PGB.Application.DTOs.Users;
using PGB.Domain.Entities;

namespace PGB.Application.Interfaces;

public interface IService
{
    Task<bool> SaveBookOrder(Order bookOrder);
    Task<bool> ReturnBook(Order bookReturn);
    Task<IEnumerable<Book>> OrderBooks(Order bookOrder);
    Task<bool> SaveUserBanInfo(BannedUserInfo bannedUserInfo);
    Task<bool> BanUser(BannedUserDto banned);
    Task<IEnumerable<BannedUser>> GetAllBannedUsers();
    Task<BannedUserDto> GetBannedUser(int id);
    Task<bool> UnbanUser(int id);
    Task<bool> AddPenalty(UserPenalty userPenalty);
    Task<bool> RemovePenalty(int id);
}
