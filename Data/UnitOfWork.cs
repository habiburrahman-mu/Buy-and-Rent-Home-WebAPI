using BuyandRentHomeWebAPI.Data.Repo;
using BuyandRentHomeWebAPI.Data.Interfaces;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BuyRentHomeDbContext _dataContext;

        public UnitOfWork(BuyRentHomeDbContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public ICityRepository CityRepository => 
            new CityRepository(_dataContext);

        public IUserRepository UserRepository => 
            new UserRepository(_dataContext);

        public IPropertyRepository PropertyRepository => 
            new PropertyRepository(_dataContext);

        public IPropertyTypeRepository PropertyTypeRepository =>
            new PropertyTypeRepository(_dataContext);

        public IFurnishingTypeRepository FurnishingTypeRepository => 
            new FurnishingTypeRepository(_dataContext);

        public ICountryRepository CountryRepository =>
            new CountryRepository(_dataContext);

        public IPhotoRepository PhotoRepository=>
            new PhotoRepository(_dataContext);

        public async Task<bool> SaveAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
