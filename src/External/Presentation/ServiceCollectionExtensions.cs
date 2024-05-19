using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Services;

namespace Presentation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton<TokenService, TokenService>();
            serviceCollection.AddScoped<IUserContextService, UserContextService>();
            return serviceCollection;
        }
    }
}
