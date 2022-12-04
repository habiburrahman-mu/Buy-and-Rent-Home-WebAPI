using AutoMapper;
using BuyandRentHomeWebAPI.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;
using BuyandRentHomeWebAPI.Models;
using BuyandRentHomeWebAPI.Services.Interfaces;
using System.Linq;
using BuyandRentHomeWebAPI.Extensions;

namespace BuyandRentHomeWebAPI.Controllers
{
    public class PhotoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISharedService _sharedService;
        private readonly IPhotoService _photoService;
        private string fileUploadDirectory = "Upload\\files";

        public PhotoController(IUnitOfWork unitOfWork, ISharedService sharedService, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _sharedService = sharedService;
            _photoService = photoService;
        }

        [HttpGet("Get/{propertyId}")]
        public async Task<IActionResult> GetPhotoListByPropertyId(int propertyId)
        {
            var photoList = await _photoService.GetPhotoListByPropertyId(propertyId);
            return Ok(photoList);
        }

        [HttpPost("Save/{propertyId}")]
        public async Task<IActionResult> SavePhotos(int propertyId)
        {
            bool result = false;
            var listOfsavedFile = new List<string>();
            var files = Request.Form.Files;
            var isPrimaryPhotoFromExistingImages =
                Convert.ToBoolean(Request.Form["IsPrimaryPhotoFromExistingImages"].FirstOrDefault());
            var primaryPhotoIdOrIndex =
                Convert.ToInt32(Request.Form["PrimaryPhotoIdOrIndex"].FirstOrDefault());
            var deletedPhotosIdString = Request.Form["DeletedPhotosId"].FirstOrDefault();
            var deletedPhotosIds = new List<int>();
            if (!deletedPhotosIdString.IsEmpty())
            {
                deletedPhotosIds = (deletedPhotosIdString ?? "").Split(',').Select(int.Parse).ToList();
            }


            result = await WriteFiles(files, listOfsavedFile);

            try
            {
                if (result)
                {
                    result = false;

                    var existingPhotos = await _unitOfWork.PhotoRepository.GetAll(x => x.PropertyId == propertyId);

                    var upatePhotos = existingPhotos.Where(x => !deletedPhotosIds.Contains(x.Id));

                    foreach (var photo in upatePhotos)
                    {
                        photo.IsPrimary = isPrimaryPhotoFromExistingImages && primaryPhotoIdOrIndex == photo.Id;
                    }

                    _unitOfWork.PhotoRepository.UpdateRange(upatePhotos);

                    var photos = new List<Photo>();
                    foreach (var item in listOfsavedFile.Select((value, index) => (value, index)))
                    {
                        Photo photo = new Photo
                        {
                            ImageUrl = item.value,
                            IsPrimary = !isPrimaryPhotoFromExistingImages && primaryPhotoIdOrIndex == item.index,
                            PropertyId = propertyId,
                            LastUpdatedOn = DateTime.Now,
                            LastUpdatedBy = _sharedService.GetUserId()
                        };

                        photos.Add(photo);
                    }
                    await _unitOfWork.PhotoRepository.InsertRange(photos);

                    var deletedPhotos = existingPhotos.Where(x => deletedPhotosIds.Contains(x.Id));
                    deletePhotosFromDbAndPath(deletedPhotos);

                    result = await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception exp)
            {
                result = false;

                foreach (String item in listOfsavedFile)
                {
                    DeleteFileFromPath(item);
                }
            }
            return Ok(result);
        }

        private async Task<bool> WriteFiles(IFormFileCollection files, List<string> listOfsavedFile)
        {
            bool isSaveSuccess = false;

            foreach (IFormFile file in files)
            {
                var fileName = file.FileName;
                var extenstion = "." + fileName.Split('.')[fileName.Split('.').Length - 1];
                var newFileName = DateTime.Now.Ticks + extenstion;

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), fileUploadDirectory);

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), fileUploadDirectory, newFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                listOfsavedFile.Add(newFileName);
            }

            isSaveSuccess = true;

            return isSaveSuccess;
        }

        private void deletePhotosFromDbAndPath(IEnumerable<Photo> deletedPhotosList)
        {
            foreach (var photo in deletedPhotosList)
            {
                this.DeleteFileFromPath(photo.ImageUrl);
            }
            _unitOfWork.PhotoRepository.DeleteRange(deletedPhotosList);
        }

        private void DeleteFileFromPath(String fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileUploadDirectory, fileName);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
        }

    }
}
