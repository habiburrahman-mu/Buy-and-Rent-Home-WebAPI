using BuyAndRentHomeWebAPI.Data.Entities;
using BuyAndRentHomeWebAPI.Data.Interfaces;

namespace BuyAndRentHomeWebAPI.Data.Repo
{
    public class CitiesAreaManagerRepository : GenericRepository<CitiesAreaManager>, ICitiesAreaManagerRepository
    {
        public CitiesAreaManagerRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
