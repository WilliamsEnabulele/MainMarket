using AutoMapper;
using MainMarket.Services.ProductAPI.Models.DTO;
using MainMarket.Services.ProductAPI.Models.Entities;

namespace MainMarket.Services.ProductAPI.Mappers;

public class MapInitializer : Profile
{
    public MapInitializer()
    {
        CreateMap<Product, ProductRequest>()
            .ForMember(c => c.Images, x => x.MapFrom(x => x.Images))
            .ReverseMap();
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.ProductImages, opts => opts.MapFrom(src => src.Images.Select(i => i.ImageUrl)));
        CreateMap<Category, CategoryRequest>().ReverseMap();
        CreateMap<CategoryResponse, Category>().ReverseMap();
        CreateMap<Image, ImageRequest>();
        CreateMap<ImageResponse, Image>();
    }
}