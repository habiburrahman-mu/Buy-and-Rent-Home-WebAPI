using AutoMapper;
using BuyandRentHomeWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Controllers
{
    public class PhotoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

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

            return Ok(result);
        }

        private async Task<bool> WriteFiles(IFormFileCollection files, List<string> listOfsavedFile)
        {
            bool isSaveSuccess = false;
            //var listOfsavedFile = new List<string>();

            try
            {
                foreach (IFormFile file in files)
                {
                    var fileName = file.FileName;
                    var extenstion = "." + fileName.Split('.')[fileName.Split('.').Length - 1];
                    var newFileName = DateTime.Now.Ticks + extenstion;

                    var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                    if (!Directory.Exists(pathBuilt))
                    {
                        Directory.CreateDirectory(pathBuilt);
                    }

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files", newFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    listOfsavedFile.Add(newFileName);
                }
                isSaveSuccess = true;
            }
            catch (Exception exp)
            {
                foreach (String item in listOfsavedFile)
                {
                    DeleteFile(item);
                    listOfsavedFile.Remove(item);
                }
                return isSaveSuccess;
            }
            return isSaveSuccess;
        }

        private void DeleteFile(String path)
        {
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
        }

    }
}
