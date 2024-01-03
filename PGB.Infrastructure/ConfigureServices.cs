using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PGB.Application.IRepositories;
using PGB.Infrastructure.Data;
using PGB.Infrastructure.Repositories;

namespace PGB.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //DbContext
        string? con = configuration.GetConnectionString("con");

        services.AddDbContext<IDBC, SqlServerDBC>(op => op.UseSqlServer(con));

        //Repositories
        services.AddScoped<IBannedUserInfoRepository, BannedUserInfoRepository>();
        services.AddScoped<IBannedUserRepository, BannedUserRepository>();
        services.AddScoped<IBookOrderRepository, BookOrderRepository>();
        services.AddScoped<IUserOrderRepository, UserOrderRepository>();
        services.AddScoped<IUserPenaltyRepository, UserPenaltyRepository>();

        return services;
    }
}
