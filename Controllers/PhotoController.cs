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

namespace BuyandRentHomeWebAPI.Controllers
{
    public class PhotoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISharedService _sharedService;
        private string fileUploadDirectory = "Upload\\files";

        public PhotoController(IUnitOfWork unitOfWork, IMapper mapper, ISharedService sharedService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sharedService = sharedService;
        }

        [HttpPost("Save/{propertyId}")]
        public async Task<IActionResult> SavePhotos(int propertyId)
        {
            bool result = false;
            var listOfsavedFile = new List<string>();
            var files = Request.Form.Files;
            var primaryPhotoIndex = 
                Convert.ToInt32(Request.Form["PrimaryPhotoIndex"].FirstOrDefault());

            result = await WriteFiles(files, listOfsavedFile);

            try
            {
                if (result)
                {
                    result = false;
                    var photos = new List<Photo>();

                    foreach (var item in listOfsavedFile.Select((value, index) => (value, index)))
                    {
                        Photo photo = new Photo
                        {
                            ImageUrl = item.value,
                            IsPrimary = primaryPhotoIndex == item.index,
                            PropertyId = propertyId,
                            LastUpdatedOn = DateTime.Now,
                            LastUpdatedBy = _sharedService.GetUserId()
                        };

                        photos.Add(photo);
                    }

                    await _unitOfWork.PhotoRepository.AddPhotos(photos);
                    result = await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception exp)
            {
                result = false;

                foreach (String item in listOfsavedFile)
                {
                    DeleteFile(item);
                }
            }
            return Ok(result);
        }

        private async Task<bool> WriteFiles(IFormFileCollection files, List<string> listOfsavedFile)
        {
            bool isSaveSuccess = false;
            //var listOfsavedFile = new List<string>();

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

        private void DeleteFile(String fileName)
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
