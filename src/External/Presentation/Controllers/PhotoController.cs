using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Presentation.Services;

namespace Presentation.Controllers;

public class PhotoController : BaseController
{
    private readonly ISharedService _sharedService;
    private readonly IPhotoService _photoService;
    private readonly IUserContextService userContextService;

    public PhotoController(ISharedService sharedService, IPhotoService photoService, IUserContextService userContextService)
    {
        _sharedService = sharedService;
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
        var files = Request.Form.Files;

        var isPrimaryPhotoFromExistingImages =
            Convert.ToBoolean(Request.Form["IsPrimaryPhotoFromExistingImages"].FirstOrDefault());
        var primaryPhotoIdOrIndex =
            Convert.ToInt32(Request.Form["PrimaryPhotoIdOrIndex"].FirstOrDefault());
        var deletedPhotosIdString = Request.Form["DeletedPhotosId"].FirstOrDefault();

        var result = await _photoService.SavePhotos(propertyId, files, isPrimaryPhotoFromExistingImages, primaryPhotoIdOrIndex, deletedPhotosIdString, userContextService.GetUserId());

        
        return Ok(result);
    }

    

}
