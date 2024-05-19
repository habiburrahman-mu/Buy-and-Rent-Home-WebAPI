using Application.Interfaces;
using Application.MappingProfiles;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

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
        serviceCollection.AddScoped<ICityService, CityService>();
        serviceCollection.AddScoped<ICountryService, CountryService>();
        serviceCollection.AddScoped<IFurnishingTypeService, FurnishingTypeService>();
        serviceCollection.AddScoped<IPropertyTypeService, PropertyTypeService>();

        return serviceCollection;
    }
}
