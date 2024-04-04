using MainMarket.Services.MediaAPI.Models;
using MainMarket.Services.MediaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainMarket.Services.MediaAPI.Controllers;

[Route("api/media")]
[ApiController]
public class MediaController : ControllerBase
{
    private readonly MediaServiceFactory _mediaServiceFactory;

    public MediaController(MediaServiceFactory mediaServiceFactory)
    {
        _mediaServiceFactory = mediaServiceFactory;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var service = _mediaServiceFactory.Create("cloudinary");
        return Ok(ApiResponse<string>.Success(await service.AddSingleMedia(file)));
    }

    [HttpPost]
    [Route("bulk")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<string>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UploadMany(IEnumerable<IFormFile> files)
    {
        var service = _mediaServiceFactory.Create("cloudinary");
        return Ok(ApiResponse<IEnumerable<string>>.Success(await service.AddMediaAsync(files)));
    }

    [HttpDelete("{publicId}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(string publicId)
    {
        var service = _mediaServiceFactory.Create("cloudinary");
        return Ok(ApiResponse<bool>.Success(await service.RemoveMediaAsync(publicId)));
    }
}