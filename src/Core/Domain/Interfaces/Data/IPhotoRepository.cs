using Domain.Entities;

namespace Domain.Interfaces.Data;

public interface IPhotoRepository : IGenericRepository<Photo>
{
    Task AddPhotos(List<Photo> photosList);
}
