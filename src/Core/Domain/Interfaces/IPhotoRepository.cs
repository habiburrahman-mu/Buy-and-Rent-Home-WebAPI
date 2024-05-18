using Domain.Entities;

namespace Domain.Interfaces;

public interface IPhotoRepository : IGenericRepository<Photo>
{
    Task AddPhotos(List<Photo> photosList);
}
