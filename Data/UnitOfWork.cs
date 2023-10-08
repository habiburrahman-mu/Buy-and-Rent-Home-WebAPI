using BuyandRentHomeWebAPI.Data.Repo;
using BuyandRentHomeWebAPI.Data.Interfaces;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BuyRentHomeDbContext _dbContext;

        public UnitOfWork(BuyRentHomeDbContext dataContext)
        {
            this._dbContext = dataContext;
        }
        public ICityRepository CityRepository =>
            new CityRepository(_dbContext);

        public IUserRepository UserRepository =>
            new UserRepository(_dbContext);

        public IPropertyRepository PropertyRepository =>
            new PropertyRepository(_dbContext);

        public IPropertyTypeRepository PropertyTypeRepository =>
            new PropertyTypeRepository(_dbContext);

        public IFurnishingTypeRepository FurnishingTypeRepository =>
            new FurnishingTypeRepository(_dbContext);

        public ICountryRepository CountryRepository =>
            new CountryRepository(_dbContext);

        public IPhotoRepository PhotoRepository =>
            new PhotoRepository(_dbContext);
        public IUserPrivilegeRepository UserPrivilegeRepository =>
            new UserPrivilegeRepository(_dbContext);

        public IRoleRepository RoleRepository =>
            new RoleRepository(_dbContext);

        public ICitiesAreaManagerRepository CitiesAreaManagerRepository =>
            new CitiesAreaManagerRepository(_dbContext);

        public IVisitingRequestRepository VisitingRequestRepository => new VisitingRequestRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
