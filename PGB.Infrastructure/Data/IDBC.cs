﻿using PGB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PGB.Infrastructure.Data;

public interface IDBC
{
    DbSet<BannedUserInfo> BannedUserInfos { get; set; }
    DbSet<BannedUser> BannedUsers { get; set; }
    DbSet<Order> BookOrders { get; set; }
    DbSet<UserPenalty> UserPenalties { get; set; }
    DbSet<UserOrder> UserOrders { get; set; }

    Task<int> SaveChangesAsync();
}