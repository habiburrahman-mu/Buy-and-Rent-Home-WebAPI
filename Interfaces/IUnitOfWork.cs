using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        IUserRepository UserRepository { get; }
        IPropertyRepository PropertyRepository { get; }
        IPropertyTypeRepository PropertyTypeRepository { get; }
        IFurnishingTypeRepository FurnishingTypeRepository { get; }
        ICountryRepository CountryRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        Task<bool> SaveAsync();
    }
}
