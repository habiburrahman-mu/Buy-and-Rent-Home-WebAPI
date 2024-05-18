using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;

namespace Presentation.Controllers;

public class PhotoController : BaseController
{
    private readonly ISharedService _sharedService;
    private readonly IPhotoService _photoService;

    public PhotoController(ISharedService sharedService, IPhotoService photoService)
    {
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
        var files = Request.Form.Files;

        var isPrimaryPhotoFromExistingImages =
            Convert.ToBoolean(Request.Form["IsPrimaryPhotoFromExistingImages"].FirstOrDefault());
        var primaryPhotoIdOrIndex =
            Convert.ToInt32(Request.Form["PrimaryPhotoIdOrIndex"].FirstOrDefault());
        var deletedPhotosIdString = Request.Form["DeletedPhotosId"].FirstOrDefault();

        var result = await _photoService.SavePhotos(propertyId, files, isPrimaryPhotoFromExistingImages, primaryPhotoIdOrIndex, deletedPhotosIdString);

        
        return Ok(result);
    }

    

}
