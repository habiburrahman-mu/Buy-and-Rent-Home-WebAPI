using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Data;
using Common.Extensions;
using Domain.Models;
using Domain.Interfaces.Services;

namespace Application.Services;

public class PhotoService : IPhotoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileService fileService;

    public PhotoService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        this.fileService = fileService;
    }
    public async Task<IEnumerable<PhotoDto>> GetPhotoListByPropertyId(int propertyId)
    {
        var photoList = await _unitOfWork.PhotoRepository.GetAll(
            expression: x => x.PropertyId == propertyId,
            orderBy: x => x.OrderBy(q => q.IsPrimary));
        var photoDtoList = _mapper.Map<IEnumerable<PhotoDto>>(photoList);
        return photoDtoList;
    }

    public async Task<bool> SavePhotos(int propertyId, IEnumerable<PhotoFileDto> files, bool isPrimaryPhotoFromExistingImages, int primaryPhotoIdOrIndex,
        string? deletedPhotosIdString, int currentUserId)
    {
        bool result = false;
        var listOfsavedFile = new List<string>();

        var deletedPhotosIds = new List<int>();
        if (deletedPhotosIdString is not null && !deletedPhotosIdString.IsEmpty())
        {
            deletedPhotosIds = (deletedPhotosIdString ?? "").Split(',').Select(int.Parse).ToList();
        }


        result = await fileService.SaveFilesAsync(files, listOfsavedFile);

        try
        {
            if (result)
            {
                result = false;

                var existingPhotos = await _unitOfWork.PhotoRepository.GetAll(x => x.PropertyId == propertyId);

                var updatePhotos = existingPhotos.Where(x => !deletedPhotosIds.Contains(x.Id));

                foreach (var photo in updatePhotos)
                {
                    photo.IsPrimary = isPrimaryPhotoFromExistingImages && primaryPhotoIdOrIndex == photo.Id;
                }

                _unitOfWork.PhotoRepository.UpdateRange(updatePhotos);

                var photos = new List<Photo>();
                foreach (var item in listOfsavedFile.Select((value, index) => (value, index)))
                {
                    Photo photo = new Photo
                    {
                        ImageUrl = item.value,
                        IsPrimary = !isPrimaryPhotoFromExistingImages && primaryPhotoIdOrIndex == item.index,
                        PropertyId = propertyId,
                        LastUpdatedOn = DateTime.UtcNow,
                        LastUpdatedBy = currentUserId
                    };

                    photos.Add(photo);
                }
                await _unitOfWork.PhotoRepository.InsertRange(photos);

                var deletedPhotos = existingPhotos.Where(x => deletedPhotosIds.Contains(x.Id));
                deletePhotosFromDbAndPath(deletedPhotos);

                result = await _unitOfWork.SaveAsync();
            }
        }
        catch (Exception ex)
        {
            result = false;

            foreach (String item in listOfsavedFile)
            {
                fileService.DeleteFile(item);
            }

            throw;
        }

        return result;
    }

    //private async Task<bool> WriteFiles(IFormFileCollection files, List<string> listOfsavedFile)
    //{
    //    bool isSaveSuccess = false;

    //    foreach (IFormFile file in files)
    //    {
    //        var fileName = file.FileName;
    //        var extenstion = "." + fileName.Split('.')[fileName.Split('.').Length - 1];
    //        var newFileName = DateTime.UtcNow.Ticks + extenstion;

    //        var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), fileUploadDirectory);

    //        if (!Directory.Exists(pathBuilt))
    //        {
    //            Directory.CreateDirectory(pathBuilt);
    //        }

    //        var path = Path.Combine(Directory.GetCurrentDirectory(), fileUploadDirectory, newFileName);

    //        using (var stream = new FileStream(path, FileMode.Create))
    //        {
    //            await file.CopyToAsync(stream);
    //        }

    //        listOfsavedFile.Add(newFileName);
    //    }

    //    isSaveSuccess = true;

    //    return isSaveSuccess;
    //}

    private void deletePhotosFromDbAndPath(IEnumerable<Photo> deletedPhotosList)
    {
        foreach (var photo in deletedPhotosList)
        {
            fileService.DeleteFile(photo.ImageUrl);
        }
        _unitOfWork.PhotoRepository.DeleteRange(deletedPhotosList);
    }

    //public void DeleteFileFromPath(String fileName)
    //{
    //    var path = Path.Combine(Directory.GetCurrentDirectory(), fileUploadDirectory, fileName);
    //    FileInfo file = new FileInfo(path);
    //    if (file.Exists)
    //    {
    //        file.Delete();
    //    }
    //}
}
