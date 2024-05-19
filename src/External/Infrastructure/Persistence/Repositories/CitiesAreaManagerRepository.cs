using Domain.Entities;
using Domain.Interfaces.Data;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

public class CitiesAreaManagerRepository : GenericRepository<CitiesAreaManager>, ICitiesAreaManagerRepository
{
    public CitiesAreaManagerRepository(BuyRentHomeDbContext dbContext) : base(dbContext)
    {
    }
}
