using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories;

public class CitiesAreaManagerRepository : GenericRepository<CitiesAreaManager>, ICitiesAreaManagerRepository
{
    public CitiesAreaManagerRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
    {
    }
}
