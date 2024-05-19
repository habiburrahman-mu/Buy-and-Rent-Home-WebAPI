using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Presentation.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Presentation.Controllers;

public class PhotoController : BaseController
{
    private readonly IPhotoService _photoService;
    private readonly IUserContextService userContextService;

    public PhotoController(IPhotoService photoService, IUserContextService userContextService)
    {
        _photoService = photoService;
        this.userContextService = userContextService;
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
        var photoFiles = FormFilesToPhotoFileDtoCollection(Request.Form.Files);

        var isPrimaryPhotoFromExistingImages =
            Convert.ToBoolean(Request.Form["IsPrimaryPhotoFromExistingImages"].FirstOrDefault());
        var primaryPhotoIdOrIndex =
            Convert.ToInt32(Request.Form["PrimaryPhotoIdOrIndex"].FirstOrDefault());
        var deletedPhotosIdString = Request.Form["DeletedPhotosId"].FirstOrDefault();

        var result = await _photoService.SavePhotos(propertyId, photoFiles, isPrimaryPhotoFromExistingImages, primaryPhotoIdOrIndex, deletedPhotosIdString, userContextService.GetUserId());

        
        return Ok(result);
    }

    private IEnumerable<PhotoFileDto> FormFilesToPhotoFileDtoCollection(IFormFileCollection formFileCollection)
    {
        var photoFileDtos = formFileCollection.Select(file =>
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return new PhotoFileDto
            {
                FileName = file.FileName,
                Content = memoryStream.ToArray()
            };
        });
        return photoFileDtos;
    }

}
