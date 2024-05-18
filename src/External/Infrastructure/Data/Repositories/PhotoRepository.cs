using Domain.Interfaces;
using Domain.Entities;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories;

public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
{
    private readonly BuyRentHomeDbContext _dataContext;

    public PhotoRepository(BuyRentHomeDbContext dataContext) : base(dataContext)
    {
        this._dataContext = dataContext;
    }

    public async Task AddPhotos(List<Photo> photosList)
    {
        await _dataContext.Photos.AddRangeAsync(photosList);
    }
}
