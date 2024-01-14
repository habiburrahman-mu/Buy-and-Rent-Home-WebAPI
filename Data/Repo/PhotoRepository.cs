using BuyAndRentHomeWebAPI.Data.Interfaces;
using BuyAndRentHomeWebAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Data.Repo
{
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
}
