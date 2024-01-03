using Microsoft.EntityFrameworkCore;
using PGB.Application.IRepositories;
using PGB.Domain.Entities;
using PGB.Infrastructure.Data;

namespace PGB.Infrastructure.Repositories;

public class BannedUserRepository : IBannedUserRepository
{
    private readonly IDBC _db;
    public BannedUserRepository(IDBC db) => _db = db;




    public async Task<bool> BanAsync(BannedUser bannedUser)
    {
        var user = await GetAsync(bannedUser.UserId);
        if (user is null)
        {
            await _db.BannedUsers.AddAsync(bannedUser);
            await _db.SaveChangesAsync();
            return true;
        }
        else return false;
    }

    public async Task<BannedUser> GetAsync(int userId)
    {
        var bannedUser = await _db.BannedUsers.FirstOrDefaultAsync(x=>x.UserId==userId);
        return bannedUser;
    }

    public async Task<List<BannedUser>> GetAllAsync()
    {
        var bannedUsers = await _db.BannedUsers.ToListAsync();
        return bannedUsers;
    }

    public async Task<bool> UnbanAsync(int id)
    {
        var user  = await GetAsync(id);
        if (user is null)
            return false;
        _db.BannedUsers.Remove(user);
        await _db.SaveChangesAsync();
        return true;
    }

    
    
    
    
    
 
}
