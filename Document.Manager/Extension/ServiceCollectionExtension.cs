using Document.Manager.Gateways.Context;
using Document.Manager.Gateways.FileServices;
using Document.Manager.Repositories;
using Document.Manager.Services;
using Microsoft.EntityFrameworkCore;

namespace Document.Manager.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<ApplicationContext>(x => x.UseSqlServer(connectionString));
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IUserRepository, UserRepository>();
    }
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IUserService, UserService>()
            .AddScoped<IFileService, FileService>();
    }
}
