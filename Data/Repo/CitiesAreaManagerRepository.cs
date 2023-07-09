using BuyandRentHomeWebAPI.Data.Entities;
using BuyandRentHomeWebAPI.Data.Interfaces;

namespace BuyandRentHomeWebAPI.Data.Repo
{
    public class CitiesAreaManagerRepository : GenericRepository<CitiesAreaManager>, ICitiesAreaManagerRepository
    {
        public CitiesAreaManagerRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
