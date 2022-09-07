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
        Task<bool> SaveAsync();
    }
}
