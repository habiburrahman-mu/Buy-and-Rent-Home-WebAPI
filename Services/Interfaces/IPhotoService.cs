using BuyAndRentHomeWebAPI.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyAndRentHomeWebAPI.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<IEnumerable<PhotoDto>> GetPhotoListByPropertyId(int propertyId);
        Task<bool> SavePhotos(int propertyId, IFormFileCollection files, bool isPrimaryPhotoFromExistingImages, int primaryPhotoIdOrIndex,
            string deletedPhotosIdString);
        void DeleteFileFromPath(String fileName);
    }
}
