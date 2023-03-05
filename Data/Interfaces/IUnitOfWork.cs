using System;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository CityRepository { get; }
        IUserRepository UserRepository { get; }
        IPropertyRepository PropertyRepository { get; }
        IPropertyTypeRepository PropertyTypeRepository { get; }
        IFurnishingTypeRepository FurnishingTypeRepository { get; }
        ICountryRepository CountryRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IUserPrivilegeRepository UserPrivilegeRepository{ get; }
        Task<bool> SaveAsync();
    }
}
