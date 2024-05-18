using Application.DTOs;

namespace Application.Interfaces;

public interface IPhotoService
{
    Task<IEnumerable<PhotoDto>> GetPhotoListByPropertyId(int propertyId);
    Task<bool> SavePhotos(int propertyId, IFormFileCollection files, bool isPrimaryPhotoFromExistingImages, int primaryPhotoIdOrIndex,
        string deletedPhotosIdString);
    void DeleteFileFromPath(String fileName);
}
