using bidPursuit.Domain.Interfaces;
using bidPursuit.Domain.Services;
using bidPursuit.Infrastructure.Helper;
using bidPursuit.Infrastructure.Persistence;
using bidPursuit.Infrastructure.Repositories;
using bidPursuit.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bidPursuit.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddDbContext<BidPursuitDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        Services.AddScoped<IUserRepository, UserRepository>(); 
        Services.AddScoped<IUnitOfWork, UnitOfWork>();
        Services.AddScoped<IJwtService, JwtService>();
        Services.AddScoped<IPasswordHelper, PasswordHelper>();
        Services.AddScoped<IAuctionRepository, AuctionRepository>();
        Services.AddScoped<IVehicleRepository, VehicleRepository>();
        Services.AddScoped<IBidRepository, BidRepository>();
        Services.AddScoped<IAuctionParticipantRepository, AuctionParticipantRepository>();
        Services.AddScoped<ILotNumberService, LotNumberService>();
        return Services;
    }
}
