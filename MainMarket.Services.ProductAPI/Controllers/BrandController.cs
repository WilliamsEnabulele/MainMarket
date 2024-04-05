using MainMarket.Services.ProductAPI.Models;
using MainMarket.Services.ProductAPI.Models.DTO;
using MainMarket.Services.ProductAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainMarket.Services.ProductAPI.Controllers;

[Route("api/brands")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<BrandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateBrand(BrandRequest request)
    {
        var brand = await _brandService.Create(request);
        return Ok(ApiResponse<BrandResponse>.Success(brand));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<BrandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBrands()
    {
        var brands = await _brandService.GetBrands();
        return Ok(ApiResponse<List<BrandResponse>>.Success(brands));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<BrandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(string id)
    {
        var brand = await _brandService.GetBrand(id);
        return Ok(ApiResponse<BrandResponse>.Success(brand));
    }

    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<BrandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> UpdateBrand(BrandRequest request, string id)
    {
        var brand = await _brandService.UpdateBrand(request, id);
        return Ok(ApiResponse<BrandResponse>.Success(brand));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<BrandResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> RemoveBrand(string id)
    {
        var isDeleted = await _brandService.DeleteBrand(id);
        return Ok(ApiResponse<bool>.Success(isDeleted));
    }
}