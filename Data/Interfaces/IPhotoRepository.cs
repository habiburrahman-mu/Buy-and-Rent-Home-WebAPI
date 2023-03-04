using BuyandRentHomeWebAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Interfaces
{
    public interface IPhotoRepository : IGenericRepository<Photo>
    {
        Task AddPhotos(List<Photo> photosList);
    }
}
