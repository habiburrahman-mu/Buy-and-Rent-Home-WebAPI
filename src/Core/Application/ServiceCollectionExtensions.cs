using Application.Interfaces;
using Application.MappingProfiles;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        serviceCollection.AddScoped<ISharedService, SharedService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IPhotoService, PhotoService>();
        serviceCollection.AddScoped<IPropertyService, PropertyService>();
        serviceCollection.AddScoped<IRoleService, RoleService>();
        serviceCollection.AddScoped<IUserPrivilegeService, UserPrivilegeService>();
        serviceCollection.AddScoped<ICitiesAreaManagerService, CitiesAreaManagerService>();
        serviceCollection.AddScoped<IVisitingRequestService, VisitingRequestService>();

        return serviceCollection;
    }
}
