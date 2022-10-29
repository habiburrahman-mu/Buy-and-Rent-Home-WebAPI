using BuyandRentHomeWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyandRentHomeWebAPI.Data.Interfaces
{
    public interface IPhotoRepository
    {
        Task AddPhotos(List<Photo> photosList);
    }
}
