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

namespace BuyandRentHomeWebAPI.Controllers
{
    public class PhotoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private string fileUploadDirectory = "Upload\\files";

        public PhotoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("Save/{propertyId}")]
        [AllowAnonymous]
        public async Task<IActionResult> SavePhotos(int propertyId)
        {
            bool result = false;
            var listOfsavedFile = new List<string>();
            var files = Request.Form.Files;

            result = await WriteFiles(files, listOfsavedFile);

            try
            {
                if (result)
                {
                    result = false;
                    var photos = new List<Photo>();

                    foreach (string item in listOfsavedFile)
                    {
                        Photo photo = new Photo
                        {
                            ImageUrl = item,
                            IsPrimary = false,
                            PropertyId = propertyId,
                            LastUpdatedOn = DateTime.Now,
                            LastUpdatedBy = 4
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
