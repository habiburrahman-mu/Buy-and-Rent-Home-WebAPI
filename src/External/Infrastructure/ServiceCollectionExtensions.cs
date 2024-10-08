using Domain.Interfaces.Data;
using Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        serviceCollection.AddDbContext<BuyRentHomeDbContext>(options => options.UseSqlServer(connectionString));

        //var currentAssembly = typeof(BuyRentHomeDbContext).Assembly;
        //var respositoryTypes = currentAssembly
        //    .GetTypes()
        //    .Where(x => x.IsInterface && x.Name.EndsWith("Repository") && x != typeof(GenericRepository<>));
        
        //foreach (var respositoryType in respositoryTypes)
        //{
        //    foreach(var interfaceItem in respositoryType.GetInterfaces())
        //    {
        //        serviceCollection.AddScoped(interfaceItem, respositoryType);
        //    }
        //}



        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<IFileService, FileService>();
        serviceCollection.AddScoped<INotificationService, NotificationService>();
        serviceCollection.AddSignalR();

        return serviceCollection;
    }
}
