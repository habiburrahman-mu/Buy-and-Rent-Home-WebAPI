using Application.DTOs;
using Domain.Models;

namespace Application.Interfaces;

public interface IPhotoService
{
    Task<IEnumerable<PhotoDto>> GetPhotoListByPropertyId(int propertyId);
    Task<bool> SavePhotos(int propertyId, IEnumerable<PhotoFileDto> files, bool isPrimaryPhotoFromExistingImages, int primaryPhotoIdOrIndex, string? deletedPhotosIdString, int currentUserId);
}
